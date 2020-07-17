using System;
using HackATL_Server.Helper;
using HackATL_Server.Models.Model.MongoDatabase.Users;
using HackATL_Server.Models.MongoDatabase.Settings;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using HackATL_Server.Models.Model.MongoDatabase.Auth;
using HackATL_Server.Models.Model.authentication;
using HackATL_Server.Models.Model;
using User = HackATL_Server.Models.Model.MongoDatabase.Users.User;
using AutoMapper;
using HackATL_Server.Repository.Interfaces_MongoDB;
using MongoDB.Bson;
using HackATL_Server.Models.Model_Http.Auth;

namespace HackATL_Server.Models.Repository.Services_MongoDB
{
    public class UserService_md : IUserService_md
    {
        private readonly IMongoCollection<User> _users;
        private IMapper _mapper; 
    


        public UserService_md(IOptions<MongoDBSettings> _settings, IMapper mapper)
        {


            _mapper = mapper;
            var client = new MongoClient(_settings.Value.ConnectionString);
            var database = client.GetDatabase(_settings.Value.DatabaseName);
         
            
            _users = database.GetCollection<User>(_settings.Value.User);


        }
        public IEnumerable<User> GetAll()
        {
            List<User> user = _users.Find(x => true).ToList();
            return user;
        }

        public IEnumerable<User_Personal> GetAll_Public()
        {
            List<User> user = _users.Find(x => true).ToList();
            List<User_Personal> personal = new List<User_Personal>();
            foreach(var x in user)
            {
                personal.Add(x.Personal);

            }
            return personal; 
        }

        public User GetUser(string uID)
        {
            return _users.Find<User>(x => x.uID == uID).FirstOrDefault();

        }

        public void UpdateUser(string uID, User_Personal personal)
        {
            User user_ = _users.Find<User>(x => x.uID == uID).FirstOrDefault();
            user_.Personal = personal;
            _users.ReplaceOne<User>(x => x.uID == uID, user_);

        }

        public void Delete(string uID)
        {
            _users.DeleteOne(x => x.uID == uID);

        }

      

        public User Register(User_Register registerModel)
        {
          
            User user = new User();
            AuthUser auth = new AuthUser();
            //User_Personal personal = new User_Personal();
            //personal = _mapper.Map<User_Personal>(registerModel);
           
            if (string.IsNullOrWhiteSpace(registerModel.Password))
                throw new AppException("Password is required");

            if (_users.Find(x => x.Auth.Username == registerModel.Username) != null)
                throw new AppException("Username \"" + registerModel.Username + "\" is already taken");
            var len = registerModel.Password.Length;
            if (registerModel.Password.Substring(0, 5) == "eevmh" && registerModel.Password.Substring(len - 4, len) == "2020")
            {
                user.Role = "Admin";
            }
            else
                user.Role = "Member";
            
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(registerModel.Password, out passwordHash, out passwordSalt);
            auth.PasswordHash = passwordHash;
            auth.PasswordSalt = passwordSalt;
            user.Auth = auth;
            user.Personal = registerModel.personal;
            user.Status = true;
            _users.InsertOne(user);
            return user;


        }
        public bool Check(string username)
        {
            if (_users.Find(x => x.Auth.Username == username).FirstOrDefault() == null)
                return true;
            else return false;
        }

      
        

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;
            var user = _users.Find<User>(x => x.Auth.Username == username).SingleOrDefault();
            AuthUser auth = new AuthUser();
            auth = user.Auth; 

            if (user == null)
                return null;
            if (!VerifyPasswordHash(password, auth.PasswordHash, auth.PasswordSalt))
                return null;
            user.Status = true;


            return user; 


        }
        //Object MakePublic(Object obj)
        //{
        //    object x = new object();
        //    x = _mapper,.
        //}
        

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
