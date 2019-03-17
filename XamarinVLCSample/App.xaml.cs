using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinVLCSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            if (Current.MainPage is ContentPage)
            {
                var page = Current.MainPage as ContentPage;
                if (page.GetType().Name == "MainPage")
                {
                    var videoPlayerPage = page as MainPage;
                    videoPlayerPage.OnSleep();
                }
            }
        }

        protected override void OnResume()
        {
            if (Current.MainPage is ContentPage)
            {
                var page = Current.MainPage as ContentPage;
                if (page.GetType().Name == "MainPage")
                {
                    var videoPlayerPage = page as MainPage;
                    videoPlayerPage.OnResume();
                }
            }
        }
    }
}
