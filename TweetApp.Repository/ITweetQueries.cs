using System;
using System.Collections.Generic;
using TweetApp.Repository.TweetAppEntity;

namespace TweetApp.Repository
{
    public interface ITweetQueries
    {
        bool UserRegister(User userRegister);
        User Userlogin(string userId);
        string UserExist(string emailId);
        bool AddTweet(Tweet tweet);
        List<TweetsandUsers> GetUserTweets(string userId);
        List<AllUsers> GetAllUsers();
        bool UpdatePassword(string userId, string oldPassword, string newPassword);
        bool ForgotPasswordEmail(string emailId);
        bool ForgotPassword(string emailId, string newPassword);
        List<TweetsandUsers> GetUserandTweetList();

        List<TweetsandUsers> LikesandDislikesCount(int tweetId);

        bool UpdateDisLikes(int tweetId);

        bool UpdateLikes(int tweetId);

        List<CommentsOnTweet> FetchTweetComments(int tweetId);

        bool AddTweetComments(AddTweetComments comments, int tweetId);
        List<TweetsandUsers> GetOtherUserTweets(string userId);

    }
}
