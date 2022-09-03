using UnityEngine;
using UnityEngine.UI;

namespace Prefabs.Player.HUD.Scripts
{
    /// <summary>
    /// Represent a bar
    /// </summary>
    public class Bar : MonoBehaviour
    {
        /// <summary>
        /// The maximum bar size in pixels
        /// </summary>
        private const int MaxBarSizePx = 1500;

        /// <summary>
        /// The higher bar max value
        /// </summary>
        private const int MaxBarValue = 3000;

        /// <summary>
        /// The current value
        /// </summary>
        private int _currentValue;


        /// <summary>
        /// The maximum value
        /// </summary>
        private int _maxValue;

        /// <summary>
        /// The slider
        /// </summary>
        private Slider _slider;

        /// <summary>
        /// The rect transform for slider
        /// </summary>
        private RectTransform _sliderRectTransform;

        /// <summary>
        /// Called before the first frame update
        /// </summary>
        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _sliderRectTransform = GetComponent<RectTransform>();
        }

        private void CheckMaxValue()
        {
            _maxValue = _maxValue switch
            {
                < 1 => 1,
                > MaxBarValue => MaxBarValue,
                _ => _maxValue
            };

            CheckCurrentValue();
        }

        /// <summary>
        /// Check the current value and update it if necessary
        /// </summary>
        private void CheckCurrentValue()
        {
            if (_currentValue > _maxValue)
            {
                _currentValue = _maxValue;

                _slider.value = 1;
            }
        }

        /// <summary>
        /// Check if the bar is empty
        /// </summary>
        /// <returns>TRUE if the bar is empty, FALSE otherwise</returns>
        public bool IsEmpty()
        {
            return _slider.value == 0;
        }

        /// <summary>
        /// Check if the bar is full
        /// </summary>
        /// <returns>TRUE if the bar is full, FALSE otherwise</returns>
        public bool IsFull()
        {
            return _slider.value >= 1.0;
        }

        /// <summary>
        /// Get the current value
        /// </summary>
        /// <returns>The current value</returns>
        public int GetCurrentValue()
        {
            return _currentValue;
        }

        /// <summary>
        /// Set the value
        /// </summary>
        /// <param name="value">The new value</param>
        public void SetCurrentValue(int value)
        {
            _currentValue = Mathf.Clamp(value, 0, _maxValue);

            _slider.value = Mathf.Ceil(Mathf.Clamp(_currentValue, 0, _maxValue)) / _maxValue;
        }

        public void AddCurrentValue(int qte)
        {
            SetCurrentValue(_currentValue + qte);
        }

        public void SubtractCurrentValue(int qte)
        {
            SetCurrentValue(_currentValue - qte);
        }

        /// <summary>
        /// Get the max value
        /// </summary>
        /// <returns>The max value</returns>
        public int GetMaxValue()
        {
            return _maxValue;
        }

        /// <summary>
        /// Set the max value
        /// </summary>
        /// <param name="value">The new max value</param>
        /// <param name="fill">TRUE to fill the bar, FALSE otherwise</param>
        public void SetMaxValue(int value, bool fill)
        {
            _maxValue = value;

            CheckMaxValue();

            var barSizePercent = Mathf.Clamp(value, 0, _maxValue) / (float) MaxBarValue;

            _sliderRectTransform.sizeDelta = new Vector2(
                MaxBarSizePx * barSizePercent, _sliderRectTransform.rect.height
            );

            if (fill)
            {
                SetCurrentValue(_maxValue);
            }
        }

        /// <summary>
        /// Add the specified quantity to the max value
        /// </summary>
        /// <param name="qte">The quantity to add</param>
        /// <param name="fill">TRUE to fill the bar, FALSE otherwise</param>
        public void AddMaxValue(int qte, bool fill)
        {
            SetMaxValue(_maxValue + qte, fill);
        }

        /// <summary>
        /// Subtract the specified quantity to the max value
        /// </summary>
        /// <param name="qte">The quantity to subtract</param>
        /// <param name="fill">TRUE to fill the bar, FALSE otherwise</param>
        public void SubtractMaxValue(int qte, bool fill)
        {
            SetMaxValue(_maxValue - qte, fill);
        }
    }
}