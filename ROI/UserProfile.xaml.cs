using CsvHelper;
using CsvHelper.Configuration;
using ROI.CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ROI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfile : ContentPage
    {
        public UserProfile()
        {
            InitializeComponent();
            var x = Application.Current.Properties.Keys;
            if (x.Count > 0)
            {
                var value = Application.Current.Properties["OKAY"] as CsvMap;
                txtContactID.Text = value.contactID.ToString();
                txtContactName.Text = value.name.ToString();
                txtAddressCity.Text = value.addressCity.ToString();
                txtAddressCountry.Text = value.addressCountry.ToString();
                txtAddressState.Text = value.addressState.ToString();
                txtAddressStreet.Text = value.addressStreet.ToString();
                txtAddressZip.Text = value.addressZip.ToString();
                txtDepartment.Text = value.department.ToString();
                txtPhone.Text = value.phone.ToString();
                Application.Current.Properties.Remove("OKAY");
            }
        }
        private void btnSave_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtContactID.Text))
            {
                DisplayAlert("Warning", "Contact ID is Required", "OK");
                return;
            }
            else if (string.IsNullOrEmpty(txtContactName.Text))
            {
                DisplayAlert("Warning", "Contact Name is Required", "OK");
                return;
            }
            else if (string.IsNullOrEmpty(txtAddressZip.Text))
            {
                DisplayAlert("Warning", "Zip Code is Required", "OK");
                return;
            }
            else if (string.IsNullOrEmpty(txtDepartment.Text))
            {
                DisplayAlert("Warning", "Department is Required", "OK");
                return;
            }
            else if (string.IsNullOrEmpty(txtPhone.Text))
            {
                DisplayAlert("Warning", "Phone Number is Required", "OK");
                return;
            }
            string pth = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "abc.csv");
            if (!File.Exists(pth))
            {
                File.Create(pth);
            }
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Don't write the header again.
                HasHeaderRecord = false,
            };
            CsvMap obj = new CsvMap();
            obj.contactID = Convert.ToInt32(txtContactID.Text);
            obj.name = txtContactName.Text;
            obj.phone = txtPhone.Text;
            obj.department = txtDepartment.Text;
            obj.addressStreet = txtAddressStreet.Text;
            obj.addressCity = txtAddressCity.Text;
            obj.addressState = txtAddressState.Text;
            obj.addressZip = txtAddressZip.Text;
            obj.addressCountry = txtAddressCountry.Text;
            List<CsvMap> lst = new List<CsvMap>();
            lst.Add(obj);
            var getalldata = File.ReadAllLines(pth);
            var chec = obj.contactID;
            foreach (var item in getalldata)
            {

                var splitted = item.Split(',');
                if (string.IsNullOrEmpty(splitted[0]))
                {
                    continue;
                }
                if (chec == Convert.ToInt32(splitted[0]))
                {
                    continue;
                }
                obj = new CsvMap();
                obj.contactID = Convert.ToInt32(splitted[0]);
                obj.name = splitted[1];
                obj.phone = splitted[2];
                obj.department = splitted[3];
                obj.addressStreet = splitted[4];
                obj.addressCity = splitted[5];
                obj.addressState = splitted[6];
                obj.addressZip = splitted[7];
                obj.addressCountry = splitted[8];
                lst.Add(obj);
            }
            File.WriteAllText(pth, "");
            using (var stream = File.OpenWrite(pth))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, config))
            {

                csv.WriteRecords(lst);
                csv.Flush();
            }
            //var mmz = File.ReadAllText(pth);
            DisplayAlert("Success", "Record Saved ", "OK");
        }
        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
        private void btnDelete_Clicked(object sender, EventArgs e)
        {
            try
            {//deleting the record from the csv file.
                if (string.IsNullOrEmpty(txtContactID.Text))
                {
                    DisplayAlert("Warning", "Please Select the Record to Delete!", "OK");
                    return;
                }
                string pth = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "abc.csv");
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    // Don't write the header again.
                    HasHeaderRecord = false,
                };
                var getalldata = File.ReadAllLines(pth);
                CsvMap obj = new CsvMap();
                List<CsvMap> lst = new List<CsvMap>();
                foreach (var item in getalldata)
                {
                    var splitted = item.Split(',');
                    if (string.IsNullOrEmpty(splitted[0]))
                    {
                        continue;
                    }
                    if (Convert.ToInt32(txtContactID.Text) == Convert.ToInt32(splitted[0]))
                    {
                        continue;
                    }
                    obj = new CsvMap();
                    obj.contactID = Convert.ToInt32(splitted[0]);
                    obj.name = splitted[1];
                    obj.phone = splitted[2];
                    obj.department = splitted[3];
                    obj.addressStreet = splitted[4];
                    obj.addressCity = splitted[5];
                    obj.addressState = splitted[6];
                    obj.addressZip = splitted[7];
                    obj.addressCountry = splitted[8];
                    lst.Add(obj);
                }
                File.WriteAllText(pth, "");
                using (var stream = File.OpenWrite(pth))
                using (var writer = new StreamWriter(stream))
                using (var csv = new CsvWriter(writer, config))
                {

                    csv.WriteRecords(lst);
                    csv.Flush();
                }
                DisplayAlert("Success", "Record Deleted!", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Warning", ex.Message, "OK");
            }
        }





    }
}