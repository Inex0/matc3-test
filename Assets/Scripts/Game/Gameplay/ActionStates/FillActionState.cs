using System.Collections.Generic;
using Core.Interface;
using Cysharp.Threading.Tasks;

namespace Game.Gameplay.ActionStates
{
    public class FillActionState : ActionStateBase
    {
        public override async UniTask Execute(BoardElementsFactory boardElementsFactory, IState state)
        {
            var field = state.field;
            var tasks = new List<UniTask>();

            for (var i = 0; i < field.elements.Count; i++)
            {
                var element = field.elements[i];

                if (!boardElementsFactory.IsElementExists(element.id))
                {
                    var boardElement = boardElementsFactory.CreateBoardElement(element);
                    var task = boardElement.RenderAppearing(element.point);
                    tasks.Add(task);
                }
            }

            await UniTask.WhenAll(tasks);
        }
    }
}
