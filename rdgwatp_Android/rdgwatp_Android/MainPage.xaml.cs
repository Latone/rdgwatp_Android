using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace rdgwatp_Android
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Title = "Main Page";
            Button toMapPageBtn = new Button
            {
                Text = "К игре",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            toMapPageBtn.Clicked += ToMapPage;

            Content = new StackLayout { Children = { toMapPageBtn} };
        }
        private async void ToMapPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapPage());
        }
    }
}