using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace AppUsuario.Servicios
{
    public class MailJetEmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        
        public MailJetEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailjetClient client = new MailjetClient(Environment.GetEnvironmentVariable("****************************1234"), Environment.GetEnvironmentVariable("****************************abcd"))
            {
                Version = ApiVersion.V3_1,
            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
            .Property(Send.Messages, new JArray 
            {
                new JObject
                {
                    {
       "From",
       new JObject {
        {"Email", "aroch2222@gmail.com"},
        {"Name", "Alejandro"}
       }
      }, {
       "To",
       new JArray {
        new JObject {
         {
          "Email",
          "aroch2222@gmail.com"
         }, {
          "Name",
          "Alejandro"
         }
        }
       }
      }, {
       "Subject",
       "Greetings from Mailjet."
      }, {
       "TextPart",
       "My first Mailjet email"
      }, {
       "HTMLPart",
       "<h3>Dear passenger 1, welcome to <a href='https://www.mailjet.com/'>Mailjet</a>!</h3><br />May the delivery force be with you!"
      }, {
       "CustomID",
       "AppGettingStartedTest"
      }
                }
            });
            await client.PostAsync(request);
        }
    }
}
