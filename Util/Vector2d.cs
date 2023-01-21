using System;

namespace ParticleGame.Util
{
    public sealed class Vector2d
    {
        public static readonly Vector2d Zero = new Vector2d();

        public int X { get; set; }
        public int Y { get; set; }

        public Vector2d() : this(0, 0)
        {
        }

        public Vector2d(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static Vector2d Of(int x, int y)
        {
            return new Vector2d(x, y);
        }

        public static bool operator !=(Vector2d vec1, Vector2d vec2) => vec1.X != vec2.X || vec1.Y != vec2.Y;

        public static bool operator ==(Vector2d vec1, Vector2d vec2) => vec1.X == vec2.X && vec1.Y == vec2.Y;

        public static Vector2d operator +(Vector2d vec1, Vector2d vec2) => new Vector2d(vec1.X + vec2.X, vec1.Y + vec2.Y);

        public static Vector2d operator -(Vector2d vec1, Vector2d vec2) => new Vector2d(vec1.X - vec2.X, vec1.Y - vec2.Y);

        public override string ToString()
        {
            return $"({this.X};{this.Y})";
        }

        public override bool Equals(object obj)
        {
            return obj is Vector2d d &&
                   X == d.X &&
                   Y == d.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
