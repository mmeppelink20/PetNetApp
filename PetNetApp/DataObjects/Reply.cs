using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class Reply
    {
        public int ReplyId { get; set; }
        public int PostId { get; set; }
        public int ReplyAuthor { get; set; }
        [Required(ErrorMessage = "Reply cant be empty")]
        [RegularExpression(@"^(?!\s*$)(\s|\S)+$",
         ErrorMessage = "Reply cant be only space(s)")]
        public string ReplyContent { get; set; }
        public DateTime ReplyDate { get; set; }
        public bool ReplyVisibility { get; set; }
    }

    public class ReplyVM : Reply
    {
        public string ReplierGivenName { get; set; }
        public string ReplierFamilyName { get; set; }
        public bool UserReplyReport { get; set; }
    }

}
