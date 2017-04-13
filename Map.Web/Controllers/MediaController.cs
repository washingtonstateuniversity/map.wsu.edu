using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Http;
using Map.Models;
using Map.Web.Services;
using Map.Data;
using Map.Web.Filters;
using WebApi.OutputCache.V2;
using Map.Data.Services;
using System.Text;
using System.Web;

namespace Map.Controllers
{
    public class MediaController : BaseController
    {
		private ImageService imageService = new ImageService();
		private IPlaceService placeService = new PlaceService();

		[HttpGet]
		[Route("api/v1/media/{id}")]
		public HttpResponseMessage Download(int id, int placeid = 0, int w = 0, int h = 0, String m = null)
		{
			int p = 0;
			bool protect = false;
			string pre = null, mark = null;
			IRepository<media_repo> repo = new Repository<media_repo>();
			media_repo image = repo.GetReference<media_repo>(id);
			string uploadPath = "";
			if (image.path != null)
			{
				uploadPath = image.path;
				uploadPath = Regex.Replace(uploadPath, "(.*)(\\\\.*?)(.*)", "$1");
				if (!uploadPath.EndsWith("\\"))
					uploadPath += "\\";
			}
			else
			{
				// build the path for the new image
				uploadPath = HttpContext.Current.Server.MapPath("/uploads");

				if (placeid != 0)
				{
					place place = placeService.Get(placeid);
					uploadPath += @"place\" + place.id + @"\";

					//check for place level image existence
					string orgFile = HttpContext.Current.Server.MapPath(uploadPath + id + ".ext");
					if (!File.Exists(orgFile))
					{
						//it didn't so lets take a look at the pool for the image
						string newuploadPath = HttpContext.Current.Request.ApplicationPath + @"\uploads\";
						string neworgFile = HttpContext.Current.Server.MapPath(newuploadPath + id + ".ext");
						if (File.Exists(neworgFile))
						{
							uploadPath = HttpContext.Current.Request.ApplicationPath + @"\uploads\";
						}
					}
				}
			}

			if (String.IsNullOrWhiteSpace(m))
			{
				m = "constrain";
				// this will be site prefenece for max served iamges.
				w = 1000;
				h = 1000;
			}

			// Store the file with constraints in the filename
			string arg = (!String.IsNullOrEmpty(pre) ? "_" + pre + "_" : "");
			arg += (w != 0 ? "w_" + w + "_" : "");
			arg += (h != 0 ? "h_" + h + "_" : "");
			arg += (p != 0 ? "p_" + p + "_" : "");
			arg += (protect != false ? "pro_true_" : "");
			arg += (!String.IsNullOrEmpty(m) ? "m_" + m + "_" : "");
			arg += (!String.IsNullOrEmpty(mark) ? "mark_" + mark + "_" : "");

			string newFile = HttpContext.Current.Server.MapPath(uploadPath + id + arg + ".ext");

			// if the process image doesn't Exist yet create it
			if (!File.Exists(newFile))
			{
				string baseFile = uploadPath + id + ".ext";
				if (!File.Exists(baseFile))
				{
					baseFile = uploadPath + image.file_name;
				}

				if (File.Exists(HttpContext.Current.Server.MapPath(baseFile)))
				{
					System.Drawing.Image processed_image = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(baseFile));
					//set some defaults
					ImageService.imageMethod methodChoice = ImageService.imageMethod.Percent;
					ImageService.Dimensions dimensional = ImageService.Dimensions.Width;

					//choose medth of sizing and set their defaults
					switch (m)
					{
						case "percent":
							methodChoice = ImageService.imageMethod.Percent;
							break;
						case "constrain":
							methodChoice = ImageService.imageMethod.Constrain;
							dimensional = w != 0 ? ImageService.Dimensions.Width : ImageService.Dimensions.Height;
							break;
						case "fixed":
							methodChoice = ImageService.imageMethod.Fixed;
							break;
						case "crop":
							methodChoice = ImageService.imageMethod.Crop;
							break;
					}

					imageService.process(id, processed_image, newFile, methodChoice, p, h, w, dimensional, protect, mark, image.ext);
				}
				else
				{
					//@todo put in placeholder if the original doesn't exist
				}
			}

			String contentType = imageService.mimeFinder(image.ext.ToLower()); // "applicaton/image";

			HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
			var path = HttpContext.Current.Server.MapPath(uploadPath + id + arg + ".ext");

			if (File.Exists(path))
			{
				var stream = new FileStream(HttpContext.Current.Server.MapPath(uploadPath + id + arg + ".ext"), FileMode.Open, FileAccess.Read);
				result.Content = new StreamContent(stream);
				result.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
			}

			return result;
		}
	}
}
