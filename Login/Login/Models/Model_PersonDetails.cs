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


        string _doctorName = string.Empty;
        [JsonProperty("DoctorName")] // dapat same sa column name ng database table
        public string DoctorName
        {
            get => _doctorName;
            set
            {
                if(_doctorName != value)
                {
                    _doctorName = value;
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

        // do the rest ^^
        public string Specialization { get; set; }
        public List<Specialization> Specializations { get; set; }
        public string Schedule { get; set; }
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