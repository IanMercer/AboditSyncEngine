using NUnit.Framework;
using System;
using AboditSyncEngine;
using FluentAssertions;

namespace AboditSyncEngineTests
{
    [TestFixture ()]
    [Category("Range")]
    public class RangeTest
    {
        [Test ()]
        public void ICanCreateARangeOfIntegers ()
        {
            Range<int> range = new Range<int> (1, 100);
            range.Start.Should ().Be (1);
            range.End.Should ().Be (100);
        }

        [Test ()]
        public void RangesOfIntegersIntersectOrNot ()
        {
            Range<int> range1 = new Range<int> (  1,  50);
            Range<int> range2 = new Range<int> ( 51, 100);
            Range<int> range3 = new Range<int> (  2,  75);

            range1.Intersects (range2).Should ().Be (false, "The ranges do not overlap");
            range1.Intersects (range3).Should ().Be (true, "The ranges overlap");
            range2.Intersects (range3).Should ().Be (true, "The ranges overlap");
        }

        [Test ()]
        public void OverlappingRangesMerge ()
        {
            Range<int> range1 = new Range<int> (  1,  50);
            Range<int> range2 = new Range<int> ( 49, 100);

            var merged = range1.Merge (range2);

            merged.Start.Should ().Be (1);
            merged.End.Should ().Be (100);
        }
    }
}

