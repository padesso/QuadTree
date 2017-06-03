using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuadTreeTDD;

namespace QuadTreeTestProject
{
    [TestClass]
    public class AxisAlignedBoundingBoxTest
    {
        [TestMethod]
        public void TestAxisAlignedBoundingBox()
        {
            //Assemble
            Vector testCenterPoint = new Vector(10, 10);
            AxisAlignedBoundingBox aabb = new AxisAlignedBoundingBox(testCenterPoint, 10, 10);

            //Assert
            Assert.AreEqual(aabb.CenterPoint, testCenterPoint);
            Assert.AreEqual(aabb.Width, 10);
            Assert.AreEqual(aabb.Height, 10);  
        }

        [TestMethod]
        public void TestAxisAlignedBoundingBoxContains()
        {
            //Assemble
            Vector testCenterPoint = new Vector(10, 10);
            AxisAlignedBoundingBox aabb = new AxisAlignedBoundingBox(testCenterPoint, 10, 10);

            //Act
            Vector testContainsPoint = new Vector(7, 7);
            Vector testNotContainsPoint = new Vector(20, 20);

            //Assert
            Assert.IsTrue(aabb.Contains(testContainsPoint));
            Assert.IsFalse(aabb.Contains(testNotContainsPoint));
        }

        [TestMethod]
        public void TestAxisAlignedBoundingBoxIntersects()
        {
            //Assemble
            Vector testCenterPoint = new Vector(10, 10);
            AxisAlignedBoundingBox aabb = new AxisAlignedBoundingBox(testCenterPoint, 10, 10);

            //Act
            Vector testCenterPoint_int = new Vector(5, 5);
            AxisAlignedBoundingBox aabb_int = new AxisAlignedBoundingBox(testCenterPoint_int, 10, 10);

            Vector testCenterPoint_not_int = new Vector(50, 50);
            AxisAlignedBoundingBox aabb_not_int = new AxisAlignedBoundingBox(testCenterPoint_not_int, 10, 10);

            //Assert
            Assert.IsTrue(aabb.Intersect(aabb_int));
            Assert.IsFalse(aabb.Intersect(aabb_not_int));
        }
    }
}
