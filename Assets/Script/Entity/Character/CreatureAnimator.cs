using UnityEngine;

namespace Script.Character
{
    [RequireComponent(typeof(Animator))]
    public class CreatureAnimator : MonoBehaviour
    {
        private const int SliceCount = 4;

        public static readonly string[] DirectionArray =
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
            if (direction.magnitude > 0.01f)
            {
                _lastDirection = DirectionToIndex(direction);
                _animator.Play(DirectionArray[_lastDirection]);
            }
        }

        private int DirectionToIndex(Vector2 direction)
        {
            var nomrDir = direction.normalized;
            var step = 360f / SliceCount;
            var halfStep = step / 2;
            var angle = Vector2.SignedAngle(Vector2.up, nomrDir);
            angle += halfStep;
            if (angle < 0) angle += 360;
            var stepCount = angle / step;
            return Mathf.FloorToInt(stepCount);
        }
    }
}