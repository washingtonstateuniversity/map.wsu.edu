using System;
using System.Collections.Generic;
using System.IO;

namespace Map.Models {

    public class place_data
    {
        virtual public int Id { get; set; }
        virtual public place Place { get; set; }
        virtual public String Key { get; set; }
        virtual public String Data { get; set; }
    }

}

