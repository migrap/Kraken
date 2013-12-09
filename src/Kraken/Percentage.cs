using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kraken {
    public struct Percentage {
        public double Value;

        public Percentage(double value) {
            Value = value;
        }

        public Percentage(string value) {
            var pct = (Percentage)TypeDescriptor.GetConverter(typeof(Percentage)).ConvertFromString(value);
            Value = pct.Value;
        }

        public override string ToString() {
            return ToString(CultureInfo.InvariantCulture);
        }

        public string ToString(CultureInfo Culture) {
            return TypeDescriptor.GetConverter(GetType()).ConvertToString(null, Culture, this);
        }

        public static implicit operator double(Percentage value) {
            return value.Value;
        }

        public static implicit operator Percentage(double value) {
            return new Percentage(value);
        }

        public static implicit operator Percentage(string value) {
            return new Percentage(value);
        }

        public static double operator *(Percentage percentage, double value) {
            return percentage.Value * value;
        }

        public static Percentage operator *(Percentage lhs, Percentage rhs) {
            return new Percentage(lhs.Value * rhs.Value);
        }

        public static double operator /(Percentage percentage, double value) {
            return percentage.Value / value;
        }

        public static Percentage operator /(Percentage lhs, Percentage rhs) {
            return new Percentage(lhs.Value / rhs.Value);
        }

        public static Percentage operator +(Percentage lhs, Percentage rhs) {
            return new Percentage(lhs.Value + rhs.Value);
        }

        public static Percentage operator -(Percentage lhs, Percentage rhs) {
            return new Percentage(lhs.Value - rhs.Value);
        }

        public static Percentage Parse(string value) {
            return new Percentage(value);
        }
    }
}