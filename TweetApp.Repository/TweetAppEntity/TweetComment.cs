using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

#nullable disable

namespace TweetApp.Repository.TweetAppEntity
{
    public partial class TweetComment
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public int? TweetId { get; set; }
        public string UserId { get; set; }
        public string Comments { get; set; }
    }
}
