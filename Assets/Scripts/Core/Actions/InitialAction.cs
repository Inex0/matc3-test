using System.Collections.Generic;
using Core.Actions.Interface;
using Core.Elements;
using Core.Extensions;
using Core.Interface;

namespace Core.Actions
{
    public class InitialAction : IInitialAction
    {
        private readonly IMemory _memory;

        public InitialAction(IMemory memory)
        {
            _memory = memory;
        }

        public IState Apply(IState previousState = null)
        {
            var field = _memory.stateBySteps.Count > 0
                ? _memory.stateBySteps[^1].GetLastState().field.Copy()
                : new Field(new List<IElement>());

            return _memory.stateFactory.CreateState(field, this, previousState);
        }
    }
}
