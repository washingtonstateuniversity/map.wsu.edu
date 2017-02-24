using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

namespace Map.Models
{
    public class styles 
    {
        virtual public int id { get; set; }
        virtual public string name { get; set; }
        virtual public geometrics_types type { get; set; }
        virtual public string style_obj { get; set; }
        virtual public IList<style_options> _option { get; set; }
        virtual public IList<geometric_events> g_event { get; set; }
        virtual public IList<zoom_levels> _zoom { get; set; }

        virtual public String getoptionValue(String mouseevent, String option)
        {

            var values = new Dictionary<string, object>();
            if (!String.IsNullOrWhiteSpace(this.style_obj) && this.style_obj != "{}")
            {
                var jss = new  JavaScriptSerializer();
                var options = jss.Deserialize<Dictionary<string, dynamic>>(this.style_obj);
                options.ToList<KeyValuePair<string, dynamic>>();
                foreach (KeyValuePair<string, dynamic> op in options)
                {
                    values.Add(op.Key, op.Value);
                }
            }

            String option_str = "";

            dynamic value;
            if (values.TryGetValue("events", out value))
                if (value.TryGetValue(mouseevent, out value))
                    if (value.TryGetValue(option, out value))
                        option_str = value.ToString();
            return option_str;
        }        
    }
       
}

