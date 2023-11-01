using System.Collections.Generic;
using Core.Elements;

namespace Core.Actions.Interface
{
    public interface IDeleteAction : IAction
    {
        List<IElement> deleteElements { get; }
    }
}
