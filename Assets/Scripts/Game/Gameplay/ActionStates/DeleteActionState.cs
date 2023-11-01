using System.Collections.Generic;
using Core.Actions.Interface;
using Core.Interface;
using Cysharp.Threading.Tasks;

namespace Game.Gameplay.ActionStates
{
    public class DeleteActionState : ActionStateBase
    {
        public override async UniTask Execute(BoardElementsFactory boardElementsFactory, IState state)
        {
            if (state.action is IDeleteAction deleteAction)
            {
                var deletedElements = deleteAction.deleteElements;
                var tasks = new List<UniTask>();

                if (deletedElements.Count > 0)
                {
                    await UniTask.WaitWhile(boardElementsFactory.IsAnyElementUsesGravity);
                }

                for (var i = 0; i < deletedElements.Count; i++)
                {
                    var deletedElement = deletedElements[i];
                    var deletedBoardElement = boardElementsFactory.GetBoardElementById(deletedElement.id);
                    var task = deletedBoardElement.RenderDelete().ContinueWith(() => boardElementsFactory.DeleteBoardElement(deletedBoardElement));
                    tasks.Add(task);
                }

                await UniTask.WhenAll(tasks);
            }
        }
    }
}
