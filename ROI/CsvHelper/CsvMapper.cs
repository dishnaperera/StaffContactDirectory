using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ROI.CsvHelper
{
    public class CsvMapper : ClassMap<CsvMap>
    {
        public CsvMapper()
        {
            Map(m => m.contactID).Index(0);
            Map(m => m.name).Index(1);
            Map(m => m.phone).Index(2);
            Map(m => m.department).Index(3);
            Map(m => m.addressStreet).Index(4);
            Map(m => m.addressCity).Index(5);
            Map(m => m.addressState).Index(6);
            Map(m => m.addressZip).Index(7);
            Map(m => m.addressCountry).Index(8);
        }
    }
}
