namespace Prefabs.Player.Scripts
{
    /// <summary>
    /// Represent a timer to manage player stamina
    /// </summary>
    public class PlayerStaminaTimer : Timer
    {
        /// <summary>
        /// The gained stamina per seconds
        /// </summary>
        private readonly int _gainedStaminaPerSeconds;

        /// <summary>
        /// The loosed stamina per seconds
        /// </summary>
        private readonly int _loosedStaminaPerSeconds;

        /// <summary>
        /// The player
        /// </summary>
        private readonly Player _player;

        private bool _previouslyRunning;

        /// <summary>
        /// Create a new player stamina timer
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="endTime">The end time</param>
        /// <param name="loosedStaminaPerSeconds">The loosed stamina per seconds</param>
        /// <param name="gainedStaminaPerSeconds">The gained stamina per seconds</param>
        public PlayerStaminaTimer(
            Player player,
            float endTime,
            int loosedStaminaPerSeconds,
            int gainedStaminaPerSeconds
        ) : base(endTime)
        {
            _player = player;
            _loosedStaminaPerSeconds = loosedStaminaPerSeconds;
            _gainedStaminaPerSeconds = gainedStaminaPerSeconds;
        }

        protected override void OnEnd()
        {
            if (_player.InMovement() && _player.IsRunning())
            {
                _previouslyRunning = true;

                _player.LooseStamina(_loosedStaminaPerSeconds);

                if (!_player.HasStamina())
                {
                    Pause(1);
                }
            }
            else if (_previouslyRunning)
            {
                _previouslyRunning = false;
                Pause(0.25f);
            }
            else if (_player.IsGrounded())
            {
                _player.GainStamina(_gainedStaminaPerSeconds);
            }
        }
    }
}