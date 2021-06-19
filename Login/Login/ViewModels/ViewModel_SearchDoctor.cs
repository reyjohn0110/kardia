using Login.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Login.ViewModels
{
    public class ViewModel_SearchDoctor : ViewModelBase
    {

        #region properties
        
        public ObservableCollection<Model_DoctorData> Doctors { get; set; } = new ObservableCollection<Model_DoctorData>();
        #endregion

        #region ctors
        public ViewModel_SearchDoctor()
        {
            InitCommands();
        }
        #endregion

        #region commands
        public ICommand Command_Filter { get; set; }
        #endregion

        #region command methods
        async void Command_Filter_Tap()
        {
            base.IsBusy = true;

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
                    await base.Alert("", "INVALID");
                }
                else if (result4 == "No Data")
                {
                    await base.Alert("", "no DTA");
                }
                else
                {
                    //DataTable dt4 = new DataTable();
                    //dt4 = (DataTable)JsonConvert.DeserializeObject(result4, (typeof(DataTable))); // your response is in json text need to deserialize into datatable
                    //data4 = new ObservableCollection<string>();
                    //foreach (DataRow datarow4 in dt4.Rows)
                    //{
                    //    data4.Add(datarow4["last_name"].ToString() + ", " + datarow4["first_name"].ToString() + ", " + datarow4["middle_name"].ToString() + "" + datarow4["title"].ToString());
                    //}

                    //List<Model_PersonDetails> _doctors = new List<Model_PersonDetails>();
                    var des = (List<Model_DoctorData>)JsonConvert.DeserializeObject(result4);
                    if(des != null)
                    {
                        this.Doctors.Clear();

                        // mas mabilis kesa foreach
                        for(int i = 0; i < des.Count; i++)
                        {
                            this.Doctors.Add(des[i]);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                await base.Alert("Error", ex.Message, "ok");
            }

            base.IsBusy = false;
        }
        #endregion

        #region methods
        void InitCommands()
        {
            //if (Command_Filter == null) Command_Filter = new Command(
            //    new Action(async () =>
            //    {
            //        await Command_Filter_Tap();
            //    })
            //);
            if (Command_Filter == null) Command_Filter = new Command(Command_Filter_Tap);
        }
        #endregion

        
    }
}
