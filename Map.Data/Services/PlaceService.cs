using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Map.Models;
using System.Web.Script.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using NHibernate;
using System.Collections;

namespace Map.Data.Services
{
    public class PlaceService : IPlaceService
	{
        public IEnumerable<searchPlace> Search(string query)
        {
            List<searchPlace> results = new List<searchPlace>();
            if (!String.IsNullOrEmpty(query) && query.Length >= 3)
            {
                using (var session = SessionFactoryHelper.CreateSessionFactory().OpenSession())
                {
                    using (var tx = session.BeginTransaction())
                    {
                        // Search places
                        String searchprime_name = @"SELECT new searchPlace(p.prime_name, p.prime_name, p.id, 'false') FROM place AS p WHERE p.abbrev_name LIKE :name or p.prime_name LIKE :name order by p.prime_name ASC";
                        IQuery q = session.CreateQuery(searchprime_name);
                        q.SetParameter("name", "%" + query + "%");
                        results.AddRange(q.List<searchPlace>());

                        // Search place names
                        String placenamesql = "SELECT new searchPlace(pn.name, pn.name, pn.place_id, 'false') FROM place_names AS pn WHERE pn.name LIKE :name order by pn.name ASC";
                        q = session.CreateQuery(placenamesql);
                        q.SetParameter("name", "%" + query + "%");
                        results.AddRange(q.List<searchPlace>());

                        // Order results and put in a delimiter to delineate related results
                        results.OrderBy(x => x.label);
                        results.Add(new searchPlace("------", "------", 0, "header"));

                        // Search tags and return associated places
                        String tagsql = "select new searchPlace(p.prime_name, p.prime_name, p.id, 'true') FROM tags t inner join t.places p WHERE t.name LIKE :name order by p.prime_name ASC";
                        q = session.CreateQuery(tagsql);
                        q.SetParameter("name", "%" + query + "%");
                        results.AddRange(q.List<searchPlace>());
                    }
                }
            }

            return results.Distinct<searchPlace>();
        }

        public IEnumerable<place> GetAll()
        {
            IRepository<place> repo = new Repository<place>();
            return repo.GetAll<place>();
        }

        public place Get(int id)
        {
            IRepository<place> repo = new Repository<place>();
            return repo.GetReference<place>(id);
        }

