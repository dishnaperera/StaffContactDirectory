using CsvHelper;
using CsvHelper.Configuration;
using ROI.CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace ROI
{
    public partial class MainPage : ContentPage
    {
        
        private void loaddata()
        {
            //Path of the csv file named as abc.csv
            string pth = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "abc.csv");
            //csv header configuration file.
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };
            //checks if the file exists
            if (File.Exists(pth))
            {//reading the csv file
                var z = File.ReadAllText(pth);
                //checks if the file is not empty.
                if (!string.IsNullOrEmpty(z.ToString()))
                {
                    List<CsvMap> lst2 = new List<CsvMap>();
                    using (var stream = File.OpenRead(pth))
                    using (var writer = new StreamReader(stream))
                    using (var csv = new CsvReader(writer, config))
                    {
                        using (var dr = new CsvDataReader(csv))
                        {//reading the csv file with the data reader.
                            while (dr.Read())
                            {
                                var mm = dr[0].ToString();
                                if (!string.IsNullOrEmpty(mm))
                                {
                                    CsvMap obj2 = new CsvMap();
                                    obj2.contactID = dr.GetInt32(0);
                                    obj2.name = dr.GetString(1).ToString();
                                    obj2.phone = dr.GetString(2).ToString();
                                    obj2.department = dr.GetString(3);
                                    obj2.addressStreet = dr.GetString(4);
                                    obj2.addressCity = dr.GetString(5);
                                    obj2.addressState = dr.GetString(6);
                                    obj2.addressZip = dr.GetString(7);
                                    obj2.addressCountry = dr.GetString(8);
                                    lst2.Add(obj2);
                                }
                            }
                        }
                    }
                    PersonView.ItemsSource = lst2;
                }
            }
            else
            {
                //record initializing into the list and also saving to the csv file.
                //File.Create(pth);
                List<CsvMap> lst2 = new List<CsvMap>();
                CsvMap obj = new CsvMap();
                obj.contactID = 1;
                obj.name = "John Smith";
                obj.phone = "02 9988 2211";
                obj.department = "1";
                obj.addressStreet = "1 Code Lane";
                obj.addressCity = "Javaville";
                obj.addressState = "NSW";
                obj.addressZip = "100";
                obj.addressCountry = "Australia";
                //lst2.Add(obj);
                CsvMap obj1 = new CsvMap();
                obj1.contactID = 2;
                obj1.name = "Sue White";
                obj1.phone = "03 8899 2255";
                obj1.department = "2";
                obj1.addressStreet = "16 Bit Way";
                obj1.addressCity = "Byte Cove";
                obj1.addressState = "QLD";
                obj1.addressZip = "1101";
                obj1.addressCountry = "Australia";
                //lst2.Add(obj);
                CsvMap obj2 = new CsvMap();
                obj2.contactID = 3;
                obj2.name = "Bob O’Bits";
                obj2.phone = "05 7788 2255";
                obj2.department = "3";
                obj2.addressStreet = "8 Silicon Road";
                obj2.addressCity = "Cloud Hills";
                obj2.addressState = "VIC";
                obj2.addressZip = "1001";
                obj2.addressCountry = "Australia";
                //lst2.Add(obj);
                CsvMap obj3 = new CsvMap();
                obj3.contactID = 4;
                obj3.name = "Mary Blue";
                obj3.phone = "06 4455 9988";
                obj3.department = "2";
                obj3.addressStreet = "4 Processor Boulevard";
                obj3.addressCity = "Appletson";
                obj3.addressState = "NT";
                obj3.addressZip = "1010";
                obj3.addressCountry = "Australia";
                //lst2.Add(obj);
                CsvMap obj4 = new CsvMap();
                obj4.contactID = 5;
                obj4.name = "Mick Green";
                obj4.phone = "02 9988 1122";
                obj4.department = "3";
                obj4.addressStreet = "700 Bandwidth Street";
                obj4.addressCity = "Bufferland";
                obj4.addressState = "NSW";
                obj4.addressZip = "110";
                obj4.addressCountry = "Australia";
                //lst2.Add(obj);
                PersonView.ItemsSource = lst2;
                using (var stream = File.Create(pth))
                using (var writer = new StreamWriter(stream))
                using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteRecord(obj);
                    csvWriter.NextRecord();
                    csvWriter.WriteRecord(obj1);
                    csvWriter.NextRecord();
                    csvWriter.WriteRecord(obj2);
                    csvWriter.NextRecord();
                    csvWriter.WriteRecord(obj3);
                    csvWriter.NextRecord();
                    csvWriter.WriteRecord(obj4);
                    csvWriter.Flush();
                }
            }
        }
        protected override void OnAppearing()
        {
            
            base.OnAppearing();
            loaddata();
        }
        private void PersonView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var z = e.SelectedItem as CsvMap;
            //saving data to the properties for later use.
            Application.Current.Properties["OKAY"] = z;
            Application.Current.SavePropertiesAsync();
            //moving from one form to another form.
            Navigation.PushAsync(new UserProfile());
        }

        private void btnAddContact_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserProfile());
        }
    }
}