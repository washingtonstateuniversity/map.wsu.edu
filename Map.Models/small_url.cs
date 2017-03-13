using System;

namespace Map.Models
{
    public class small_url
    {
		public small_url()
		{
		}

		public small_url(int _id, string _sm_url)
		{
			this.id = _id;
			this.sm_url = _sm_url;
		}

		virtual public int id { get; set; }
        virtual public String sm_url { get; set; }
        virtual public String or_url { get; set; }
     }
}
