using System;
using System.Collections.Generic;
using System.Text;

namespace TweetApp.Repository.TweetAppEntity
{
    public class TweetsandUsers
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ImgName { get; set; }
        public string UserTweets { get; set; }
        public int? Likes { get; set; }
        public int? DisLikes { get; set; }
        public bool visible { get; set; }

    }
}
