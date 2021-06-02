using System;
using System.Collections.Generic;
using System.Text;

namespace TweetApp.Repository.TweetAppEntity
{
    public class CommentsOnTweet
    {
        public int Id { get; set; }
        public int? TweetId { get; set; }
        public string UserId { get; set; }
        public string Comments { get; set; }
        public string TweetPost { get; set; }
    }
}
