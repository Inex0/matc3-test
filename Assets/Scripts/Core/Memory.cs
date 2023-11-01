using System.Collections.Generic;
using Core.Factory;
using Core.Interface;

namespace Core
{
    public class Memory : IMemory
    {
        public IBoard board { get; }
        public ElementFactory elementFactory { get; }
        public ActionFactory actionFactory { get; }
        public StateFactory stateFactory { get; }
        public List<IState> stateBySteps { get; }
        public Point fromPoint { get; set; }
        public Point toPoint { get; set; }

        public Memory(IBoard board, ElementFactory elementsFactory, ActionFactory actionsFactory, StateFactory statesFactory, List<IState> states)
        {
            this.board = board;
            elementFactory = elementsFactory;
            actionFactory = actionsFactory;
            stateFactory = statesFactory;
            stateBySteps = states;
        }
    }
}
