// From ASP.Net Web Stack [http://aspnetwebstack.codeplex.com/]
// Used under the Apache License

// ReSharper Disable All

using System;
using System.Diagnostics;

namespace SimpleRazor.Attributes
{
    [DebuggerDisplay("({Position})\"{Value}\"")]
    public class PositionTagged<T>
    {
        public PositionTagged(T value, int offset)
        {
            this.Position = offset;
            this.Value = value;
        }

        public int Position { get; private set; }
        public T Value { get; private set; }

        public override bool Equals(object obj)
        {
            PositionTagged<T> other = obj as PositionTagged<T>;
            return other != null &&
                   other.Position == this.Position &&
                   Equals(other.Value, this.Value);
        }

        public override int GetHashCode()
        {
            return HashCodeCombiner.Start()
                .Add(this.Position)
                .Add(this.Value)
                .CombinedHash;
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }

        public static implicit operator T(PositionTagged<T> value)
        {
            return value.Value;
        }

        public static implicit operator PositionTagged<T>(Tuple<T, int> value)
        {
            return new PositionTagged<T>(value.Item1, value.Item2);
        }

        public static bool operator ==(PositionTagged<T> left, PositionTagged<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PositionTagged<T> left, PositionTagged<T> right)
        {
            return !Equals(left, right);
        }
    }
}
