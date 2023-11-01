using System.Collections.Generic;
using Core.Elements;

namespace Core.Interface
{
    public interface IField
    {
        IReadOnlyList<IElement> elements { get; }
        bool SwapElements(Point fromPoint, Point toPoint);
        IElement GetElementAt(Point point);
        List<IElement> DeleteAndGetElementByPoints(List<Point> points);
        void AddOrReplaceElementByPoint(IElement element);
        void RemoveElement(IElement element);
        IField Copy();
    }
}
