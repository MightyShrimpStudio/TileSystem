using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.GameBoard
{
    [RequireComponent(typeof(Collider))]
    public class TileController : MonoBehaviour
    {
        public event OnSelectDelegate OnSelect;
        public event OnDeSelectDelegate OnDeSelect;
        
        public Vector3 size = Vector3.one;
        
        public List<TileController> neighbours = new List<TileController>();
        public Character.CharacterController currentCharacterController;
        public Entity.EntityController currentEntityController;

        private Collider _collider;
        private bool _isHovered = false;
        private bool _isSelected = false;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        public bool IsFreeToMoveIn(Character.CharacterController characterController)
        {
            bool result = true;
            if (currentCharacterController != null) result = currentCharacterController.OnHit(characterController);
            if (currentEntityController != null) result = currentEntityController.OnHit(characterController);
            return result;
        }
        
        
        public void MoveIn(Character.CharacterController characterController)
        {
            currentCharacterController = characterController;
            
        }

        public void MoveOut()
        {
            
            currentCharacterController = null;
        }

        private void OnMouseDown()
        {
            Debug.Log(name + " is selected");
            _isSelected = true;
            OnSelect?.Invoke(name);
        }
        
        void OnMouseOver()
        {
            if (!_isHovered)
            {
                Debug.Log(name + " is hovered");
                _isHovered = true;
            }
        }

        void OnMouseExit()
        {
            Debug.Log(name + " hovering end");
            _isHovered = false;
        }

        public void DeSelect()
        {
            if (!_isSelected)
            {
                _isSelected = false;
                OnDeSelect?.Invoke();
            }
        }
    }

    public delegate void OnSelectDelegate(String name);
    public delegate void OnDeSelectDelegate();
}
