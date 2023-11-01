using System.Collections.Generic;
using Core.Interface;
using UnityEngine;

namespace Core.Utilities
{
    public static class MatchEngineUtility
    {
        public static bool HasMatch(IField field, Point point, Vector2Int boardSize)
        {
            var templates = GetPointTemplates(point, boardSize.x, boardSize.y);
            return templates.Exists(x => HasMatch(field, x));
        }

        public static List<Point> GetMatchPoints(IField field, Point point, Vector2Int boardSize)
        {
            var templates = GetPointTemplates(point, boardSize.x, boardSize.y);
            var matchPoints = new List<Point>();

            for (var i = 0; i < templates.Count; i++)
            {
                var template = templates[i];

                if (HasMatch(field, template))
                {
                    matchPoints.AddRange(template);
                }
            }

            return matchPoints;
        }

        private static bool HasMatch(IField field, List<Point> points)
        {
            for (var i = 0; i < points.Count - 1; i++)
            {
                var element0 = field.GetElementAt(points[i]);
                var element1 = field.GetElementAt(points[i + 1]);

                if (element0 == null || element1 == null || element0.type != element1.type)
                {
                    return false;
                }
            }

            return true;
        }

        private static List<List<Point>> GetPointTemplates(Point point, int maxRow, int maxColumn)
        {
            var result = new List<List<Point>>();
            var row = point.rowIndex;
            var column = point.columnIndex;

            if (row - 1 >= 0 && row + 1 < maxRow)
            {
                result.Add(new List<Point> { point, new(row + 1, column), new(row - 1, column) });
            }

            if (row + 2 < maxRow)
            {
                result.Add( new List<Point> { point, new(row + 1, column), new(row + 2, column) });
            }

            if (row - 2 >= 0)
            {
                result.Add(new List<Point> { point, new(row - 1, column), new(row - 2, column) });
            }

            if (column - 1 >= 0 && column + 1 < maxColumn)
            {
                result.Add(new List<Point> { point, new(row, column + 1), new(row, column - 1) });
            }
            
            if (column + 2 < maxColumn)
            {
                result.Add(new List<Point> { point, new(row, column + 1), new(row, column + 2) });
            }
            
            if (column - 2 >= 0)
            {
                result.Add(new List<Point> { point, new(row, column - 1), new(row, column - 2) });
            }

            return result;
        }
    }
}
