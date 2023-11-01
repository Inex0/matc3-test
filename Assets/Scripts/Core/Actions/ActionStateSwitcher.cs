using Core.Actions.Interface;
using Core.Interface;

namespace Core.Actions
{
    public class ActionStateSwitcher
    {
        private readonly IMemory _memory;

        public ActionStateSwitcher(IMemory memory)
        {
            _memory = memory;
        }

        public IState Build()
        {
            var initialState = ApplyAction<IInitialAction>();
            var gravityState = initialState;

            do
            {
                var fillState = ApplyAction<IFillAction>(gravityState);
                gravityState = ApplyAction<IGravityAction>(fillState);
            } while (gravityState.field.elements.Count != _memory.board.points.Count);

            return gravityState;
        }

        public IState Apply()
        {
            var initialState = ApplyAction<IInitialAction>();

            return ApplyInternal(initialState);
        }

        public IState ApplyAutoStep()
        {
            var initialState = ApplyAction<IInitialAction>();
            var autoStepState = ApplyAction<IAutoStepAction>(initialState);

            return ApplyInternal(autoStepState);
        }

        private IState ApplyInternal(IState previousState)
        {
            if (_memory.fromPoint.Equal(_memory.toPoint))
            {
                return ApplyAction<IEmptyAction>(previousState);
            }

            var swapState = ApplyAction<ISwapAction>(previousState);
            var deleteState = ApplyAction<IDeleteAction>(swapState);
            var anyIterationHappened = false;

            while (IsNextIterationAvailable(deleteState))
            {
                var gravityAction = ApplyAction<IGravityAction>(deleteState);
                var fillAction = ApplyAction<IFillAction>(gravityAction);
                gravityAction = ApplyAction<IGravityAction>(fillAction);
                deleteState = ApplyAction<IDeleteAction>(gravityAction);
                anyIterationHappened = true;
            }

            if (anyIterationHappened)
            {
                return deleteState;
            }

            return ApplyAction<ISwapAction>(deleteState);
        }

        private bool IsNextIterationAvailable(IState state)
        {
            return state.action is IDeleteAction deleteAction && deleteAction.deleteElements.Count > 0
                   || state.field.elements.Count != _memory.board.points.Count;
        }

        private IState ApplyAction<TAction>(IState previousState = null) where TAction : IAction
        {
            return _memory.actionFactory.CreateAction<TAction>(_memory).Apply(previousState);
        }
    }
}
