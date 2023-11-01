using System.Collections.Generic;
using Core.Actions.Interface;
using Core.Elements;
using Core.Interface;
using Core.Utilities;

namespace Core.Actions
{
    public class DeleteAction : IDeleteAction
    {
        private readonly IMemory _memory;

        public List<IElement> deleteElements { get; private set; }

        public DeleteAction(IMemory memory)
        {
            _memory = memory;
        }

        public IState Apply(IState previousState = null)
        {
            var field = previousState?.field.Copy();
            var points = GetMatchPoints(field);

            deleteElements = field?.DeleteAndGetElementByPoints(points);

            return _memory.stateFactory.CreateState(field, this, previousState);
        }

        private List<Point> GetMatchPoints(IField field)
        {
            var matchPoints = new List<Point>();

            for (var i = 0; i < _memory.board.points.Count; i++)
            {
                var point = _memory.board.points[i];
                var points = MatchEngineUtility.GetMatchPoints(field, point, _memory.board.size);

                if (points.Count > 0)
                {
                    matchPoints.AddRange(points);
                }
            }

            return matchPoints;
        }
    }
}
