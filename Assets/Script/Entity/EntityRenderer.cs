using System;
using UnityEngine;

namespace Script.Entity
{
    [RequireComponent(typeof(EntityAnimator))]
    public class EntityRenderer : MonoBehaviour
    {
        private EntityAnimator _entityAnimator;

        private void Awake()
        {
            _entityAnimator = GetComponent<EntityAnimator>();
        }
    }
}