		/*
		public String create_place_json(place[] items, String view, String style, bool all, bool dyno)
		{
			string appPath = "/";
			string rooturl = "https://map.wsu.edu/";
			String cachePath = appPath + "cache/places/";
			String placeList = "";
			String jsonStr = "";
			int count = 0;
			foreach (place item in items)
			{
				if ((item != null && item.status != null && item.status.id == 3 && item.isPublic) || all)
				{
					if (item.coordinate != null)
					{
						string file = item.id + (!String.IsNullOrWhiteSpace(view) ? "_" + view + "_" : "") + (!String.IsNullOrWhiteSpace(style) ? "_" + style + "_" : "") + "_render" + ".ext";
						if (!File.Exists(cachePath + file) || dyno)
						{
							//dynamic value;
							var jss = new JavaScriptSerializer();
							string details = ((!string.IsNullOrEmpty(item.details)) ? processFields(item.details, item).Replace("\"", @"\""").Replace('\r', ' ').Replace('\n', ' ') : "");

							String mainimage = "";
							if (item.Images.Count > 0)
							{
								//NOTE THIS IS AN EXAMPLE OF A DEFAULT SIZE; SIZE IS CHOSEN ON THE CLEINT SIDE
								int width = 148;
								int height = 100;
								if (item.Images[0].orientation == "v")
								{
									width = 100;
									height = 148;
								}
								String size = "w=" + width + "&h=" + height + "";
								mainimage = @"""prime_image"":{
                                        ""src"":""" + rooturl + "media/download.castle?placeid=" + (item.id) + "&id=" + (item.Images[0].id) + @""",
                                        ""thumb_params"":""&m=crop&" + (size) + @""",
                                        ""caption"":""" + (String.IsNullOrEmpty(item.Images[0].caption) ? "" : item.Images[0].caption) + @""",
                                        ""orientation"":""" + (item.Images[0].orientation) + @"""
                                    },";
							}

							String labeling = "";
							if (item.hideTitles != true)
							{
								labeling = @"""labels"":{
                                    ""title"":""" + (!string.IsNullOrEmpty(item.infoTitle) ? item.infoTitle.Trim() : item.prime_name.Trim()) + @""",
                                    ""prime_abbrev"":""" + (!string.IsNullOrEmpty(item.abbrev_name) ? item.abbrev_name.Trim() : "") + @""",
                                    ""other_names"":""""
                                },";
							}



							String imgGallery = "";
							String media_obj = "";
							if (item.Images.Count > 1)
							{
								String galImg = "";
								int c = 0;
								bool hasImg = false;
								bool hasVid = false;
								media_obj += @"""media"":[";
								foreach (media_repo media in item.Images)
								{
									if (c > 0)
									{
										galImg += "<a href='" + rooturl + "media/download.castle?placeid=" + item.id + "&id=" + media.id + "' alt='" + media.caption + "'  hidefocus='true' class='headImage orientation_" + media.orientation + "'>" +
											//"<span class=' imgEnlarge'></span>" +
											"<img src='" + rooturl + "media/download.castle?placeid=" + item.id + "&id=" + media.id + "&m=constrain&h=156' alt='" + media.caption + "' class='img-main' />" +
										"</a>";
										if (media.type.name == "general_image") hasImg = true;
										if (media.type.name == "general_video") hasVid = true;
									}
									c++;


									media_obj += @"
                                        {
                                            ""id"":""" + media.id + @""",
                                            ""caption"":""" + (media.caption) + @""",
                                            ""orientation"":""" + (media.orientation) + @""",
                                            ""type"":""" + (media.type.name) + @"""
                                        },";
								}
								media_obj = media_obj.TrimEnd(',') + @"],";
								String nav = "<div class='navArea'>" + (hasImg && hasVid ? "<a href='#' class='photos active' hidefocus='true'>Photos</a>" : "") +
									(c > 2 ? "<ul class='cNav'>" +
									//repeatStr("<li><a href='#' hidefocus='true'>{$i}</a></li>", item.Images.Count - 1) +
									"</ul>" : "") + (hasImg && hasVid ? "<a href='#' class='vids' hidefocus='true'>Video</a>" : "") + "</div>";
								String gallery = "<div class='cycleArea'><div class='cycle'>" + (c > 2 ? "<a href='#' class='prev' hidefocus='true'>Prev</a>" : "") + "<div class='cWrap'><div class='items'>" + galImg + "</div></div>" + (c > 2 ? "<a href='#' class='next' hidefocus='true'>Next</a>" : "") + "</div>" + nav + "</div>";

								imgGallery += @"
                                        {
                                            ""block"":""" + gallery + @""",
                                            ""title"":""" + (hasImg ? "Views" : "") + (hasImg && hasVid ? "/" : "") + (hasVid ? "Vids" : "") + @"""
                                        }";
							}


							String autoAccessibility = "";
							if (item.autoAccessibility)
							{
								string renderedTxt = autoProcessFeilds(item).Trim();
								//autoProcessFeilds(item)
								//processFields(defaultAccessibility, item)
								if (!String.IsNullOrEmpty(renderedTxt))
								{
									autoAccessibility += @"
                                        {
                                            ""block"":""" + "<ul>" + jsonEscape(renderedTxt) + @"</ul>" + @""",
                                            ""title"":""Accessibility""
                                        }";
								}
							}
							String infotabs = "";
							if (item.infotabs.Count > 0 || imgGallery != "" || autoAccessibility != "")
							{
								infotabs += @"[";

								String tabStr = "";

								infotabs += @"
                                            {
                                                ""block"":""" + details + @""",
                                                ""title"":""Overview""
                                            }";
								if (item.infotabs.Count > 0)
								{
									int c = 0;
									foreach (infotabs tab in item.infotabs)
									{
										c++;
										if (tab.title == "Views")
										{
											if (!String.IsNullOrEmpty(imgGallery))
											{
												tabStr += imgGallery;
												if (c < item.infotabs.Count) tabStr += ",";
											}
										}
										else
										{

											//string content = processFields(tab.content, item).Replace("\"", @"\""").Replace('\r', ' ').Replace('\n', ' ');
											string content = jsonEscape(autoFieldProcessing(item, tab.content));
											if (!String.IsNullOrWhiteSpace(content))
											{
												tabStr += @"
                                                {
                                                    ""block"":""" + content.Replace("\"", @"\""") + @""",
                                                    ""title"":""" + tab.title + @"""
                                                }";
												if (c < item.infotabs.Count) tabStr += ",";
											}
										}

									}
								}

								infotabs += (!string.IsNullOrEmpty(tabStr) ? "," : "") + tabStr.TrimEnd(',');
								infotabs += (!string.IsNullOrEmpty(autoAccessibility) ? "," : "") + autoAccessibility;
								infotabs += @"]";
							}
							else
							{
								if (String.IsNullOrEmpty(item.details))
								{
									infotabs += @"""""";
								}
								else
								{
									string content = jsonEscape(autoFieldProcessing(item, details));
									infotabs += @"""" + content + @"""";
								}
							}

							String style_obj = @"""style"":{
                                            ""icon"":""" + (!String.IsNullOrWhiteSpace(item.pointImg) ? rooturl + @"Content/images/map_icons/" + item.pointImg : "null") + @"""
                                            },";


							placeList = @"
                                {
                                    ""id"":""" + item.id + @""",
                                    ""position"":{
                                                ""latitude"":""" + item.latitude + @""",
                                                ""longitude"":""" + item.longitude + @"""
                                                },
                                    ""summary"":""" + ((!string.IsNullOrEmpty(item.summary)) ? StripHtml(jsonEscape(item.summary), false) : Truncate(StripHtml(jsonEscape(details), false), 65) + "...") + @""",
                                    " + mainimage + @"
                                    " + labeling + @"
                                    " + media_obj + @"
                                    " + style_obj + @"
                                    ""content"":" + infotabs + @",
                                    ""metadata"":[
                                                {
                                                    ""name"":""gamedayparkingpercentfull"",
                                                    ""value"":""" + item.percentfull + @"""
                                                }
                                            ],
                                    ""shapes"":" + loadPlaceShape(item) + @"
                                }";
							placeList = jsonEscape(placeList);

							// the goal with this is to make sure the maps don't break by simple testing that the data can be read
							// if it can not be read then we place a friendly showing that the data is bad to keep the map working
							bool dataGood = true;

							try { jss.Deserialize<Dictionary<string, dynamic>>(placeList); }
							catch (Exception e)
							{
								dataGood = false;
							}

							if (dataGood)
							{
								item.outputError = false;
								//ActiveRecordMediator<place>.Save(item);
								//ActiveRecordMediator<place>.Refresh(item);
							}
							else
							{
								item.outputError = true;
								//ActiveRecordMediator<place>.Save(item);
								//ActiveRecordMediator<place>.Refresh(item);
								placeList = @"{""error"":""Error in the output.  This place needs to be edited.""}";
							}
							if (dataGood)
							{
								//setJsonCache(cachePath, file, placeList);
							}
						}
						jsonStr += System.IO.File.ReadAllText(cachePath + file) + ",";
						count++;
					}
				}
				else
				{
					if (item != null)
					{
						item.outputError = true;
						//ActiveRecordMediator<place>.Save(item);
					}

				}

			}
			String json = "";
			json += @"  {
";
			if (count > 0)
			{
				json += @"""markers"":[";
				json += jsonStr.TrimEnd(',');
				json += @"]";
			}

			json += @"
    }";
			return json;
		}

		public string StripHtml(string html, bool allowHarmlessTags)
		{
			if (html == null || html == string.Empty)
				return string.Empty;
			if (allowHarmlessTags)
				return System.Text.RegularExpressions.Regex.Replace(html, "", string.Empty);
			return System.Text.RegularExpressions.Regex.Replace(html, "<[^>]*>", string.Empty);
		}

		public string stripNonSenseContent(string inputString)
		{
			return stripNonSenseContent(inputString, false);
		}

		public string stripNonSenseContent(string inputString, bool stripComments)
		{
			String output = Regex.Replace(inputString, @"<p>\s{0,}</p>", string.Empty);
			output = Regex.Replace(output, @"\t{1,}", " ");
			output = output.Replace('\t', ' ');
			output = Regex.Replace(output, @"\r\n{1,}", " ");
			output = Regex.Replace(output, @"=""\s{0,}(.*?)\s{0,}""", @"=""$1""");
			output = Regex.Replace(output, @"='\s{0,}(.*?)\s{0,}'", @"='$1'");
			output = Regex.Replace(output, @">\s{1,}<", @"><");//set for code with idea never presented in copy too
			if (stripComments) output = Regex.Replace(output, @"<!--(?!<!)[^\[>].*?-->", @"");
			//Just in case it's in type string
			output = output.Replace("\\r\\n", " ");
			output = output.Replace("\r\n", " ");
			output = output.Replace('\r', ' ');
			output = output.Replace('\n', ' ');
			//the retrun is for real.  clears both dos and machine carrage returns but not string
			output = output.Replace(@"
", "");
			output = Regex.Replace(output, @"\s{2,}", " ");
			return output;
		}
		public string jsonEscape(string inputString)
		{
			String output = stripNonSenseContent(inputString);
			output = output.Replace("\"", @"\""");
			output = output.Replace("\\\"", "\"");
			output = output.Replace(@"\", @"\\");
			output = output.Replace(@"\\", @"\");
			return output;
		}

		public string UrlEncode(string inputString)
		{
			String output = System.Uri.EscapeDataString(inputString);
			return output;
		}
		public string Truncate(string source, int length)
		{
			if (source.Length > length)
			{
				source = source.Substring(0, length);
			}
			return source;
		}

	public string autoFieldProcessing(place place, string text)
		{
			if (place.model != null)
			{
				//log.Info("________________________________________________________________________________\nLoading feilds For:" + place.prime_name+"("+place.id+")\n");
				//List<AbstractCriterion> typeEx = new List<AbstractCriterion>();
				//typeEx.Add(Expression.Eq("model", "placeController"));
				//typeEx.Add(Expression.Eq("set", place.model.id));
				String sql = "FROM field_types where model = :model and set = :set";
				IList<field_types> ft = null;
				using (var session = SessionFactoryHelper.CreateSessionFactory().OpenSession())
				{
					IQuery q = session.CreateQuery(sql);
					q.SetParameter("model", "placeController");
					q.SetParameter("set", place.model.id);
					ft = q.List<field_types>();
				}
				List<string> fields = new List<string>();


				//log.Error(" place:" + place.prime_name);
				Hashtable hashtable = new Hashtable();
				hashtable["place"] = place;

				if (ft != null)
				{
					foreach (field_types ft_ in ft)
					{
						string value = "";
						value = getFieldVal(ft_, place);
						hashtable["" + ft_.alias] = value;
						//log.Info("hashtable[" + ft_.alias+"]" + value);
					}
				}
				//text = helperService.proccessText(hashtable, text, false).Trim();
				//log.Info("text:" + text);
			}
			return text;
		}*/
	}
}
