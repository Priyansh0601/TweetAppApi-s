using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TweetApp.Repository.TweetAppEntity;

namespace TweetApp.Repository
{
    public class TweetQueries : ITweetQueries
    {
        /// <summary>
        /// Checks the user is exists or not.
        /// </summary>
        /// <param name="emailId"> based on userID.</param>
        /// <returns>returns the user details if found.</returns>
        public string UserExist(string emailId)
        {
            using (var dbContext = new TweetAppUseCaseContext())
            {
                var user = dbContext.Users.Where(s => s.EmailId == emailId).Select(p => p.EmailId).FirstOrDefault();
                return user;
            }
        }

        /// <summary>
        /// user login.
        /// </summary>
        /// <param name="userId">based on user id fetches the encoded password</param>
        /// <returns></returns>
        public User Userlogin(string userId)
        {
            using (var dbContext = new TweetAppUseCaseContext())
            {
                var user = dbContext.Users.Where(s => s.EmailId == userId).FirstOrDefault();
                return user;

            }
        }

        /// <summary>
        /// registered the new user
        /// </summary>
        /// <param name="userRegister">user details.</param>
        public bool UserRegister(User userRegister)
        {
            using (var dbContext = new TweetAppUseCaseContext())
            {
                dbContext.Users.Add(userRegister);
                var result = dbContext.SaveChanges();
                return result > 0;

            }
        }

        /// <summary>
        /// adds the new tweet.
        /// </summary>
        /// <param name="tweet">tweet</param>
        public bool AddTweet(Tweet tweet)
        {
            using (var dbContext = new TweetAppUseCaseContext())
            {
                dbContext.Tweets.Add(tweet);
                var result = dbContext.SaveChanges();
                return result > 0;
            }

        }

        /// <summary>
        /// get the particular user tweets.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>returns the list of tweets.</returns>
        public List<TweetsandUsers> GetUserTweets(string userId)
        {
            using (var dbContext = new TweetAppUseCaseContext())
            {
                //var tweets = dbContext.Tweets.Where(x => x.UserId == userId).ToList();
                //return tweets;
                var userTweets = (from tweet in dbContext.Tweets
                                  join user in dbContext.Users on tweet.UserId equals user.EmailId
                                  where (tweet.UserId == user.EmailId && tweet.UserId==userId)
                                  select new TweetsandUsers
                                  {
                                      Id = tweet.Id,
                                      UserId = tweet.UserId,
                                      FirstName = user.Firstname,
                                      CreatedDate = tweet.CreatedDate,
                                      ImgName = user.Imgname,
                                      UserTweets = tweet.UserTweets,
                                      Likes=tweet.Likes,
                                      visible=false
                                      
                                      
                                  }).ToList();
                return userTweets;
            }
        }


        /// <summary>
        /// get the particular user tweets.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>returns the list of tweets.</returns>
        public List<TweetsandUsers> GetOtherUserTweets(string userId)
        {
            using (var dbContext = new TweetAppUseCaseContext())
            {
                //var tweets = dbContext.Tweets.Where(x => x.UserId == userId).ToList();
                //return tweets;
                var userTweets = (from tweet in dbContext.Tweets
                                  join user in dbContext.Users on tweet.UserId equals user.EmailId
                                  where (tweet.UserId == user.EmailId && tweet.UserId != userId)
                                  select new TweetsandUsers
                                  {
                                      Id=tweet.Id,
                                      UserId = tweet.UserId,
                                      FirstName = user.Firstname,
                                      CreatedDate = tweet.CreatedDate,
                                      ImgName = user.Imgname,
                                      UserTweets = tweet.UserTweets,
                                      Likes=tweet.Likes,
                                      visible=false
                                  }).ToList();
                return userTweets;
            }
        }

        /// <summary>
        /// Get all the user list.
        /// </summary>
        /// <returns>returns the all user list.</returns>
        public List<AllUsers> GetAllUsers()
        {
            using (var dbContext = new TweetAppUseCaseContext())
            {
                var users = dbContext.Users.Select(p => new AllUsers
                {
                    FirstName = p.Firstname,
                    LastName = p.Lastname
                }).ToList();
                return users;
            }

        }

        /// <summary>
        /// Get all users and their respective Tweets.
        /// </summary>
        /// <returns>Returns the list users names and tweets.</returns>
        public List<TweetsandUsers> GetUserandTweetList()
        {
            using (var dbContext = new TweetAppUseCaseContext())
            {
                var userTweets = (from tweets in dbContext.Tweets
                                  join user in dbContext.Users on tweets.UserId equals user.EmailId
                                  where (tweets.UserId == user.EmailId)
                                  select new TweetsandUsers
                                  {
                                      UserId = tweets.UserId,
                                      FirstName = user.Firstname,
                                      CreatedDate = tweets.CreatedDate,
                                      ImgName = user.Imgname,
                                      UserTweets = tweets.UserTweets,
                                      visible=false                                 
                                  }).ToList();
                return userTweets;
            }

        }
        /// <summary>
        /// updates the password.
        /// </summary>
        /// <param name="userId">Userid.</param>
        /// <param name="newPassword">Newpassword.</param>
        /// <returns></returns>

