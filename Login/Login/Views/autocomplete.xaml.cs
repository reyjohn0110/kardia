using Login.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Login.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class autocomplete : ContentPage
    {
        //ObservableCollection<string> data = new ObservableCollection<string>();
        //ObservableCollection<string> data1 = new ObservableCollection<string>();
        //ObservableCollection<string> data3 = new ObservableCollection<string>();
        //ObservableCollection<string> data4 = new ObservableCollection<string>();
        //ObservableCollection<string> data5 = new ObservableCollection<string>();
       

        public autocomplete()
        {
            InitializeComponent();

            ((ViewModel_SearchDoctor)this.BindingContext).Init();

            //LoadCities();
            //LoadSpecializations();
            //LoadDoctors();
            // LoadData5();
        }

        /*
        //Search Location
        public async void LoadCities()
        {
            try
            {

                HttpClient client = new HttpClient();

                var postData = new List<KeyValuePair<string, string>>();
                string query = "SELECT refcitymun.citymunCode , refcitymun.citymunDesc,refprovince.provDesc, refprovince.provCode FROM " +
                    "refcitymun LEFT JOIN refprovince ON refcitymun.provCode = refprovince.provCode WHERE refcitymun.provCode LIKE '%a%' OR refprovince.provDesc " +
                    "LIKE '%a%' OR refcitymun.citymunCode LIKE '%a%' OR refcitymun.citymunDesc LIKE '%a%'" +
                    "UNION SELECT refcitymun.citymunCode , refcitymun.citymunDesc,refprovince.provDesc, refprovince.provCode " +
                    "FROM refcitymun RIGHT JOIN refprovince ON refcitymun.provCode = refprovince.provCode WHERE refcitymun.provCode LIKE '%a%' " +
                    "OR refprovince.provDesc LIKE '%a%' OR refcitymun.citymunCode " +
                    "LIKE '%a%' OR refcitymun.citymunDesc LIKE '&a%' ORDER BY citymunDesc ASC";

                postData.Add(new KeyValuePair<string, string>("query", query));
                postData.Add(new KeyValuePair<string, string>("apikey", "clair3m0ntf3rr0nd!"));

                var content = new FormUrlEncodedContent(postData);

                client.BaseAddress = new Uri("http://api.clairemontferrond.tech/kardia/read.php");

                var response = await client.PostAsync("http://api.clairemontferrond.tech/kardia/read.php", content); //change content to null if no postdata to be post
                var result = response.Content.ReadAsStringAsync().Result;

                if (result == "Invalid")
                {
                    await DisplayAlert("", "INVALID", "Ok");
                }
                else if (result == "No Data")
                {
                    await DisplayAlert("", "NO DATA", "Ok");
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt = (DataTable)JsonConvert.DeserializeObject(result, (typeof(DataTable))); // your response is in json text need to deserialize into datatable
                    data = new ObservableCollection<string>();
                    foreach (DataRow datarow in dt.Rows)
                    {
                        data.Add(datarow["citymunDesc"].ToString() + ", " + datarow["provDesc"].ToString());

                    }

                }
            }
            catch (Exception eX)
            {


                await DisplayAlert("", "" + eX.ToString(), "Ok");
            }
        }

        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            provincelist.IsVisible = true;
            provincelist.BeginRefresh();

            try
            {
                var dataEmpty = data.Where(i => i.ToLower().Contains(e.NewTextValue.ToLower()));

                if (string.IsNullOrWhiteSpace(e.NewTextValue))
                    provincelist.IsVisible = false;
                else if (dataEmpty.Max().Length == 0)
                    provincelist.IsVisible = false;
                else
                    provincelist.ItemsSource = data.Where(i => i.ToLower().Contains(e.NewTextValue.ToLower()));
            }
            catch (Exception ex)
            {
                provincelist.IsVisible = false;

            }
            provincelist.EndRefresh();

        }

        private void countryListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //EmployeeListView.IsVisible = false;  
            String listsd = e.Item as string;
            searchBar.Text = listsd;
            provincelist.IsVisible = false;

            ((ListView)sender).SelectedItem = null;
        }



        ////Search specialization
        public async void LoadSpecializations()
        {
            try
            {

                HttpClient client1 = new HttpClient();

                var postData1 = new List<KeyValuePair<string, string>>();
                string query1 = "select * from specialization";
                postData1.Add(new KeyValuePair<string, string>("query", query1));
                postData1.Add(new KeyValuePair<string, string>("apikey", "clair3m0ntf3rr0nd!"));

                var content1 = new FormUrlEncodedContent(postData1);

                client1.BaseAddress = new Uri("http://api.clairemontferrond.tech/kardia/read.php");

                var response1 = await client1.PostAsync("http://api.clairemontferrond.tech/kardia/read.php", content1); //change content to null if no postdata to be post
                var result1 = response1.Content.ReadAsStringAsync().Result;

                if (result1 == "Invalid")
                {
                    await DisplayAlert("", "INVALID", "Ok");
                }
                else if (result1 == "No Data")
                {
                    await DisplayAlert("", "no DTA", "Ok");
                }
                else
                {
                    DataTable dt1 = new DataTable();
                    dt1 = (DataTable)JsonConvert.DeserializeObject(result1, (typeof(DataTable))); // your response is in json text need to deserialize into datatable
                    data1 = new ObservableCollection<string>();
                    foreach (DataRow datarow1 in dt1.Rows)
                    {
                        data1.Add(datarow1["name"].ToString());
                    }

                }
            }
            catch (Exception e)
            {


                await DisplayAlert("", "" + e.ToString(), "Ok");

            }


        }
        private void searchBar_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            specialtylist.IsVisible = true;
            specialtylist.BeginRefresh();

            try
            {
                var dataEmpty1 = data1.Where(i => i.ToLower().Contains(e.NewTextValue.ToLower()));

                if (string.IsNullOrWhiteSpace(e.NewTextValue))
                    specialtylist.IsVisible = false;
                else if (dataEmpty1.Max().Length == 0)
                    specialtylist.IsVisible = false;
                else
                    specialtylist.ItemsSource = data1.Where(i => i.ToLower().Contains(e.NewTextValue.ToLower()));
            }
            catch (Exception ex)
            {
                specialtylist.IsVisible = false;

            }
            specialtylist.EndRefresh();
        }

        private void specialtylist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //EmployeeListView.IsVisible = false;  

            String listcity = e.Item as string;
            searchBarspecialization.Text = listcity;
            specialtylist.IsVisible = false;

            ((ListView)sender).SelectedItem = null;
        }

        //Search doctor
        public async void LoadDoctors()
        {
            try
            {

                HttpClient client3 = new HttpClient();

                var postData3 = new List<KeyValuePair<string, string>>();
                string query3 = "select * from doctors";
                postData3.Add(new KeyValuePair<string, string>("query", query3));
                postData3.Add(new KeyValuePair<string, string>("apikey", "clair3m0ntf3rr0nd!"));

                var content3 = new FormUrlEncodedContent(postData3);

                client3.BaseAddress = new Uri("http://api.clairemontferrond.tech/kardia/read.php");

                var response3 = await client3.PostAsync("http://api.clairemontferrond.tech/kardia/read.php", content3); //change content to null if no postdata to be post
                var result3 = response3.Content.ReadAsStringAsync().Result;

                if (result3 == "Invalid")
                {
                    await DisplayAlert("", "INVALID", "Ok");
                }
                else if (result3 == "No Data")
                {
                    await DisplayAlert("", "no DTA", "Ok");
                }
                else
                {
                    DataTable dt3 = new DataTable();
                    dt3 = (DataTable)JsonConvert.DeserializeObject(result3, (typeof(DataTable))); // your response is in json text need to deserialize into datatable
                    data3 = new ObservableCollection<string>();
                    foreach (DataRow datarow3 in dt3.Rows)
                    {
                        data3.Add(datarow3["last_name"].ToString() + ", " + datarow3["first_name"].ToString() + ", " + datarow3["middle_name"].ToString() + "" + datarow3["title"].ToString());
                    }

                }
            }
            catch (Exception e)
            {


                await DisplayAlert("", "" + e.ToString(), "Ok");

            }


        }

        private void searchbarDoctor_TextChanged(object sender, TextChangedEventArgs e)
        {

            //doctorlist.IsVisible = true;
            //doctorlist.BeginRefresh();

            //try
            //{
            //    var dataEmpty3 = data3.Where(i => i.ToLower().Contains(e.NewTextValue.ToLower()));

            //    if (string.IsNullOrWhiteSpace(e.NewTextValue))
            //        doctorlist.IsVisible = false;
            //    else if (dataEmpty3.Max().Length == 0)
            //        doctorlist.IsVisible = false;
            //    else
            //        doctorlist.ItemsSource = data3.Where(i => i.ToLower().Contains(e.NewTextValue.ToLower()));
            //}
            //catch (Exception ex)
            //{
            //    doctorlist.IsVisible = false;

            //}
            //doctorlist.EndRefresh();
        }

        private void doctorlist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //String subdoc = e.Item as string;
            //searchbarDoctor.Text = subdoc;
            //doctorlist.IsVisible = false;

            //((ListView)sender).SelectedItem = null;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {

                HttpClient client4 = new HttpClient();

                var postData4 = new List<KeyValuePair<string, string>>();
                string query4 = "SELECT a.id,a.first_name,a.last_name FROM doctors a LEFT JOIN doctor_specialization b ON a.id = b.s_id";
                postData4.Add(new KeyValuePair<string, string>("query", query4));
                postData4.Add(new KeyValuePair<string, string>("apikey", "clair3m0ntf3rr0nd!"));

                var content4 = new FormUrlEncodedContent(postData4);

                client4.BaseAddress = new Uri("http://api.clairemontferrond.tech/kardia/read.php");

                var response4 = await client4.PostAsync("http://api.clairemontferrond.tech/kardia/read.php", content4); //change content to null if no postdata to be post
                var result4 = response4.Content.ReadAsStringAsync().Result;

                string specialization = "";

                if (result4 == "Invalid")
                {
                    await DisplayAlert("", "INVALID", "Ok");
                }
                else if (result4 == "No Data")
                {
                    await DisplayAlert("", "no DTA", "Ok");
                }
                else
                {
                    DataTable dt4 = new DataTable();
                    dt4 = (DataTable)JsonConvert.DeserializeObject(result4, (typeof(DataTable))); // your response is in json text need to deserialize into datatable
                    data4 = new ObservableCollection<string>();
                    foreach (DataRow datarow4 in dt4.Rows)
                    {
                        data4.Add(datarow4["last_name"].ToString() + ", " + datarow4["first_name"].ToString() + ", " + datarow4["middle_name"].ToString() + "" + datarow4["title"].ToString());
                    }

                }
            }
            catch (Exception ex)
            {


                await DisplayAlert("", "" + ex.ToString(), "Ok");

            }
        }
        

 */     
    }
}