using UnityEngine;

namespace Core.Extensions
{
    public static class TransformExtensions
    {
        public static void DeleteChilds(this Transform transform)
        {
            foreach (Transform item in transform)
            {
                Transform.Destroy(item.gameObject);
            }
        }
    }
}
