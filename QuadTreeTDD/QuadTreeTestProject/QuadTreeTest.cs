using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuadTreeTDD;
using System.Collections.Generic;

namespace QuadTreeTestProject
{
    [TestClass]
    public class QuadTreeTest
    {
        [TestMethod]
        public void TestQuadTreeInsert()
        {
            //Assemble
            Vector testCenter = new Vector(50, 50);
            AxisAlignedBoundingBox testBounds = new AxisAlignedBoundingBox(testCenter, 100, 100);
            QuadTree tree = new QuadTree(testBounds);

            //Act
            Vector testPoint = new Vector(30, 30);
            Vector TestNotPoint = new Vector(200, 200);

            //Assert
            Assert.IsTrue(tree.Insert(testPoint));
            Assert.IsFalse(tree.Insert(TestNotPoint));           
        }

        [TestMethod]
        public void TestQuadTreeQuery()
        {
            //Assemble
            Vector testCenter = new Vector(50, 50);
            AxisAlignedBoundingBox testBounds = new AxisAlignedBoundingBox(testCenter, 100, 100);
            QuadTree tree = new QuadTree(testBounds);

            //Act
            Vector testPoint = new Vector(30, 30);
            tree.Insert(testPoint);
            List<Vector> positions = tree.QueryBounds(testBounds);

            //Assert
            Assert.AreEqual(positions[0], testPoint);
        }
    }
}
