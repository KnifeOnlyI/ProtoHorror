using UnityEngine;
using Random = System.Random;

namespace Prefabs.Desk
{
    [ExecuteAlways]
    public class Desk : MonoBehaviour
    {
        public GameObject computerModel;

        public bool random = true;
        public bool computer1;
        public bool computer2;
        public bool computer3;

        private readonly Vector3 _computer1Pos = new(-0.35f, 1.55f, 0);
        private readonly Vector3 _computer1Rot = new(0, 180f, 0);

        private readonly Vector3 _computer2Pos = new(-0.05f, 1.55f, 1.172f);
        private readonly Vector3 _computer2Rot = new(0, -150, 0);

        private readonly Vector3 _computer3Pos = new(-0.05f, 1.55f, -1.172f);
        private readonly Vector3 _computer3Rot = new(0, -210, 0);
        private bool _previousComputer1;
        private bool _previousComputer2;
        private bool _previousComputer3;

        private bool _previousRandom;

        private void Start()
        {
            if (Application.isEditor)
            {
                OnEditorStart();
            }
        }

        private void Update()
        {
            if (Application.isEditor)
            {
                OnEditorUpdate();
            }
        }

        private void OnEditorStart()
        {
            _previousRandom = random;

            if (!random)
            {
                return;
            }

            SetRandomComputers();
        }

        private void OnEditorUpdate()
        {
            // If random mode just changed
            if (random != _previousRandom)
            {
                _previousRandom = random;

                // If random mode just activated
                if (random)
                {
                    SetRandomComputers();
                }
            }

            // If the computer 1 state just changed 
            if (computer1 != _previousComputer1)
            {
                UpdateComputer1();

                random = false;
            }

            // If the computer 2 state just changed 
            if (computer2 != _previousComputer2)
            {
                UpdateComputer2();

                random = false;
            }

            // If the computer 3 state just changed 
            if (computer3 != _previousComputer3)
            {
                UpdateComputer3();

                random = false;
            }
        }

        private void UpdateComputer1()
        {
            _previousComputer1 = computer1;

            if (computer1)
            {
                CreateComputer(1, _computer1Pos, _computer1Rot);
            }
            else
            {
                DestroyComputer(1);
            }
        }

        private void UpdateComputer2()
        {
            _previousComputer2 = computer2;

            if (computer2)
            {
                CreateComputer(2, _computer2Pos, _computer2Rot);
            }
            else
            {
                DestroyComputer(2);
            }
        }

        private void UpdateComputer3()
        {
            _previousComputer3 = computer3;

            if (computer3)
            {
                CreateComputer(3, _computer3Pos, _computer3Rot);
            }
            else
            {
                DestroyComputer(3);
            }
        }

        private void UpdateAllComputers()
        {
            UpdateComputer1();
            UpdateComputer2();
            UpdateComputer3();
        }

        private void SetRandomComputers()
        {
            var nb = new Random().Next(1, 4);

            switch (nb)
            {
                case 1:
                    computer1 = true;
                    computer2 = false;
                    computer3 = false;
                    break;
                case 2:
                    computer1 = true;
                    computer2 = true;
                    computer3 = false;
                    break;
                case 3:
                    computer1 = true;
                    computer2 = true;
                    computer3 = true;
                    break;
            }

            UpdateAllComputers();

            _previousComputer1 = computer1;
            _previousComputer2 = computer2;
            _previousComputer3 = computer3;
        }

        private void CreateComputer(int number, Vector3 pos, Vector3 rot)
        {
            if (ComputerExists(number))
            {
                return;
            }

            var computer = Instantiate(computerModel, gameObject.transform, true);

            computer.name = GetComputerName(number);

            computer.transform.localPosition = pos;
            computer.transform.localRotation = Quaternion.Euler(rot);
        }

        private void DestroyComputer(int number)
        {
            var computer = GetComputer(number);

            if (computer != null)
            {
                DestroyImmediate(computer.gameObject);
            }
        }

        private Transform GetComputer(int number)
        {
            return transform.Find(GetComputerName(number));
        }

        private bool ComputerExists(int number)
        {
            return GetComputer(number) != null;
        }

        private static string GetComputerName(int number)
        {
            return $"Computer{number}";
        }
    }
}