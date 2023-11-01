using Core.Actions.Interface;

namespace Core.Interface
{
    public interface IState
    {
        IField field { get; }
        IAction action { get; }
        IState previousState { get; set; }
        IState nextState { get; set; }
    }
}