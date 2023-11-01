using System.Collections.Generic;
using Core;
using Core.Elements;
using Core.Extensions;
using Game.Gameplay.GameElements;
using UnityEngine;

namespace Game.Gameplay
{
    public class BoardElementsFactory : MonoBehaviour
    {
        [SerializeField]
        private Transform _boardRoot;

        [SerializeField]
        private Transform _elementsRoot;
        
        [Space]
        [SerializeField]
        private GameplayAssets _gameplayAssets;
        
        private readonly List<GameBoardCell> _boardCellPrefabs = new();
        private readonly List<BoardElementItem> _elementPrefabs = new();

        public IReadOnlyList<GameBoardCell> boardCellPrefabs => _boardCellPrefabs;
        
        public GameBoardCell CreateBoardCellByPoint(Point point)
        {
            var boardCellPrefab = Instantiate(_gameplayAssets.gameBoardCellPrefab, _elementsRoot);
            boardCellPrefab.SetPoint(point);
            _boardCellPrefabs.Add(boardCellPrefab);

            return boardCellPrefab;
        }

        public BoardElementItem CreateBoardElement(IElement element)
        {
            var elementPrefab = Instantiate(_gameplayAssets.boardElementItemPrefab, _boardRoot);
            elementPrefab.SetElement(element);
            _elementPrefabs.Add(elementPrefab);

            return elementPrefab;
        }

        public BoardElementItem GetBoardElementById(string id)
        {
            return _elementPrefabs.Find(x => x.id == id);
        }

        public bool IsElementExists(string id)
        {
            return _elementPrefabs.Exists(x => x.id == id);
        }

        public void DeleteBoardElement(BoardElementItem deletedBoardElement)
        {
            _elementPrefabs.Remove(deletedBoardElement);
            Destroy(deletedBoardElement.gameObject);
        }

        public bool IsAnyElementUsesGravity()
        {
            return _elementPrefabs.Exists(x => x.isGravity);
        }
        
        public void Clear()
        {
            ClearBoardCells();
            ClearElementPrefabs();
        }

        private void ClearBoardCells()
        {
            _boardCellPrefabs.Clear();
            _boardRoot.DeleteChilds();
        }

        private void ClearElementPrefabs()
        {
            _elementPrefabs.Clear();
            _elementsRoot.DeleteChilds();
        }
    }
}
