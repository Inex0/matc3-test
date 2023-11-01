using System.Collections.Generic;
using Core.Actions.Interface;
using Core.Elements;
using Core.Extensions;
using Core.Interface;
using Core.Utilities;

namespace Core.Actions
{
    public class FillAction : IFillAction
    {
        private readonly IMemory _memory;

        public FillAction(IMemory memory)
        {
            _memory = memory;
        }

        public IState Apply(IState previousState = null)
        {
            var boardSize = _memory.board.size;
            var previousField = previousState?.field.Copy();
            var field = new Field(new List<IElement>(boardSize.x * boardSize.y));
            var maxRow = boardSize.x;
            var rowWithNewElements = -1;
            var newElementsInRow = new List<IElement>();
            var createElementMethods = _memory.elementFactory.GetCreateElementMethods();

            for (var row = 0; row < boardSize.x; row++)
            {
                for (var column = 0; column < boardSize.y; column++)
                {
                    var point = new Point(row, column);
                    var element = previousField?.GetElementAt(point);

                    if (element != null)
                    {
                        field.AddOrReplaceElementByPoint(element.Copy());

                        continue;
                    }

                    if (rowWithNewElements != -1 && rowWithNewElements != row)
                    {
                        continue;
                    }

                    rowWithNewElements = row;

                    createElementMethods.Shuffle();

                    for (var i = 0; i < createElementMethods.Count; i++)
                    {
                        var newElement = createElementMethods[i](point);
                        field.AddOrReplaceElementByPoint(newElement);

                        if (!MatchEngineUtility.HasMatch(field, point, boardSize))
                        {
                            newElementsInRow.Add(newElement);

                            break;
                        }

                        field.RemoveElement(newElement);
                    }
                }
            }

            newElementsInRow.ForEach(x => x.point = new Point(maxRow, x.point.columnIndex));
            return _memory.stateFactory.CreateState(field, this, previousState);
        }
    }
}
