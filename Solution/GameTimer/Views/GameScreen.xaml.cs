using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GameTimer.ViewModels;

namespace GameTimer.Views
{
    /// <summary>
    /// Interaction logic for GameScreen.xaml
    /// </summary>
    public partial class GameScreen : UserControl
    {
        private GameScreenViewModel ViewModel => (GameScreenViewModel) DataContext;

        private DateTime lastStartTime;

        public GameScreen()
        {
            InitializeComponent();
            EventManager.RegisterClassHandler(typeof(Window),
                Keyboard.KeyUpEvent, new KeyEventHandler(OnKeyUp), true);
            
        }

        private void OnKeyUp(Object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space:
                    ViewModel.NextTurnCommand.Execute(null);
                    break;
                case Key.Escape:
                    ViewModel.PauseCommand.Execute(null);
                    break;
                case Key.Back:
                    ViewModel.ReturnToPreviousTurnCommand.Execute(null);
                    break;
            }
        }
        
        private void ToStartScreen_OnClick(Object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Are you sure want to open start screen?", "Start screen opening", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                var screenInfo = new ScreenInfo(new StartScreen(), new StartScreenViewModel(ViewModel.App)
                {
                    PlayerListViewModel = { PlayerInfos = new ObservableCollection<PlayerInfo>(ViewModel.Players) }
                });
                ViewModel.App.OpenScreen.Execute(screenInfo);
            }
        }

        private void NextTurnButton_OnClick(Object sender, RoutedEventArgs e)
        {
            ViewModel.NextTurnCommand.Execute(null);
        }

        private void PauseButton_OnClick(Object sender, RoutedEventArgs e)
        {
            ViewModel.PauseCommand.Execute(null);
        }

        private void ReturnToPreviousTurnButton_OnClick(Object sender, RoutedEventArgs e)
        {
            ViewModel.ReturnToPreviousTurnCommand.Execute(null);
        }
    }
}
