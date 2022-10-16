using UnityEngine;
using System;

namespace Assets.CodeBase.Data
{
    [Serializable]
    public class ValidatorSettings
    {
        public float MaxValueValidator;
        public float MinValueValidator;

        public ValidatorSettings(float maxValueValidator, float minValueValidator)
        {
            MaxValueValidator = maxValueValidator;
            MinValueValidator = minValueValidator;
        }

        public float Validate(float toValidate) => Mathf.Clamp(toValidate, MinValueValidator, MaxValueValidator);
    }
}
