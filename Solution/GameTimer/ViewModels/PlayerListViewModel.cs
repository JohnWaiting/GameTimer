using System;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Mvvm;

namespace GameTimer.ViewModels
{
    internal class PlayerListViewModel : BindableBase
    {
        private const Int32 DefaultItemsCount = 4;

        private ObservableCollection<PlayerInfo> _playerInfos;
        public ObservableCollection<PlayerInfo> PlayerInfos
        {
            get => _playerInfos;
            set => SetProperty(ref _playerInfos, value);
        }
        
        public DelegateCommand<String> AddPlayerCommand { get; }

        public DelegateCommand<PlayerInfo> RemovePlayerCommand { get; }

        public PlayerListViewModel()
        {
            _playerInfos = new ObservableCollection<PlayerInfo>();
            AddPlayerCommand = new DelegateCommand<String>(AddPlayerCommand_ExecuteMethod); ;
            RemovePlayerCommand = new DelegateCommand<PlayerInfo>(RemovePlayerCommand_ExecuteMethod); ;

            for (Int32 i = 0; i < DefaultItemsCount; i++)
            {
                AddPlayerCommand.Execute(null);
            }
        }

        private void RemovePlayerCommand_ExecuteMethod(PlayerInfo player)
        {
            PlayerInfos.Remove(player);
        }

        private void AddPlayerCommand_ExecuteMethod(String name)
        {
            PlayerInfos.Add(new PlayerInfo() { Name = name ?? GetNewItemName() });
        }

        private String GetNewItemName()
        {
            const String textBeforeNumber = "Player ";
            Int32 newItemNumber = int.MinValue;
            foreach (PlayerInfo playerInfo in PlayerInfos)
            {
                if (playerInfo.Name.StartsWith(textBeforeNumber) && 
                    int.TryParse(playerInfo.Name.Substring(textBeforeNumber.Length), out Int32 number) && 
                    number >= newItemNumber)
                {
                    newItemNumber = number + 1;
                }
            }

            if (newItemNumber == int.MinValue)
            {
                newItemNumber = 0;
            }

            return textBeforeNumber + newItemNumber;
        }
    }
}
