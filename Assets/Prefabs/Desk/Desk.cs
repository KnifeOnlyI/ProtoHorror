using UnityEngine;
using Utils;

namespace Prefabs.Desk
{
    [ExecuteAlways]
    public class Desk : MonoBehaviour
    {
        [Range(0, 3)] public int nbComputers;
        public bool mouse;
        public bool keyboard;

        private GameObject _computerScreen1;
        private GameObject _computerScreen2;
        private GameObject _computerScreen3;
        private GameObject _computerScreen4;
        private GameObject _computerScreen5;
        private GameObject _keyboard;
        private GameObject _mouse;
        private bool _previousKeyboard;
        private bool _previousMouse;

        private int _previousNbComputers;

        private void Awake()
        {
            _computerScreen1 = transform.Find("ComputerScreen1").gameObject;
            _computerScreen2 = transform.Find("ComputerScreen2").gameObject;
            _computerScreen3 = transform.Find("ComputerScreen3").gameObject;
            _computerScreen4 = transform.Find("ComputerScreen4").gameObject;
            _computerScreen5 = transform.Find("ComputerScreen5").gameObject;
            _mouse = transform.Find("Mouse").gameObject;
            _keyboard = transform.Find("Keyboard").gameObject;

            if (Application.isPlaying)
            {
                OnPlayStart();
            }
            else if (Application.isEditor)
            {
                ManageGameObjectsInEditor();
            }
        }

        private void Update()
        {
            if (Application.isEditor)
            {
                OnEditorUpdate();
            }
        }

        private void OnPlayStart()
        {
            // Destroy the not needed game objects
            switch (nbComputers)
            {
                case 0:
                    Destroy(_computerScreen1);
                    Destroy(_computerScreen2);
                    Destroy(_computerScreen3);
                    Destroy(_computerScreen4);
                    Destroy(_computerScreen5);
                    break;
                case 1:
                    GameObjectUtils.ShowGameObject(_computerScreen1);
                    Destroy(_computerScreen2);
                    Destroy(_computerScreen3);
                    Destroy(_computerScreen4);
                    Destroy(_computerScreen5);
                    break;
                case 2:
                    Destroy(_computerScreen1);
                    Destroy(_computerScreen2);
                    Destroy(_computerScreen3);
                    GameObjectUtils.ShowGameObject(_computerScreen4);
                    GameObjectUtils.ShowGameObject(_computerScreen5);
                    break;
                case 3:
                    GameObjectUtils.ShowGameObject(_computerScreen1);
                    GameObjectUtils.ShowGameObject(_computerScreen2);
                    GameObjectUtils.ShowGameObject(_computerScreen3);
                    Destroy(_computerScreen4);
                    Destroy(_computerScreen5);
                    break;
            }

            if (mouse)
            {
                GameObjectUtils.ShowGameObject(_mouse);
            }
            else
            {
                Destroy(_mouse);
            }

            if (keyboard)
            {
                GameObjectUtils.ShowGameObject(_keyboard);
            }
            else
            {
                Destroy(_keyboard);
            }
        }

        private void OnEditorUpdate()
        {
            // If the developer has just changed the desk configuration, update the game objects visibility
            if (nbComputers != _previousNbComputers || mouse != _previousMouse || keyboard != _previousKeyboard)
            {
                ManageGameObjectsInEditor();

                _previousNbComputers = nbComputers;
                _previousMouse = mouse;
                _previousKeyboard = keyboard;
            }
        }

        private void ManageGameObjectsInEditor()
        {
            switch (nbComputers)
            {
                case 0:
                    GameObjectUtils.HideGameObject(_computerScreen1);
                    GameObjectUtils.HideGameObject(_computerScreen2);
                    GameObjectUtils.HideGameObject(_computerScreen3);
                    GameObjectUtils.HideGameObject(_computerScreen4);
                    GameObjectUtils.HideGameObject(_computerScreen5);
                    break;
                case 1:
                    GameObjectUtils.ShowGameObject(_computerScreen1);
                    GameObjectUtils.HideGameObject(_computerScreen2);
                    GameObjectUtils.HideGameObject(_computerScreen3);
                    GameObjectUtils.HideGameObject(_computerScreen4);
                    GameObjectUtils.HideGameObject(_computerScreen5);
                    break;
                case 2:
                    GameObjectUtils.HideGameObject(_computerScreen1);
                    GameObjectUtils.HideGameObject(_computerScreen2);
                    GameObjectUtils.HideGameObject(_computerScreen3);
                    GameObjectUtils.ShowGameObject(_computerScreen4);
                    GameObjectUtils.ShowGameObject(_computerScreen5);
                    break;
                case 3:
                    GameObjectUtils.ShowGameObject(_computerScreen1);
                    GameObjectUtils.ShowGameObject(_computerScreen2);
                    GameObjectUtils.ShowGameObject(_computerScreen3);
                    GameObjectUtils.HideGameObject(_computerScreen4);
                    GameObjectUtils.HideGameObject(_computerScreen5);
                    break;
            }

            if (mouse)
            {
                GameObjectUtils.ShowGameObject(_mouse);
            }
            else
            {
                GameObjectUtils.HideGameObject(_mouse);
            }

            if (keyboard)
            {
                GameObjectUtils.ShowGameObject(_keyboard);
            }
            else
            {
                GameObjectUtils.HideGameObject(_keyboard);
            }
        }
    }
}