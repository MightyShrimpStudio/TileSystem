using Script.GameBoard;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Character
{
    [RequireComponent(typeof(CharacterRenderer), typeof(Collider))]

    public class CharacterController : MonoBehaviour
    {

        [FormerlySerializedAs("currentTileChontroller")] [FormerlySerializedAs("currentTile")] public TileController currentTileController;

        private CharacterRenderer _characterRenderer;
        
        private void Awake()
        {
            _characterRenderer = GetComponent<CharacterRenderer>();
        }
        
        public bool OnHit(CharacterController characterController)
        {
            return true;
        }
        
        public void Move(TileController to)
        {
            if (to.IsFreeToMoveIn(this))
            {
                currentTileController.MoveOut();
                to.MoveIn(this);
                currentTileController = to;
            }
        }

        public void DeSelect()
        {
            throw new System.NotImplementedException();
        }
    }
}
