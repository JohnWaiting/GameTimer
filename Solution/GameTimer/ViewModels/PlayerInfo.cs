using System;
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
    }
}