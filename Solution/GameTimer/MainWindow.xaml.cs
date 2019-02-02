using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using GameTimer.ViewModels;
using GameTimer.Views;
using Prism.Commands;

namespace GameTimer
{
    internal interface IApp
    {
        DelegateCommand<ScreenInfo> OpenScreen { get; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IApp
    {
        public DelegateCommand<ScreenInfo> OpenScreen { get; } 

        public MainWindow()
        {
            OpenScreen = new DelegateCommand<ScreenInfo>(OpenScreenCommand_ExecuteMethod);

            InitializeComponent();
            var startScreenInfo = new ScreenInfo(new StartScreen(), new StartScreenViewModel(this));
            OpenScreen.Execute(startScreenInfo);
        }

        private void OpenScreenCommand_ExecuteMethod(ScreenInfo screenInfo)
        {
            ScreenContainer.Children.Clear();

            FrameworkElement view = screenInfo.View;
            view.DataContext = screenInfo.DataContext;
            view.HorizontalAlignment = HorizontalAlignment.Stretch;
            view.VerticalAlignment = VerticalAlignment.Center;
            view.Margin = new Thickness(0);

            ScreenContainer.Children.Add(view);
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
