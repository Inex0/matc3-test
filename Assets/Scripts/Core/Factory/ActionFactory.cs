using Core.Actions;
using Core.Actions.Interface;
using Core.Interface;

namespace Core.Factory
{
    public class ActionFactory
    {
        public IAction CreateAction<TAction>(IMemory memory) where TAction : IAction
        {
            if (typeof(TAction) == typeof(IInitialAction))
            {
                return new InitialAction(memory);
            }

            if (typeof(TAction) == typeof(ISwapAction))
            {
                return new SwapAction(memory);
            }

            if (typeof(TAction) == typeof(IDeleteAction))
            {
                return new DeleteAction(memory);
            }

            if (typeof(TAction) == typeof(IGravityAction))
            {
                return new GravityAction(memory);
            }

            if (typeof(TAction) == typeof(IFillAction))
            {
                return new FillAction(memory);
            }

            if (typeof(TAction) == typeof(IEmptyAction))
            {
                return new EmptyAction(memory);
            }

            if (typeof(TAction) == typeof(IAutoStepAction))
            {
                return new AutoStepAction(memory);
            }

            return null;
        }
    }
}