using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Login.Models
{
    public class Model_DoctorData : INotifyPropertyChanged
    {
        public string id { get; set; } = string.Empty;

        string _firstName = string.Empty;
        [JsonProperty("first_name")]
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    NotifyUI();
                }
            }
        }

        string _lastName = string.Empty;
        [JsonProperty("last_name")]
        public string LastName
        {
            get => _lastName;
            set
            {
                if(_lastName != value)
                {
                    _lastName = value;
                    NotifyUI();
                }
            }
        }

        string _title = string.Empty;
        [JsonProperty("Title")]
        public string Title
        {
            get => _title;
            set
            {
                if(_title != value)
                {
                    _title = value;
                    NotifyUI();
                }
            }
        }

        string _hospitalClinic = string.Empty;
        [JsonProperty("hospital_clinic")]
        public string HospitalClinic
        {
            get => _hospitalClinic;
            set
            {
                if (_hospitalClinic != value)
                {
                    _hospitalClinic = value;
                    NotifyUI();
                }
            }
        }

        string _city = string.Empty;
        [JsonProperty("citymunDesc")]
        public string City
        {
            get => _city;
            set
            {
                if (_city != value)
                {
                    _city = value;
                    NotifyUI();
                }
            }
        }

        string _province = string.Empty;
        [JsonProperty("provDesc")]
        public string Province
        {
            get => _province;
            set
            {
                if (_province != value)
                {
                    _province = value;
                    NotifyUI();
                }
            }
        }

        string _specialization = string.Empty;
        [JsonProperty("specialization")]
        public string Specialization
        {
            get => _specialization;
            set
            {
                if (_specialization != value)
                {
                    _specialization = value;
                    NotifyUI();
                }
            }
        }

        string _schedule = string.Empty;
        [JsonProperty("schedule")]
        public string Schedule
        {
            get => _schedule;
            set
            {
                if (_schedule != value)
                {
                    _schedule = value;
                    NotifyUI();
                }
            }
        }

        //public List<Specialization> Specializations { get; set; }

        public string ImageURL { get; set; }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void NotifyUI([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}