using Prefabs.Player.HUD.Scripts;
using UnityEngine;

namespace Prefabs.Player.Scripts
{
    /// <summary>
    /// Represent the player
    /// </summary>
    public class Player : MonoBehaviour
    {
        /// <summary>
        /// The initial life
        /// </summary>
        public int initialLife;

        /// <summary>
        /// The initial stamina
        /// </summary>
        public int initialStamina;

        /// <summary>
        /// The initial mana
        /// </summary>
        public int initialMana;

        /// <summary>
        /// The HUD game object
        /// </summary>
        private PlayerHUD _hud;

        /// <summary>
        /// The component to manage movements
        /// </summary>
        private PlayerMovement _movement;

        /// <summary>
        /// Called before the first frame update
        /// </summary>
        private void Awake()
        {
            var t = transform;

            _hud = t.Find("HUD").GetComponent<PlayerHUD>();
            _movement = GetComponent<PlayerMovement>();
        }

        /// <summary>
        /// Called before the first update
        /// </summary>
        private void Start()
        {
            _hud.LifeBar.SetMaxValue(initialLife, true);
            _hud.StaminaBar.SetMaxValue(initialStamina, true);
            _hud.ManaBar.SetMaxValue(initialMana, true);
        }

        /// <summary>
        /// Check if has stamina
        /// </summary>
        /// <returns>TRUE if has stamina, FALSE otherwise</returns>
        public bool HasStamina()
        {
            return !_hud.StaminaBar.IsEmpty();
        }

        /// <summary>
        /// Gain the specified quantity of stamina
        /// </summary>
        /// <param name="value">The stamina to gain</param>
        public void GainStamina(int value)
        {
            _hud.StaminaBar.AddCurrentValue(value);
        }

        /// <summary>
        /// Loose the specified quantity of stamina
        /// </summary>
        /// <param name="value">The stamina to loose</param>
        public void LooseStamina(int value)
        {
            _hud.StaminaBar.SubtractCurrentValue(value);
        }

        /// <summary>
        /// Check if is on the ground
        /// </summary>
        /// <returns>TRUE if is grounded, FALSE otherwise</returns>
        public bool IsGrounded()
        {
            return _movement.IsGrounded();
        }

        /// <summary>
        /// Check if is in horizontal movement
        /// </summary>
        /// <returns>TRUE if the player is horizontal movement, FALSE otherwise</returns>
        public bool InMovement()
        {
            return _movement.InMovement();
        }

        /// <summary>
        /// Check if is running
        /// </summary>
        /// <returns>TRUE if the player is running FALSE otherwise</returns>
        public bool IsRunning()
        {
            return _movement.IsRunning();
        }
    }
}