using System.Diagnostics;
using System.Numerics;

namespace Day12
{
    public class Element
    {
        public Element(Complex position)
        {
            Position = position;
        }

        public Complex Position { get; set; }
        public Direction CoveredDirections { get; set; } = Direction.None;
        public bool HasOpenDirection(Direction direction) => (CoveredDirections & direction) == Direction.None;

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Position == (obj as Element).Position;
        }
    }
}