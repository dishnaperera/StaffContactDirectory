using System;
using System.Collections.Generic;
using System.Text;

namespace ROI.CsvHelper
{
    public class CsvMap
    {
        public int contactID { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string department { get; set; }
        public string addressStreet { get; set; }
        public string addressCity { get; set; }
        public string addressState { get; set; }
        public string addressZip { get; set; }
        public string addressCountry { get; set; }
    }
}
