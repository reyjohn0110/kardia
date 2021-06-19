using Login.Models;
using Login.Services;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Login.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class emailpage : ContentPage
    {
        public emailpage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var emailText = email.Text;

            try
            {
                IApi apiResponse = RestService.For<IApi>(App.HostUrl);
                SendOtpRequest sendOtpRequest = new SendOtpRequest
                {
                    Emailorphone = emailText,
                    Type = 1
                };
                SendOtpResponse response = await apiResponse.SendOtp(sendOtpRequest);
                Console.WriteLine(response);
                await Navigation.PushAsync(new Verificationpage(response.Verification_id, 1, true));
            }
            catch (ApiException exception)
            {
                var error = await exception.GetContentAsAsync<MainResponse>();
                Dictionary<string, string[]> errors = error.Errors;
                await DisplayAlert(
                    "Error",
                    exception.StatusCode.ToString() == "UnprocessableEntity" && errors != null && errors.Count != 0 ? errors.First().Value[0] : error.Message,
                    "Okay"
                );
            }
        }
    }
}