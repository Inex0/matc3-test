using System.Collections.Generic;
using Core.Actions.Interface;
using Core.Extensions;
using Core.Interface;
using Core.Utilities;

namespace Core.Actions
{
    public class AutoStepAction : IAutoStepAction
    {
        private readonly IMemory _memory;

        public bool hasSuccessStep { get; private set; }

        public AutoStepAction(IMemory memory)
        {
            _memory = memory;
        }

        public IState Apply(IState previousState = null)
        {
            if (TryGetSuccessStep(previousState.field.Copy(), out var fromPoint, out var toPoint))
            {
                hasSuccessStep = true;
                _memory.fromPoint = fromPoint;
                _memory.toPoint = toPoint;
            }

            return _memory.stateFactory.CreateState(previousState.field.Copy(), this, previousState);
        }

        private bool TryGetSuccessStep(IField field, out Point fromPoint, out Point toPoint)
        {
            var allPoints = _memory.board.points;
            var successSwapPoints = new List<(Point fromPoint, Point toPoint)>();

            for (var i = 0; i < allPoints.Count; i++)
            {
                var swapPoints = GetSwapPoints(allPoints[i]);

                for (var j = 0; j < swapPoints.Count; j++)
                {
                    var (from, to) = swapPoints[j];
                    var isSwapSuccess = field.SwapElements(from, to);

                    if (!isSwapSuccess)
                    {
                        continue;
                    }

                    if (MatchEngineUtility.HasMatch(field, from, _memory.board.size))
                    {
                        successSwapPoints.Add((from, to));
                    }

                    field.SwapElements(from, to);
                }
            }

            var randomSwapPoints = successSwapPoints.RandomItem();
            fromPoint = randomSwapPoints.fromPoint;
            toPoint = randomSwapPoints.toPoint;

            return successSwapPoints.Count > 0;
        }

        private static List<(Point, Point)> GetSwapPoints(Point inputPoint)
        {
            var row = inputPoint.rowIndex;
            var column = inputPoint.columnIndex;

            var points1 = (inputPoint, new Point(row + 1, column));
            var points2 = (inputPoint, new Point(row - 1, column));
            var points3 = (inputPoint, new Point(row, column + 1));
            var points4 = (inputPoint, new Point(row, column - 1));

            return new List<(Point, Point)> { points1, points2, points3, points4 };
        }
    }
}
