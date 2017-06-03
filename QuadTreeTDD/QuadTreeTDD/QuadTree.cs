using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTreeTDD
{
    public class QuadTree
    {
        const int NODE_CAPACITY = int.MaxValue;

        private AxisAlignedBoundingBox bounds;
        private List<Vector> positions = new List<Vector>(NODE_CAPACITY);

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
            northWest = new QuadTree(
                new AxisAlignedBoundingBox(new Vector(0.5f * bounds.CenterPoint.X, 0.5f * bounds.CenterPoint.Y), 
                    bounds.HalfWidth, bounds.HalfHeight));

            northEast = new QuadTree(
                new AxisAlignedBoundingBox(new Vector(bounds.CenterPoint.X + 0.5f * bounds.CenterPoint.X, 0.5f * bounds.CenterPoint.Y),
                    bounds.HalfWidth, bounds.HalfHeight));

            southWest = new QuadTree(
                new AxisAlignedBoundingBox(new Vector(0.5f * bounds.CenterPoint.X, bounds.CenterPoint.Y + 0.5f * bounds.CenterPoint.Y),
                    bounds.HalfWidth, bounds.HalfHeight));

            southEast = new QuadTree(
                new AxisAlignedBoundingBox(new Vector(bounds.CenterPoint.X + 0.5f * bounds.CenterPoint.X, bounds.CenterPoint.Y + 0.5f * bounds.CenterPoint.Y), bounds.HalfWidth, bounds.HalfHeight));
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
                return false; // object cannot be added

            // If there is space in this quad tree, add the object here
            if (positions.Count < NODE_CAPACITY)
            {
                positions.Add(position);
                return true;
            }

            // Otherwise, subdivide and then add the point to whichever node will accept it
            if (northWest == null)
                Subdivide();

            if (northWest.Insert(position)) return true;
            if (northEast.Insert(position)) return true;
            if (southWest.Insert(position)) return true;
            if (southEast.Insert(position)) return true;

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

            // Check objects at this quad level
            for (int positionIndex = 0; positionIndex < positions.Count; positionIndex++)
            {
                if (bounds.Contains(positions[positionIndex]))
                    positionsInBounds.Add(this.positions[positionIndex]);
            }

            // Terminate here, if there are no children
            // We only need to check on since the subdivide method instantiates all sub-quads
            if (northWest == null)
                return positionsInBounds;

            // Otherwise, add the points from the children
            positionsInBounds.Concat(northWest.QueryBounds(bounds));
            positionsInBounds.Concat(northEast.QueryBounds(bounds));
            positionsInBounds.Concat(southWest.QueryBounds(bounds));
            positionsInBounds.Concat(southEast.QueryBounds(bounds));
            
            return positionsInBounds;
        }
    }
}
