using Settings;
using UnityEngine;
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
        public float interractRange = 2.0f;

        /// <summary>
        /// The camera 
        /// </summary>
        private Camera _camera;

        /// <summary>
        /// The flag to indicated if a target for interractions is available or not
        /// </summary>
        private bool _hasInterractTarget;

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
        private void OnInterract()
        {
            _interractTarget?.Interract(_player);
        }
    }
}