        public bool UpdatePassword(string userId, string oldPassword, string newPassword)
        {
            using (var dbContext = new TweetAppUseCaseContext())
            {
                var userDetails = dbContext.Users.Where(x => x.EmailId == userId && x.Password == oldPassword).FirstOrDefault();
                if (userDetails != null)
                {
                    userDetails.Password = newPassword;
                    dbContext.Users.Update(userDetails);
                    var created = dbContext.SaveChanges();
                    return created > 0;
                }

                return false;

            }
        }

        /// <summary>
        /// Forgotpassword email.
        /// </summary>
        /// <param name="emailId">based on emaild.</param>
        /// <returns>return the status.</returns>
        public bool ForgotPasswordEmail(string emailId)
        {
            using (var dbContext = new TweetAppUseCaseContext())
            {
                var userDetails = dbContext.Users.Where(s => s.EmailId == emailId).FirstOrDefault();
                if (userDetails != null)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Forgot password.
        /// </summary>
        /// <param name="emailId">based on emailId.</param>
        /// <param name="newPassword">based on new password.</param>
        /// <returns>returns the status.</returns>
        public bool ForgotPassword(string emailId, string newPassword)
        {
            using (var dbContext = new TweetAppUseCaseContext())
            {
                var userDetails = dbContext.Users.Where(s => s.EmailId == emailId).FirstOrDefault();
                if (userDetails != null)
                {
                    userDetails.Password = newPassword;
                    dbContext.Update(userDetails);
                    var result = dbContext.SaveChanges();
                    return result > 0;
                }
                return false;
            }
        }

        /// <summary>
        /// Add new tweet comments.
        /// </summary>
        /// <param name="comments">comment need to add</param>
        /// <returns>return the status.</returns>
        public bool AddTweetComments(AddTweetComments comments, int tweetId)
        {
            using (var dbContext = new TweetAppUseCaseContext())
            {
                var addComment = new TweetComment()
                {
                    UserId = comments.UserId,
                    TweetId = tweetId,
                    Comments = comments.Comments
                };
                dbContext.TweetComments.Add(addComment);
                var result = dbContext.SaveChanges();
                return result > 0;
            }
        }

        /// <summary>
        /// Fetches the tweet comments.
        /// </summary>
        /// <param name="tweetId">Based on tweetId</param>
        /// <returns>returns the list of tweet comments.</returns>
        public List<CommentsOnTweet> FetchTweetComments(int tweetId)
        {
            using (var dbContext = new TweetAppUseCaseContext())
            {
                var userTweets = (from tweets in dbContext.Tweets
                                  join comment in dbContext.TweetComments on tweets.Id equals comment.TweetId
                                  where (tweets.Id==comment.TweetId && comment.TweetId==tweetId)
                                  select new CommentsOnTweet
                                  {
                                      TweetId=comment.TweetId,
                                      UserId=comment.UserId,
                                      Comments=comment.Comments,
                                      TweetPost=tweets.UserTweets

                                  }).ToList();
                return userTweets;
            }

        }

        /// <summary>
        /// Updates te likes.
        /// </summary>
        /// <param name="tweetId">based on tweetId.</param>
        /// <returns>return the status.</returns>
        public bool UpdateLikes(int tweetId)
        {
            using (var dbContext = new TweetAppUseCaseContext())
            {
                var userDetails = dbContext.Tweets.Where(x => x.Id == tweetId).FirstOrDefault();
                if (userDetails != null)
                {
                    userDetails.Likes = userDetails.Likes+1;
                    dbContext.Tweets.Update(userDetails);
                    var created = dbContext.SaveChanges();
                    return created > 0;
                }

                return false;

            }
        }

        /// <summary>
        /// Likes and Dislikes Count.
        /// </summary>
        /// <param name="tweetId">based on tweetId.</param>
        /// <returns>returns the tweet.</returns>
        public List<TweetsandUsers> LikesandDislikesCount(string userId)
        {
            using (var dbContext = new TweetAppUseCaseContext())
            {
                var userTweets = (from tweets in dbContext.Tweets
                                  join user in dbContext.Users on tweets.UserId equals user.EmailId
                                  where (tweets.UserId == user.EmailId && user.EmailId != userId)
                                  select new TweetsandUsers
                                  {
                                      Id=tweets.Id,
                                      UserId = tweets.UserId,
                                      FirstName = user.Firstname,
                                      CreatedDate = tweets.CreatedDate,
                                      ImgName = user.Imgname,
                                      UserTweets = tweets.UserTweets,
                                      Likes=tweets.Likes
                                  }).ToList();
                return userTweets;
            }
        }

            /// <summary>
            /// UpdateDislikes.
            /// </summary>
            /// <param name="tweetId">BasedontweetId.</param>
            /// <returns>return the status.</returns>
            public bool UpdateDisLikes(int tweetId)
        {
            using (var dbContext = new TweetAppUseCaseContext())
            {
                var userDetails = dbContext.Tweets.Where(x => x.Id == tweetId).FirstOrDefault();
                if (userDetails != null)
                {
                    int? count = userDetails.DisLikes;
                    userDetails.DisLikes = userDetails.DisLikes + 1;
                    dbContext.Tweets.Update(userDetails);
                    var created = dbContext.SaveChanges();
                    return created > 0;
                }

                return false;

            }
        }

    }
}
