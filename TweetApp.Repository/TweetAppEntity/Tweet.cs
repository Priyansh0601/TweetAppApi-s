using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

#nullable disable

namespace TweetApp.Repository.TweetAppEntity
{
    public partial class Tweet
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public int TweetId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserTweets { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? Likes { get; set; }
        public int? DisLikes { get; set; }
    }
}
