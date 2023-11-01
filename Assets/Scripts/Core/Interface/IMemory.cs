using System.Collections.Generic;
using Core.Factory;

namespace Core.Interface
{
    public interface IMemory
    {
        IBoard board { get; }
        ElementFactory elementFactory { get; }
        ActionFactory actionFactory { get; }
        StateFactory stateFactory { get; }
        List<IState> stateBySteps { get; }
        Point fromPoint { get; set; }
        Point toPoint { get; set; }
    }
}
