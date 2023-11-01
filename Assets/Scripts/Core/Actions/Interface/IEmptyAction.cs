using Core.Elements;

namespace Core.Actions.Interface
{
    public interface IEmptyAction : IAction
    {
        IElement element { get; }
    }
}
