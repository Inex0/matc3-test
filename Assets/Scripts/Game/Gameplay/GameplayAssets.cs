using Game.Gameplay.GameElements;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Gameplay
{
    public class GameplayAssets : ScriptableObject
    {
        [SerializeField]
        private GameBoardCell _boardCellPrefab;

        [SerializeField]
        private BoardElementItem elementItemPrefab;

        public GameBoardCell gameBoardCellPrefab => _boardCellPrefab;
        public BoardElementItem boardElementItemPrefab => elementItemPrefab;
    }
}
