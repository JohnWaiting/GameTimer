using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using GameTimer.ViewModels;
using Prism.Commands;
using Prism.Mvvm;

namespace GameTimer
{
    internal class GameScreenViewModel : BaseScreenViewModel
    {
        private DispatcherTimer _timer;

        private DateTime _lastStartTime;

        public IReadOnlyList<PlayerInfo> Players { get; }

        private PlayerInfo _currentPlayer;
        public PlayerInfo CurrentPlayer
        {
            get => _currentPlayer;
            private set => SetProperty(ref _currentPlayer, value);
        }

        private TurnInfo _currentTurn;
        public TurnInfo CurrentTurn
        {
            get => _currentTurn;
            private set => SetProperty(ref _currentTurn, value);
        }

        public ICommand NextTurnCommand { get; }
        public ICommand PauseCommand { get; }
        public ICommand ReturnToPreviousTurnCommand { get; }

        public GameScreenViewModel(IApp app, IEnumerable<PlayerInfo> players) : base(app)
        {
            NextTurnCommand = new DelegateCommand(NextTurnCommand_OnExecute);
            PauseCommand = new DelegateCommand(PauseCommand_OnExecute);
            ReturnToPreviousTurnCommand = new DelegateCommand(ReturnToPreviousTurnCommand_OnExecute);

            Players = players.ToList();
            CurrentPlayer = Players.First();

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 1, 0), DispatcherPriority.Background,
                OnTimerTick, Dispatcher.CurrentDispatcher);
        }

        private void OnTimerTick(Object sender, EventArgs e)
        {
            if (CurrentTurn != null)
            {
                CurrentTurn.Duration = (DateTime.Now - _lastStartTime);
            }
        }

        private void NextTurnCommand_OnExecute()
        {
            _lastStartTime = DateTime.Now;;
            if (CurrentTurn == null)
            {
                CurrentTurn = new TurnInfo(DateTime.Now);
            }
        }

        private void PauseCommand_OnExecute()
        {
            throw new NotImplementedException();
        }

        private void ReturnToPreviousTurnCommand_OnExecute()
        {
            throw new NotImplementedException();
        }
    }

    internal class TurnInfo : BindableBase
    {
        private TimeSpan _duration;

        public TimeSpan Duration
        {
            get => _duration;
            set => SetProperty(ref _duration, value);
        }

        public DateTime StartTime { get; }

        public DateTime? EndTime { get; set; }

        public TurnInfo(DateTime startTime)
        {
            StartTime = startTime;
        }
    }
}