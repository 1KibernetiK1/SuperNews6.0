using Microsoft.AspNetCore.Mvc;
using SuperNews.Domains;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SuperNews.Models
{
    public class ChatViewModel : Chat 
    {
        public int ChatId { get; set; }
        //public DateTime CommentDate { get; set; }
        public string strChatDate { get {; return this.ChatDate.ToString("dd-MM-yyyy"); } }

        public ChatViewModel()
        {

        }

      
    }
}
