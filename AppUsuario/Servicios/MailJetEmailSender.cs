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
        private readonly OpcionesMailJet _opcionesMailJet;
        
        public MailJetEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            _opcionesMailJet = _configuration.GetSection("MailJet").Get<OpcionesMailJet>();

            MailjetClient client = new MailjetClient(_opcionesMailJet.AppKey, _opcionesMailJet.SecretKey)
            {
                Version = ApiVersion.V3_1,
            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
             .Property(Send.Messages, new JArray {
     new JObject {
      {
       "From",
       new JObject {
        {"Email", "appusuariozoho@zohomail.com"},
        {"Name", "Alex"}
       }
      }, {
       "To",
       new JArray {
        new JObject {
         {
          "Email",
          email
         }, {
          "Name",
          "Alex"
         }
        }
       }
      }, {
       "Subject",
       subject
      }, {
       "HTMLPart",
       htmlMessage
      }
     }
             });
            await client.PostAsync(request);
        }
    }
}
