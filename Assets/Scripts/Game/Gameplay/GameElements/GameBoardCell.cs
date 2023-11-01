using Core;
using Core.Extensions;
using UnityEngine;

namespace Game.Gameplay.GameElements
{
    public class GameBoardCell : MonoBehaviour
    {
        public Point point { get; private set; }

        public void SetPoint(Point givenPoint)
        {
            point = givenPoint;
            transform.position = givenPoint.ToVector2();
        }

        public bool Contains(Vector2 otherPoint)
        {
            var localPosition = otherPoint - (Vector2)transform.position;

            return localPosition.x is < Constants.HALF_CELL_STEP and > -Constants.HALF_CELL_STEP
                   && localPosition.y is < Constants.HALF_CELL_STEP and > -Constants.HALF_CELL_STEP;
        }
    }
}
