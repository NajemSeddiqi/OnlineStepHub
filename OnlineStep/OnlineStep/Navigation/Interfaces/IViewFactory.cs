/*
 Title   : Xamplate
 Author  : Tomislav Erić
 Summary : ViewModel First Navigation, View to ViewModel autowireing
 Website : https://github.com/tomislaveric/Xamplate 
*/

using Xamarin.Forms;

namespace OnlineStep.Navigation.Interfaces
{
    public interface IViewFactory
    {
        void Register<TViewModel, TView>() where TViewModel : class, IViewModelBase where TView : Page;
        Page Resolve<TViewModel>() where TViewModel : class, IViewModelBase;
    }
}