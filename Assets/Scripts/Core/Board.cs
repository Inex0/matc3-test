using System.Collections.Generic;
using Core.Interface;
using UnityEngine;

namespace Core
{
    public class Board : IBoard
    {
        private readonly List<Point> _points = new();

        public Vector2Int size { get; private set; }
        public IReadOnlyList<Point> points => _points;


        public void Build(uint rows, uint columns)
        {
            size = new Vector2Int((int)rows, (int)columns);
            _points.Clear();

            for (var x = 0; x < rows; x++)
            {
                for (var y = 0; y < columns; y++)
                {
                    _points.Add(new Point(x, y));
                }
            }
        }
    }
}
