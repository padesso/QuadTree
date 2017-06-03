using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuadTreeTDD;

namespace QuadTreeTestProject
{
    [TestClass]
    public class BodyTest
    {
        [TestMethod]
        public void TestBody()
        {
            //Assemble
            Vector testPosition = new Vector(10, 20);
            Vector testForce = new Vector(20, 10);
            float testMass = 100;

            Body body = new Body(testPosition, testForce, testMass);

            //Assert
            Assert.AreEqual(body.Position.X, testPosition.X);
            Assert.AreEqual(body.Position.Y, testPosition.Y);

            Assert.AreEqual(body.Force.X, testForce.X);
            Assert.AreEqual(body.Force.Y, testForce.Y);

            Assert.AreEqual(body.Mass, testMass);
        }
    }
}
