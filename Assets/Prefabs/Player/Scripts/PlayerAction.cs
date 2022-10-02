using Settings;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace Prefabs.Player.Scripts
{
    /// <summary>
    /// Represent the player actions components
    /// </summary>
    public class PlayerAction : MonoBehaviour
    {
        /// <summary>
        /// The flag to indicated if the player can interract or not
        /// </summary>
        public bool canInterract = true;

        /// <summary>
        /// The maximum range for interractions
        /// </summary>
        public float interractRange = 3.0f;

        /// <summary>
        /// The camera 
        /// </summary>
        private Camera _camera;

        /// <summary>
        /// The flag to indicated if a target for interractions is available or not
        /// </summary>
        private bool _hasInterractTarget;

        /// <summary>
        /// The flag to indicates if the player is in interraction
        /// </summary>
        private bool _inInterraction;

        /// <summary>
        /// The raycast for interractions
        /// </summary>
        private Ray _interractRay;

        /// <summary>
        /// The target for interraction
        /// </summary>
        private IInterractable _interractTarget;

        /// <summary>
        /// The player component
        /// </summary>
        private Player _player;

        /// <summary>
        /// The previous target for interraction
        /// </summary>
        private IInterractable _previousInterractTarget;

        /// <summary>
        /// The transform in the Behavior base class
        /// </summary>
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;

            var body = _transform.Find("Body");

            _player = GetComponent<Player>();
            _camera = body.Find("Camera").GetComponent<Camera>();
        }

        private void Update()
        {
            UpdateInterractableTarget();
        }

        /// <summary>
        /// Update the interractable target
        /// </summary>
        private void UpdateInterractableTarget()
        {
            _previousInterractTarget = _interractTarget;
            _interractTarget = null;
            _player.SetCursorType(CursorTypes.Base);

            if (canInterract)
            {
                _interractRay = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

                var hit = RaycastUtils.Cast(_interractRay, interractRange);

                if (hit != null)
                {
                    var hitTransform = hit.Value.transform;

                    if (hitTransform.tag.Contains(Tags.Interractable))
                    {
                        ManageInterractableHit(hitTransform);
                    }
                }
            }

            if (_previousInterractTarget != null && _interractTarget == null && _inInterraction)
            {
                _previousInterractTarget.Uninterract(_player);
                _inInterraction = false;
            }
        }

        /// <summary>
        /// Manage the specified interractable hit
        /// </summary>
        /// <param name="hit">The interractable hit to manage</param>
        private void ManageInterractableHit(Component hit)
        {
            var interractable = hit.GetComponent<IInterractable>();

            if (interractable != null)
            {
                if (!interractable.CanInterract(_player))
                {
                    _player.SetCursorType(CursorTypes.Forbidden);
                }
                else
                {
                    _player.SetCursorType(CursorTypes.Interract);
                    _interractTarget = interractable;
                }
            }
        }

        /// <summary>
        /// When OnInterract input detected
        /// </summary>
        // ReSharper disable once UnusedMember.Local
        private void OnInterract(InputValue value)
        {
            switch (value.isPressed)
            {
                case true when !_inInterraction && _interractTarget != null:
                    _inInterraction = true;
                    _interractTarget.Interract(_player);
                    break;
                case false when _inInterraction:
                    _inInterraction = false;
                    _interractTarget?.Uninterract(_player);
                    break;
            }
        }

        private void OnSave()
        {
            SaveUtils.SavePlayer(_player);
        }

        private void OnLoad()
        {
            var data = SaveUtils.LoadPlayer();

            _player.SetMaxLife(data.maxLife, false);
            _player.SetMaxStamina(data.maxStamina, false);
            _player.SetMaxMana(data.maxMana, false);

            _player.SetLife(data.life);
            _player.SetStamina(data.stamina);
            _player.SetMana(data.mana);

            _player.SetPosition(SerializationUtils.UnserializeVector3(data.position));
        }
    }
}