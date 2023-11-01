using Core.Actions.Interface;
using Core.Elements;
using Core.Interface;

namespace Core.Actions
{
    public class EmptyAction : IEmptyAction
    {
        private readonly IMemory _memory;

        public IElement element { get; private set; }

        public EmptyAction(IMemory memory)
        {
            this._memory = memory;
        }

        public IState Apply(IState previousState = null)
        {
            var field = previousState.field.Copy();
            element = field.GetElementAt(_memory.fromPoint);

            return _memory.stateFactory.CreateState(field, this, previousState);
        }
    }
}
