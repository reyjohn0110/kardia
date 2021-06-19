using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Login.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public App App => (App)Application.Current;

        bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if(_isBusy != value)
                {
                    _isBusy = value;
                    NotifyUI();
                }
            }
        }

        public async Task Alert(string title, string message, string cancel = "Ok")
        {
            await this.App.MainPage.DisplayAlert(title, message, cancel);
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
