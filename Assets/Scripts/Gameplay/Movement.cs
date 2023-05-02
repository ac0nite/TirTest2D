using UnityEditor;
using UnityEngine;

namespace Gameplay
{
    public interface IMovement
    {
        Vector2 Velocity { get; set; }
    }
    public class Movement : MonoBehaviour, IMovement
    {
        [SerializeField] public Rigidbody2D _rigidbody;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private EdgeCollider2D _edgeCollder;
        private Vector3 _point0;
        private Vector3 _point1;
        private float _t;
        
        [SerializeField] private Vector2 _direction = Vector2.zero;
        [Range(0f, 20f), SerializeField] private float _force = 1f;
        [Range(0f,10f), SerializeField] private float _speed = 1;
        [Range(0f,90f), SerializeField] private float _amplitude = 1;
        private float _degress;
        
        private Vector2 _defaultVelocity = Vector3.zero;
        private int _sign = 1;

        public Rigidbody2D Rigidbody => _rigidbody;

        private void Start()
        {
             //var dir  = new Vector2(UnityEngine.Random.Range(0,1f), UnityEngine.Random.Range(0,1f));
            //var dir  = new Vector2(0.9f, -0.4f);
            //var dir = _direction;
            //_force = UnityEngine.Random.Range(1f, 2f);
            //_rigidbody.AddForce(dir.normalized * _force, ForceMode2D.Impulse);
            //Debug.Log($"DIR:{dir}");

            Velocity = _direction * _force;
        }
        
        
        private void Draw(Vector3 original, Vector3 direction, Color color)
        {
            var ray = new Ray(original, direction);
            Debug.DrawLine(ray.origin, ray.GetPoint(.8f), color, .1f);
            Debug.DrawLine(ray.GetPoint(.8f), ray.GetPoint(.85f), Color.cyan, .1f);
        }

        private void FixedUpdate()
        {
            //Draw(_rigidbody.position, _rigidbody.velocity, Color.green);
            //_rigidbody.velocity = _rigidbody.velocity.Rotate(DegressWithFixedUpdate);

            // var angle = DegressWithFixedUpdate;
            // var dir = _direction.Rotate(angle);
            // Draw(transform.position, _direction, Color.red);
            // Draw(transform.position, dir, Color.green);
            
            
            var angle = DegressWithFixedUpdate;
            // var dir = _defaultVelocity.Rotate(angle);
            _rigidbody.velocity = _defaultVelocity.Rotate(angle);
            Draw(_rigidbody.position, _defaultVelocity, Color.red);
            //Draw(transform.position, dir, Color.green);
            Draw(_rigidbody.position, _rigidbody.velocity, Color.green);
        }

        private float DegressWithUpdate => _sign * _amplitude * Mathf.Sin(_t += Time.deltaTime * _speed);
        private float DegressWithFixedUpdate => _sign * _amplitude * Mathf.Sin(_t += Time.fixedDeltaTime * _speed);
        private float DegressWithLateUpdate => _sign * _amplitude * Mathf.Sin(_t += Time.fixedDeltaTime * _speed);

        // private void OnCollisionEnter2D(Collision2D collision)
        // {
        //     // Debug.Log($"{collision.contacts.Length}", collision.collider.gameObject);
        //     // Velocity = Vector3.Reflect(_rigidbody.velocity, collision.contacts[0].normal);
        // }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            //Debug.Log($"OnCollisionStay2D");
            var normal = (-collision.contacts[0].normal).Rotate(45);
            var reflect = Vector3.Reflect(_rigidbody.velocity, normal);
            
            Draw(_rigidbody.position, reflect, Color.blue);
            Draw(_rigidbody.position, normal, Color.magenta);
            
            Velocity = reflect;
        }
        
        
        private void OnDrawGizmos()
            {
            // Gizmos.color = Color.red;
            // Gizmos.DrawSphere(_point0, 0.1f);
            // Handles.Label(_point0, $"{(Vector2)(_point0)}");
            // Gizmos.color = Color.green;
            // Gizmos.DrawSphere(_point1, 0.1f);
            
            var pos = transform.position;
            pos.x += 0.2f;
            
            Handles.Label(pos, $"[{_rigidbody.velocity.magnitude}]");
        }
        
        Vector2 GetClosestPoint(Vector2 position)
        {
            Vector2[] points = _edgeCollder.points;
            float shortestDistance = Vector2.Distance(position,points[0]);
            Vector2 closestPoint = points[0];
            foreach(Vector2 point in points)
            {
                if(Vector2.Distance(position,point) < shortestDistance)
                {
                    shortestDistance = Vector2.Distance(position,point);
                    closestPoint = point;
                }
            }
            return closestPoint;
        }

        public Vector2 Velocity
        {
            get => _rigidbody.velocity;
            set
            {
                _t = 0f;
                _rigidbody.velocity = value.normalized * _force;
                _defaultVelocity = _rigidbody.velocity;
                _sign = RandomSign;
                
                // Debug.Log($"{_sign}");
            }
        }

        private int RandomSign => UnityEngine.Random.value < .5 ? 1 : -1;
        //private int RandomSign => (int) ((UnityEngine.Random.Range(0,2) - 0.5) * 2);
    }

    public static class Vector2Extension
    {
        public static Vector2 Rotate(this Vector2 v, float degrees)
        {
            return Quaternion.Euler(0, 0, degrees) * v;
        }
        
        public static Vector2 Rotate2(this Vector2 v, float degrees) {
            float radians = degrees * Mathf.Deg2Rad;
            float sin = Mathf.Sin(radians);
            float cos = Mathf.Cos(radians);
         
            float tx = v.x;
            float ty = v.y;
 
            return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
        }
    }
}