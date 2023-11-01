using Core.Interface;
using Game.Gameplay;
using UnityEngine;

namespace Core.Extensions
{
    public static class GameExtensions
    {
        public static IState GetFirstState(this IState state)
        {
            var currentState = state;

            while (true)
            {
                var previousState = currentState.previousState;

                if (previousState == null)
                {
                    break;
                }

                currentState = previousState;
            }

            return currentState;
        }

        public static IState GetLastState(this IState state)
        {
            var currentState = state;

            while (true)
            {
                var nextState = currentState.nextState;

                if (nextState == null)
                {
                    break;
                }

                currentState = nextState;
            }

            return currentState;
        }

        public static bool IsTheSameRowOrColumn(this Point point, Point other)
        {
            return point.rowIndex == other.rowIndex || point.columnIndex == other.columnIndex;
        }
        
        public static Vector2 ToVector2(this Point point)
        {
            return new Vector2(Constants.CELL_STEP * point.columnIndex, Constants.CELL_STEP * point.rowIndex);
        }
    }
}
