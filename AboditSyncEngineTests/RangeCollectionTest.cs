using NUnit.Framework;
using System;
using System.Linq;
using AboditSyncEngine;
using FluentAssertions;

namespace AboditSyncEngineTests
{
    [TestFixture ()]
    [Category("RangeCollection")]
    public class RangeCollectionTest
    {
        [Test ()]
        public void ICanCreateARangeCollectionOfIntegersAndAddToIt ()
        {
            var rangeCollection = new RangeCollection<int> ();
            var random = new Random ();

            var randomRanges = Enumerable.Range(1, 100).Select(i => new Range<int>(random.Next(0, 100), random.Next(101, 200)));

            rangeCollection.AddRange (randomRanges);
            // Not a lot we can say about it, other than it should have some but less than 101 ranges in it

            rangeCollection.Ranges.Count ().Should ().BeGreaterOrEqualTo (1);
            rangeCollection.Ranges.Count ().Should ().BeLessOrEqualTo (100);
        }

        [Test ()]
        public void RangesCoallesceWithinARangeCollection ()
        {
            Range<int> range1 = new Range<int> (  1,  33);
            Range<int> range2 = new Range<int> ( 32,  67);
            Range<int> range3 = new Range<int> ( 66, 100);

            var rangeCollection = new RangeCollection<int> ();
            rangeCollection.AddRange (new []{ range1, range2, range3 });

            rangeCollection.Ranges.Should ().HaveCount (1, "all the ranges should collapse to one");
            var first = rangeCollection.Ranges.First ();

            first.Start.Should ().Be (1, "the collapsed range should start at 1");
            first.End.Should ().Be (100, "the collapsed range should end at 100");
        }

        [Test ()]
        public void RangesShouldBeReturnedInSortedOrder ()
        {
            var range1 = new Range<int> (  1,  33);
            var range2 = new Range<int> ( 34,  66);
            var range3 = new Range<int> ( 67, 100);

            var rangeCollection = new RangeCollection<int> ();
            rangeCollection.AddRange (new []{ range3, range2, range1 });

            rangeCollection.Ranges.Should ().HaveCount (3, "the ranges were distinct");

            var result = rangeCollection.Ranges.ToArray ();

            result[0].Should().Be(range1);
            result[1].Should().Be(range2);
            result[2].Should().Be(range3);
        }
    }
}