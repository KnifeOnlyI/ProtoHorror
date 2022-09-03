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
        private RaycastHit _interractTarget;

        /// <summary>
        /// The transform in the Behavior base class
        /// </summary>
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;

            var body = _transform.Find("Body");

            _camera = body.Find("Camera").GetComponent<Camera>();
        }

        private void Update()
        {
            _interractRay = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        }

        /// <summary>
        /// When OnInterract input detected
        /// </summary>
        // ReSharper disable once UnusedMember.Local
        private void OnInterract()
        {
            if (canInterract)
            {
                var hit = RaycastUtils.Cast(_interractRay, interractRange);

                if (hit != null)
                {
                    Debug.Log("Interract with object");
                }
            }
        }
    }
}