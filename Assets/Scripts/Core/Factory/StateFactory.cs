using Core.Actions.Interface;
using Core.Interface;

namespace Core.Factory
{
    public class StateFactory
    {
        public IState CreateState(IField field, IAction action, IState previousState)
        {
            return new State(field, action, previousState);
        }
    }
}