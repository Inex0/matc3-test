using Game.Gameplay.GameElements;

namespace Core.Elements
{
    public class BoardElement : IElement
    {
        public string id { get; }
        public ElementType type { get; }
        public Point point { get; set; }

        public BoardElement(string elementId, Point elementPoint, ElementType elementType)
        {
            id = elementId;
            type = elementType;
            point = elementPoint;
        }
        
        public IElement Copy()
        {
            return new BoardElement(id, point, type);
        }
    }
}