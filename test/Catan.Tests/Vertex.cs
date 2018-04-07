using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Catan.Tests
{
    public class Vertex
    {
        static private ConcurrentDictionary<string, Vertex> _dict = new ConcurrentDictionary<string, Vertex>();

        public static Vertex Create(Field field1, Field field2, Field field3)
        {
            var key = $"{field1}-{field2}-{field3}";
            var vertex = new Vertex { Field1 = field1, Field2 = field2, Field3 = field3 };
            return _dict.GetOrAdd(key, vertex);
        }

        private Vertex()
        {
        }
        public IBuilding Building { get; private set; }

        public Field Field1 { get; set; }
        public Field Field2 { get; set; }
        public Field Field3 { get; set; }

        internal void Build(IBuilding building)
        {
            if (this.Building != null)
                throw new CantCreateBuildingException();
            Building = building;
        }

        public override string ToString()
        {
            return $"{Field1}-{Field2}-{Field3}";
        }
    }
}
