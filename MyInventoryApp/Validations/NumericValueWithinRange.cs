using System;
namespace MyInventoryApp.Validations
{
    public class NumericValueWithinRange 
        : IValidationRule<double>
    {

        public double MinRange { get; set; } = double.MinValue;

        public double MaxRange { get; set; } = double.MaxValue;

        public string ValidationMessage { get; set; }

        public bool Check(double value)
        {
            return value >= MinRange && value <= MaxRange;
        }
    }
}
