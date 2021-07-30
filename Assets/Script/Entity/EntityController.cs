using System;
using UnityEngine;

namespace Script.Entity
{
    [RequireComponent(typeof(EntityRenderer), typeof(Collider))]
    public class EntityController : MonoBehaviour
    {

        private EntityRenderer _entityRenderer;
        private Collider _collider;

        private void Awake()
        {
            _entityRenderer = GetComponent<EntityRenderer>();
            _collider = GetComponent<Collider>();
        }

        public bool OnHit(Character.CharacterController characterController)
        {
            return true;
        }

        public void DeSelect()
        {
            throw new NotImplementedException();
        }
    }
}
