using Login.Models;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;

using Xamarin.Forms;

namespace Login.ViewModels
{
    public class ViewModel_SearchDoctor : ViewModelBase
    {
        #region vars
        string _baseAPIServer = "https://api.clairemontferrond.tech/kardia/read.php";
        // backer lists
        // where you will store the records from the API server
        List<Model_Location> _locationBacker = new List<Model_Location>();
        List<Model_Specialization> _specializationBacker = new List<Model_Specialization>();
        List<Model_DoctorData> _doctorsBacker = new List<Model_DoctorData>();
        #endregion

        #region properties
        public ObservableCollection<Model_Location> Location { get; set; } = new ObservableCollection<Model_Location>();
        public ObservableCollection<Model_Specialization> Specialization { get; set; } = new ObservableCollection<Model_Specialization>();
        public ObservableCollection<Model_DoctorData> Doctors { get; set; } = new ObservableCollection<Model_DoctorData>();

        string _locationQuery = string.Empty;
        public string LocationQuery
        {
            get => _locationQuery;
            set
            {
                if (_locationQuery != value)
                {
                    this.Location.Clear();
                    _locationQuery = value;
                    base.NotifyUI();

                    if (!string.IsNullOrEmpty(value))
                    {
                        var locs = this._locationBacker.Where(x => x.CityName.ToLower().Contains(value.ToLower()));
                        if (locs != null)
                        {
                            for (int i = 0; i < locs.Take(20).Count(); i++)
                            {
                                this.Location.Add(locs.ElementAt(i));
                            }
                        }
                    }
                }
            }
        }

        string _specializationQuery = string.Empty;
        public string SpecializationQuery
        {
            get => _specializationQuery;
            set
            {
                if (_specializationQuery != value)
                {
                    this.Specialization.Clear();
                    _specializationQuery = value;
                    base.NotifyUI();

                    if (!string.IsNullOrEmpty(value))
                    {
                        var spec = this._specializationBacker.Where(x => x.Title.ToLower().Contains(value.ToLower()));
                        if (spec != null)
                        {
                            for (int i = 0; i < spec.Take(20).Count(); i++)
                            {
                                this.Specialization.Add(spec.ElementAt(i));
                            }
                        }
                    }
                }
            }
        }

        string _doctorQuery = string.Empty;
        public string DoctorQuery
        {
            get => _doctorQuery;
            set
            {
                if (_doctorQuery != value)
                {
                    this.Doctors.Clear();
                    _doctorQuery = value;
                    base.NotifyUI();

                    if (!string.IsNullOrEmpty(value))
                    {
                        var docs = this._doctorsBacker.Where(x => x.FirstName.ToLower().Contains(value.ToLower()));
                        if (docs != null)
                        {
                            for (int i = 0; i < docs.Take(20).Count(); i++)
                            {
                                this.Doctors.Add(docs.ElementAt(i));
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < _doctorsBacker.Count(); i++)
                        {
                            this.Doctors.Add(_doctorsBacker.ElementAt(i));
                        }
                    }
                }
            }
        }

        Model_Location _selectedLocation = null;
        public Model_Location SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                _selectedLocation = value;
                base.NotifyUI();

                if (value != null)
                    this.LocationQuery = value.CityName + ", " + value.Province;
            }
        }

        Model_Specialization _selectedSpecialization = null;
        public Model_Specialization SelectedSpecialization
        {
            get => _selectedSpecialization;
            set
            {
                _selectedSpecialization = value;
                base.NotifyUI();

                if (value != null)
                    this.SpecializationQuery = value.Title + " ";
            }
        }
        #endregion

