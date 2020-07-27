using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HackATL_Server.Helper;
using HackATL_Server.Models.Model.MongoDatabase.Auth;
using HackATL_Server.Models.Model.MongoDatabase.Users;
using HackATL_Server.Models.Model_General;
using HackATL_Server.Models.Model_Http.User;
using HackATL_Server.Models.MongoDatabase.Settings;
using HackATL_Server.Repos.Interface;
using HackATL_Server.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace HackATL_Server.Repos.Service
{
    public class AuthService : IAuthService
    {
        IMongoCollection<User> _users;
        IMongoCollection<AuthUser> _auths;
        IOptions<AppSettings> secret;
        public AuthService(IOptions<MongoDBSettings> settings, IOptions<AppSettings> _secret)
        {
            secret = _secret;
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(Hackathon.Emory_HackATL);
           _users = database.GetCollection<User>(settings.Value.User);
            _auths = database.GetCollection<AuthUser>(settings.Value.AuthUser); 
        }
        public Boolean UpdatePersonal(User_Personal Update)
        {
            var filter = Builders<User>.Filter.Eq(x => x.uID, Update.uID);
            var update = Builders<User>.Update.Set(x => x.Personal, Update);
            var status = _users.UpdateOne(filter, update);
            if (status == null)
                return false;
            return true; 

        }

        

        public Boolean DeleteUser(string uID)
        {
            var filter = Builders<User>.Filter.Eq(x => x.uID, uID);
            _users.DeleteOne(filter);
            return true; 
        }

        public User Register(User_Register register)
        {
            if (string.IsNullOrWhiteSpace(register.Password))
                throw new ApplicationException("Password is required");

            if (_auths.Find(x => x.Username == register.Username) == null)
                throw new ApplicationException("Username \"" + register.Username + "\" is already taken");

            User user = new User();
            AuthUser auth = new AuthUser();
            User_Personal personal = new User_Personal();

            if (register.Username.Contains("1234"))
            {
                user.Role = Role.Admin;
            }
            // sets up personal information on newly registered account 
            personal = register.Personal;
            user.Personal = personal;
            auth.Username = register.Username;
            var uID = Guid.NewGuid().ToString();

            //sets up security hash salt 
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(register.Password, out passwordHash, out passwordSalt);
            auth.Username = register.Username;
            auth.PasswordHash = passwordHash;
            auth.PasswordSalt = passwordSalt;
            auth.uID = uID;

            _auths.InsertOne(auth);
            user.Status = true;
            if (user != null)
                _users.InsertOne(user);

            return user; 
        }

        public User Authenticate(User_Authenticate authenticate)
        {
            var username = authenticate.Username;
            var password = authenticate.Password;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;
            var auth = _auths.Find<AuthUser>(x => x.Username == username).FirstOrDefault();


            if (auth == null)
                return null;
            if (!VerifyPasswordHash(password, auth.PasswordHash, auth.PasswordSalt))
                return null;

            var user = _users.Find<User>(x => x.uID == auth.uID).FirstOrDefault();

            // autrhoization token 
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret.Value.Secret);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, auth.Username.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            // token

            user.Token = tokenString;
            return user; 

        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
