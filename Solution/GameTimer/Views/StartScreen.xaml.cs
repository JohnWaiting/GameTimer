using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using GameTimer.ViewModels;
using MessageBox = System.Windows.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace GameTimer.Views
{
    /// <summary>
    /// Interaction logic for StartScreen.xaml
    /// </summary>
    public partial class StartScreen : UserControl
    {
        private StartScreenViewModel ViewModel => (StartScreenViewModel) DataContext;

        public StartScreen()
        {
            InitializeComponent();
        }

        private void StartGame_OnClick(Object sender, RoutedEventArgs e)
        {
            if (ViewModel.PlayerListViewModel.PlayerInfos.Count > 1)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Are you sure want to start the game?", "Game starting", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    ViewModel.App.OpenScreen.Execute(new ScreenInfo(new GameScreen(), new GameScreenViewModel(ViewModel.App, ViewModel.PlayerListViewModel.PlayerInfos)));
                }
            }
            else
            {
                MessageBox.Show("Need at least two players to start");
            }
        }
    }
}
