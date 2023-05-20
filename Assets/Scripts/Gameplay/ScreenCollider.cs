using System;
using System.Collections.Generic;
using Gameplay.Enemy;
using UnityEngine;

namespace Gameplay
{
    public class ScreenCollider : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private EdgeCollider2D _edgeCollider;
        [SerializeField] private int _offset = 0;
        [SerializeField] private bool _isTriggerCollider = false;
        [SerializeField] private bool _drawSide = false;


        private void Start()
        {
            InitialiseEdgeCollider();
            _edgeCollider.isTrigger = _isTriggerCollider;
        }

        private void InitialiseEdgeCollider()
        {
            var edges = new List<Vector2>();
            edges.Add(_camera.ScreenToWorldPoint(new Vector2(-_offset, -_offset)));
            edges.Add(_camera.ScreenToWorldPoint(new Vector2(Screen.width + _offset, -_offset)));
            edges.Add(_camera.ScreenToWorldPoint(new Vector2(Screen.width + _offset, Screen.height + _offset)));
            edges.Add(_camera.ScreenToWorldPoint(new Vector2(-_offset, Screen.height + _offset)));
            edges.Add(_camera.ScreenToWorldPoint(new Vector2(-_offset, -_offset)));
            _edgeCollider.SetPoints(edges);
        }

        private void Draw(Vector3 original, Vector3 direction, Color color)
        {
            var ray = new Ray(original, direction);
            Debug.DrawLine(ray.origin, ray.GetPoint(1), color, .1f);
            Debug.DrawLine(ray.GetPoint(1), ray.GetPoint(1.1f), Color.cyan, .1f);
        }

        private void OnDrawGizmos()
        {
            if(!_drawSide)
                return;
            
            Gizmos.color = Color.red;
            var points = _edgeCollider.points;
            for (int i = 1; i < points.Length; i++)
            {
                Gizmos.DrawLine(points[i-1], points[i]);
            }
        }

        // void OnCollisionEnter2D(Collision2D collision)
        // {
        //     TryToReflect(collision.collider);
        // }
        //
        // void OnCollisionStay2D(Collision2D collision)
        // {
        //     TryToReflect(collision.collider);
        // }


        private void OnTriggerStay2D(Collider2D collider)
        {
            TryToReflect(collider);
        }
        
        private void OnTriggerEnter2D(Collider2D collider)
        {
            TryToReflect(collider);
        }

        private void TryToReflect(Collider2D collider)
        {
            
            if (collider.TryGetComponent<IBird>(out IBird bird))
            {
                //var movement = collider.GetComponent<IMovement>();
                RaycastHit2D[] hit2D = Physics2D.RaycastAll(collider.transform.position, bird.Movement.Velocity);
            
                Vector2 contactPoint = hit2D[1].point;
                Vector2 normal = Vector2.Perpendicular(contactPoint - GetClosestPoint(collider.transform.position)).normalized;
                bird.Movement.Velocity = Vector2.Reflect(bird.Movement.Velocity, normal);   
            }
        }

        // void OnTriggerEnter2D(Collider2D collider)
        // {
        //     //Debug.Log($"OnTriggerEnter2D");
        //     
        //     Rigidbody2D colliderRB = collider.GetComponent<Rigidbody2D>();
        //     //contact point is gotten by raycasting in the colliders velocity direction at the colliders position.
        //     RaycastHit2D[] hit2D = Physics2D.RaycastAll(collider.transform.position, colliderRB.velocity);
        //     //second one is being used because first one is self, could probably ignore self-layer and get as Physics2D.Raycast() instead
        //
        //     // var ray = new Ray(colliderRB.position, colliderRB.velocity);
        //     // Debug.DrawLine(ray.origin, ray.GetPoint(1), Color.red, 5f);
        //     
        //     Debug.Log($"OnTriggerEnter2D Hits:{hit2D.Length}", colliderRB.gameObject);
        //     //Vector2 contactPoint = hit2D.Length > 1 ? hit2D[1].point : hit2D[0].point;
        //     
        //     Vector2 contactPoint = hit2D[1].point;
        //     //Get normal of contact point by creating a line from the contact point to the closest collider point and rotating 90°
        //     Vector2 normal = Vector2.Perpendicular(contactPoint - GetClosestPoint(collider.transform.position)).normalized;
        //     //reflect the current velocity at the edge normal
        //     colliderRB.velocity = Vector2.Reflect(colliderRB.velocity, normal);
        //     
        //     // ray = new Ray(colliderRB.position, colliderRB.velocity);
        //     
        //     // Debug.DrawLine(ray.origin, ray.GetPoint(1), Color.green, 5f);
        //     // Debug.Log($"{colliderRB.velocity} -> {colliderRB.velocity.normalized}");
        //     //Debug.DrawLine(colliderRB.position, colliderRB.velocity.normalized, Color.green, 1f);
        // }

        Vector2 GetClosestPoint(Vector2 position)
        {
            Vector2[] points = _edgeCollider.points;
            float shortestDistance = Vector2.Distance(position, points[0]);
            Vector2 closestPoint = points[0];
            foreach (Vector2 point in points)
            {
                if (Vector2.Distance(position, point) < shortestDistance)
                {
                    shortestDistance = Vector2.Distance(position, point);
                    closestPoint = point;
                }
            }
        
            return closestPoint;
        }
    }
}