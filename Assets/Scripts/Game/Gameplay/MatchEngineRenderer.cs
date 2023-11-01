using System;
using System.Reactive.Subjects;
using Core;
using Core.Extensions;
using Core.Interface;
using Cysharp.Threading.Tasks;
using Game.Gameplay.ActionStates;
using Game.Gameplay.GameElements;
using UnityEngine;

namespace Game.Gameplay
{
    public class MatchEngineRenderer : MonoBehaviour
    {
        [SerializeField]
        private BoardElementsFactory _boardElementsFactory;

        [SerializeField]
        private GameBoard _gameBoard;

        [SerializeField]
        private GameCamera _gameCamera;

        [SerializeField]
        private GameInput _gameInput;

        public IObservable<IStepInput> onStepInputUpdated => _onStepInputUpdated;
        
        public IObservable<bool> onBuildCompleted => _onBuildCompleted;

        private readonly ISubject<IStepInput> _onStepInputUpdated = new Subject<IStepInput>();
        private readonly ISubject<bool> _onBuildCompleted = new Subject<bool>();
        
        private StateByActionFactory _stateByActionFactory;
        private Point _fromPoint = Point.undefined;


        public void Initialize()
        {
            _stateByActionFactory = new StateByActionFactory(_boardElementsFactory);
        }

        public async void Build(uint rows, uint columns, IState state)
        {
            var bounds = GetBoardBounds(rows, columns);
            _gameCamera.Init(bounds);
            _gameBoard.Initialize(_boardElementsFactory, rows, columns);

            await RenderStateAsync(state);
            _onBuildCompleted.OnNext(true);
        }

        public async UniTask RenderStateAsync(IState state)
        {
            _gameInput.SetEnabled(false);

            while (true)
            {
                if (state == null)
                {
                    break;
                }

                await _stateByActionFactory.GetState(state);

                state = state.nextState;
            }

            _gameInput.SetEnabled(true);
        }

        private void Awake()
        {
            _gameInput.onDownClicked.Subscribe(HandleDownClick).AddTo(this.GetCancellationTokenOnDestroy());
            _gameInput.onUpClicked.Subscribe(HandleUpClick).AddTo(this.GetCancellationTokenOnDestroy());
            _gameInput.onDragging.Subscribe(HandleDragging).AddTo(this.GetCancellationTokenOnDestroy());
        }

        private void Start()
        {
            _gameInput.SetEnabled(true);
        }

        private void HandleDownClick(Vector2 screenPoint)
        {
            TryGetPoint(screenPoint, out _fromPoint);
        }

        private void HandleUpClick(Vector2 screenPoint)
        {
            if (_fromPoint.isUndefined || !TryGetPoint(screenPoint, out var toPoint) || !toPoint.Equal(_fromPoint))
            {
                return;
            }

            CallStepInputUpdated(toPoint);
        }

        private void HandleDragging(Vector2 screenPoint)
        {
            if (_fromPoint.isUndefined || !TryGetPoint(screenPoint, out var toPoint)
                                       || toPoint.Equal(_fromPoint) ||
                                       !toPoint.IsTheSameRowOrColumn(_fromPoint))
            {
                return;
            }

            CallStepInputUpdated(toPoint);
        }

        private void CallStepInputUpdated(Point toPoint)
        {
            _onStepInputUpdated.OnNext(new StepInput(_fromPoint, toPoint));
            _fromPoint = Point.undefined;
        }

        private bool TryGetPoint(Vector2 screenPoint, out Point point)
        {
            var worldPoint = _gameCamera.GetWorldPoint(screenPoint);

            for (var i = 0; i < _boardElementsFactory.boardCellPrefabs.Count; i++)
            {
                var boardCellActor = _boardElementsFactory.boardCellPrefabs[i];

                if (boardCellActor.Contains(worldPoint))
                {
                    point = boardCellActor.point;
                    return true;
                }
            }

            point = Point.undefined;
            return false;
        }

        private static Bounds GetBoardBounds(uint rows, uint columns)
        {
            var bounds = new Bounds();
            var offset = 0.75f * Constants.CELL_STEP * Vector2.one;
            var max = new Vector2(Constants.CELL_STEP * (columns - 1), Constants.CELL_STEP * (rows - 1)) + offset;
            var min = -offset;

            bounds.Encapsulate(max);
            bounds.Encapsulate(min);

            return bounds;
        }
    }
}