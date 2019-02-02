using System;
using System.Windows.Media;
using Prism.Mvvm;

namespace GameTimer.ViewModels
{
    internal class PlayerInfo : BindableBase
    {
        public Guid Id { get; }= Guid.NewGuid();

        private String _name;
        public String Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private Color _color;
        public Color Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }
    }
}