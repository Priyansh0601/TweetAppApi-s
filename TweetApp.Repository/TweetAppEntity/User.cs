using System;
using System.Collections.Generic;

#nullable disable

namespace TweetApp.Repository.TweetAppEntity
{
    public partial class User
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string Imgname { get; set; }
    }
}
