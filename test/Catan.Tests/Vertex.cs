using System;

namespace Catan.Tests
{
    public class Vertex
    {
        public Vertex()
        {
        }
        public IBuilding Building { get; private set; }

        public Field Field1 { get; set; }
        public Field Field2 { get; set; }
        public Field Field3 { get; set; }

        internal void Build(IBuilding building)
        {
            Building = building;
        }
    }
}
