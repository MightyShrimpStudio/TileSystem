using System;
using UnityEngine;

namespace Script.Character
{
    [RequireComponent(typeof(Animator))]
    public class CreatureAnimator : MonoBehaviour
    {
        
        private Animator _animator;
        private int _lastDirection;
        private const int SliceCount = 4;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        
        public void SetDirection(Vector2 direction)
        {
            if (direction.magnitude > 0.01f)
            {
                _lastDirection = DirectionToIndex(direction);
                _animator.Play(DirectionArray[_lastDirection]);
            }
        }

        private int DirectionToIndex(Vector2 direction)
        {
            Vector2 nomrDir = direction.normalized;
            float step = 360f / SliceCount;
            float halfStep = step / 2;
            float angle = Vector2.SignedAngle(Vector2.up, nomrDir);
            angle += halfStep;
            if (angle < 0)
            {
                angle += 360;
            }
            float stepCount = angle / step;
            return Mathf.FloorToInt(stepCount);
        }
        
        public static readonly string[] DirectionArray =
        {
            "DirUp",  "DirLeft",  "DirDown",  "DirRight"
        };
    }
}