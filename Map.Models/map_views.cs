using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Map.Models {
    public class map_views : publish_base {
        virtual public int id { get; set; }
        virtual public string name { get; set; }
        virtual public string alias { get; set; }
        virtual public string key { get; set; }
        virtual public string cache { get; set; }
        virtual public bool show_global_nav { get; set; }
        virtual public bool commentable { get; set; }
        virtual public bool sharable { get; set; }
        virtual public int width { get; set; }
        virtual public int height { get; set; }
        virtual public string fit_to_bound { get; set; }
        virtual public string json_style_override { get; set; }
        virtual public string marker_json_style_override { get; set; }
        virtual public string shape_json_style_override { get; set; }
        virtual public IList<users> Authors { get; set; }
        virtual public Byte[] center { get; set; }
        virtual public media_repo media { get; set; }
        virtual public string staticMap { get; set; }
        virtual public IList<comments> Comments { get; set; }
        virtual public IList<comments> comments_pub { get; set; }
        virtual public IList<fields> field { get; set; }
        [JsonIgnore]
        virtual public IList<place> Places { get;  set; }
        virtual public IList<geometrics> geometrics { get; set; }
        virtual public styles forced_shapes_style { get; set; }
        virtual public styles forced_marker_style { get; set; }
        virtual public campus campus { get; set; }
        virtual public String options_obj { get; set; }
    }
}

