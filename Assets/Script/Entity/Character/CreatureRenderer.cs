using UnityEngine;

namespace Script.Entity.Character
{
    [RequireComponent(typeof(CreatureAnimator))]
    public class CreatureRenderer : MonoBehaviour
    {
        private CreatureAnimator _creatureAnimator;

        private void Awake()
        {
            _creatureAnimator = GetComponent<CreatureAnimator>();
        }
    }
}