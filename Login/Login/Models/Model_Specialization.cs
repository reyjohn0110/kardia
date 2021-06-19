using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Login.Models
{
    public class Model_Specialization : INotifyPropertyChanged
    {
        public int Id { get; set; } = 0;

        string _title = string.Empty;
        [JsonProperty("name")]
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

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void NotifyUI([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
