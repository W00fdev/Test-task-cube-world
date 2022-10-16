using System;
using TMPro;

namespace Assets.CodeBase.Data
{
    public class InputFieldListener<TValue> where TValue : struct, IConvertible
    {
        private readonly TMP_InputField _inputField;
        private readonly ValidatorSettings _dataValidator;

        private readonly Action<TValue> _updateValueAction;

        private readonly char _decimalDelimiter = ',';
        private readonly bool _isDecimalValue;

        public InputFieldListener(TMP_InputField inputField, ValidatorSettings dataValidator, Action<TValue> updateValueAction)
        {
            _inputField = inputField;
            _dataValidator = dataValidator;
            _updateValueAction = updateValueAction;

            _isDecimalValue = typeof(TValue) == typeof(double) || typeof(TValue) == typeof(float);
        }

        public void OnValueChangedValidate(string newValue)
        {
            if (newValue.Length == 0)
            {
                _inputField.text = _dataValidator.Validate(default).ToString();
                return;
            }

            string clampedValueText = _dataValidator.Validate(Convert.ToSingle(newValue)).ToString();
            if (_isDecimalValue == true)
                if (HasDelimiterInTheEnd(newValue, _decimalDelimiter) == true)
                    clampedValueText += _decimalDelimiter;

            _inputField.SetTextWithoutNotify(clampedValueText);

            static bool HasDelimiterInTheEnd(string newValue, char decimalDelimiter)
            {
                return newValue.Contains(decimalDelimiter) 
                    && newValue.LastIndexOf(decimalDelimiter) == newValue.Length - 1;
            }
        }

        public void ListenInputField(string newValue)
        {
            if (newValue.Length == 0)
                return;

            // It also can be done with Reflection.GetMethod("Parse");
            TValue valueToReturn = (TValue)Convert.ChangeType(newValue, typeof(TValue));
            _updateValueAction?.Invoke(valueToReturn);
        }
    }
}
