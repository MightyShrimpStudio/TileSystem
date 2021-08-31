using System.Collections.Generic;
using System.Linq;
using Script.Entity.Character;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace Script.GameBoard.Tile
{
    [RequireComponent(typeof(TileRenderer), typeof(Collider))]
    public class TileController : MonoBehaviour
    {
        public List<TileController> neighbours = new List<TileController>();
        
        public CreatureController currentCreatureController;

        public bool IsSelectable
        {
            get => _isSelectable;
            set
            {
                _isSelectable = value;
                MyTileRender.SetSelectable(value);
            }
        }

        public TileRenderer MyTileRender { get; private set; }

        private bool _isSelectable = false;
        
        private void Awake()
        {
            MyTileRender = GetComponent<TileRenderer>();
        }

        private void OnMouseDown()
        {
            if (IsSelectable) OnSelect?.Invoke(this);
        }

        public event IsSelectedDelegate OnSelect;

        public void RemoveCharacter()
        {
            currentCreatureController = null;
        }

        public void AddCharacter(CreatureController creatureController)
        {
            currentCreatureController = creatureController;
        }

        public HashSet<TileController> RecursiveNeighbourSearch(int move, HashSet<TileController> tiles)
        {
            if (move != 0)
                tiles = neighbours.Aggregate(tiles,
                    (current, neighbour) => neighbour.RecursiveNeighbourSearch(move - 1, current));

            tiles.Add(this);
            return tiles;
        }

        public bool IsFree()
        {
            return currentCreatureController == null;
        }
    }

    public delegate void IsSelectedDelegate(TileController selectedTile);
}