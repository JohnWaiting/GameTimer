using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private DateTime? _currentStartTime;

        public IReadOnlyList<PlayerInfo> Players { get; }
        
        private TurnInfo _currentTurn;
        public TurnInfo CurrentTurn
        {
            get => _currentTurn;
            private set => SetProperty(ref _currentTurn, value);
        }

        public ObservableCollection<TurnInfo> History { get; } = new ObservableCollection<TurnInfo>();

        public ICommand NextTurnCommand { get; }
        public ICommand PauseCommand { get; }
        public ICommand ReturnToPreviousTurnCommand { get; }

        public GameScreenViewModel(IApp app, IEnumerable<PlayerInfo> players) : base(app)
        {
            NextTurnCommand = new DelegateCommand(NextTurnCommand_OnExecute);
            PauseCommand = new DelegateCommand(PauseCommand_OnExecute);
            ReturnToPreviousTurnCommand = new DelegateCommand(ReturnToPreviousTurnCommand_OnExecute);

            Players = players.ToList();

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 333), DispatcherPriority.Background,
                OnTimerTick, Dispatcher.CurrentDispatcher);
        }

        private void OnTimerTick(Object sender, EventArgs e)
        {
            if (CurrentTurn != null && _currentStartTime.HasValue)
            {
                CurrentTurn.Duration = (DateTime.Now - _currentStartTime.Value);
            }
        }

        private void NextTurnCommand_OnExecute()
        {
            TurnInfo currentTurn = CurrentTurn;
            CurrentTurn = null;
            _currentStartTime = DateTime.Now;

            if (currentTurn == null)
            {
                CurrentTurn = new TurnInfo(DateTime.Now, Players.First());
            }
            else
            {
                currentTurn.EndTime = _currentStartTime;
                PlayerInfo nextPlayer = Players[(Players.IndexOf(currentTurn.Player) + 1) % Players.Count];
                CurrentTurn = new TurnInfo(_currentStartTime.Value, nextPlayer);
            }
            History.Add(CurrentTurn);
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
        public PlayerInfo Player { get; }

        private DateTime? _endTime;
        public DateTime? EndTime
        {
            get => _endTime;
            set => SetProperty(ref _endTime, value);
        }

        public TurnInfo(DateTime startTime, PlayerInfo player)
        {
            StartTime = startTime;
            Player = player;
        }
    }

    public static class Extensions
    {
        public static Int32 IndexOf<T>(this IReadOnlyList<T> readOnlyList, T item, Int32 startIndex = 0)
        {
            for (int i = 0; i < readOnlyList.Count; i++)
            {
                if (Equals(item, readOnlyList[i]))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}