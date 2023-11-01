using Game.Gameplay.GameElements;

namespace Core.Elements
{
    public interface IElement
    {
        string id { get; }
        ElementType type { get; }
        Point point { get; set; }
        IElement Copy();
    }
}
