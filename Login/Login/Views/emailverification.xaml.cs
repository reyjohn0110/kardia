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
    public partial class emailverification : ContentPage
    {
        public emailverification()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //next page filling up information
            //await Navigation.PushAsync(new signup());
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
    }
}