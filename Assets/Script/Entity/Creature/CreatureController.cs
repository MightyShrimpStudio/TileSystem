using Script.GameBoard.Tile;
using UnityEngine;

namespace Script.Entity.Creature
{
    [RequireComponent(typeof(CreatureRenderer))]
    public class CreatureController : MonoBehaviour
    {
        public bool isSpawned;

        private CreatureRenderer _creatureRenderer;
        public CreatureStats CreatureStats { get; set; }
        public TileController CurrentTile { get; private set; }

        private void Awake()
        {
            _creatureRenderer = GetComponent<CreatureRenderer>();
        }

        public void Move(TileController destinationTile)
        {
            if (isSpawned) CurrentTile.RemoveCharacter();
            destinationTile.AddCharacter(this);
            CurrentTile = destinationTile;
            var position = new Vector3(CurrentTile.transform.position.x,
                CurrentTile.transform.position.y + 1,
                CurrentTile.transform.position.z);
            transform.position = position;
        }
    }
}