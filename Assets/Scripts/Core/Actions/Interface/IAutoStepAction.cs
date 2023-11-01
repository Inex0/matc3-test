namespace Core.Actions.Interface
{
    public interface IAutoStepAction : IAction
    {
        bool hasSuccessStep { get; }
    }
}
