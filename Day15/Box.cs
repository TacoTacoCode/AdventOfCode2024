using System.Numerics;

namespace Day15
{
    public class Box
    {
        public Box(Complex left, Complex right)
        {
            Left = left;
            Right = right;
        }

        public Complex Left { get; set; }
        public Complex Right { get; set; }

        public void Moved(Complex offset)
        {
            Left += offset;
            Right += offset;
        }

        public bool IsBox(Complex LeftOrRight)
        {
            return LeftOrRight == Left || LeftOrRight == Right;
        }

        public override string ToString()
        {
            return Left.ToString();
        }
    }
}