using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTreeTDD
{
    public class QuadTree
    {
        //const int NODE_CAPACITY = int.MaxValue;

        private AxisAlignedBoundingBox bounds;
        //private List<Vector> positions = new List<Vector>(NODE_CAPACITY);

        private Vector position;

        private QuadTree northWest;
        private QuadTree northEast;
        private QuadTree southWest;
        private QuadTree southEast;

        public QuadTree(AxisAlignedBoundingBox bounds)
        {
            this.bounds = bounds;           
        }

        /// <summary>
        /// Create the quads as four equal regions in reference to this quad.
        /// </summary>
        private void Subdivide()
        {
            //TODO: fix this!
            northWest = new QuadTree(
                new AxisAlignedBoundingBox(new Vector(bounds.CenterPoint.X - 0.5f * bounds.HalfWidth, bounds.CenterPoint.Y - 0.5f * bounds.HalfHeight), 
                    bounds.HalfWidth, bounds.HalfHeight));

            northEast = new QuadTree(
                new AxisAlignedBoundingBox(new Vector(bounds.CenterPoint.X + 0.5f * bounds.HalfWidth, bounds.CenterPoint.Y - 0.5f * bounds.HalfHeight),
                    bounds.HalfWidth, bounds.HalfHeight));

            southWest = new QuadTree(
                new AxisAlignedBoundingBox(new Vector(bounds.CenterPoint.X - 0.5f * bounds.HalfWidth, bounds.CenterPoint.Y + 0.5f * bounds.HalfHeight),
                    bounds.HalfWidth, bounds.HalfHeight));

            southEast = new QuadTree(
                new AxisAlignedBoundingBox(new Vector(bounds.CenterPoint.X + 0.5f * bounds.HalfWidth, bounds.CenterPoint.Y + 0.5f * bounds.HalfHeight),
                bounds.HalfWidth, bounds.HalfHeight));
        }

        /// <summary>
        /// Add a position to the quadtree
        /// </summary>
        /// <param name="position">A Vector to add to the quadtree<./param>
        /// <returns>True if the insertion was successful.</returns>
        public bool Insert(Vector position)
        {
            // Ignore objects that do not belong in this quad tree
            if (!this.bounds.Contains(position))
                return false; // object cannot be added to this quad

            //If the quad is not full, fill it
            if (this.position == null)
            {
                this.position = position;
                return true;
            }

            // Otherwise, subdivide and then add the point to whichever node will accept it
            if (northWest == null)
                Subdivide();

            if (northWest.Insert(position))
                return true;

            if (northEast.Insert(position))
                return true;

            if (southWest.Insert(position))
                return true;

            if (southEast.Insert(position))
                return true;
            
            // Otherwise, the point cannot be inserted for some unknown reason (this should never happen)
            return false;
        }        

        /// <summary>
        /// Find all points within an axis aligned bounding box.
        /// </summary>
        /// <param name="bounds">An axis aligned bounding box to find all points in the quadtree.</param>
        /// <returns>A list of Vectors representing all positions within the AABB passed.</returns>
        public List<Vector> QueryBounds(AxisAlignedBoundingBox bounds)
        {
            List<Vector> positionsInBounds = new List<Vector>();
            
            //Get out early if the bounds passed isn't in this quad (or a child quad)
            if (!this.bounds.Intersect(bounds))
                return positionsInBounds;

            if (bounds.Contains(this.position))
                positionsInBounds.Add(this.position);

            // Terminate here, if there are no children (external node)
            // We only need to check one since the subdivide method instantiates all sub-quads
            if (northWest == null)
                return positionsInBounds;

            // Otherwise, add the positions from the children
            if(northWest.position != null)
                positionsInBounds.AddRange(northWest.QueryBounds(bounds));

            if (northEast.position != null)
                positionsInBounds.AddRange(northEast.QueryBounds(bounds));

            if (southWest.position != null)
                positionsInBounds.AddRange(southWest.QueryBounds(bounds));

            if (southEast.position != null)
                positionsInBounds.AddRange(southEast.QueryBounds(bounds));
            
            return positionsInBounds;
        }
    }
}
