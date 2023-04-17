using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class ScreenCollider : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private EdgeCollider2D _edgeCollider;


        private void Awake()
        {
            InitialiseEdgeCollider();
        }

        private void InitialiseEdgeCollider()
        {
            var edges = new List<Vector2>();
            edges.Add(_camera.ScreenToWorldPoint(Vector3.zero));
            edges.Add(_camera.ScreenToWorldPoint(new Vector2(Screen.width, 0)));
            edges.Add(_camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)));
            edges.Add(_camera.ScreenToWorldPoint(new Vector2(0, Screen.height)));
            edges.Add(_camera.ScreenToWorldPoint(Vector3.zero));
            _edgeCollider.SetPoints(edges);
        }

        // void OnCollisionEnter2D(Collision2D collision)
        // {
        //     Rigidbody2D rigidbody = collision.transform.GetComponent<Rigidbody2D>();
        //     
        //     //Debug.Log($"Collision:{rigidbody.velocity}");
        //     
        //     // Draw(rigidbody.transform.position, rigidbody.velocity, Color.black);
        //     // Draw(collision.contacts[0].point, -collision.contacts[0].normal, Color.blue);
        //     
        //     var reflect = Vector2.Reflect(rigidbody.velocity, -collision.contacts[0].normal);
        //     
        //     rigidbody.velocity = reflect;
        //     
        //     //Draw(rigidbody.position, rigidbody.velocity, Color.black);
        // }

        private void Draw(Vector3 original, Vector3 direction, Color color)
        {
            var ray = new Ray(original, direction);
            Debug.DrawLine(ray.origin, ray.GetPoint(1), color, .01f);
            Debug.DrawLine(ray.GetPoint(1), ray.GetPoint(1.1f), Color.cyan, .01f);
        }
        
        // void OnTriggerEnter2D(Collider2D collider)
        // {
        //     Rigidbody2D colliderRB = collider.GetComponent<Rigidbody2D>();
        //     //contact point is gotten by raycasting in the colliders velocity direction at the colliders position.
        //     RaycastHit2D[] hit2D = Physics2D.RaycastAll(collider.transform.position, colliderRB.velocity);
        //     //second one is being used because first one is self, could probably ignore self-layer and get as Physics2D.Raycast() instead
        //
        //     var ray = new Ray(colliderRB.position, colliderRB.velocity);
        //     Debug.DrawLine(ray.origin, ray.GetPoint(1), Color.red, 5f);
        //     
        //     Debug.Log($"OnTriggerEnter2D Hits:{hit2D.Length}", colliderRB.gameObject);
        //     Vector2 contactPoint = hit2D.Length > 1 ? hit2D[1].point : hit2D[0].point;
        //     //Get normal of contact point by creating a line from the contact point to the closest collider point and rotating 90°
        //     Vector2 normal = Vector2.Perpendicular(contactPoint - GetClosestPoint(collider.transform.position)).normalized;
        //     //reflect the current velocity at the edge normal
        //     colliderRB.velocity = Vector2.Reflect(colliderRB.velocity, normal);
        //     
        //     ray = new Ray(colliderRB.position, colliderRB.velocity);
        //     Debug.DrawLine(ray.origin, ray.GetPoint(1), Color.green, 5f);
        //     Debug.Log($"{colliderRB.velocity} -> {colliderRB.velocity.normalized}");
        //     //Debug.DrawLine(colliderRB.position, colliderRB.velocity.normalized, Color.green, 1f);
        // }
        //
        //
        // //Goes through edgeCollider Points and returns the one closest to position
        // Vector2 GetClosestPoint(Vector2 position)
        // {
        //     Vector2[] points = _edgeCollider.points;
        //     float shortestDistance = Vector2.Distance(position, points[0]);
        //     Vector2 closestPoint = points[0];
        //     foreach (Vector2 point in points)
        //     {
        //         if (Vector2.Distance(position, point) < shortestDistance)
        //         {
        //             shortestDistance = Vector2.Distance(position, point);
        //             closestPoint = point;
        //         }
        //     }
        //
        //     return closestPoint;
        // }
    }
}