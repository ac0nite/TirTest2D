using System.Collections.Generic;
using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        float distanceTravelled;
        private Vector2[] points = new Vector2[3];
        private VertexPath _testPath;
        private List<int> _indexes;

        void Start() {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }

            points[0] = new Vector2(-1, 0);
            points[1] = new Vector2(2, 0);
            points[2] = new Vector2(0, 3);


            _indexes = pathCreator.bezierPath.AnchorIndexes;
            Debug.Log(points);
            // var points = pathCreator.bezierPath.NumAnchorPoints;
            //
            // Debug.Log(points);
            for (int i = 0; i < _indexes.Count; i++)
            {
                Debug.Log($"[{_indexes[i]}]: {pathCreator.bezierPath.GetPoint(_indexes[i])}");
            }

            _testPath = GeneratePath(points);
        }

        private VertexPath GeneratePath(Vector2[] p)
        {
            BezierPath bezierPath = new BezierPath(p, PathSpace.xy, true);
            return new VertexPath(bezierPath, pathCreator.transform);
        }

        void Update()
        {
            if (pathCreator != null)
            {
                distanceTravelled += speed * Time.deltaTime;
                //transform.position = _testPath.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                //transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                var point = pathCreator.bezierPath.GetPoint(_indexes[0]);
                pathCreator.bezierPath.MovePoint(_indexes[0], point + new Vector3(0, .1f, 0), false);
            }
            
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                var point = pathCreator.bezierPath.GetPoint(_indexes[0]);
                pathCreator.bezierPath.MovePoint(_indexes[0], point + new Vector3(0, -.1f, 0), false);
            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}