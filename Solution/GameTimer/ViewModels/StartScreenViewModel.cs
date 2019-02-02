using System;
using System.Linq;
using Prism.Commands;
using Prism.Mvvm;

namespace GameTimer.ViewModels
{
    internal class StartScreenViewModel : BaseScreenViewModel
    {
        private PlayerListViewModel _playerListViewModel = new PlayerListViewModel();
        public PlayerListViewModel PlayerListViewModel
        {
            get => _playerListViewModel;
            set => SetProperty(ref _playerListViewModel, value);
        }
        
        public StartScreenViewModel(IApp app) : base(app)
        {
        }
    }
}
