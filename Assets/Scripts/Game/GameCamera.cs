using UnityEngine;

namespace Game
{
    public class GameCamera : MonoBehaviour
    {
        [SerializeField]
        private Camera _gameCamera;

        [SerializeField]
        [Range(0.1f, 2)]
        private float _sizeFactor = 0.5f;

        public void Init(Bounds bounds, float verticalOffset = 0)
        {
            var width = bounds.size.x;
            var orthographicSize = _sizeFactor * width / _gameCamera.aspect;

            var center = bounds.center;
            var position = _gameCamera.transform.position;
            position.x = center.x;
            position.y = center.y;

            _gameCamera.orthographicSize = orthographicSize;
            _gameCamera.transform.position = position + Vector3.up * verticalOffset;
        }
        
        public Vector2 GetWorldPoint(Vector2 screenPoint)
        {
            return _gameCamera.ScreenToWorldPoint(screenPoint);
        }
    }
}