using UnityEngine;

namespace Common
{
    public static class Vector2Extension
    {
        // public static Vector2 Rotate(this Vector2 v, float degrees)
        // {
        //     return Quaternion.Euler(0, 0, degrees) * v;
        // }
        
        public static Vector2 Rotate(this Vector2 v, float degrees) {
            float radians = degrees * Mathf.Deg2Rad;
            float sin = Mathf.Sin(radians);
            float cos = Mathf.Cos(radians);
         
            float tx = v.x;
            float ty = v.y;
 
            return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
        }
    }
}