        #region ctors
        public ViewModel_SearchDoctor()
        {
            InitCommands();

            // dummy data
            //this.Doctors.Clear();
            //this.Doctors.Add(new Model_DoctorData()
            //{
            //    FirstName = "Firstname",
            //    LastName = "Lastname",
            //    Title = "Title",
            //    HospitalClinic = "Hospital Clinic",
            //    City = "City",
            //    Province = "Province",
            //    Specialization = "Specialization",
            //    Schedule = "Schedule"
            //});

            //this.Doctors.Add(new Model_DoctorData()
            //{
            //    FirstName = "Firstname",
            //    LastName = "Lastname",
            //    Title = "Title",
            //    HospitalClinic = "Hospital Clinic",
            //    City = "City",
            //    Province = "Province",
            //    Specialization = "Specialization",
            //    Schedule = "Schedule"
            //});
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
                //string query4 = "SELECT a.id,a.first_name,a.last_name FROM doctors a LEFT JOIN doctor_specialization b ON a.id = b.s_id";
                string query = "SELECT d.id, d.first_name, d.last_name, d.title, f.hospital_clinic, c.citymunDesc, p.provDesc, s.name as specialization, df.schedule FROM `doctors` d, `doctor_facilities` df, `facilities` f, `refcitymun` c, `refprovince` p, `doctor_specialization` ds, `specialization` s WHERE df.d_id = d.id and f.city = c.id and ds.d_id = d.id and ds.s_id = s.id and c.provCode=p.provCode ";
                query += $" and s.id={this.SelectedSpecialization.Id}";
                query += $" and c.id={this.SelectedLocation.Id}";
                query += " order by d.first_name LIMIT 20";
                postData4.Add(new KeyValuePair<string, string>("query", query));
                postData4.Add(new KeyValuePair<string, string>("apikey", "clair3m0ntf3rr0nd!"));

                var content4 = new FormUrlEncodedContent(postData4);

                client4.BaseAddress = new Uri(_baseAPIServer);

                var response4 = await client4.PostAsync(_baseAPIServer, content4); //change content to null if no postdata to be post
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
                    var docs = (List<Model_DoctorData>)JsonConvert.DeserializeObject(result4, typeof(List<Model_DoctorData>));
                    if (docs != null)
                    {
                        this._doctorsBacker.Clear();
                        this.Doctors.Clear();

                        // mas mabilis kesa foreach
                        for (int i = 0; i < docs.Count; i++)
                        {
                            this._doctorsBacker.Add(docs[i]);
                            this.Doctors.Add(docs[i]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await base.Alert("Error", "No Results Found", "ok");
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

        public async void Init()
        {
            if (await GetLocations())
            {
                bool result = await GetSpecializations();
                if (!result)
                {
                    await base.Alert("ERROR", "Unable to resolve specializations");
                }
            }
            else
            {
                await base.Alert("ERROR", "Unable to resolve locations");
            }
        }

        async Task<bool> GetLocations()
        {
            base.IsBusy = true;
            bool ok = false;

            try
            {
                HttpClient client = new HttpClient();

                var postData = new List<KeyValuePair<string, string>>();
                //string query = "SELECT refcitymun.citymunCode, refcitymun.citymunDesc, refprovince.provDesc, refprovince.provCode FROM " +
                //    "refcitymun LEFT JOIN refprovince ON refcitymun.provCode = refprovince.provCode WHERE refcitymun.provCode LIKE '%a%' OR refprovince.provDesc " +
                //    "LIKE '%a%' OR refcitymun.citymunCode LIKE '%a%' OR refcitymun.citymunDesc LIKE '%a%'" +
                //    "UNION SELECT refcitymun.citymunCode , refcitymun.citymunDesc,refprovince.provDesc, refprovince.provCode " +
                //    "FROM refcitymun RIGHT JOIN refprovince ON refcitymun.provCode = refprovince.provCode WHERE refcitymun.provCode LIKE '%a%' " +
                //    "OR refprovince.provDesc LIKE '%a%' OR refcitymun.citymunCode " +
                //    "LIKE '%a%' OR refcitymun.citymunDesc LIKE '&a%' ORDER BY citymunDesc ASC";

                string query = "SELECT c.id, c.citymunDesc, p.provCode, p.provDesc  from `refcitymun` c, `refprovince` p where c.provCode=p.provCode order by c.citymunDesc";

                postData.Add(new KeyValuePair<string, string>("query", query));
                postData.Add(new KeyValuePair<string, string>("apikey", "clair3m0ntf3rr0nd!"));

                var content = new FormUrlEncodedContent(postData);

                client.BaseAddress = new Uri(_baseAPIServer);

                var response = await client.PostAsync(_baseAPIServer, content); //change content to null if no postdata to be post
                var result = response.Content.ReadAsStringAsync().Result;

                if (result == "Invalid")
                {
                    await base.Alert("", "INVALID", "Ok");
                }
                else if (result == "No Data")
                {
                    await base.Alert("", "NO DATA", "Ok");
                }
                else
                {
                    //DataTable dt = new DataTable();
                    //dt = (DataTable)JsonConvert.DeserializeObject(result, (typeof(DataTable))); // your response is in json text need to deserialize into datatable
                    //data = new ObservableCollection<string>();
                    //foreach (DataRow datarow in dt.Rows)
                    //{
                    //    data.Add(datarow["citymunDesc"].ToString() + ", " + datarow["provDesc"].ToString());

                    //}


                    var locs = (List<Model_Location>)JsonConvert.DeserializeObject(result, typeof(List<Model_Location>));
                    if (locs != null)
                    {
                        this._locationBacker.Clear();
                        this.Location.Clear();

                        for (int i = 0; i < locs.Count; i++)
                        {
                            this._locationBacker.Add(locs[i]);
                        }

                        ok = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ok = false;
                await base.Alert("ERROR", ex.Message);
            }

            base.IsBusy = false;

            return ok;
        }

        async Task<bool> GetSpecializations()
        {
            base.IsBusy = true;
            bool ok = false;

            try
            {
                HttpClient client1 = new HttpClient();

                var postData1 = new List<KeyValuePair<string, string>>();
                string query1 = "select * from specialization";
                postData1.Add(new KeyValuePair<string, string>("query", query1));
                postData1.Add(new KeyValuePair<string, string>("apikey", "clair3m0ntf3rr0nd!"));

                var content1 = new FormUrlEncodedContent(postData1);

                client1.BaseAddress = new Uri(_baseAPIServer);

                var response1 = await client1.PostAsync(_baseAPIServer, content1); //change content to null if no postdata to be post
                var result = response1.Content.ReadAsStringAsync().Result;

                if (result == "Invalid")
                {
                    await base.Alert("", "INVALID", "Ok");
                }
                else if (result == "No Data")
                {
                    await base.Alert("", "no DTA", "Ok");
                }
                else
                {
                    //DataTable dt1 = new DataTable();
                    //dt1 = (DataTable)JsonConvert.DeserializeObject(result1, (typeof(DataTable))); // your response is in json text need to deserialize into datatable
                    //data1 = new ObservableCollection<string>();
                    //foreach (DataRow datarow1 in dt1.Rows)
                    //{
                    //    data1.Add(datarow1["name"].ToString());
                    //}

                    var spec = (List<Model_Specialization>)JsonConvert.DeserializeObject(result, typeof(List<Model_Specialization>));
                    if (spec != null)
                    {
                        this._specializationBacker.Clear();

                        for (int i = 0; i < spec.Count; i++)
                        {
                            this._specializationBacker.Add(spec[i]);
                        }

                        ok = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ok = false;
                await base.Alert("ERROR", ex.Message);
            }

            base.IsBusy = false;

            return ok;
        }
        #endregion
    }
}