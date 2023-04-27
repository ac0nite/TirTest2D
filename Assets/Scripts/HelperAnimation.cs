using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class HelperAnimation : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _spriteRenderer = GetComponentInParent<SpriteRenderer>();
            _rigidbody = GetComponentInParent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _spriteRenderer.flipX = _rigidbody.velocity.x < 0;
        }
    }
}