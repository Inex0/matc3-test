using Core.Actions.Interface;
using Core.Elements;
using Core.Interface;

namespace Core.Actions
{
    public class GravityAction : IGravityAction
    {
        private readonly IMemory _memory;

        public GravityAction(IMemory memory)
        {
            _memory = memory;
        }

        public IState Apply(IState previousState = null)
        {
            var field = previousState?.field.Copy();
            ApplyGravity(field);

            return _memory.stateFactory.CreateState(field, this, previousState);
        }

        private void ApplyGravity(IField field)
        {
            var board = _memory.board;
            var maxRow = board.size.x;

            for (var i = 0; i < board.points.Count; i++)
            {
                var point = board.points[i];
                var element = field.GetElementAt(point);

                if (element != null)
                {
                    continue;
                }

                var closestElementInColumn = GetClosestElementInColumn(field, point, maxRow);

                if (closestElementInColumn == null)
                {
                    continue;
                }

                closestElementInColumn.point = point;
            }
        }

        private IElement GetClosestElementInColumn(IField field, Point point, int maxRow)
        {
            var row = point.rowIndex;
            var column = point.columnIndex;

            while (row != maxRow)
            {
                var element = field.GetElementAt(new Point(++row, column));

                if (element != null)
                {
                    return element;
                }
            }

            return null;
        }
    }
}
