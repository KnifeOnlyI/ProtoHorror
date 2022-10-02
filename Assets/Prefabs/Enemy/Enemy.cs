using UnityEngine;

namespace Prefabs.Enemy
{
    [ExecuteAlways]
    public class Enemy : MonoBehaviour
    {
        public Transform lookTarget;

        public bool updateInEditor;

        public bool executeManualUpdate;

        private Vector3 _previousPosition;
        private Vector3 _previousTargetPosition;

        private void Update()
        {
            if (!transform || !updateInEditor && !executeManualUpdate)
            {
                return;
            }

            if (executeManualUpdate ||
                _previousTargetPosition != lookTarget.position ||
                _previousPosition != transform.position)
            {
                transform.LookAt(lookTarget);

                _previousTargetPosition = lookTarget.position;
            }

            executeManualUpdate = false;
            _previousPosition = transform.position;
        }
    }
}