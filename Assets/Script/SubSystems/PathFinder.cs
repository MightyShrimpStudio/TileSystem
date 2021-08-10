using System.Collections.Generic;
using Script.Entity.Character;
using Script.GameBoard.Tile;

namespace Script.SubSystems
{
    public class PathFinder
    {
        private HashSet<TileController> tiles;
        
        public void Allow(CreatureController creatureController)
        {
            TileController starterTile = creatureController.CurrentTile;
            int move = creatureController.creatureStats.move;
            tiles = starterTile.RecursiveNeighbourSearch(move, new HashSet<TileController>());
            foreach (var tile in tiles)
            {
                tile.isSelectable = true;
            }
        }
        
        public void Cleanup()
        {
            foreach (var tile in tiles)
            {
                tile.isSelectable = false;
            }
        }
    }
}