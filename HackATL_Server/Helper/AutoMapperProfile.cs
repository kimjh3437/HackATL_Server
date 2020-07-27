using System;
using AutoMapper;
using HackATL_Server.Models.Model;

using HackATL_Server.Models.Model.MongoDatabase.Users;


namespace HackATL_Server.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
           
            //CreateMap<User, UserModel>();
            //CreateMap<RegisterModel, User>();
            //CreateMap<UpdateModel, User>();
            //CreateMap<User, PublicModel>();


            //CreateMap<RegisterModel, User_Personal>(); //User_MongoDB 
        } 
    }
}
