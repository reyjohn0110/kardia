using Login.Services;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Login.Models
{
    public class DoctorListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Doctor> DoctorsList { get; set; }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                ResetDoctors(value);
            }
        }

        public DoctorListViewModel()
        {
            DoctorsList = new ObservableCollection<Doctor>();
            PopulateDoctors();
        }

        private CancellationTokenSource throttleCts = new CancellationTokenSource();

        public void ResetDoctors(string value)
        {
            Interlocked.Exchange(ref this.throttleCts, new CancellationTokenSource()).Cancel();
            Task.Delay(TimeSpan.FromMilliseconds(500), this.throttleCts.Token) // if no keystroke occurs, carry on after 500ms
                .ContinueWith(
                    delegate {
                        DoctorsList.Clear();
                        PopulateDoctors(value);
                    }, // Pass the changed text to the PerformSearch function
                    CancellationToken.None,
                    TaskContinuationOptions.OnlyOnRanToCompletion,
                    TaskScheduler.FromCurrentSynchronizationContext());
        }

        public async void PopulateDoctors(string SearchText = "")
        {
            try
            {
                IApi apiResponse = RestService.For<IApi>(App.HostUrl);
                SearchDoctorRequest searchDoctorRequest = new SearchDoctorRequest
                {
                    search = SearchText ?? ""
                };
                DoctorResponse response = await apiResponse.GetDoctors(searchDoctorRequest);

                foreach (Doctor doctor in response?.Data)
                {
                    DoctorsList.Add(doctor);
                }
            }
            catch (ApiException e)
            {
                Console.WriteLine(e);
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Unable to load Doctors",
                    "Okay"
               );

            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class DoctorData
    {
        public string DoctorName { get; set; }
        public string Title { get; set; }
        public string Specialization { get; set; }
        public List<Specialization> Specializations { get; set; }
        public string Schedule { get; set; }
        public string ImageURL { get; set; }
    }
}
