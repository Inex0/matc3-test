using System.Collections.Generic;
using Core.Actions;
using Core.Extensions;
using Core.Factory;
using Core.Interface;

namespace Core
{
    public class GameEngine : IGameEngine
    {
        private readonly List<IState> _states;
        private readonly IMemory _memory;
        private readonly ActionStateSwitcher _actionStateSwitcher;
        private readonly IBoard _board;

        public GameEngine()
        {
            _board = new Board();
            _states = new List<IState>();
            _memory = new Memory(_board, new ElementFactory(), new ActionFactory(), new StateFactory(), _states);
            _actionStateSwitcher = new ActionStateSwitcher(_memory);
        }

        public IState BuildLevel(uint rows, uint columns)
        {
            _board.Build(rows, columns);
            _states.Clear();

            var state = _actionStateSwitcher.Build().GetFirstState();
            _states.Add(state);

            return state;
        }

        public IState GetStateByInput(IStepInput stepInput)
        {
            _memory.fromPoint = stepInput.fromPoint;
            _memory.toPoint = stepInput.toPoint;

            var state = _actionStateSwitcher.Apply().GetFirstState();
            _states.Add(state);

            return state;
        }

        public IState AutoStep()
        {
            var state = _actionStateSwitcher.ApplyAutoStep().GetFirstState();
            _states.Add(state);

            return state;
        }
    }
}
