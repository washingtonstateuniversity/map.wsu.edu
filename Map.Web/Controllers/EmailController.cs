using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using Map.Models;
using Map.Data;
using Map.Web.Filters;
using WebApi.OutputCache.V2;
using Map.Data.Services;
using System.Text;
using System.Net.Mail;
using System.IO;

using NVelocity;
using NVelocity.App;

namespace Map.Controllers
{
    public class EmailController : BaseController
	{
        [HttpGet]
        [Route("api/v1/email/")]
        public HttpResponseMessage Get(String email = "", String name = "", String reported_url = "", String place_name = "", int place_id = 0, String ua = "", String description = "", String issueType = "", String data = "")
		{
			var emailTemplatePath = HttpContext.Current.Server.MapPath(@"~/Views/Mail/place_errors.vm");
			var emailTemplate = System.IO.File.ReadAllText(emailTemplatePath);

			VelocityEngine velocity = new VelocityEngine();
			velocity.Init();

			VelocityContext context = new VelocityContext();
			context.Put("name", name);
			context.Put("email", email);
			context.Put("date", DateTime.Now);
			context.Put("reported_url", reported_url);
			context.Put("place_name", place_name);
			context.Put("place_id", place_id);
			context.Put("ua", ua);
			context.Put("description", description);
			context.Put("issueType", issueType);
			context.Put("data", data);

			StringWriter writer = new StringWriter();
			velocity.Evaluate(context, writer, "logtag", emailTemplate);
			var resultHtml = writer.GetStringBuilder().ToString();

			SmtpClient client = new SmtpClient("mail.wsu.edu");

			MailMessage mailMessage = new MailMessage();
			mailMessage.From = new MailAddress("noreply@wsu.edu");
			mailMessage.To.Add("nathan_owen@wsu.edu");
			mailMessage.Subject = "Reported Map error: " + issueType;
			mailMessage.Body = resultHtml;
			mailMessage.IsBodyHtml = true;
			client.Send(mailMessage);

			return this.JsonString("{\"Success\":true}");
		}
    }
}
