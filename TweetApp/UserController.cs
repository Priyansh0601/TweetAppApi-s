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
    public class UserController : ControllerBase
    {
        private readonly ITweetService service;

        /// <summary>
        /// create the object of usercontroller.
        /// </summary>
        /// <param name="service">service dependenc injection.</param>
        public UserController(ITweetService service)
        {
            this.service = service;
        }

        /// <summary>
        /// user login
        /// </summary>
        /// <param name="userId">based on userId.</param>
        /// <param name="password">based on password</param>
        /// <returns>returns the status message.</returns>
        
        [Route("UserLogin/{userId}/{password}")]
        [HttpGet]
        public User UserLogin(string userId, string password)
        {
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(password))
                {
                    var user = this.service.UserLogin(userId, password);
                    //if (user != null)
                    //{
                    //    token = new Token() { UserId =user., Username = result.Username, Tokens = this.GenerateJwtToken(username), Message = "Success" };
                    //}
                    return user;
                }
            }

            catch (TweetException ex)
            {
                throw new TweetException(BadRequest("error in userLogin") + ex.Message);
            }
            return null;


        }

        /// <summary>
        /// new user registration.
        /// </summary>
        /// <param name="userRegistration">userRegiastration.</param>
        /// <returns>returns the status message of user register.</returns>
        [Route("UserRegister")]
        [HttpPost]
        public IActionResult UserRegister([FromBody] User userRegistration)
        {
            try
            {
                if (userRegistration != null)
                {
                    if (userRegistration.ImgName == null)
                    {
                        userRegistration.ImgName = "TU.jpg";
                    }
                    var statusMessage = this.service.UserRegistration(userRegistration);
                    return Ok(new { status = statusMessage });
                }
            }
            catch (TweetException ex)
            {
                throw new TweetException("error in user register" + ex.Message);
            }
            return Ok(new { status = Messages.EnterUserDetails });


        }

        [Route("UserExists/{emailId}")]
        [HttpGet]
        public string UserExists(String emailId)
        {
            try
            {               
                    var statusMessage = this.service.UserExists(emailId);
                if (!string.IsNullOrEmpty(statusMessage)) 
                { 
                    return statusMessage; 
                }
                return null;
                    
                
            }
            catch (TweetException ex)
            {
                throw new TweetException("error in user register" + ex.Message);
            }


        }

        /// <summary>
        /// Updates the user password.
        /// </summary>
        /// <param name="userId">based on userId.</param>
        /// <param name="newPassword">updates the new password.</param>
        /// <returns>returns the status message whether the password is updated or not.</returns>
        [Route("resetpassword/{userId}/{oldpassword}/{newPassword}")]
        [HttpPut]
        public IActionResult PasswordUpdate(string userId, string oldpassword, string newPassword)
        {
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(newPassword))
                {

                    var result = this.service.UpdatePassword(userId, oldpassword, newPassword);
                    if (result == true)
                    {
                        return Ok(new { status = Messages.PasswordUpdated });
                    }

                }
            }
            catch (TweetException ex)
            {
                throw new TweetException("error in password update" + ex.Message);
            }

            return Ok(new { status = Messages.PasswordNotUpdated });
        }

        /// <summary>
        /// Password update emailid
        /// </summary>
        /// <param name="emailid">emailid.</param>
        /// <returns>return the status message</returns>
        [Route("ForgotPasswordEmailId/{emailId}")]
        [HttpGet]
        public IActionResult ForgotPasswordEmailId(string emailId)
        {
            try
            {
                if (!string.IsNullOrEmpty(emailId))
                {
                    var result = this.service.ForgotPasswordEmailId(emailId);
                    return Ok(new { status = result });

                }
            }
            catch (TweetException ex)
            {
                throw new TweetException("error in forgot email id " + ex.Message);
            }

            return Ok(new { status = Messages.NoChangepassword });
        }

        /// <summary>
        /// Pass
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [Route("ForgotPassword/{userId}/{newPassword}")]
        [HttpPut]
        public IActionResult ForgotPassword(string userId, string newPassword)
        {
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(newPassword))
                {
                    var result = this.service.ForgotPassword(userId, newPassword);
                    return Ok(new { status = result });

                }
            }
            catch (TweetException ex)
            {
                throw new TweetException("error in password update" + ex.Message);
            }

            return Ok(new { status = Messages.PasswordNotUpdated });
        }
    }
}
