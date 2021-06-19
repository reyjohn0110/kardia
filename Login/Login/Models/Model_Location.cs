using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Login.Models
{
    public class Model_Location : INotifyPropertyChanged
    {
        string _cityName = string.Empty;
        public string CityName
        {
            get => _cityName;
            set
            {
                if(_cityName != value)
                {
                    _cityName = value;
                    NotifyUI();
                }
            }
        }

        string _province = string.Empty;
        public string Province
        {
            get => _province;
            set
            {
                if (_province != value)
                {
                    _cityName = value;
                    NotifyUI();
                }
            }
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void NotifyUI([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
