using Microsoft.AspNetCore.Mvc;
using SuperNews.Domains;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SuperNews.Models
{
    public class CommentsViewModel : Comment 
    {
        public int commentId { get; set; }
        //public DateTime CommentDate { get; set; }
        public string strCommentDate { get {; return this.CommentDate.ToString("dd-MM-yyyy"); } }

        public CommentsViewModel()
        {

        }

      
    }
}
