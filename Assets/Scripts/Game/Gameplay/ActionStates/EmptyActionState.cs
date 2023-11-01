using Core;
using Core.Actions.Interface;
using Core.Interface;
using Cysharp.Threading.Tasks;

namespace Game.Gameplay.ActionStates
{
    public class EmptyActionState : ActionStateBase
    {
        public override async UniTask Execute(BoardElementsFactory boardElementsFactory, IState state)
        {
            if (state.action is IEmptyAction noAction)
            {
                var elementActor = boardElementsFactory.GetBoardElementById(noAction.element.id);

                if (elementActor != null)
                {
                    await elementActor.RenderEmpty();
                }
            }
        }
    }
}
