using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace Catan.Tests
{

    public class DesertFieldShould
    {
        private readonly Field _sut;
        private readonly Field[] _fields;
        private readonly Tablero _tablero;

        public DesertFieldShould()
        {
            _sut = new Field();
            _fields = Enumerable.Range(0, 18).Select(x => new Field()).ToArray();
            _tablero = new Tablero();
        }

        private void IsRightClose(Field source, Func<Field, Edge> edge, Field next)
        {
            edge(_sut).Up.Field1.ShouldBe(_sut, "some cool message");
        }

        private void EdgeVertexAllFields(Vertex vertex, Field field1, Field field2, Field field3)
        {
            vertex.Field1.ShouldBe(field1);
            vertex.Field2.ShouldBe(field2);
            vertex.Field3.ShouldBe(field3);
        }

        [Fact]
        public void BuildEdge1Field_GivenDesert()
        {
            var field1 = _fields[0];
            var field2 = _fields[1];
            var field6 = _fields[5];

            _tablero.Build(_sut, _fields, () => new Token(5));


            _sut.Edge1.ShouldNotBeNull();

            IsRightClose(_sut, x => x.Edge1, field1);

            EdgeVertexAllFields(_sut.Edge1.Up, _sut, field1, field2);
            EdgeVertexAllFields(_sut.Edge1.Down, _sut, field6, field1);

            //vertice the edge1 up tiene que ser igual a vertice edge2 down;
            _sut.Edge1.Up.ShouldBe(_sut.Edge2.Down);


            //field1 edge4 sea igual a desierto edg1
            field1.Edge4.ShouldBe(_sut.Edge1);

        }

        [Fact]
        public void BuildEdge2Field_GivenDesert()
        {
            var field1 = _fields[0];
            var field2 = _fields[1];
            var field3 = _fields[2];
            var field4 = _fields[3];
            var field5 = _fields[4];
            var field6 = _fields[5];

            _sut.Build(field1, field2, field3, field4, field5, field6, () => new Token(0));
            _sut.Edge2.ShouldNotBeNull();

            _sut.Edge2.Down.ShouldBe(_sut.Edge1.Up);

            IsRightClose(_sut, x => x.Edge2, field2);
            EdgeVertexAllFields(_sut.Edge2.Up, _sut, field2, field3);

            field2.Edge5.ShouldBe(_sut.Edge2);

        }

        [Fact]
        public void BuildEdge3Field_GivenDesert()
        {
            var field1 = _fields[0];
            var field2 = _fields[1];
            var field3 = _fields[2];
            var field4 = _fields[3];
            var field5 = _fields[4];
            var field6 = _fields[5];

            _sut.Build(field1, field2, field3, field4, field5, field6, () => new Token(0));
            _sut.Edge3.ShouldNotBeNull();
            _sut.Edge3.Down.ShouldBe(_sut.Edge2.Up);

            IsRightClose(_sut, x => x.Edge3, field3);
            EdgeVertexAllFields(_sut.Edge3.Up, _sut, field3, field4);

            field3.Edge6.ShouldBe(_sut.Edge3);

        }

        [Fact]
        public void BuildEdg1Field_GivenField1AndSut()
        {
            var field1 = _fields[0];
            var field2 = _fields[1];
            var field3 = _fields[2];
            var field4 = _fields[3];
            var field5 = _fields[4];
            var field6 = _fields[5];
            var field7 = _fields[6];
            var field8 = _fields[7];
            var field18 = _fields[17];

            _sut.Build(field1, field2, field3, field4, field5, field6, () => new Token(0));

            field1.Build(field7, field8, field2, _sut, field6, field18, () => new Token(0));

            _sut.Edge1.Up.Field2.Edge1.Up.Field2.ShouldBe(field7);
            _sut.Edge1.Down.Field3.Edge1.Down.Field2.ShouldBe(field18);
        }


        [Fact]
        public void BuildEdg1Field_GivenField7AndSut()
        {

            var field1 = _fields[0];
            var field2 = _fields[1];
            var field3 = _fields[2];
            var field4 = _fields[3];
            var field5 = _fields[4];
            var field6 = _fields[5];
            var field7 = _fields[6];
            var field8 = _fields[7];
            var field18 = _fields[17];
            var waterThreeByOne = new WaterThreeByOne();
            var waterEmpty = new WaterEmpty();

            _sut.Build(field1, field2, field3, field4, field5, field6, () => new Token(0));

            field1.Build(field7, field8, field2, _sut, field6, field18, () => new Token(0));

            field7.Build(waterThreeByOne, waterEmpty, field8, field1, field18, waterEmpty, () => new Token(0));

            _sut.Edge1.Up.Field2.Edge1.Up.Field2.Edge1.Up.Field2.ShouldBe(waterThreeByOne);
            _sut.Edge1.Up.Field2.Edge1.Up.Field2.Edge1.Up.Field3.ShouldBe(waterEmpty);
        }
        [Fact]
        public void BuildAToken()
        {
            var field1 = _fields[0];
            var field2 = _fields[1];
            var field3 = _fields[2];
            var field4 = _fields[3];
            var field5 = _fields[4];
            var field6 = _fields[5];
            var newToken = new Token(4);
            _sut.Build(field1, field2, field3, field4, field5, field6, () => newToken);

            _sut.Token.ShouldBe(newToken);

        }

        [Fact]
        public void Build_Building()
        {

            var field1 = _fields[0];
            var field2 = _fields[1];
            var field3 = _fields[2];
            var field4 = _fields[3];
            var field5 = _fields[4];
            var field6 = _fields[5];
            var field7 = _fields[6];
            var field8 = _fields[7];
            var field18 = _fields[17];
            var waterThreeByOne = new WaterThreeByOne();
            var waterEmpty = new WaterEmpty();
            var city = new City();
            _sut.Build(field1, field2, field3, field4, field5, field6, () => new Token(0));

            field1.Build(field7, field8, field2, _sut, field6, field18, () => new Token(0));

            field7.Build(waterThreeByOne, waterEmpty, field8, field1, field18, waterEmpty, () => new Token(0));
            _tablero.Build(field1.Edge1.Up, city);

            field1.Edge1.Up.Building.ShouldBe(city);
        }

        [Fact]
        public void Cannot_build_road_does_not_exit_Building()
        {

            var field1 = _fields[0];
            var field2 = _fields[1];
            var field3 = _fields[2];
            var field4 = _fields[3];
            var field5 = _fields[4];
            var field6 = _fields[5];
            var field7 = _fields[6];
            var field8 = _fields[7];
            var field18 = _fields[17];
            var waterThreeByOne = new WaterThreeByOne();
            var waterEmpty = new WaterEmpty();
            var road = new Road();
            _sut.Build(field1, field2, field3, field4, field5, field6, () => new Token(0));

            field1.Build(field7, field8, field2, _sut, field6, field18, () => new Token(0));

            field7.Build(waterThreeByOne, waterEmpty, field8, field1, field18, waterEmpty, () => new Token(0));

            Assert.Throws<CantCreateRoadException>(() => _tablero.Build(field1.Edge1, road));

        }
        [Fact]
        public void Can_build_road()
        {

            var field1 = _fields[0];
            var field2 = _fields[1];
            var field3 = _fields[2];
            var field4 = _fields[3];
            var field5 = _fields[4];
            var field6 = _fields[5];
            var field7 = _fields[6];
            var field8 = _fields[7];
            var field18 = _fields[17];
            var waterThreeByOne = new WaterThreeByOne();
            var waterEmpty = new WaterEmpty();
            var city = new City();
            var road = new Road();

            _sut.Build(field1, field2, field3, field4, field5, field6, () => new Token(0));

            field1.Build(field7, field8, field2, _sut, field6, field18, () => new Token(0));

            field7.Build(waterThreeByOne, waterEmpty, field8, field1, field18, waterEmpty, () => new Token(0));
            _tablero.Build(field1.Edge1.Up, city);

            field1.Edge1.Up.Building.ShouldBe(city);

            _tablero.Build(field1.Edge1, road);

            field1.Edge1.Road.ShouldBe(road);
        }
    }
}
