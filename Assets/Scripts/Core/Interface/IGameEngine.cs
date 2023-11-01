
namespace Core.Interface
{
    public interface IGameEngine
    {
        IState BuildLevel(uint rows, uint columns);
        IState GetStateByInput(IStepInput stepInput);
        IState AutoStep();
    }
}