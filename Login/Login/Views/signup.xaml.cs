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
    public partial class Signup : ContentPage
    {
        private string Verification_id { get; set; }

        public Signup(string id)
        {
            this.Verification_id = id;
            InitializeComponent();
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            try
            {
                IApi apiResponse = RestService.For<IApi>(App.HostUrl);
                RegisterRequest registerRequest = new RegisterRequest
                {
                    First_name = fname.Text,
                    Middle_name = mname.Text,
                    Last_name = lname.Text,
                    Password = password.Text,
                    Verification_id = this.Verification_id,
                    Suffix = suffix.Text,
                    Gender = gender.SelectedItem.ToString()
                };
                MainResponse response = await apiResponse.Register(registerRequest);
                Console.WriteLine(response);
                await DisplayAlert("Awesome!", "Registration Successful. Please login to continue.", "Okay");
                await Navigation.PushAsync(new Log1());
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
            //await Navigation.PushAsync(new add_address());
        }
    }
}