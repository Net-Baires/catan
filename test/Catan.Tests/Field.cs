using System;

namespace Catan.Tests
{
    public class Field
    {
        private readonly int _number;

        public Edge Edge1 { get; private set; }
        public Edge Edge2 { get; private set; }
        public Edge Edge3 { get; private set; }
        public Edge Edge4 { get; private set; }
        public Edge Edge5 { get; private set; }
        public Edge Edge6 { get; private set; }
        public Token Token { get; private set; }
        public Field(int number)
        {
            _number = number;
        }

        public void Build(
            Field field1,
            Field field2,
            Field field3,
            Field field4,
            Field field5,
            Field field6,
            Func<Token> generateToken)
        {

            this.Edge1 = Edge.Create(
                Vertex.Create(this, field1, field2),
                Vertex.Create(this, field6, field1));


            this.Edge2 = Edge.Create(
                Vertex.Create(field3, this, field2),
                Vertex.Create(this, field1, field2)
            );


            this.Edge3 = Edge.Create(
                Vertex.Create(field3, this, field2),
                Vertex.Create(field4, this, field3));

            this.Edge4 = Edge.Create(
                Vertex.Create(field4, this, field3),
                Vertex.Create(field4, field5, this));

            this.Edge5 = Edge.Create(
                Vertex.Create(field4, field5, this),
                Vertex.Create(field5, field6, this)
                );

            this.Edge6 = Edge.Create(
                Vertex.Create(this, field6, field1),
                Vertex.Create(field5,field6,this)
                );


            Token = generateToken();
        }

        public override string ToString()
        {
            return $"Field {_number}";
        }
    }
    public class Token
    {
        private readonly int number;

        public Token(int number)
        {
            this.number = number;
        }
    }
}
