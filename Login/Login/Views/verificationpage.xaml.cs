using Login.Models;
using Login.Services;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Login.Views
{
    [DesignTimeVisible(false)]
    public partial class Verificationpage : ContentPage
    {
        private string Verification_id { get; set; }
        private int Verification_type { get; set; }

        private string labelTitleProperty;
        public string LabelTitleProperty
        {
            get { return labelTitleProperty; }
            set
            {
                labelTitleProperty = value;
                OnPropertyChanged(nameof(LabelTitleProperty)); // Notify that there was a change on this property
            }
        }
        private string labelDescriptionProperty;
        public string LabelDescriptionProperty
        {
            get { return labelDescriptionProperty; }
            set
            {
                labelDescriptionProperty = value;
                OnPropertyChanged(nameof(LabelDescriptionProperty)); // Notify that there was a change on this property
            }
        }

        public Verificationpage(string id, int type, bool isEmailVerification)
        {
            this.Verification_id = id;
            this.Verification_type = type;
            InitializeComponent();
            BindingContext = this;

            if (isEmailVerification)
            {
                LabelTitleProperty = "Confirm your email";
                LabelDescriptionProperty = "Please type the verification code sent to your email address";
            }
            else
            {
                LabelTitleProperty = "Confirm your phone";
                LabelDescriptionProperty = "Please type the verification code sent to your phone number";
            }
        }

        private void Entry_Focused(object sender, FocusEventArgs e)
        {
            box1.BorderColor = Color.ForestGreen;

        }

        private void Entry_Focused_1(object sender, FocusEventArgs e)
        {
            box2.BorderColor = Color.ForestGreen;

        }

        private void Entry_Focused_2(object sender, FocusEventArgs e)
        {
            box3.BorderColor = Color.ForestGreen;
        }

        private void Entry_Focused_3(object sender, FocusEventArgs e)
        {
            box4.BorderColor = Color.ForestGreen;
        }
        private void Entry_Focused_4(object sender, FocusEventArgs e)
        {
            box5.BorderColor = Color.ForestGreen;
        }

        private void Entry_Focused_5(object sender, FocusEventArgs e)
        {
            box6.BorderColor = Color.ForestGreen;
        }
        private void box1_Unfocused(object sender, FocusEventArgs e)
        {
            box1.BorderColor = Color.Gray;
            box2.BorderColor = Color.Gray;
            box3.BorderColor = Color.Gray;
            box4.BorderColor = Color.Gray;
            box5.BorderColor = Color.Gray;
            box6.BorderColor = Color.Gray;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var code = code1.Text + code2.Text + code3.Text + code4.Text + code5.Text + code6.Text;

            try
            {
                IApi apiResponse = RestService.For<IApi>(App.HostUrl);
                VerificationRequest verificationRequest = new VerificationRequest
                {
                    Code = code,
                    Type = this.Verification_type,
                    Verification_id = this.Verification_id
                };
                VerificationResponse response = await apiResponse.VerificationOtp(verificationRequest);
                Console.WriteLine(response);
                if (this.Verification_type == 1)
                {
                    await Navigation.PushAsync(new Signup(this.Verification_id));
                } else
                {
                    await Navigation.PushAsync(new dashboard());
                   // await DisplayAlert("Dashboard", "Welcome to Dashboard", "Okay");
                }
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

        private void code1_Completed(object sender, EventArgs e)
        {

           /* var box1 = code1.Text;
            var box2 = code2.Text;
            var box3 = code3.Text;

            if (box1.Length >= 1)
            {
                code1.Unfocus();
                code2.Focus();
            }
            else if (box3.Length >= 1)
            {
                code2.Unfocus();
                code3.Focus();
            }*/    
        }

        private void code2_Completed(object sender, EventArgs e)
        {
            /*var c2 = code2.Text;
            var c3 = code3.Text;

            if (c2.Length == 1)
            {
                code2.Unfocus();
                code3.Focus();
            }*/
        }
    }
}