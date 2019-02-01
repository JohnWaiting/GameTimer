using System;
using Prism.Commands;
using Prism.Mvvm;

namespace GameTimer.ViewModels
{
    internal class MainWindowViewModel : BindableBase
    {
        private PlayerListViewModel _playerListViewModel = new PlayerListViewModel();
        public PlayerListViewModel PlayerListViewModel
        {
            get => _playerListViewModel;
            set => SetProperty(ref _playerListViewModel, value);
        }

        public MainWindowViewModel()
        {
        }
    }
}
