using System;
using System.Collections.Generic;
using System.Linq;

namespace AboditSyncEngine
{
    /// <summary>
    /// Range collection stores a collection of ranges sorted by start time and can compact to remove any overlaps.
    /// </summary>
    public class RangeCollection<T> where T:IComparable<T>
    {
        public IEnumerable<Range<T>> Ranges {get { return this.ranges.OrderBy(x => x.Start).ToList(); }}

        private List<Range<T>> ranges = new List<Range<T>>();

        public void Add(Range<T> range)
        {
            lock (this)
            {
                this.ranges.Add (range);
                this.Compact ();
            }
        }

        public void AddRange(IEnumerable<Range<T>> ranges)
        {
            lock (this)
            {
                this.ranges.AddRange (ranges);
                this.Compact ();
            }
        }


        private void Compact()
        {
            /*
             * An efficient approach is to first sort the intervals according to starting time. 
             * Once we have the sorted intervals, we can combine all intervals in a linear traversal. 
             * The idea is, in sorted array of intervals, if range[i] doesn’t overlap with range[i-1], 
             * then range[i+1] cannot overlap with range[i-1] because starting time of range[i+1] 
             * must be greater than or equal to range[i].
             */

            lock (this)
            {
                if (this.ranges.Count < 2)
                    return;

                var sorted = this.ranges.OrderBy (x => x.Start).ToList ();

                var current = sorted.First ();
                var result = new List<Range<T>> ();

                foreach (var range in sorted)
                {
                    if (current.Intersects (range))
                        current = current.Merge (range);
                    else
                    {
                        result.Add (current);
                        current = range;
                    }
                }

                result.Add (current);

                this.ranges = result;
            }
        }

        public override string ToString ()
        {
            return string.Format ("[RangeCollection: Ranges=[{0}{1}]]", string.Join(",", this.Ranges.Select(x => x.ToString())), this.ranges.Count > 10 ? ",..." : "");
        }
    }
}