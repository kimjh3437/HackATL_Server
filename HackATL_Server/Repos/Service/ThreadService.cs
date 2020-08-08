using System;
using System.Collections.Generic;
using HackATL_Server.Models.Model.MongoDatabase.Forums;
using HackATL_Server.Models.Model.MongoDatabase.Users;
using HackATL_Server.Models.Model_Http.Forum;
using HackATL_Server.Models.MongoDatabase.Settings;
using HackATL_Server.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using HackATL_Server.Repos.Interface;

namespace HackATL_Server.Repos.Service
{
    public class ThreadService : IThreadService
    {
        IMongoCollection<Thread> _threads;
        IOptions<MongoDBSettings> _settings;
        IMongoCollection<User> _users;
        public ThreadService(
            IOptions<MongoDBSettings>settings)
        {
            _settings = settings;
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(Hackathon.Emory_HackATL);
            _threads = database.GetCollection<Thread>(settings.Value.Thread);
            _users = database.GetCollection<User>(settings.Value.User);
        }

        public Thread CreateThread(Thread_Detail create)
        {
            
            Thread thread = new Thread();
            var threadID = Guid.NewGuid().ToString();
            Thread_Detail detail = new Thread_Detail();
            detail = create;
            detail.threadID = threadID;
            thread.Detail = detail;
            thread.threadID = threadID;
            _threads.InsertOne(thread);
            return thread; 

        }

        public Boolean DeleteThread(string threadID)
        {

            var filter = Builders<Thread>.Filter.Eq(x => x.threadID, threadID);
            _threads.DeleteOne(filter);
            return true; 
        }

        public Boolean EditThread(Thread_Detail edit)
        {
            var filter = Builders<Thread>.Filter.Eq(x => x.threadID, edit.threadID);
            var update = Builders<Thread>.Update.Set(x => x.Detail, edit);
            var status = _threads.UpdateOne(filter, update);
            if (status != null)
                return true;
            return false; 
        }
        public Thread_Comment Thread_AddComment(Thread_Comment comment)
        {
            Comment t = new Comment();
            t = comment.Comment;
            if (t.commentID == null)
                t.commentID = Guid.NewGuid().ToString();
            var filter = Builders<Thread>.Filter.Eq(x => x.threadID, comment.threadID);
            var update = Builders<Thread>.Update.AddToSet(x => x.Comments, t);
            var status = _threads.UpdateOne(filter, update);
            if (status == null)
                return null;
            return comment;
            //------------------------------------------------------------------------------
            //
            // Add notification to all the subscribers and members that initiate this method  
            //
            //------------------------------------------------------------------------------


        }

        public Boolean Thread_AddRemoveFavorite(Thread_Favorite favorite)
        {
            var user = _users.Find<User>(x => x.uID == favorite.uID).FirstOrDefault();
            User_Thread thread = new User_Thread();
            thread = user.Threads;
            List<string> favoriteList = new List<string>();
            favoriteList = thread.Favorites;
            if (favoriteList.Contains(favorite.threadID))
            {
                var filter = Builders<User>.Filter.Eq(x => x.uID, favorite.uID);
                var update = Builders<User>.Update.AddToSet(x => x.Threads.Favorites, favorite.threadID);
                var status = _users.UpdateOne(filter, update);
                if (status != null)
                    return true;
                return false;

            }
            else
            {
                var filter = Builders<User>.Filter.Eq(x => x.uID, favorite.uID);
                var update = Builders<User>.Update.Pull(x => x.Threads.Favorites, favorite.threadID);
                var status = _users.UpdateOne(filter, update);
                if (status != null)
                    return true;
                return false;

            }
            //------------------------------------------------------------------------------
            //
            // Add notification to all the subscribers and members that initiate this method  
            //
            //------------------------------------------------------------------------------




        }
    }
}
