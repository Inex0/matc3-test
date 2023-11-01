using Core.Actions.Interface;
using Core.Interface;

namespace Core.Actions
{
    public class SwapAction : ISwapAction
    {
        private readonly IMemory _memory;

        public Point fromPoint { get; private set; }
        public Point toPoint { get; private set; }

        public SwapAction(IMemory memory)
        {
            _memory = memory;
        }

        public IState Apply(IState previousState = null)
        {
            fromPoint = _memory.fromPoint;
            toPoint = _memory.toPoint;

            var field = previousState.field.Copy();
            field.SwapElements(_memory.fromPoint, _memory.toPoint);

            return _memory.stateFactory.CreateState(field, this, previousState);
        }
    }
}
