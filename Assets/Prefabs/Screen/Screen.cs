using UnityEngine;

namespace Prefabs.Screen
{
    public class Screen : MonoBehaviour, IInterractable
    {
        public bool canBeInterracted;

        public bool isOn;

        public Material onMaterial;

        public Material offMaterial;

        private Light _light;

        private MeshRenderer _screenMesh;

        // Start is called before the first frame update
        private void Start()
        {
            UpdateState(isOn);

            _light = transform.Find("Light").GetComponent<Light>();
            _screenMesh = transform.Find("Screen").GetComponent<MeshRenderer>();
        }

        public void Interract(Player.Scripts.Player player)
        {
            if (CanInterract(player))
            {
                isOn = !isOn;

                UpdateState(isOn);
            }
        }

        public void Uninterract(Player.Scripts.Player player)
        {
        }

        public bool CanInterract(Player.Scripts.Player player)
        {
            return canBeInterracted;
        }

        private void UpdateState(bool value)
        {
            if (value)
            {
                TurnOn();
            }
            else
            {
                TurnOff();
            }
        }

        private void TurnOff()
        {
            if (_light && _screenMesh)
            {
                _light.intensity = 0;
                _screenMesh.material = offMaterial;
            }
        }

        private void TurnOn()
        {
            if (_light && _screenMesh)
            {
                _light.intensity = 3;
                _screenMesh.material = onMaterial;
            }
        }
    }
}