using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HackATL_Server.Models.Model.Chat_related
{
    public class UserChat_LogList
    {
        [Key]
        public string Id { get; set; }
        public List<UserChatList_Group> ChatList { get; set; }

    }

    


    public class UserChatList_Component
    {
        [Key]
        public string Uid { get; set; }
        public string Name { get; set; } // new 
        public string Username { get; set; }

    }



    //--------------------------------------------------
    //Somehow all duplicates

    public class UserChatList_Group
    {
        [Key]
        public string RId { get; set; }
        public List<UserChatList_Component> UsersList { get; set; }

    }

    public class ChatRoom_participants
    {
        [Key]
        public string RId { get; set; }
        public List<UserChatList_Component> members { get; set; }


    }

    public class AddChatRoom
    {
        public string Rid { get; set; }
        public List<UserChatList_Component> Users { get; set; }
    }


    //--------------------------------------------------
    // unit : single chatroom

    public class UserChatList_LogHistory
    {
        [Key]
        public string RId { get; set; }
        public List<User_chatlog> Log { get; set; }
    }


    // unit : single message
    public class User_chatlog
    {
        [Key]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
    }


    // unit : single user 
    // one that gets stored 
    public class UserChat
    {
        [Key]
        public string UId { get; set; }
        public List<UserChatList_LogHistory> ChatLog { get; set; }

    }

    //--------------------------------------------------

   
}
