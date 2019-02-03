using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
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
                    ViewModel.NextTurnCommand.Execute();
                    break;
                case Key.Escape:
                    OnPauseOrResume();
                    break;
                case Key.Back:
                    ViewModel.ReturnToPreviousTurnCommand.Execute();
                    break;
            }
        }
        
        private void ToStartScreen_OnClick(Object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Are you sure want to open start screen?", "Start screen opening", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                ToStartScreen();
            }
        }

        private void ToStartScreen()
        {
            var screenInfo = new ScreenInfo(new StartScreen(), new StartScreenViewModel(ViewModel.App)
            {
                PlayerListViewModel = { PlayerInfos = new ObservableCollection<PlayerInfo>(ViewModel.Players) }
            });
            ViewModel.App.OpenScreen.Execute(screenInfo);
        }

        private void NextTurnButton_OnClick(Object sender, RoutedEventArgs e)
        {
            ViewModel.NextTurnCommand.Execute();
        }

        private void PauseResumeButton_OnClick(Object sender, RoutedEventArgs e)
        {
            OnPauseOrResume();
        }

        private void OnPauseOrResume()
        {
            if (ViewModel.IsPaused)
            {
                ViewModel.ResumeCommand.Execute();
            }
            else
            {
                ViewModel.PauseCommand.Execute();
            }
        }

        private void ReturnToPreviousTurnButton_OnClick(Object sender, RoutedEventArgs e)
        {
            ViewModel.ReturnToPreviousTurnCommand.Execute();
        }

        private void EndGame_OnClick(Object sender, RoutedEventArgs e)
        {
            if (ViewModel.History.Any())
            {
                ViewModel.PauseCommand.Execute();
                ViewModel.CurrentTurn.EndTime = DateTime.Now;
                MessageBoxResult dialogResult = MessageBox.Show("Are you sure want to end game?", "Game ending", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    SaveGameResults();
                    ToStartScreen();

                }
            }
        }

        private void SaveGameResults()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("Turns:");
            foreach (TurnInfo turnInfo in ViewModel.History)
            {
                result.AppendLine(String.Join("\t", turnInfo.Player.Id, turnInfo.Player.Name, turnInfo.StartTime, turnInfo.Duration,
                    turnInfo.EndTime));
            }

            result.AppendLine();
            result.AppendLine("Players:");

            foreach (PlayerInfo player in ViewModel.Players)
            {
                TimeSpan sum = TimeSpan.Zero;
                Int32 count = 0;
                foreach (TurnInfo step in ViewModel.History.Where(x => x.Player.Id == player.Id))
                {
                    sum += step.Duration;
                    count++;
                }

                TimeSpan average = TimeSpan.FromTicks(sum.Ticks / count);
                result.AppendLine(String.Join("\t", player.Id, player.Name, $"sum:{sum}", $"average:{average}"));
            }

            String fileName = ViewModel.History.First().StartTime.ToString("yyyy-MM-dd-HH-mm-ss");
            String path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            
            File.WriteAllText(path, result.ToString());
            MessageBox.Show($"Game results saved to file {fileName}");
        }
    }
}
