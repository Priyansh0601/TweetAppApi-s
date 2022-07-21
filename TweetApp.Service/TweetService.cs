using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using TweetApp.Repository;
using TweetApp.Repository.TweetAppEntity;

namespace TweetApp.Service
{
    public class TweetService :ITweetService
    {
        private readonly ITweetQueries queries;
        private readonly IConfiguration config;
        /// <summary>
        /// create the instance of tweet service.
        /// </summary>
        /// <param name="queries"></param>
        public TweetService(ITweetQueries queries)
        {
            this.queries = queries;
        }
        

        /// <summary>
        ///  check that wheather that user is existed or not. if found returns alread existed message, else save the user details.
        /// </summary>
        /// <param name="userRegister">user details.</param>
        /// <returns>returns the status message.</returns>
        public string UserRegistration(User userRegister)
        {
            if (userRegister != null)
            {
                var user = this.queries.UserExist(userRegister.EmailId);
                if (user == null)
                {
                    
                    userRegister.Password = this.EncodePassword(userRegister.Password);
                    var result = queries.UserRegister(userRegister);
                    if (result == true)
                    {
                        return Messages.UserRegister;
                    }
                }

            }
            return Messages.UserExists;
        }

        /// <summary>
        /// User Login
        /// </summary>
        /// <param name="userID">based on userId.</param>
        /// <param name="password">based on password.</param>
        /// <returns>returns the status message.</returns>
        public User UserLogin(string userID, string password)
        {
            if (!string.IsNullOrEmpty(userID) && !string.IsNullOrEmpty(password))
            {
                
                var user = this.queries.Userlogin(userID);
               
                if (user != null)
                {
                    var decodedPassword = DecodePassword(user.Password);
                    if (user != null && password == decodedPassword)
                    {
                        return user;

                    }
                }
            }

            return null;
        }

        public string UserExists(String emailId)
        {
            
            var user = this.queries.UserExist(emailId); 
            return user;

        }

        /// <summary>
        /// Gets the particular user tweets.
        /// </summary>
        /// <param name="userId">Based on userId.</param>
        /// <returns>returns the list of tweets.</returns>
        public List<TweetsandUsers> GetUserTweets(string userId)
        {
            var tweets = this.queries.GetUserTweets(userId);
            return tweets;
        }

        /// <summary>
        /// Gets the particular user tweets.
        /// </summary>
        /// <param name="userId">Based on userId.</param>
        /// <returns>returns the list of tweets.</returns>
        public List<TweetsandUsers> GetOtherUsersTweets(string userId)
        {
           
            var tweets = this.queries.GetOtherUserTweets(userId);
            return tweets;
        }

        public string AddNewTweet(Tweet tweet)
        {
            if (tweet != null)
            {
                tweet.Likes = 0;
                tweet.DisLikes = 0;
                var result = this.queries.AddTweet(tweet);
                if (result == true)
                {
                    return Messages.TweetAdded;
                }
            }
            return Messages.TweetNotAdded;
        }

        /// <summary>
        /// get all the user list.
        /// </summary>
        /// <returns>returns the list of users.</returns>
        public List<AllUsers> AllUserList()
        {
            var userList = this.queries.GetAllUsers();
            return userList;
        }

        /// <summary>
        /// Get  the user and tweet 
        /// </summary>
        /// <returns>returns the all users tweets.</returns>
        public List<TweetsandUsers> GetUserandTweetList()
        {
            var result = this.queries.GetUserandTweetList();
            return result;
        }

        /// <summary>
        /// updates the new password.
        /// </summary>
        /// <param name="userId">based on userId.</param>
        /// <param name="newPassword">will add the new password.</param>
        /// <returns>returns the boolean value.</returns>
        public bool UpdatePassword(string userId, string oldpassword, string newPassword)
        {
            newPassword = this.EncodePassword(newPassword);
            oldpassword = this.EncodePassword(oldpassword);
            var result = this.queries.UpdatePassword(userId, oldpassword, newPassword);
            return result;
        }

        /// <summary>
        /// ForgotPasswordEmailId.
        /// </summary>
        /// <param name="emailId">based on emailId.</param>
        /// <returns> returns the status.</returns>
        public string ForgotPasswordEmailId(string emailId)
        {
            var result = this.queries.ForgotPasswordEmail(emailId);
            if (result == true)
            {
                return Messages.changePassword;
            }
            return Messages.NoChangepassword;
        }

        /// <summary>
        /// Forgot Password.
        /// </summary>
        /// <param name="emailId">EmailId.</param>
        /// <param name="newPassword">New Password.</param>
        /// <returns></returns>
        public string ForgotPassword(string emailId, string newPassword)
        {
            newPassword = this.EncodePassword(newPassword);
            var result = this.queries.ForgotPassword(emailId, newPassword);
            if (result == true)
            {
                return Messages.PasswordUpdated;
            }

            return Messages.PasswordNotUpdated;
        }

        /// <summary>
        /// Get the likes and updates the count.
        /// </summary>
        /// <param name="tweetId">based on tweetId.</param>
        /// <returns>return the count.</returns>
        public List<TweetsandUsers> GetLikes(int tweetId, string userId)
        {
            List<TweetsandUsers> tweetList =null;
            if (tweetId != 0)
            {
                bool tweet = this.queries.UpdateLikes(tweetId);
                if (tweet)
                {
                    tweetList = this.queries.LikesandDislikesCount(tweetId);
                }
                return tweetList;
            }
            return null;
        }

        /// <summary>
        /// Get the Dislikes and updates the count.
        /// </summary>
        /// <param name="tweetId">based on tweetId.</param>
        /// <returns>return the count.</returns>
        public List<TweetsandUsers> GetDisLikes(int tweetId)
        {
            string userId=null;
            List<TweetsandUsers> tweetList = null;
            if (tweetId != 0)
            {
                bool tweet = this.queries.UpdateDisLikes(tweetId);
                if (tweet)
                {
                    tweetList = this.queries.LikesandDislikesCount(tweetId);
                }
                return tweetList;
            }
            return null;
        }

        /// <summary>
        /// Add the new tweet comments.
        /// </summary>
        /// <param name="comments">new comment</param>
        /// <returns> return the status.</returns>
        public bool AddTweetComment(AddTweetComments comments, int tweetId)
        {
            if(comments != null)
            {
                var result = this.queries.AddTweetComments(comments, tweetId);
                return result;
            }
            return false;
        }

        /// <summary>
        /// Get the tweets comments.
        /// </summary>
        /// <param name="tweetId">based on tweetId.</param>
        /// <returns>return the list of tweet comments.</returns>
        public List<CommentsOnTweet> GetTweetComments(int tweetId)
        {
            if(tweetId != 0)
            {
                var commentList = this.queries.FetchTweetComments(tweetId);
                return commentList;
            }
            return null;
        }
        /// <summary>
        /// Encodes the password
        /// </summary>
        /// <param name="password">passord.</param>
        /// <returns>Returns the encoded password.</returns>
        private string EncodePassword(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (TweetException ex)
            {
                throw new TweetException("error in encode password" + ex.Message);
            }
        }

        /// <summary>
        /// Decodes the password.
        /// </summary>
        /// <param name="password">password.</param>
        /// <returns>Returns the Decoded password.</returns>
        private string DecodePassword(string password)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(password);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (TweetException ex)
            {
                throw new TweetException("error in decode password" + ex.Message);
            }
        }
    }
}
