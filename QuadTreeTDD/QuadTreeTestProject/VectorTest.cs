using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuadTreeTDD;

namespace QuadTreeTestProject
{
    [TestClass]
    public class VectorTest
    {
        [TestMethod]
        public void TestVector()
        {
            //Assemble
            Vector vect = new Vector(10, 20);

            //Assert
            Assert.AreEqual(vect.X, 10);
            Assert.AreEqual(vect.Y, 20);

            //Act
            vect.X = 20;
            vect.Y = 10;

            //Assert
            Assert.AreEqual(vect.X, 20);
            Assert.AreEqual(vect.Y, 10);
        }
    }
}
