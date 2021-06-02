using System;
using System.Collections.Generic;
using TweetApp.Repository.TweetAppEntity;

namespace TweetApp.Service
{
    public interface ITweetService
    {
        string UserRegistration(User userRegister);
        string UserExists(string emailId);
        User UserLogin(string userID, string password);
        string AddNewTweet(Tweet tweet);
        List<TweetsandUsers> GetUserTweets(string userId);
        List<TweetsandUsers> GetOtherUsersTweets(string userId);
        List<AllUsers> AllUserList();
        List<TweetsandUsers> GetUserandTweetList();
        bool UpdatePassword(string userId, string oldPassword, string newPassword);
        string ForgotPasswordEmailId(string emailId);
        string ForgotPassword(string emailId, string newPassword);
        List<TweetsandUsers> GetLikes(int tweetId, string userId);
        List<TweetsandUsers> GetDisLikes(int tweetId);
        bool AddTweetComment(AddTweetComments comments, int tweetId);
        List<CommentsOnTweet> GetTweetComments(int tweetId);
    }
}
