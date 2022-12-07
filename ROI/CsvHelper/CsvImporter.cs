using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace ROI.CsvHelper
{
    public class CsvImporter
    {
        public static List<CsvMap> ImportSomeRecords(string fileName)
        {
            var myRecords = new List<CsvMap>();

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
            Stream stream = assembly.GetManifestResourceStream(fileName);

            using (var reader = new System.IO.StreamReader(stream))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<CsvMapper>();

                    int contactID;
                    string name;
                    string phone;
                    string department;
                    string addressStreet;
                    string addressCity;
                    string addressState;
                    string addressZip;
                    string addressCountry;



                    //reads Csv file
                    csv.Read();
                    //Skip Header
                    csv.ReadHeader();

                    while (csv.Read())
                    {
                        contactID = csv.GetField<int>(0);
                        name = csv.GetField<string>(1);
                        phone = csv.GetField<string>(2);
                        department = csv.GetField<string>(3);
                        addressStreet = csv.GetField<string>(4);
                        addressCity = csv.GetField<string>(5);
                        addressState = csv.GetField<string>(6);
                        addressZip = csv.GetField<string>(7);
                        addressCountry = csv.GetField<string>(8);

                        myRecords.Add(CreateRecord(contactID, name, phone, department, addressStreet, addressCity, addressState, addressZip, addressCountry));
                    }
                }
            }
            return myRecords;
        }

        public static CsvMap CreateRecord(int contactID, string name, string phone, string department, string addressStreet, string addressCity, string addressState, string addressZip, string addressCountry)
        {
            CsvMap record = new CsvMap();
            record.contactID = contactID;
            record.name = name;
            record.phone = phone;
            record.department = department;
            record.addressStreet = addressStreet;
            record.addressCity = addressCity;
            record.addressState = addressState;
            record.addressZip = addressZip;
            record.addressCountry = addressCountry;

            return record;

        }
    }
}
