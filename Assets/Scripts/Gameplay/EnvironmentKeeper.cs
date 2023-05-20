using UnityEngine;

namespace Gameplay
{
    public class EnvironmentKeeper : MonoBehaviour
    {
        [SerializeField] public SpriteRenderer BackgroundSpriteRenderer;
        [SerializeField] public Transform CannonSpawnPoint;
        [SerializeField] public Transform EdgesParentTransform;
    }
}