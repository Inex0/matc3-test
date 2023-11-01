using Core.Interface;
using Cysharp.Threading.Tasks;

namespace Game.Gameplay.ActionStates
{
    public class GravityActionState : ActionStateBase
    {
        public override UniTask Execute(BoardElementsFactory boardElementsFactory, IState state)
        {
            var field = state.field;

            for (var i = 0; i < field.elements.Count; i++)
            {
                var element = field.elements[i];
                var elementActor = boardElementsFactory.GetBoardElementById(element.id);

                if (elementActor == null)
                {
                    continue;
                }

                if (!element.point.Equal(elementActor.point))
                {
                    elementActor.RenderGravity(element.point).Forget();
                }
            }

            return UniTask.CompletedTask;
        }
    }
}
