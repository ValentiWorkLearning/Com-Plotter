using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ComPlotter;

namespace ComPlotterTests
{
    [TestClass]
    public class SerialControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TryСonfigureWithEmptyName()
        {
            ISerialController testController = new SerialController();
            testController.Configure( null, "baud", "2", "none" );
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TryСonfigureWithEmptyBaud()
        {
            ISerialController testController = new SerialController();
            testController.Configure( "COM1", null, "2", "none" );
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TryСonfigureWithEmptyStopBits()
        {
            ISerialController testController = new SerialController();
            testController.Configure( "COM1", "baud", null , "none" );
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TryСonfigureWithEmptyParityControl()
        {
            ISerialController testController = new SerialController();
            testController.Configure( "COM1", "baud", "2", null );
        }
    }
}
