using Core;
using UnityEngine;

namespace Game.Gameplay.GameElements
{
    public class GameBoard : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _boardFrameRenderer;

        private GameBoardCell _gameBoardCell;
        private BoardElementsFactory _boardElementsFactory;
        
        public void Initialize(BoardElementsFactory boardElementsFactory, uint rows, uint columns)
        {
            _boardElementsFactory = boardElementsFactory;
            _boardElementsFactory.Clear();

            _boardFrameRenderer.size = Constants.CELL_STEP * (new Vector2(rows, columns) + 0.24f * Vector2.one);
            _boardFrameRenderer.transform.position = -(Constants.HALF_CELL_STEP + 0.04f) * Vector2.one;

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    _gameBoardCell = _boardElementsFactory.CreateBoardCellByPoint(new Point(x, y));
                }
            }
        }
    }
}
