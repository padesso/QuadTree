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
            this.Bounds = bounds;           
        }

        /// <summary>
        /// Create the quads as four equal regions in reference to this quad.
        /// </summary>
        private void Subdivide()
        {
            NorthWest = new QuadTree(
                new AxisAlignedBoundingBox(new Vector(Bounds.CenterPoint.X - 0.5f * Bounds.HalfWidth, Bounds.CenterPoint.Y - 0.5f * Bounds.HalfHeight), 
                    Bounds.HalfWidth, Bounds.HalfHeight));

            NorthEast = new QuadTree(
                new AxisAlignedBoundingBox(new Vector(Bounds.CenterPoint.X + 0.5f * Bounds.HalfWidth, Bounds.CenterPoint.Y - 0.5f * Bounds.HalfHeight),
                    Bounds.HalfWidth, Bounds.HalfHeight));

            SouthWest = new QuadTree(
                new AxisAlignedBoundingBox(new Vector(Bounds.CenterPoint.X - 0.5f * Bounds.HalfWidth, Bounds.CenterPoint.Y + 0.5f * Bounds.HalfHeight),
                    Bounds.HalfWidth, Bounds.HalfHeight));

            SouthEast = new QuadTree(
                new AxisAlignedBoundingBox(new Vector(Bounds.CenterPoint.X + 0.5f * Bounds.HalfWidth, Bounds.CenterPoint.Y + 0.5f * Bounds.HalfHeight),
                Bounds.HalfWidth, Bounds.HalfHeight));
        }

        /// <summary>
        /// Add a position to the quadtree
        /// </summary>
        /// <param name="position">A Vector to add to the quadtree<./param>
        /// <returns>True if the insertion was successful.</returns>
        public bool Insert(Vector position)
        {
            // Ignore objects that do not belong in this quad tree
            if (!this.Bounds.Contains(position))
                return false; // object cannot be added to this quad

            //If the quad is not full and it is an external node, fill it
            if (this.Position == null && NorthWest == null)
            {
                this.Position = position;
                return true;
            }
            
            // Otherwise, subdivide and shift down current position.
            if (NorthWest == null)
                Subdivide();

            //Shift down the current position as this is now an internal node
            if (NorthWest.Insert(this.Position))
            {
                this.Position = null;
            }
            else if (NorthEast.Insert(this.Position))
            {
                this.Position = null;
            }
            else if (SouthWest.Insert(this.Position))
            {
                this.Position = null;
            }
            else if (SouthEast.Insert(this.Position))
            {
                this.Position = null;
            }

            //Attempt to insert new node
            if (NorthWest.Insert(position))
                return true;

            if (NorthEast.Insert(position))
                return true;

            if (SouthWest.Insert(position))
                return true;

            if (SouthEast.Insert(position))
                return true;
            
            // The point cannot be inserted for  unknown reasons (how did we get here?
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
            if (!this.Bounds.Intersect(bounds))
                return positionsInBounds;

            //if (bounds.Contains(this.Position))
            //    positionsInBounds.Add(this.Position);

            // Terminate here, if there are no children (external node)
            // We only need to check one since the subdivide method instantiates all sub-quads
            if (this.Position != null && NorthWest == null)
            {
                positionsInBounds.Add(this.Position);
                return positionsInBounds;
            }

            // Otherwise, add the positions from the children
            if(NorthWest != null)
                positionsInBounds.AddRange(NorthWest.QueryBounds(bounds));

            if (NorthEast != null)
                positionsInBounds.AddRange(NorthEast.QueryBounds(bounds));

            if (SouthWest != null)
                positionsInBounds.AddRange(SouthWest.QueryBounds(bounds));

            if (SouthEast != null)
                positionsInBounds.AddRange(SouthEast.QueryBounds(bounds));
            
            return positionsInBounds;
        }

        public QuadTree NorthWest { get => northWest; set => northWest = value; }
        public QuadTree NorthEast { get => northEast; set => northEast = value; }
        public QuadTree SouthWest { get => southWest; set => southWest = value; }
        public QuadTree SouthEast { get => southEast; set => southEast = value; }
        public Vector Position { get => position; set => position = value; }
        public AxisAlignedBoundingBox Bounds { get => bounds; set => bounds = value; }

        public QuadTree FindQuad(Vector pos)
        {
            if (Position == pos)
                return this;

            //TODO: recursively traverse tree to find matching quads

            return null;
        }

        public bool DeletePosition(Vector pos)
        {
            //TODO: find the first (?) position that matches, deleted it and shift the tree upwardB 
            return true;
        }
    }
}
