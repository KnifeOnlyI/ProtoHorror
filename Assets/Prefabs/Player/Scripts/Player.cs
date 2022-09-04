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

        private Vector3? _newPosition;

        private Transform _transform;

        /// <summary>
        /// Called before the first frame update
        /// </summary>
        private void Awake()
        {
            _transform = transform;

            _hud = _transform.Find("HUD").GetComponent<PlayerHUD>();
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

        private void Update()
        {
            if (_newPosition.HasValue)
            {
                Debug.Log("Set new position : " + _newPosition.Value.x + " " + _newPosition.Value.y + " " +
                          _newPosition.Value.z);

                _transform.position = _newPosition.Value;
                _newPosition = null;
            }
        }

        /// <summary>
        /// Set the cursor type
        /// </summary>
        /// <param name="cursorType">The type of cursor to set</param>
        public void SetCursorType(CursorTypes cursorType)
        {
            _hud.SetCursorType(cursorType);
        }

        /// <summary>
        /// Check if has full life
        /// </summary>
        /// <returns>TRUE if has full life, FALSE otherwise</returns>
        public bool HasFullLife()
        {
            return _hud.LifeBar.IsFull();
        }

        /// <summary>
        /// Gain the specified quantity of life
        /// </summary>
        /// <param name="value">The life to gain</param>
        public void GainLife(int value)
        {
            _hud.LifeBar.AddCurrentValue(value);
        }

        /// <summary>
        /// Loose the specified quantity of life
        /// </summary>
        /// <param name="value">The life to loose</param>
        public void LooseLife(int value)
        {
            _hud.LifeBar.SubtractCurrentValue(value);
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

        public int GetLife()
        {
            return _hud.LifeBar.GetCurrentValue();
        }

        public void SetMaxLife(int value, bool fill)
        {
            _hud.LifeBar.SetMaxValue(value, fill);
        }

        public void SetLife(int value)
        {
            _hud.LifeBar.SetCurrentValue(value);
        }

        public int GetStamina()
        {
            return _hud.StaminaBar.GetCurrentValue();
        }

        public void SetStamina(int value)
        {
            _hud.StaminaBar.SetCurrentValue(value);
        }

        public void SetMaxStamina(int value, bool fill)
        {
            _hud.StaminaBar.SetMaxValue(value, fill);
        }

        public int GetMana()
        {
            return _hud.ManaBar.GetCurrentValue();
        }

        public void SetMana(int value)
        {
            _hud.ManaBar.SetCurrentValue(value);
        }

        public void SetMaxMana(int value, bool fill)
        {
            _hud.ManaBar.SetMaxValue(value, fill);
        }

        public Vector3 GetPosition()
        {
            return _transform.position;
        }

        public void SetPosition(Vector3 value)
        {
            _newPosition = value;
        }
    }
}