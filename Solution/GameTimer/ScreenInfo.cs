using System;
using System.Windows;

namespace GameTimer
{
    public class ScreenInfo
    {
        public FrameworkElement View { get; }
        public Object DataContext { get; }

        public ScreenInfo(FrameworkElement view, Object dataContext)
        {
            View = view;
            DataContext = dataContext;
        }
    }
}