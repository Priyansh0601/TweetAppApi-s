using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApp.Repository.TweetAppEntity;
using TweetApp.Service;

namespace TweetApp
{
    //[EnableCors("MODPolicy")]
    [ApiController]
    [Produces("application/json")]
    public class TweetController : ControllerBase
    {
        private readonly ITweetService service;

        /// <summary>
        /// Create the instance of the tweetController.
        /// </summary>
        /// <param name="service">service.</param>
        public TweetController(ITweetService service)
        {

            this.service = service;
        }

        /// <summary>
        /// Adds the new tweet
        /// </summary>
        /// <param name="tweet">Tweet.</param>
        /// <returns>returns the status message.</returns>

        [Route("AddNewTweet")]
        [HttpPost]
        public IActionResult AddNewTweet([FromBody]Tweet tweet)
        {
            string message = null;
            try
            {
                if (tweet != null)
                {
                    message = this.service.AddNewTweet(tweet);
                }
                return Ok(new { status = message });

            }
            catch (TweetException ex)
            {
                throw new TweetException("error in add new tweet" + ex.Message);
            }
        }

        /// <summary>
        /// Views all user tweets.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>returns all the user tweets.</returns>
        [Route("ViewUserAllTweets/{userId}")]
        [HttpGet]
        public List<TweetsandUsers> ViewUserAllTweets(string userId)
        {
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    var tweets = this.service.GetUserTweets(userId);
                    return tweets;
                }
                return null;
            }
            catch (TweetException ex)
            {
                throw new TweetException("error in add new tweet" + ex.Message);
            }


        }

        /// <summary>
        /// Views all user tweets.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>returns all the user tweets.</returns>
        [Route("ViewOtherUsersAllTweets/{userId}")]
        [HttpGet]
        public List<TweetsandUsers> ViewOtherUsersAllTweets(string userId)
        {
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    var tweets = this.service.GetOtherUsersTweets(userId);
                    return tweets;
                }
                return null;
            }
            catch (TweetException ex)
            {
                throw new TweetException("error in add new tweet" + ex.Message);
            }


        }

        /// <summary>
        /// Get all the users list.
        /// </summary>
        /// <returns>returns all the users.</returns>
        [Route("GetAllUserList")]
        [HttpGet()]
        public List<AllUsers> GetAllUserList()
        {
            try
            {
                var userList = this.service.AllUserList();
                return userList;
            }
            catch (TweetException ex)
            {
                throw new TweetException("error occurred at get all user List" + ex.Message);
            }
        }

        [Route("GetAllUserandTweetList")]
        [HttpGet]
        public List<TweetsandUsers> GetAllUserandTweetList()
        {
            try
            {
                var userList = this.service.GetUserandTweetList();
                return userList;
            }
            catch (TweetException ex)
            {
                throw new TweetException("error occurred at get all user List" + ex.Message);
            }
        }

        [Route("TweetLike/{tweetId}/{userId}")]
        [HttpGet]
        public List<TweetsandUsers> TweetLikes(int tweetId, string userId)
        {
            try
            {
                var tweet = this.service.GetLikes(tweetId, userId);
                return tweet;
            }
            catch (TweetException ex)
            {
                throw new TweetException("error occurred at TweetLikes" + ex.Message);
            }
        }

        [Route("TweetDisLike/{tweetId}")]
        [HttpGet]
        public List<TweetsandUsers> TweetDisLikes(int tweetId, string userId)
        {
            try
            {
                var count = this.service.GetDisLikes(tweetId);
                return count;
            }
            catch (TweetException ex)
            {
                throw new TweetException("error occurred at TweetDisLikes" + ex.Message);
            }
        }

        [Route("getTweetComments/{tweetId}")]
        [HttpGet]
        public List<CommentsOnTweet> GetTweetComments(int tweetId)
        {
            try
            {
                var count = this.service.GetTweetComments(tweetId);
                return count;
            }
            catch (TweetException ex)
            {
                throw new TweetException("error occurred at GetTweetComments" + ex.Message);
            }
        }

        [Route("addTweetComments/{tweetId}")]
        [HttpPost]
        public bool AddTweetComments([FromBody] Repository.TweetAppEntity.AddTweetComments comments,int tweetId)
        {
            try
            {
                var status = this.service.AddTweetComment(comments,tweetId);
                return status;
            }
            catch (TweetException ex)
            {
                throw new TweetException("error occurred at AddTweetComments" + ex.Message);
            }
        }

    }
}
