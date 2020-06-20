using OnlineStep.Navigation;
using OnlineStep.ViewModels;
using OnlineStep.Views;
using Xamarin.Forms;

namespace OnlineStep
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            LoadTypes();
        }

        private void LoadTypes()
        {
            var bootstrapper = new Bootstrapper(this);
            bootstrapper.Run();
        }

        protected override void OnStart(){}

        protected override void OnSleep(){}

        protected override void OnResume(){}
    }
}
