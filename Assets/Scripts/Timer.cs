using UnityEngine;

/// <summary>
/// Represent a timer
/// </summary>
public abstract class Timer
{
    /// <summary>
    /// The end time
    /// </summary>
    private readonly float _endTime;

    /// <summary>
    /// The flag to indicate if the timer must be played in loop
    /// </summary>
    private readonly bool _loop;

    /// <summary>
    /// The flag to indicate if the timer is in play
    /// </summary>
    private bool _inPlay;

    /// <summary>
    /// The pause end time
    /// </summary>
    private float _pauseEndTime;

    /// <summary>
    /// The current pause time
    /// </summary>
    private float _pauseTime;

    /// <summary>
    /// The current time
    /// </summary>
    private float _time;

    /// <summary>
    /// Create a new timer
    /// </summary>
    /// <param name="endTime">The end time</param>
    /// <param name="loop">The flag to indicate if the timer must be played in loop</param>
    /// <param name="autostart">The flag to indicate if the timer must be started immediatly</param>
    public Timer(float endTime, bool loop = true, bool autostart = true)
    {
        _endTime = endTime;
        _loop = loop;

        if (autostart)
        {
            Start();
        }
    }

    /// <summary>
    /// Start the timer at the previous time
    /// </summary>
    public void Start()
    {
        _inPlay = true;
        _pauseTime = 0;
        _pauseEndTime = 0;

        OnStart();
    }

    /// <summary>
    /// Pause the timer (do not change the time)
    /// <param name="time">The time to pause</param>
    /// </summary>
    public void Pause(float time = 0)
    {
        _inPlay = false;
        _pauseTime = 0;
        _pauseEndTime = time;

        OnPause();
    }

    /// <summary>
    /// Stop the timer (set the time to 0)
    /// </summary>
    public void Stop()
    {
        _inPlay = false;
        _time = 0;

        OnStop();
    }

    /// <summary>
    /// Reset the timer (set the time to 0 but keep playing if in play)
    /// </summary>
    public void Reset()
    {
        _time = 0;

        OnReset();
    }

    /// <summary>
    /// Update the timer state
    /// </summary>
    public void Update()
    {
        if (_inPlay)
        {
            _time += Time.deltaTime;

            if (_time >= _endTime)
            {
                _time = 0;

                if (!_loop)
                {
                    _inPlay = false;
                }

                OnEnd();
            }

            OnUpdatePlaying();
        }
        else
        {
            if (_pauseEndTime > 0)
            {
                _pauseTime += Time.deltaTime;

                if (_pauseTime > _pauseEndTime)
                {
                    OnEndTimedPause();
                    Start();
                }

                OnUpdateTimePause();
            }
            else
            {
                OnUpdateNotPlaying();
            }
        }

        OnUpdate();
    }

    /// <summary>
    /// Get the timer time
    /// </summary>
    /// <returns>The timer time</returns>
    public float GetTime()
    {
        return _time;
    }

    /// <summary>
    /// Get the timer end time
    /// </summary>
    /// <returns>The timer end time</returns>
    public float EndTime()
    {
        return _endTime;
    }

    /// <summary>
    /// Get the flag to indicate if the timer is in play
    /// </summary>
    /// <returns>The flag to indicate if the timer is in play</returns>
    public bool IsPlaying()
    {
        return _inPlay;
    }

    /// <summary>
    /// Get the flag to indicate if the timer must be played in loop
    /// </summary>
    /// <returns>The flag to indicate if the timer must be played in loop</returns>
    public bool IsLoop()
    {
        return _loop;
    }

    /// <summary>
    /// The event called on timer start
    /// </summary>
    protected virtual void OnStart()
    {
    }

    /// <summary>
    /// The event called on timer pause
    /// </summary>
    protected virtual void OnPause()
    {
    }

    /// <summary>
    /// The event called on timer stop
    /// </summary>
    protected virtual void OnStop()
    {
    }

    /// <summary>
    /// The event called on timer rest
    /// </summary>
    protected virtual void OnReset()
    {
    }

    /// <summary>
    /// The event called on timer end
    /// </summary>
    protected virtual void OnEnd()
    {
    }

    /// <summary>
    /// The event called on timer end of time pause
    /// </summary>
    protected virtual void OnEndTimedPause()
    {
    }

    /// <summary>
    /// The event called on timer update (when the timer is in play or not)
    /// </summary>
    protected virtual void OnUpdate()
    {
    }

    /// <summary>
    /// The event called on timer update (when the timer is in play)
    /// </summary>
    protected virtual void OnUpdatePlaying()
    {
    }

    /// <summary>
    /// The event called on timer update (when the timer is not in play)
    /// </summary>
    protected virtual void OnUpdateNotPlaying()
    {
    }

    /// <summary>
    /// The event called on timer update (when the timer is on time pause)
    /// </summary>
    protected virtual void OnUpdateTimePause()
    {
    }
}