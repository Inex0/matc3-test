using Core.Actions.Interface;
using Core.Interface;
using Cysharp.Threading.Tasks;

namespace Game.Gameplay.ActionStates
{
    public class SwapActionState : ActionStateBase
    {
        public override async UniTask Execute(BoardElementsFactory boardElementsFactory, IState state)
        {
            if (state.action is ISwapAction swapAction)
            {
                var fromPoint = swapAction.fromPoint;
                var toPoint = swapAction.toPoint;
                var previousField = state.previousState.field;

                var fromElement = previousField.GetElementAt(fromPoint);
                var toElement = previousField.GetElementAt(toPoint);

                var fromElementActor = boardElementsFactory.GetBoardElementById(fromElement.id);
                var toElementActor = boardElementsFactory.GetBoardElementById(toElement.id);
                var task0 = fromElementActor.MoveTo(toPoint);
                var task1 = toElementActor.MoveFrom(fromPoint);

                await UniTask.WhenAll(task0, task1);
            }
        }
    }
}
