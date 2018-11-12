using RangeTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComPlotter.Algorithms
{
    class DensityDistribution
    {
        public DensityDistribution( int _rangesCount )
        {
            m_rangeTree = new RangeTree<byte, RangeItem>( new RangeItemComparer() );

            int rangeDistance = MaxRightRangeValue / _rangesCount;
            int leftRangeValue = 0;
            int rightRangeValue = rangeDistance - 1;

            for (int i = 0; i < _rangesCount; i++)
            {
                
                m_rangeTree.Add(new RangeItem {
                        Range = new Range< byte >(
                                (byte) leftRangeValue
                            ,   (byte) rightRangeValue )
                            ,   Content = i.ToString()
                        } 
                    );

                leftRangeValue = rightRangeValue + 1;
                rightRangeValue += rangeDistance;
            }
        }

        public int GetIntervalIndexOfValue(byte _value)
        {
            var range = m_rangeTree.Query(_value);
            int rangeIndex = Int32.Parse(range[0].Content);
            return rangeIndex;
        }

        class RangeItem : IRangeProvider<byte>
        {
            public Range<byte> Range { get; set; }
            public string Content { get; set; }
        }

        class RangeItemComparer : IComparer<RangeItem>
        {
            public int Compare(RangeItem x, RangeItem y)
            {
                return x.Range.CompareTo(y.Range);
            }
        }

        private RangeTree<byte, RangeItem> m_rangeTree;

        const int MaxRightRangeValue = 256;
    }
}
