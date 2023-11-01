using System.Collections.Generic;
using Core.Elements;
using Core.Interface;

namespace Core
{
    public class Field : IField
    {
        private readonly List<IElement> _elements;

        public IReadOnlyList<IElement> elements => _elements;

        public Field(List<IElement> elements)
        {
            _elements = elements;
        }

        public bool SwapElements(Point fromPoint, Point toPoint)
        {
            var fromElement = GetElementAt(fromPoint);
            var toElement = GetElementAt(toPoint);

            if (fromElement == null || toElement == null)
            {
                return false;
            }

            fromElement.point = toPoint;
            toElement.point = fromPoint;

            return true;
        }

        public IElement GetElementAt(Point point)
        {
            return _elements.Find(x => x.point.Equal(point));
        }

        public List<IElement> DeleteAndGetElementByPoints(List<Point> points)
        {
            var deletedElements = new List<IElement>(points.Count);

            for (var i = 0; i < points.Count; i++)
            {
                var element = GetElementAt(points[i]);

                if (element == null)
                {
                    continue;
                }

                _elements.Remove(element);
                deletedElements.Add(element);
            }

            return deletedElements;
        }

        public void AddOrReplaceElementByPoint(IElement element)
        {
            _elements.RemoveAll(x => x.point.Equal(element.point));
            _elements.Add(element);
        }

        public void RemoveElement(IElement element)
        {
            _elements.RemoveAll(x => x == element);
        }

        public IField Copy()
        {
            var copyElements = new List<IElement>(_elements.Count);

            for (var i = 0; i < _elements.Count; i++)
            {
                copyElements.Add(_elements[i].Copy());
            }

            return new Field(copyElements);
        }
    }
}
