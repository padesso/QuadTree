using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTreeTDD
{
    public class AxisAlignedBoundingBox
    {
        private Vector centerPoint;
        private float width;
        private float height;

        public AxisAlignedBoundingBox(Vector centerPoint, float width, float height)
        {
            this.centerPoint = centerPoint;
            this.width = width;
            this.height = height;
        }

        public Vector TopLeft { get => new Vector(centerPoint.X - HalfWidth, centerPoint.Y - HalfHeight); }
        public Vector CenterPoint { get => centerPoint; set => centerPoint = value; }
        public float Width { get => width; set => width = value; }
        public float Height { get => height; set => height = value; }

        public float HalfWidth { get => width / 2.0f; }
        public float HalfHeight{ get => height / 2.0f; }
        
        /// <summary>
        /// Check if a position is within the box.
        /// </summary>
        /// <param name="position">Position to be checked.</param>
        /// <returns>Bool based on whether position vector is within the AABB.</returns>
        public bool Contains(Vector position)
        {
            if (position.X >= (this.centerPoint.X - HalfWidth) &&
                position.X < (this.centerPoint.X + HalfWidth) &&
                position.Y >= (this.centerPoint.Y - HalfHeight) &&
                position.Y < (this.centerPoint.Y + HalfHeight))
                return true;

            return false;
        }

        /// <summary>
        /// Check if two axis aligned bounding boxes intersect with one another
        /// </summary>
        /// <param name="bounds">The AABB to check against.</param>
        /// <returns>A bool based on if the AABB's in question intersect.</returns>
        public bool Intersect(AxisAlignedBoundingBox bounds)
        {
            if (this.CenterPoint.X + this.HalfWidth < bounds.CenterPoint.X - bounds.HalfWidth) return false; // left
            if (this.CenterPoint.X - this.HalfWidth > bounds.CenterPoint.X + bounds.HalfWidth) return false; // right
            if (this.CenterPoint.Y + this.HalfHeight < bounds.CenterPoint.Y - bounds.HalfHeight) return false; // above
            if (this.CenterPoint.Y - this.HalfHeight > bounds.CenterPoint.Y + bounds.HalfHeight) return false; // below
            return true; // overlap
        }
    }
}
