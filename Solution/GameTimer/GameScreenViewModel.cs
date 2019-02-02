using System.Collections.Generic;
using System.Linq;
using GameTimer.ViewModels;

namespace GameTimer
{
    internal class GameScreenViewModel : BaseScreenViewModel
    {
        public IReadOnlyList<PlayerInfo> Players { get; }

        public GameScreenViewModel(IApp app, IEnumerable<PlayerInfo> players) : base(app)
        {
            Players = players.ToList();
        }
    }
}