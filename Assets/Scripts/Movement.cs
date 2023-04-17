using System;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] public Rigidbody2D _rigidbody;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private EdgeCollider2D _edgeCollder;
        private Vector3 _point0;
        private Vector3 _point1;

        private void Start()
        {
             //var dir  = new Vector2(UnityEngine.Random.Range(0,1f), UnityEngine.Random.Range(0,1f));
            var dir  = new Vector2(0.9f, -0.4f);
            _rigidbody.AddForce(dir * UnityEngine.Random.Range(5,5), ForceMode2D.Impulse);
            Debug.Log($"DIR:{dir}");
        }

        private void FixedUpdate()
        {
            Debug.Log($"FU:{_rigidbody.velocity}");
            Draw(_rigidbody.transform.position, _rigidbody.velocity, Color.red);
        }
        
        private void Draw(Vector3 original, Vector3 direction, Color color)
        {
            var ray = new Ray(original, direction);
            Debug.DrawLine(ray.origin, ray.GetPoint(.8f), color, .01f);
            Debug.DrawLine(ray.GetPoint(.8f), ray.GetPoint(.85f), Color.cyan, .01f);
        }

        // private void OnCollisionEnter2D(Collision2D collision)
        // {
        //     Debug.Log($"OnCollisionEnter2D ENEMY");
        //     
        //     var r = new Ray(collision.contacts[0].point, -collision.contacts[0].normal);
        //     Debug.DrawLine(r.origin, r.GetPoint(1f), Color.blue, 1f);
        //     
        //     var v = Vector3.Reflect(_rigidbody.velocity, -collision.contacts[0].normal); 
        //     
        //     r = new Ray(_rigidbody.transform.position, v.normalized);
        //     Debug.DrawLine(r.origin, r.GetPoint(1f), Color.green, 1f);
        //     
        //     _rigidbody.velocity = v; 
        // }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            //contact point is gotten by raycasting in the colliders velocity direction at the colliders position.
            RaycastHit2D[] hit2D = Physics2D.RaycastAll(_collider.transform.position, _rigidbody.velocity);
            // var ray = new Ray(_collider.transform.position, _rigidbody.velocity);
            // Debug.DrawLine(ray.origin, ray.GetPoint(1f), Color.red, 10f);
            //second one is being used because first one is self, could probably ignore self-layer and get as Physics2D.Raycast() instead
            
            Debug.Log($"OnTriggerEnter2D Hits:{hit2D.Length}");
            
            _point0 = hit2D[0].point;
            _point1 = hit2D.Length > 1 ? hit2D[1].point : Vector2.zero;
            
            Draw(_point0, _point1 - _point0, Color.red);

            
            Vector2 contactPoint = hit2D[1].point;
            //Get normal of contact point by creating a line from the contact point to the closest collider point and rotating 90°
            Vector2 normal = Vector2.Perpendicular(contactPoint - GetClosestPoint(_collider.transform.position)).normalized;
            //reflect the current velocity at the edge normal
            _rigidbody.velocity = Vector2.Reflect(_rigidbody.velocity, normal);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_point0, 0.1f);
            Handles.Label(_point0, $"{(Vector2)(_point0)}");
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_point1, 0.1f);
            Handles.Label(_point1, $"{(Vector2)(_point1)}");
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
    }
}