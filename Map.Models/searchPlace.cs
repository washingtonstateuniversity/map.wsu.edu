using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map.Models
{
    public class searchPlace
    {
        private String _label, _labelvalue;
        private int _place_id;
        private String _related;

        public searchPlace(String _label, string _value, int _place_id)
        {
            this._label = _label;
            this._labelvalue = _value;
            this._place_id = _place_id;
            this._related = "false";
        }

        public searchPlace(String _label, string _value, int _place_id, String _related)
        {
            this._label = _label;
            this._labelvalue = _value;
            this._place_id = _place_id;
            this._related = _related;
        }

        public searchPlace(String _label, string _value, String _place_id, String _related)
        {
            this._label = _label;
            this._labelvalue = _value;
            Int32.TryParse(_place_id, out this._place_id);
            this._related = _related;
        }

        public string label
        {
            get
            {
                return _label;
            }

            set
            {
                _label = value;
            }
        }

        public int place_id
        {
            get
            {
                return _place_id;
            }

            set
            {
                _place_id = value;
            }
        }

        public String related
        {
            get
            {
                return _related;
            }

            set
            {
                _related = value;
            }
        }

        public string value
        {
            get
            {
                return _labelvalue;
            }

            set
            {
                this._labelvalue = value;
            }
        }
    }
}
