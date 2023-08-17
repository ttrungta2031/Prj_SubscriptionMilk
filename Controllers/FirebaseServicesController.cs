using Firebase.Auth;
using Firebase.Storage;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SubscriptionMilk.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SubscriptionMilk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirebaseServicesController : ControllerBase
    {

        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _config;
        private readonly SubcriptionMilkContext _context;
        private static string apiKey = "AIzaSyDDVy4GLRDjM5exKyfY-o8cdRosy0CTQ_Y";

        private static string Bucket = "subscriptionmilk.appspot.com";
        private static string AuthEmail = "testfirebase@gmail.com";
        private static string AuthPassword = "Test@123";
        public FirebaseServicesController(IConfiguration config, SubcriptionMilkContext context, IHostingEnvironment env)
        {
            _config = config;
            _context = context;
            _env = env;
        }

        [HttpPost("logincustomer")]
        public async Task<IActionResult> loginCustomer(string email, string password)
        {
            var a = GetAccount_byEmail(email);


            if (a != null)
            {
                var hasuser = _context.Accounts.SingleOrDefault(p => (p.Email.ToUpper() == email.ToUpper()) && password == p.Password && p.IsAdmin == false);
                if (hasuser == null)
                {
                    return BadRequest(" The Account not exist or Invalid email/password!!");
                }

                if (hasuser != null)
                {
                    var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
                    var authen = await auth.SignInWithEmailAndPasswordAsync(a.Email, password);
                    string tokenfb = authen.FirebaseToken;
                    string uidfb = authen.User.LocalId;
                    int iddb = hasuser.Id;
                    if (tokenfb != "")
                    {
                        return Ok(new { StatusCode = 200, Message = "Login with role customer successful",uidfb, iddb });
                    }

                }
            }
            return BadRequest("The Account not exist or Invalid email/password!!");
        }



        [HttpPost("loginadmin")]
        public async Task<IActionResult> loginAdmin(string email, string password)
        {
            var a = GetAccount_byEmail(email);


            if (a != null)
            {
                var hasuser = _context.Accounts.SingleOrDefault(p => (p.Email.ToUpper() == email.ToUpper()) && password == p.Password && p.IsAdmin == true);
                if (hasuser == null)
                {
                    return BadRequest(" The Account not exist or Invalid email/password!!");
                }

                if (hasuser != null)
                {
                    var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
                    var authen = await auth.SignInWithEmailAndPasswordAsync(a.Email, password);
                    string tokenfb = authen.FirebaseToken;
                    string uidfb = authen.User.LocalId;
                    int iddb = hasuser.Id;
                    if (tokenfb != "")
                    {
                        return Ok(new { StatusCode = 200, Message = "Login with role Admin successful",uidfb,iddb });
                    }

                }
            }
            return BadRequest("The Account not exist or Invalid email/password!!");
        }



        private Models.Account GetAccount_byEmail(string email)
        {
            var account = _context.Accounts.Where(a => a.Email.ToUpper() == email.ToUpper()).FirstOrDefault();

            if (account == null)
            {
                return null;
            }

            return account;
        }



    }
}
