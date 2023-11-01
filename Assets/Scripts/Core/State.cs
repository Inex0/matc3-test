using Core.Actions.Interface;
using Core.Interface;

namespace Core
{
    public class State : IState
    {
        public IField field { get; }
        public IAction action { get; }
        public IState previousState { get; set; }
        public IState nextState { get; set; }

        public State(IField field, IAction action, IState previousState)
        {
            this.field = field;
            this.action = action;

            if (previousState != null)
            {
                previousState.nextState = this;
                this.previousState = previousState;
            }
        }
    }
}
