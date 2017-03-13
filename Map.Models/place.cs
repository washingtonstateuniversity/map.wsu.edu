using System;
using System.Collections.Generic;
using System.IO;
using NHibernate.Spatial.Type;
using Microsoft.SqlServer.Types;
using Newtonsoft.Json;

namespace Map.Models
{
    public class place : publish_base
    {
		public place() { }
		public place(int _id, string _name)
		{
			this.id = _id;
			this.prime_name = _name;
		}

		virtual public String getBestName()
        {
            return !string.IsNullOrEmpty(infoTitle) ? infoTitle.Trim() : prime_name.Trim();
        }

        virtual public int id { get; set; }
        virtual public string infoTitle { get; set; }
        virtual public string prime_name { get; set; }
        virtual public string abbrev_name { get; set; }
        virtual public string summary { get; set; }
        virtual public string details { get; set; }
        virtual public string address { get; set; }
        virtual public string street { get; set; }
        virtual public string city { get; set; }
        virtual public int zip_code { get; set; }
        virtual public int plus_four_code { get; set; }
        [JsonIgnore]
        virtual public Byte[] coordinate { get; set; }
        [JsonIgnore]
        virtual public bool hideTitles { get; set; }
        virtual public bool autoAccessibility { get; set; }
        virtual public IList<place_names> names { get; set; }
		virtual public IList<place_types> place_types { get; set; }
		[JsonIgnore]
        virtual public place_models model { get; set; }
        virtual public media_repo media { get; set; }
        virtual public string staticMap { get; set; }
        virtual public string pointImg { get; set; }
        virtual public campus campus { get; set; }
        virtual public String percentfull { get; set; }
        virtual public IList<schools> schools { get; set; }
        virtual public IList<colleges> colleges { get; set; }
        virtual public IList<departments> departments { get; set; }
        virtual public IList<admindepartments> admindepartments { get; set; }
        virtual public IList<categories> categories { get; set; }
        virtual public IList<programs> programs { get; set; }
        virtual public IList<map_views> views { get; set; }
        virtual public IList<fields> fields { get; set; }
        virtual public IList<tags> tags { get; set; }
        [JsonIgnore]
        virtual public IList<usertags> usertags { get; set; }
        virtual public IList<media_repo> Images { get; set; }
        virtual public IList<comments> comments { get; set; }
        virtual public IList<comments> comments_pub { get; set; }
		[JsonIgnore]
		virtual public IList<infotabs> infotabs { get; set; }
        [JsonIgnore]
        virtual public IList<users> authors { get; set; }
        virtual public IList<place_data> Placedata { get; set; }
        [JsonProperty("shapes")]
        virtual public IList<geometrics> geometrics { get; set; }
        virtual public float latitude
        {
            get {
                if (coordinate != null)
                {
                    return getLat();
                }
                else return 0;
            }

            set
            {
            }
        }

        virtual public float longitude
        {
            get
            {
                if (coordinate != null)
                {
                    return getLng();
                }
                else return 0;
            }

            set
            {
            }
        }

        public static SqlGeography AsGeography(byte[] bytes)
        {
            SqlGeography geo = new SqlGeography();
            using (MemoryStream stream = new System.IO.MemoryStream(bytes))
            {
                using (BinaryReader rdr = new System.IO.BinaryReader(stream))
                {
                    geo.Read(rdr);
                }
            }

            return geo;
        }

        virtual public float getLat()
        {
            SqlGeography spatial = place.AsGeography(this.coordinate);
            float lat = 0;
            float.TryParse(spatial.Lat.ToString(), out lat);
            return lat;
        }

        virtual public float getLng()
        {
            SqlGeography spatial = place.AsGeography(this.coordinate);
            float lng = 0;
            float.TryParse(spatial.Long.ToString(), out lng);
            return lng;
        }
    }
}