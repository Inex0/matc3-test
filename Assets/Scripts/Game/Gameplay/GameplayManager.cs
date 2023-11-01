using System;
using Core;
using Core.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Gameplay
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField]
        private MatchEngineRenderer _matchEngineRenderer;

        private IGameEngine _gameEngine;
        private uint _autoStepsAmount;

        private void Awake()
        {
            _gameEngine = new GameEngine();
            _matchEngineRenderer.Initialize();
            _matchEngineRenderer.onStepInputUpdated.Subscribe(HandleStepInputUpdated).AddTo(this.GetCancellationTokenOnDestroy());
            _matchEngineRenderer.onBuildCompleted.Subscribe(HandleLevelBuildCompleted).AddTo(this.GetCancellationTokenOnDestroy());
        }

        public void Initialize(uint rows, uint columns, uint autoStepsAmount = 0)
        {
            BuildLevel(rows, columns);
            _autoStepsAmount = autoStepsAmount;
        }

        private void BuildLevel(uint rows, uint columns)
        {
            var state = _gameEngine.BuildLevel(rows, columns);
            _matchEngineRenderer.Build(rows, columns, state);
        }

        private async void SetAutoPlayMode()
        {
            var currentStep = 0;
            while (_autoStepsAmount > 0 && currentStep < _autoStepsAmount)
            {
                var state = _gameEngine.AutoStep();
                await _matchEngineRenderer.RenderStateAsync(state);
                currentStep++;
                //todo draw steps amount in UI
                Debug.Log($"[{nameof(GameplayManager)}] CurrentStep: {currentStep}");
            }
        }

        private void HandleStepInputUpdated(IStepInput stepInput)
        {
            var state = _gameEngine.GetStateByInput(stepInput);
            _matchEngineRenderer.RenderStateAsync(state).Forget();
        }

        private void HandleLevelBuildCompleted(bool isCompleted)
        {
            if (isCompleted && _autoStepsAmount > 0)
            {
                SetAutoPlayMode();
            }
        }
    }
}