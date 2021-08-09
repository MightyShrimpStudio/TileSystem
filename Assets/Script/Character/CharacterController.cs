using Script.GameBoard;
using Script.GameBoard.Tile;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Character
{
    [RequireComponent(typeof(CharacterRenderer), typeof(Collider))]

    public class CharacterController : MonoBehaviour
    {

        public TileController currentTile;

        private CharacterRenderer _characterRenderer;
        
        private void Awake()
        {
            _characterRenderer = GetComponent<CharacterRenderer>();
        }
        
        public bool OnHit(CharacterController characterController)
        {
            return true;
        }
        
        public void Move(TileController destinationTile)
        {
            currentTile.RemoveCharacter();
            destinationTile.AddCharacter(this);
        }

        public void DeSelect()
        {
            throw new System.NotImplementedException();
        }
    }
}
