using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace Prefabs.Player.HUD.Scripts
{
    /// <summary>
    /// Represent the player HUD
    /// </summary>
    public class PlayerHUD : MonoBehaviour
    {
        /// <summary>
        /// The base cursor
        /// </summary>
        private Image _baseCursor;

        /// <summary>
        /// The forbidden cursor
        /// </summary>
        private Image _forbiddenCursor;

        /// <summary>
        /// The interract cursor
        /// </summary>
        private Image _interractCursor;

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
            var panel = transform.Find("Panel");

            LifeBar = panel.Find("LifeBar").GetComponent<Bar>();
            StaminaBar = panel.Find("StaminaBar").GetComponent<Bar>();
            ManaBar = panel.Find("ManaBar").GetComponent<Bar>();
            _baseCursor = panel.Find("BaseCursor").GetComponent<Image>();
            _interractCursor = panel.Find("InterractCursor").GetComponent<Image>();
            _forbiddenCursor = panel.Find("ForbiddenCursor").GetComponent<Image>();

            SetCursorType(CursorTypes.Base);
        }

        /// <summary>
        /// Set the cursor type
        /// </summary>
        /// <param name="cursorType">The type of cursor to set</param>
        /// <exception cref="InvalidEnumArgumentException">If a not manage cursor type is specified</exception>
        public void SetCursorType(CursorTypes cursorType)
        {
            HideAllCursors();

            switch (cursorType)
            {
                case CursorTypes.None:
                    break;
                case CursorTypes.Base:
                    _baseCursor.enabled = true;
                    break;
                case CursorTypes.Interract:
                    _interractCursor.enabled = true;
                    break;
                case CursorTypes.Forbidden:
                    _forbiddenCursor.enabled = true;
                    break;
                default:
                    throw new InvalidEnumArgumentException($"Not managed cursor type : {cursorType}");
            }
        }

        /// <summary>
        /// Hide all cursors
        /// </summary>
        private void HideAllCursors()
        {
            _baseCursor.enabled = false;
            _interractCursor.enabled = false;
            _forbiddenCursor.enabled = false;
        }
    }
}