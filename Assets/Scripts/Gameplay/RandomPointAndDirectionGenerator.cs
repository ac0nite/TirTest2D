using UnityEngine;

namespace DefaultNamespace
{
    public class RandomPointAndDirectionGenerator
    {
        public Vector2 _begin, _end;
        float _range;
        public RandomPointAndDirectionGenerator(
            float offsetViewportX,
            float offsetViewportY,
            float dirRange)
        {
            _begin = Camera.main.ViewportToWorldPoint(new Vector2(0 + offsetViewportX,1 + offsetViewportY));
            _end = Camera.main.ViewportToWorldPoint(new Vector2(1 - offsetViewportX,1 + offsetViewportY));
            _range = dirRange;
        }

        public Data Random()
        {
            var point = Point();
            return new Data() {Point = point, Direction = Direction(point.x)};
        }

        private Vector2 Point()
        {
            return Vector2.Lerp(_begin, _end, UnityEngine.Random.Range(-1f,1f));
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

        public void DebugDraw()
        {
            var data = Random();
            Debug.DrawRay(data.Point, data.Direction, Color.cyan, 1f);
            Debug.DrawLine(_begin, _end, Color.red, 1f);
        }
        
        public struct Data
        {
            public Vector2 Point;
            public Vector2 Direction;
        }
    }
}