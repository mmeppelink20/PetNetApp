using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class Post
    {
        public int PostId { get; set; }
        public int PostAuthor { get; set; }
        [Required(ErrorMessage = "Post cant be empty")]
        [RegularExpression(@"^(?!\s*$)(\s|\S)+$", 
         ErrorMessage = "Post cant be only space(s)")]
        public string PostContent { get; set; }
        public DateTime PostDate { get; set; }
        public bool PostVisibility { get; set; }
    }

    public class PostVM : Post
    {
        public string PosterGivenName { get; set; }
        public string PosterFamilyName { get; set; }
        public List<ReplyVM> Replies { get; set; }
        public int FavoriteCount { get; set; }
        public bool UserFavorited { get; set; }
        public bool UserPostReported { get; set; }
    }

}
