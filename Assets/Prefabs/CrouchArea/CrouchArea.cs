using Settings;
using UnityEngine;

namespace Prefabs.CrouchArea
{
    /// <summary>
    /// Represent an area where the player must be crouched (can't uncrouch while is in this area)
    /// </summary>
    public class CrouchArea : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag.Contains(Tags.Player))
            {
                var player = other.transform.GetComponent<Player.Scripts.Player>();

                player.SetCanUncrouch(false);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.transform.tag.Contains(Tags.Player))
            {
                var player = other.transform.GetComponent<Player.Scripts.Player>();

                player.SetCanUncrouch(true);

                if (!player.IsCrouchPressed())
                {
                    player.Uncrouch();
                }
            }
        }
    }
}