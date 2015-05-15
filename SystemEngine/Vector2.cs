using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemEngine
{
    public class Vector2
    {
        public float x;
        public float y;
        private float h;

        public Vector2()
        {
            x = 0; y = 0; h = 1;
        }
        public Vector2(int _x, int _y)
        {
            x = _x; y = _y; h = 1;
        }
        public Vector2(float _x, float _y)
        {
            x = _x; y = _y; h = 1;
        }

        public Vector2(Vector2 src)
        {
            x = src.x; y = src.y; h = src.h;
        }
        public static bool operator ==(Vector2 src1, Vector2 src2)
        {
            return src1.x == src2.x && src1.y == src2.y;
        }
        public static bool operator !=(Vector2 src1, Vector2 src2)
        {
            return src1.x != src2.x || src1.y != src2.y;
        }
        public float length()
        {
            return (float)Math.Sqrt(x * x + y * y);
        }
        public float lengthSq()
        {
            return (float)(x * x + y * y);
        }
        public static Vector2 operator +(Vector2 src1, Vector2 src2)
        {
            Vector2 ans = new Vector2(src1);
            ans.x += src2.x;
            ans.y += src2.y;
            return ans;
        }
        public static Vector2 operator -(Vector2 src1, Vector2 src2)
        {
            Vector2 ans = new Vector2(src1);
            ans.x -= src2.x;
            ans.y -= src2.y;
            return ans;
        }
        public static Vector2 operator *(Vector2 src, float value)
        {
            Vector2 ans = new Vector2(src);
            ans.x *= value;
            ans.y *= value;
            return ans;
        }
        public static Vector2 operator *(float value, Vector2 src)
        {
            Vector2 ans = new Vector2(src);
            ans.x *= value;
            ans.y *= value;
            return ans;
        }
        public static Vector2 operator /(Vector2 src, float value)
        {
            Vector2 ans = new Vector2(src);
            ans.x /= value;
            ans.y /= value;
            return ans;
        }
        public static Vector2 operator /(float value, Vector2 src)
        {
            Vector2 ans = new Vector2(src);
            ans.x /= value;
            ans.y /= value;
            return ans;
        }
        public static Vector2 operator +(Vector2 src)
        {
            return src;
        }
        public static Vector2 operator -(Vector2 src)
        {
            Vector2 ans = new Vector2(-src.x, -src.y);
            return ans;
        }
        public Vector2 reflectionX()
        {
            Vector2 ans = new Vector2(this.x, -this.y);
            return ans;
        }
        public Vector2 reflectionY()
        {
            Vector2 ans = new Vector2(-this.x, this.y);
            return ans;
        }
        public Vector2 translation(float _x, float _y)
        {
            Vector2 ans = new Vector2(this.x + _x, this.y + _y);
            return ans;
        }
        public Vector2 normalize()
        {
            Vector2 ans = new Vector2(this);
            ans /= length();
            return ans;
        }
        public float distanceTo(Vector2 src)
        {
            return Convert.ToSingle(Math.Sqrt((x - src.x) * (x - src.x) + (y - src.y) * (y - src.y)));
        }
        public float distanceToSq(Vector2 src)
        {
            return ((x - src.x) * (x - src.x) + (y - src.y) * (y - src.y));
        }
        public static float operator *(Vector2 src1, Vector2 src2)
        {
            return (src1.x * src2.x + src1.y * src2.y);
        }
        public override int GetHashCode()
        {
            return Convert.ToInt32(x * y); //такая вот хеш-функция
        }
        public override string ToString()
        {
            return ("X is " + ((int)x).ToString() + " ; Y is " + ((int)y).ToString());
        }
        public override bool Equals(Object obj)
        {
            if (!(obj is Vector2)) return false;

            Vector2 p = (Vector2)obj;
            return x == p.x & y == p.y;
        }
        public Vector2 rotation(double alpha)
        {
            //Vector2D help = new Vector2D(this);
            Vector2 ans = new Vector2();
            ans.x = this.x * Convert.ToSingle(Math.Cos(alpha)) - this.y * Convert.ToSingle(Math.Sin(alpha));
            ans.y = this.x * Convert.ToSingle(Math.Sin(alpha)) + this.y * Convert.ToSingle(Math.Cos(alpha));
            return ans;
        }
        public Vector2 rotation(float angle, Vector2 centr)
        {
            Vector2 ans = new Vector2(this);
            ans -= centr;
            ans = ans.rotation(angle);
            ans += centr;
            return ans;
        }
        public Vector2 dilatation(float _x, float _y)
        {
            Vector2 ans = new Vector2(this);
            ans.x *= _x;
            ans.y *= _y;
            return ans;
        }
        public Vector2 dilatation(float _x, float _y, Vector2 centr)
        {
            Vector2 ans = new Vector2(this);
            ans -= centr;
            ans.x *= _x;
            ans.y *= _y;
            ans += centr;
            return ans;
        }
    }
}
