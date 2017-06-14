using QuadTreeTDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBodyLib
{
    public class Body
    {
        private Vector position;
        private Vector force;
        private float mass;

        public Body(Vector position, Vector force, float mass)
        {
            this.Position = position;
            this.Force = force;
            this.Mass = mass;
        }

        public Vector Position { get => position; set => position = value; }
        public Vector Force { get => force; set => force = value; }
        public float Mass { get => mass; set => mass = value; }
    }
}
