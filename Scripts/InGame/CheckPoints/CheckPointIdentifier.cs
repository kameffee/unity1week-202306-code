using System;

namespace Unity1week202306.InGame.CheckPoints
{
    public readonly struct CheckPointIdentifier : IEquatable<CheckPointIdentifier>
    {
        public int Value { get; }

        public CheckPointIdentifier(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, "0未満です");
            }

            Value = value;
        }

        public CheckPointIdentifier Next()
        {
            return new CheckPointIdentifier(Value + 1);
        }

        public bool Equals(CheckPointIdentifier other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is CheckPointIdentifier other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public override string ToString()
        {
            return $"{nameof(Value)}: {Value}";
        }
    }
}