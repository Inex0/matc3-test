using Core.Actions.Interface;
using Core.Interface;
using Cysharp.Threading.Tasks;

namespace Game.Gameplay.ActionStates
{
    public class StateByActionFactory
    {
        private readonly BoardElementsFactory _boardElementsFactory;

        public StateByActionFactory(BoardElementsFactory boardElementsFactory)
        {
            _boardElementsFactory = boardElementsFactory;
        }

        public async UniTask GetState(IState state)
        {
            var action = state.action;

            var actionState = action switch
            {
                IFillAction => new FillActionState(),
                IEmptyAction => new EmptyActionState(),
                ISwapAction => new SwapActionState(),
                IDeleteAction => new DeleteActionState(),
                IGravityAction => new GravityActionState(),
                _ => default(ActionStateBase)
            };

            if (actionState == null)
            {
                return;
            }

            await actionState.Execute(_boardElementsFactory, state);
        }
    }
}
