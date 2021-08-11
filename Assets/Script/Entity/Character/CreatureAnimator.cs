using UnityEngine;

namespace Script.Entity.Character
{
    [RequireComponent(typeof(Animator))]
    public class CreatureAnimator : MonoBehaviour
    {
        private const int SliceCount = 4;
        private const float Step = 360f / SliceCount;

        private static readonly string[] DirectionArray =
        {
            "DirUp", "DirLeft", "DirDown", "DirRight"
        };

        private Animator _animator;
        private int _lastDirection;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetDirection(Vector2 direction)
        {
            if (!(direction.magnitude > 0.01f)) return;
            _lastDirection = DirectionToIndex(direction);
            _animator.Play(DirectionArray[_lastDirection]);
        }

        private static int DirectionToIndex(Vector2 direction)
        {
            const float halfStep = Step / 2;
            var angle = Vector2.SignedAngle(Vector2.up, direction.normalized);
            angle += halfStep;
            if (angle < 0) angle += 360;
            var stepCount = angle / Step;
            return Mathf.FloorToInt(stepCount);
        }
    }
}