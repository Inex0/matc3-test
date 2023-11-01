using Game.Gameplay;
using UnityEngine;

namespace Game.Scenes
{
    public class MainEntry : MonoBehaviour
    {
        [SerializeField]
        private uint _rowsAmount = 8;
        
        [SerializeField]
        private uint _columnsAmount = 8;
        
        [SerializeField]
        private GameplayManager _gameplayManager;

        [SerializeField]
        [Range(0, 1000000)]
        private uint _autoStepsAmount = 0;

        private void Start()
        {
            Application.targetFrameRate = 60;
            _gameplayManager.Initialize(_rowsAmount, _columnsAmount, _autoStepsAmount);
        }
    }
}