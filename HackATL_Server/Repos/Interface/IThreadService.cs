using System;
using HackATL_Server.Models.Model.MongoDatabase.Forums;
using HackATL_Server.Models.Model_Http.Forum;

namespace HackATL_Server.Repos.Interface
{
    public interface IThreadService
    {
        Thread CreateThread(Thread_Detail create);

        Boolean DeleteThread(string threadID);

        Boolean EditThread(Thread_Detail edit);

        Thread_Comment Thread_AddComment(Thread_Comment comment);

        Boolean Thread_AddRemoveFavorite(Thread_Favorite favorite);


    }
}
