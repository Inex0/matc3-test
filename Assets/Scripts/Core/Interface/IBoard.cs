using System.Collections.Generic;
using UnityEngine;

namespace Core.Interface
{
    public interface IBoard
    {
        Vector2Int size { get; }
        IReadOnlyList<Point> points { get; }
        void Build(uint rows, uint columns);
    }
}