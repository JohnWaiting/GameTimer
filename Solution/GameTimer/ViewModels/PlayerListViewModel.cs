using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using Prism.Commands;
using Prism.Mvvm;

namespace GameTimer.ViewModels
{
    internal class PlayerListViewModel : BindableBase
    {
        private static readonly Random Random = new Random();
        private static readonly Color?[] DefaultPlayerColors = {
            Colors.Red, 
            Colors.Green, 
            Colors.Blue, 
            Colors.Black, 
            Colors.Orange, 
            Colors.Brown, 
        };

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
            AddPlayerCommand = new DelegateCommand<String>(AddPlayerCommand_ExecuteMethod);
            RemovePlayerCommand = new DelegateCommand<PlayerInfo>(RemovePlayerCommand_ExecuteMethod);

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
            PlayerInfo newItem = GetNewItem();
            if (!string.IsNullOrWhiteSpace(name))
            {
                newItem.Name = name;
            }
            PlayerInfos.Add(newItem);
        }

        private PlayerInfo GetNewItem()
        {
            PlayerInfo result = new PlayerInfo();

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

            result.Name = textBeforeNumber + newItemNumber;

            Color? color =
                DefaultPlayerColors.FirstOrDefault(clr => PlayerInfos.All(playerInfo => playerInfo.Color != clr)) ??
                Color.FromArgb(byte.MaxValue, (Byte) Random.Next(), (Byte) Random.Next(), (Byte) Random.Next());

            result.Color = color.Value;

            return result;
        }
    }
}
