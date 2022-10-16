using UnityEngine;
using TMPro;

namespace Assets.CodeBase.Data
{
    public class DataListenerUI : MonoBehaviour
    {
        [Header("Set clamped values")]
        [SerializeField] private ValidatorSettings _dataValidator;

        [SerializeField] private TMP_InputField _distanceInputField;
        [SerializeField] private TMP_InputField _speedInputField;
        [SerializeField] private TMP_InputField _timeInputField;

        private SimulationSettings _dataSimulation;

        private InputFieldListener<float> _distanceListener;
        private InputFieldListener<float> _speedListener;
        private InputFieldListener<float> _timeListener;

        private void Start()
        {
            InitializeListeners();
            SubscribeListeners();
        }

        public void InitializeListener(SimulationSettings dataSimulation)
            => _dataSimulation = dataSimulation;

        private void InitializeListeners()
        {
            _distanceListener = new InputFieldListener<float>(_distanceInputField, _dataValidator, ListenDistance);
            _speedListener = new InputFieldListener<float>(_speedInputField, _dataValidator, ListenSpeed);
            _timeListener = new InputFieldListener<float>(_timeInputField, _dataValidator, ListenTime);
        }

        private void SubscribeListeners()
        {
            // Validate value and update data class
            _distanceInputField.onValueChanged.AddListener(_distanceListener.OnValueChangedValidate);
            _distanceInputField.onValueChanged.AddListener(_distanceListener.ListenInputField);

            _speedInputField.onValueChanged.AddListener(_speedListener.OnValueChangedValidate);
            _speedInputField.onValueChanged.AddListener(_speedListener.ListenInputField);

            _timeInputField.onValueChanged.AddListener(_timeListener.OnValueChangedValidate);
            _timeInputField.onValueChanged.AddListener(_timeListener.ListenInputField);
        }

        public void UnsubscribeListeners()
        {
            _distanceInputField.onValueChanged.RemoveListener(_distanceListener.OnValueChangedValidate);
            _distanceInputField.onValueChanged.RemoveListener(_distanceListener.ListenInputField);

            _speedInputField.onValueChanged.RemoveListener(_speedListener.OnValueChangedValidate);
            _speedInputField.onValueChanged.RemoveListener(_speedListener.ListenInputField);

            _timeInputField.onValueChanged.RemoveListener(_timeListener.OnValueChangedValidate);
            _timeInputField.onValueChanged.RemoveListener(_timeListener.ListenInputField);
        }

        private void ListenDistance(float listenedValue)
            => _dataSimulation.Distance = _dataValidator.Validate(listenedValue);

        private void ListenSpeed(float listenedValue)
            => _dataSimulation.Speed = _dataValidator.Validate(listenedValue);

        private void ListenTime(float listenedValue)
            => _dataSimulation.Time = _dataValidator.Validate(listenedValue);
    }
}
