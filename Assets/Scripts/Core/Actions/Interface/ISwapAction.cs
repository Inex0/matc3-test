namespace Core.Actions.Interface
{
    public interface ISwapAction : IAction
    {
        Point fromPoint { get; }
        Point toPoint { get; }
    }
}
