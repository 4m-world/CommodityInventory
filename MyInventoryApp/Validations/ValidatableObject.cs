using System.Collections.Generic;
using System.Linq;
using MyInventoryApp.ViewModels.Base;

namespace MyInventoryApp.Validations
{
    public class ValidatableObject<T>
        : ExtendedBindableObject, IValidity
    {
        readonly IList<IValidationRule<T>> _validationRules;
        IList<string> _errors;
        T _value;
        bool _isValid;

        public IList<IValidationRule<T>> Validations => _validationRules;

        public IList<string> Errors
        {
            get => _errors;
            set => UpdateAndNotifyOnChange(ref _errors, value);
        }

        public T Value
        {
            get => _value;
            set => UpdateAndNotifyOnChange(ref _value, value);
        }

        public bool IsValid
        {
            get => _isValid;
            set => UpdateAndNotifyOnChange(ref _isValid, value);
        }

        public ValidatableObject()
        {
            IsValid = true;
            _errors = new List<string>();
            _validationRules = new List<IValidationRule<T>>();
        }

        public bool Validate()
        {
            Errors.Clear();

            var errors = _validationRules.Where(v => !v.Check(Value))
                                         .Select(v => v.ValidationMessage)
                                         .ToList();

            Errors = errors;

            IsValid = !Errors.Any();

            return IsValid;
        }
    }
}
