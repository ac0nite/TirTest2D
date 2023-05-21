using System;
using System.Collections.Generic;
using Gameplay.Enemy.BirdItem;
using UnityEngine;

namespace Gameplay
{
    //bind in Scene
    public class ScreenCollider : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private EdgeCollider2D _edgeCollider;
        [SerializeField] private int _offset = 0;
        [SerializeField] private bool _isTriggerCollider = false;
        [SerializeField] private bool _drawSide = false;


        private RaycastHit2D[] _hits;
        private int _hitsCount;

        private void Start()
        {
            _hits = new RaycastHit2D[10];
            
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
            if (collider.TryGetComponent(out Bird bird))
            {
                Array.Clear(_hits, 0, 10);
                _hitsCount = Physics2D.RaycastNonAlloc(bird.transform.position, bird.Movement.Velocity, _hits);

                // if (_hitsCount <= 1)
                // {
                //     Debug.Log($"STOP {bird.name}", bird);
                // }

                Vector2 contactPoint = _hits[1].point;
                Vector2 normal = Vector2.Perpendicular(contactPoint - GetClosestPoint(bird.transform.position)).normalized;
                bird.Movement.Velocity = Vector2.Reflect(bird.Movement.Velocity, normal);   
            }
        }

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