using Unity.VisualScripting;
using UnityEngine;

namespace Utils
{
    public static class GameObjectUtils
    {
        public static void HideGameObject(GameObject gameObject)
        {
            if (!gameObject.IsDestroyed())
            {
                gameObject.SetActive(false);
            }
        }

        public static void ShowGameObject(GameObject gameObject)
        {
            if (!gameObject.IsDestroyed())
            {
                gameObject.SetActive(true);
            }
        }
    }
}