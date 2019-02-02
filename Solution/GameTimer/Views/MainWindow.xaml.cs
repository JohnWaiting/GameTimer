using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using GameTimer.ViewModels;

namespace GameTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal MainWindowViewModel ViewModel { get; } = new MainWindowViewModel();

        public MainWindow()
        {
            DataContext = ViewModel;
            InitializeComponent();
        }

        private void ApplicationClosedCommand_OnExecuted(Object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void MainWindow_OnClosing(Object sender, CancelEventArgs e)
        {
            MessageBoxResult dialogResult = 
                MessageBox.Show(
                    "Are you sure want to close application?", 
                    "Closing",
                    MessageBoxButton.YesNo);
            if (dialogResult != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}
