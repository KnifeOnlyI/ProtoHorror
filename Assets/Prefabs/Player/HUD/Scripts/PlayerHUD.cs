using UnityEngine;

namespace Prefabs.Player.HUD.Scripts
{
    /// <summary>
    /// Represent the player HUD
    /// </summary>
    public class PlayerHUD : MonoBehaviour
    {
        /// <summary>
        /// The life bar game object
        /// </summary>
        public Bar LifeBar { get; private set; }

        /// <summary>
        /// The stamina bar game object
        /// </summary>
        public Bar StaminaBar { get; private set; }

        /// <summary>
        /// The mana bar game object
        /// </summary>
        public Bar ManaBar { get; private set; }

        /// <summary>
        /// Called before the first frame update
        /// </summary>
        private void Awake()
        {
            var t = transform;
            var panel = t.Find("Panel");

            LifeBar = panel.Find("LifeBar").GetComponent<Bar>();
            StaminaBar = panel.Find("StaminaBar").GetComponent<Bar>();
            ManaBar = panel.Find("ManaBar").GetComponent<Bar>();
        }
    }
}