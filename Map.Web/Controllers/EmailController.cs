using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Map.Models;
using Map.Data;
using Map.Web.Filters;
using WebApi.OutputCache.V2;
using Map.Data.Services;
using System.Text;
using System.Net.Mail;

namespace Map.Controllers
{
    public class EmailController : BaseController
	{
        [HttpGet]
        [Route("api/v1/email/")]
        public HttpResponseMessage Get(String email, String name = "", String reported_url = "", String place_name = "", int place_id = 0, String ua = "", String description = "", String issueType = "", String data = "")
		{
			SmtpClient client = new SmtpClient("mail.wsu.edu");
			//If you need to authenticate
			MailMessage mailMessage = new MailMessage();
			mailMessage.From = new MailAddress("noreply@wsu.edu");
			mailMessage.To.Add(email);
			mailMessage.Subject = "Hello There";
			mailMessage.Body = "Hello my friend!";

			client.Send(mailMessage);
			return this.JsonString("{Success}");
		}
    }
}
