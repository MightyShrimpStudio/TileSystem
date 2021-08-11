using System.Collections.Generic;
using Script.Entity.Character;
using Script.GameBoard.Tile;

namespace Script.SubSystems
{
    public class PathFinder
    {
        private HashSet<TileController> _tiles;

        public void Allow(CreatureController creatureController)
        {
            var starterTile = creatureController.CurrentTile;
            var move = creatureController.creatureStats.move;
            _tiles = starterTile.RecursiveNeighbourSearch(move, new HashSet<TileController>());
            foreach (var tile in _tiles) tile.isSelectable = true;
        }

        public void Cleanup()
        {
            foreach (var tile in _tiles) tile.isSelectable = false;
        }
    }
}