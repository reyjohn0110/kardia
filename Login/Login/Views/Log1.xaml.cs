using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Login.Classes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Login.Services;
using Refit;
using Login.Models;

namespace Login.Views { 

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Log1 : ContentPage
    {
        private readonly int Verification_type = 2;
      
        public Log1()
        {
            InitializeComponent();
          
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                IApi apiResponse = RestService.For<IApi>(App.HostUrl);
                LoginRequest loginRequest = new LoginRequest
                {
                    Emailorphone = emailorphone.Text,
                    Password = pword.Text
                };
                SendOtpResponse response = await apiResponse.LoginUser(loginRequest);
                Console.WriteLine(response);
                await Navigation.PushAsync(new Verificationpage(response.Verification_id, this.Verification_type, this.IsValidEmail(emailorphone.Text)));
            }
            catch (ApiException exception)
            {
                var error = await exception.GetContentAsAsync<MainResponse>();
                var errors = error.Errors;

                await DisplayAlert(
                    "Error",
                    exception.StatusCode.ToString() == "UnprocessableEntity" && errors != null && errors.Count != 0 ? errors.First().Value[0] : error.Message,
                    "Okay"
                );

                if (error.Message == "Invalid Password")
                {
                    pword.Text = "";
                }
                else if (error.Message == "Invalid username or password")
                {
                    emailorphone.Text = "";
                    pword.Text = "";
                }
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new emailphone());
        }
    }
}