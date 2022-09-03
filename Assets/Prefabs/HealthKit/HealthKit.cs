using UnityEngine;

namespace Prefabs.HealthKit
{
    /// <summary>
    /// Represent an health kit
    /// </summary>
    public class HealthKit : MonoBehaviour, IInterractable
    {
        public void Interract(Player.Scripts.Player player)
        {
            player.GainLife(100);

            Destroy(gameObject);
        }

        public bool CanInterract(Player.Scripts.Player player)
        {
            return !player.HasFullLife();
        }
    }
}