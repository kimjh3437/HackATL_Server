using System;
using HackATL_Server.Models.Model.MongoDatabase.Users;
using HackATL_Server.Models.Model_Http.User;

namespace HackATL_Server.Repos.Interface
{
    public interface IAuthService
    {
        Boolean UpdatePersonal(User_Personal Update);

        Boolean DeleteUser(string uID);

        User Register(User_Register register);

        User Authenticate(User_Authenticate authenticate);



    }
}
