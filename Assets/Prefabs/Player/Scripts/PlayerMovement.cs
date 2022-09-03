using UnityEngine;
using UnityEngine.InputSystem;

namespace Prefabs.Player.Scripts
{
    /// <summary>
    /// Represent the player movements components
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        /// <summary>
        /// The mouse sensitivity
        /// </summary>
        public float mouseSensitivity = 30.0f;

        /// <summary>
        /// The walk speed
        /// </summary>
        public float walkSpeed = 1.5f;

        /// <summary>
        /// The run speed
        /// </summary>
        public float runSpeed = 5.0f;

        /// <summary>
        /// The flag to indicated if the player can run or not
        /// </summary>
        public bool canRun = true;

        /// <summary>
        /// The flag to indicated if the player can jump or not
        /// </summary>
        public bool canJump = true;

        /// <summary>
        /// The flag to indicated if the player can crouch or not
        /// </summary>
        public bool canCrouch = true;

        /// <summary>
        /// The jump height
        /// </summary>
        public float jumpHeight = 1.0f;

        /// <summary>
        /// The gravity
        /// </summary>
        public float gravity = -9.81f;

        /// <summary>
        /// The loosed stamina per seconds
        /// </summary>
        public int loosedStaminaPerSeconds = 4;

        /// <summary>
        /// The gained stamina per seconds
        /// </summary>
        public int gainedStaminaPerSeconds = 16;

        /// <summary>
        /// The loosed stamina for a jump
        /// </summary>
        public int loosedStaminaForJump = 10;

        /// <summary>
        /// The minimum height (in meters) to fall to loose life
        /// </summary>
        public int minFallingHeightToLooseLife = 5;

        /// <summary>
        /// The life to loose for every falling meters
        /// </summary>
        public int looseLifePerMetersFall = 20;

        /// <summary>
        /// The minimal distance for ground check
        /// </summary>
        public float groundDistance = 0.1f;

        /// <summary>
        /// The mask for grounds
        /// </summary>
        public LayerMask groundMask;

        /// <summary>
        /// The camera 
        /// </summary>
        private Camera _camera;

        /// <summary>
        /// The character controller to manage player movements
        /// </summary>
        private CharacterController _controller;

        /// <summary>
        /// The ground checker transform
        /// </summary>
        private Transform _groundChecker;

        /// <summary>
        /// The flag to indicated if the player is crouch
        /// </summary>
        private bool _isCrouch;

        /// <summary>
        /// The flag to indicates if the player is on the ground
        /// </summary>
        private bool _isGrounded;

        /// <summary>
        /// The flag to indicated if the move backward button is pressed
        /// </summary>
        private bool _isMoveBackwardPressed;

        /// <summary>
        /// The flag to indicated if the move forward button is pressed
        /// </summary>
        private bool _isMoveForwardPressed;

        /// <summary>
        /// The flag to indicated if the move left button is pressed
        /// </summary>
        private bool _isMoveLeftPressed;

        /// <summary>
        /// The flag to indicated if the move right button is pressed
        /// </summary>
        private bool _isMoveRightPressed;

        /// <summary>
        /// The flag to indicated if the player is running
        /// </summary>
        private bool _isRunning;

        /// <summary>
        /// The look right and left value
        /// </summary>
        private float _lookLeftRightValue;

        /// <summary>
        /// The look right and left value
        /// </summary>
        private float _lookUpDownValue;

        /// <summary>
        /// The horizontal movement
        /// </summary>
        private Vector3 _movement;

        /// <summary>
        /// The player component
        /// </summary>
        private Player _player;

        /// <summary>
        /// The height when the player last touched the ground
        /// </summary>
        private float _previousGroundedHeight;

        /// <summary>
        /// The current movement speed
        /// </summary>
        private float _speed;

        /// <summary>
        /// The timer to manage stamina
        /// </summary>
        private PlayerStaminaTimer _staminaTimer;

        /// <summary>
        /// The transform in the Behavior base class
        /// </summary>
        private Transform _transform;

        /// <summary>
        /// The up/down rotation value
        /// </summary>
        private float _upDownRotation;

        /// <summary>
        /// The velocity
        /// </summary>
        private Vector3 _velocity;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;

            _transform = transform;

            var body = _transform.Find("Body");

            _player = GetComponent<Player>();
            _controller = GetComponent<CharacterController>();
            _camera = body.Find("Camera").GetComponent<Camera>();
            _groundChecker = body.Find("GroundChecker");

            _speed = walkSpeed;

