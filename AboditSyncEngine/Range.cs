using System;

namespace AboditSyncEngine
{
    /// <summary>
    /// A Range of values of type T with a Start and a Finish
    /// </summary>
    public class Range<T> where T:IComparable<T>
    {
        public readonly T Start;
        public readonly T End;

        public Range (T start, T end)
        {
            this.Start = start;
            this.End = end;
            if (end.CompareTo(start) < 0)
                throw new ArgumentException ("Cannot create reversed ranges");
        }

        public bool Intersects (Range<T> other)
        {
            if (this.End.CompareTo(other.Start) < 0)
                return false;
            if (other.End.CompareTo(this.Start) < 0)
                return false;
            return true;
        }

        public Range<T> Merge (Range<T> other)
        {
            T newStart = this.Start.CompareTo(other.Start) < 0 ? this.Start : other.Start;
            T newEnd = this.End.CompareTo(other.End) > 0 ? this.End : other.End;
            return new Range<T> (newStart, newEnd);
        }
    }
}