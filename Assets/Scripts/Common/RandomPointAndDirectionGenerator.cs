using UnityEngine;

namespace Common
{
    public class RandomPointAndDirectionGenerator
    {
        private readonly Vector2 _begin, _end;
        private readonly float _range;

        public RandomPointAndDirectionGenerator(
            Camera camera,
            float offsetViewportX,
            float offsetViewportY,
            float dirRange)
        {
            _begin = camera.ViewportToWorldPoint(new Vector2(0 + offsetViewportX,1 + offsetViewportY));
            _end = camera.ViewportToWorldPoint(new Vector2(1 - offsetViewportX,1 + offsetViewportY));
            _range = dirRange;
        }

        public RandomData Random()
        {
            var point = Point();
            return new RandomData() {Point = point, Direction = Direction(point.x)};
        }

        private Vector2 Point()
        {
            return Vector2.Lerp(_begin, _end, UnityEngine.Random.Range(0f,1f));
        }

        private Vector2 Direction(float sign = 0)
        {
            if (sign == 0)
                return Vector2.zero - new Vector2(UnityEngine.Random.Range(_range,_range), UnityEngine.Random.Range(1f, 1f));
            if(sign > 0) 
                return Vector2.zero - new Vector2(UnityEngine.Random.Range(0,_range), UnityEngine.Random.Range(1f, 1f));
            else
                return Vector2.zero - new Vector2(UnityEngine.Random.Range(-_range,0), UnityEngine.Random.Range(1f, 1f));
        }

        public void DebugDraw(RandomData data)
        {
            Debug.DrawRay(data.Point, data.Direction, Color.cyan, 2f);
            Debug.DrawLine(_begin, _end, Color.red, 2f);
        }
        
        public struct RandomData
        {
            public Vector2 Point;
            public Vector2 Direction;
        }
    }
}