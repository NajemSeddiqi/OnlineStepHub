﻿/*
 Title   : Xamplate
 Author  : Tomislav Erić
 Summary : ViewModel First Navigation, View to ViewModel autowireing
 Website : https://github.com/tomislaveric/Xamplate 
*/

using System;
using System.Threading.Tasks;
using OnlineStep.Navigation.Interfaces;
using Xamarin.Forms;

namespace OnlineStep.Services
{
    public class Navigator : INavigator
    {
        private readonly Lazy<INavigation> _navigation;
        private readonly IViewFactory _viewFactory;
        private INavigation Navigation => _navigation.Value;

        public Navigator(Lazy<INavigation> navigation, IViewFactory viewFactory)
        {
            _navigation = navigation;
            _viewFactory = viewFactory;
        }

        public async Task PopAsync()
        {
            await Navigation.PopAsync();
        }

        public async Task PopToRootAsync()
        {
            await Navigation.PopToRootAsync();
        }

        public async Task PushAsync<TViewModel>() where TViewModel : class, IViewModelBase
        {
            var view = _viewFactory.Resolve<TViewModel>();
            await Navigation.PushAsync(view);
        }

        public async Task PushModalAsync<TViewModel>() where TViewModel : class, IViewModelBase
        {
            var view = _viewFactory.Resolve<TViewModel>();
            await Navigation.PushModalAsync(view);
        }
    }
}
