using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ComPlotter.Algorithms;

namespace ComPlotterTests
{
    [TestClass]
    public class TestDensityDistribution
    {
        [TestMethod]
        [ExpectedException( typeof( InvalidOperationException ) ) ]
        public void CreateWith0Intervals()
        {
            const int rangesCount = 0;
            DensityDistribution densityProvider = new DensityDistribution( rangesCount );
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateWithMoreThanPossibleIntervals()
        {
            const int rangesCount = 129;
            DensityDistribution densityProvider = new DensityDistribution(rangesCount);
        }

        [TestMethod]
        public void Createwith8Intervals()
        {
            const int rangesCount = 8;
            DensityDistribution densityProvider = new DensityDistribution(rangesCount);

            Assert.AreEqual( densityProvider.GetIntervalIndexOfValue( 0 ), 0 );
            Assert.AreEqual( densityProvider.GetIntervalIndexOfValue( 32 ), 1 );
            Assert.AreEqual( densityProvider.GetIntervalIndexOfValue( 255 ), 7 );

        }

        [TestMethod]
        public void Createwith128Intervals()
        {
            const int rangesCount = 128;
            DensityDistribution densityProvider = new DensityDistribution( rangesCount );

            Assert.AreEqual( densityProvider.GetIntervalIndexOfValue( 0 ), 0 );
            Assert.AreEqual( densityProvider.GetIntervalIndexOfValue( 2 ), 1 );
            Assert.AreEqual( densityProvider.GetIntervalIndexOfValue( 255 ), 127 );

        }
    }
}
