using Prism.Mvvm;

namespace GameTimer
{
    internal abstract class BaseScreenViewModel : BindableBase
    {
        public IApp App { get; }

        internal BaseScreenViewModel(IApp app)
        {
            App = app;
        }
    }
}