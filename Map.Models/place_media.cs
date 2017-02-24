using System;
using System.Collections.Generic;
using System.IO;

namespace Map.Models {
  
    public class place_media {
        virtual public int Id { get; set; }
        virtual public place Place { get; set; }
        virtual public media_repo Media { get; set; }
        virtual public int place_order { get; set; }
    }

}

