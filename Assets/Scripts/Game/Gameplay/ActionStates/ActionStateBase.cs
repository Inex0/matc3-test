using Core.Interface;
using Cysharp.Threading.Tasks;

namespace Game.Gameplay.ActionStates
{
    public abstract class ActionStateBase
    {
        public abstract UniTask Execute(BoardElementsFactory boardElementsFactory, IState state);
    }
}
