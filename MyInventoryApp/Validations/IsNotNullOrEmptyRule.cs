﻿namespace MyInventoryApp.Validations
{
    public class IsNotNullOrEmptyRule<T>
        : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            // bypass checking value type against null value
            var type = value?.GetType();
            if (type == null)
            {
                return false;
            }

            var str = value as string;

            return !string.IsNullOrWhiteSpace(str);
        }
    }
}
