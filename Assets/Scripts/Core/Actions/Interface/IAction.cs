using Core.Interface;

namespace Core.Actions.Interface
{
    public interface IAction
    {
        IState Apply(IState previousState = null);
    }
}
