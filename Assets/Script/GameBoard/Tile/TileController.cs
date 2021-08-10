using System.Collections.Generic;
using Script.Entity.Character;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.GameBoard.Tile
{
    [RequireComponent(typeof(TileRenderer), typeof(Collider))]
    public class TileController : MonoBehaviour
    {
        public List<TileController> neighbours = new List<TileController>();

        [FormerlySerializedAs("currentCharacter")]
        public CreatureController currentCreatureController;

        public TileRenderer MyTileRender { get; private set; }
        public bool isSelectable = true;

        public event IsSelectedDelegate OnSelect;

        private void Awake()
        {
            MyTileRender = GetComponent<TileRenderer>();
        }

        public void RemoveCharacter()
        {
            currentCreatureController = null;
        }

        public void AddCharacter(CreatureController creatureController)
        {
            currentCreatureController = creatureController;
        }
        
        private void OnMouseDown()
        {
            if (isSelectable)
            {
                OnSelect?.Invoke(this);
            }
        }

        /*private void OnMouseExit()
        {
        }

        private void OnMouseOver()
        {
        }*/
    }

    public delegate void IsSelectedDelegate(TileController selectedTile);
}