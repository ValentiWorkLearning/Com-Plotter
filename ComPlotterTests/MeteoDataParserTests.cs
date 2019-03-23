using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ComPlotter.Parsers;

namespace ComPlotterTests
{
    [ TestClass ]
    public class MeteoDataParserTests
    {
        [TestMethod]
        [ExpectedException( typeof( InvalidOperationException ) )]
        public void StringWithInvalidTemperature()
        {
            const string InvalidTempFormat = "PRESSURE_BEGIN20PRESSURE_ENDTEMP_BEGIN-asdqeTEMP_ENDHUMIDITY_BEGIN1233HUMIDITY_END";
            IReceivedDataParser testParser = new ReceivedDataParser();

            testParser.tryParseString( InvalidTempFormat );
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void StringWithInvalidPressure()
        {
            const string InvalidTempFormat = "PRESSURE_BEGINasddaqwPRESSURE_ENDTEMP_BEGIN20TEMP_ENDHUMIDITY_BEGIN1233HUMIDITY_END";
            IReceivedDataParser testParser = new ReceivedDataParser();

            testParser.tryParseString( InvalidTempFormat );
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void StringWithInvalidHumidity()
        {
            const string InvalidTempFormat = "PRESSURE_BEGIN20PRESSURE_ENDTEMP_BEGIN20TEMP_ENDHUMIDITY_BEGINasdddHUMIDITY_END";
            IReceivedDataParser testParser = new ReceivedDataParser();

            testParser.tryParseString( InvalidTempFormat );
        }

        [TestMethod]
        public void StringWithAllPositiveValues()
        {
            const string InvalidTempFormat = "PRESSURE_BEGIN20PRESSURE_ENDTEMP_BEGIN30TEMP_ENDHUMIDITY_BEGIN40HUMIDITY_END";
            IReceivedDataParser testParser = new ReceivedDataParser();

            testParser.tryParseString(InvalidTempFormat);

            Assert.AreEqual( testParser.getPressure(), 20 );
            Assert.AreEqual( testParser.getTemperature(), 30 );
            Assert.AreEqual( testParser.getHumidity(), 40 );
        }

        [TestMethod]
        public void StringWithAllNegativeValues()
        {
            const string InvalidTempFormat = "PRESSURE_BEGIN-20PRESSURE_ENDTEMP_BEGIN-30TEMP_ENDHUMIDITY_BEGIN-40HUMIDITY_END";
            IReceivedDataParser testParser = new ReceivedDataParser();

            testParser.tryParseString(InvalidTempFormat);

            Assert.AreEqual( testParser.getPressure(), -20 );
            Assert.AreEqual( testParser.getTemperature(), -30 );
            Assert.AreEqual( testParser.getHumidity(), -40 );
        }

        [TestMethod]
        public void StringWithNegativeTemperature()
        {
            const string InvalidTempFormat = "PRESSURE_BEGIN20PRESSURE_ENDTEMP_BEGIN-30TEMP_ENDHUMIDITY_BEGIN40HUMIDITY_END";
            IReceivedDataParser testParser = new ReceivedDataParser();

            testParser.tryParseString(InvalidTempFormat);

            Assert.AreEqual( testParser.getPressure(), 20 );
            Assert.AreEqual( testParser.getTemperature(), -30 );
            Assert.AreEqual( testParser.getHumidity(), 40 );
        }

        [TestMethod]
        public void StringInReverseOrder()
        {
            const string InvalidTempFormat = "HUMIDITY_BEGIN40HUMIDITY_ENDTEMP_BEGIN-30TEMP_ENDPRESSURE_BEGIN20PRESSURE_END";
            IReceivedDataParser testParser = new ReceivedDataParser();

            testParser.tryParseString(InvalidTempFormat);

            Assert.AreEqual( testParser.getPressure(), 20 );
            Assert.AreEqual( testParser.getTemperature(), -30 );
            Assert.AreEqual( testParser.getHumidity(), 40 );
        }
    }
}
