using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

#nullable disable

namespace TweetApp.Repository.TweetAppEntity
{
    public partial class User
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string ImgName { get; set; }
    }
}