            _previousGroundedHeight = _transform.position.y;
            _staminaTimer = new PlayerStaminaTimer(_player, 0.1f, loosedStaminaPerSeconds, gainedStaminaPerSeconds);
        }

        private void Update()
        {
            CheckGrounded();
            RotateLeftRight();
            RotateUpDown();
            Move();
            Fall();
            UpdateTimers();
        }

        /// <summary>
        /// Update all timers
        /// </summary>
        private void UpdateTimers()
        {
            _staminaTimer.Update();
        }

        /// <summary>
        /// Manage the rotation on left and right directions
        /// </summary>
        private void RotateLeftRight()
        {
            _transform.Rotate(Vector3.up * (_lookLeftRightValue * mouseSensitivity * Time.deltaTime));
        }

        /// <summary>
        /// Manage the rotation on up and down directions
        /// </summary>
        private void RotateUpDown()
        {
            _upDownRotation -= _lookUpDownValue * mouseSensitivity * Time.deltaTime;
            _upDownRotation = Mathf.Clamp(_upDownRotation, -90.0f, 90.0f);

            _camera.transform.localRotation = Quaternion.Euler(_upDownRotation, 0.0f, 0.0f);
        }

        /// <summary>
        /// Manage the movements
        /// </summary>
        private void Move()
        {
            var moveForwardBackwardValue = 0.0f;
            var moveLeftRightValue = 0.0f;

            if (_isMoveForwardPressed)
            {
                moveForwardBackwardValue = 1;
            }
            else if (_isMoveBackwardPressed)
            {
                moveForwardBackwardValue = -1;
            }

            if (_isMoveLeftPressed)
            {
                moveLeftRightValue = -1;
            }
            else if (_isMoveRightPressed)
            {
                moveLeftRightValue = 1;
            }

            var forwardBackwardMovement = _transform.forward * moveForwardBackwardValue;
            var leftRightMovement = _transform.right * moveLeftRightValue;

            _isRunning = _isRunning && _player.HasStamina() && _staminaTimer.IsPlaying();
            _speed = _isRunning ? runSpeed : walkSpeed;

            _movement = (forwardBackwardMovement + leftRightMovement).normalized * (Time.deltaTime * _speed);

            _controller.Move(_movement);
        }

        /// <summary>
        /// Check if the player is on the ground
        /// </summary>
        private void CheckGrounded()
        {
            var isGrounded = Physics.CheckSphere(_groundChecker.position, groundDistance, groundMask);

            // The player just touch the ground, calculate the damages with his previous height
            if (!_isGrounded && isGrounded)
            {
                var newHeight = _transform.position.y;

                var fallingHeight = _previousGroundedHeight - newHeight;

                if (fallingHeight > minFallingHeightToLooseLife)
                {
                    _player.LooseLife((int) (fallingHeight * looseLifePerMetersFall));
                }
            }

            _isGrounded = isGrounded;

            // The player just untouch the ground, save his height in memory to calculate the damages later
            if (_isGrounded)
            {
                _previousGroundedHeight = _transform.position.y;
            }

            if (_isGrounded && _velocity.y < 0.0f)
            {
                _velocity.y = -2.0f;
            }
        }

        /// <summary>
        /// Manage the falling movement
        /// </summary>
        private void Fall()
        {
            _velocity.y += gravity * Time.deltaTime;

            _controller.Move(_velocity * Time.deltaTime);
        }

        /// <summary>
        /// When OnRun input detected
        /// </summary>
        /// <param name="value">The input value</param>
        // ReSharper disable once UnusedMember.Local
        private void OnRun(InputValue value)
        {
            if (value.isPressed && canRun)
            {
                _isRunning = true;
            }
            else
            {
                _isRunning = false;
            }
        }

        /// <summary>
        /// When OnCrouch input detected
        /// </summary>
        /// <param name="value">The input value</param>
        // ReSharper disable once UnusedMember.Local
        private void OnCrouch(InputValue value)
        {
            if (canCrouch)
            {
                _isCrouch = value.isPressed;

                Debug.Log($"Is crouch : {_isCrouch}");
            }
        }

        /// <summary>
        /// When OnJump input detected
        /// </summary>
        /// <param name="value">The input value</param>
        // ReSharper disable once UnusedMember.Local
        private void OnJump(InputValue value)
        {
            if (canJump && value.isPressed && _isGrounded)
            {
                _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

                _player.LooseStamina(loosedStaminaForJump);
            }
        }

        /// <summary>
        /// When OnMoveForward input detected
        /// </summary>
        /// <param name="value">The input value</param>
        // ReSharper disable once UnusedMember.Local
        private void OnMoveForward(InputValue value)
        {
            _isMoveForwardPressed = value.isPressed;
        }

        /// <summary>
        /// When OnMoveBackward input detected
        /// </summary>
        /// <param name="value">The input value</param>
        // ReSharper disable once UnusedMember.Local
        private void OnMoveBackward(InputValue value)
        {
            _isMoveBackwardPressed = value.isPressed;
        }

        /// <summary>
        /// When OnMoveLeft input detected
        /// </summary>
        /// <param name="value">The input value</param>
        // ReSharper disable once UnusedMember.Local
        private void OnMoveLeft(InputValue value)
        {
            _isMoveLeftPressed = value.isPressed;
        }

        /// <summary>
        /// When OnMoveRight input detected
        /// </summary>
        /// <param name="value">The input value</param>
        // ReSharper disable once UnusedMember.Local
        private void OnMoveRight(InputValue value)
        {
            _isMoveRightPressed = value.isPressed;
        }

        /// <summary>
        /// When OnLookVertical input detected
        /// </summary>
        /// <param name="value">The input value</param>
        // ReSharper disable once UnusedMember.Local
        private void OnLookVertical(InputValue value)
        {
            _lookUpDownValue = value.Get<float>();
        }

        /// <summary>
        /// When OnLookHorizontal input detected
        /// </summary>
        /// <param name="value">The input value</param>
        // ReSharper disable once UnusedMember.Local
        private void OnLookHorizontal(InputValue value)
        {
            _lookLeftRightValue = value.Get<float>();
        }

        /// <summary>
        /// Get the flag to indicate if is grounded
        /// </summary>
        /// <returns>TRUE if is grounded, FALSE otherwise</returns>
        public bool IsGrounded()
        {
            return _isGrounded;
        }

        /// <summary>
        /// Check if is in horizontal movement
        /// </summary>
        /// <returns>TRUE if the player is horizontal movement, FALSE otherwise</returns>
        public bool InMovement()
        {
            return _movement.magnitude > 0;
        }

        /// <summary>
        /// Check if is running
        /// </summary>
        /// <returns>TRUE if the player is running FALSE otherwise</returns>
        public bool IsRunning()
        {
            return _isRunning;
        }
    }
}