using System.Collections.Generic;
using Script.Entity.Creature;
using Script.GameBoard.Tile;
using UnityEngine;

namespace Script.GameController.SubSystems
{
    public class Spawner : MonoBehaviour
    {
        public void SpawnCreatures(List<CreatureController> creatureList, List<TileController> tileList)
        {
            foreach (var creature in creatureList)
            foreach (var tile in tileList)
                if (tile.IsFree())
                {
                    SpawnCreature(creature, tile);
                    break;
                }
        }

        public void SpawnCreature(CreatureController creature, TileController tile)
        {
            creature.Move(tile);
            creature.isSpawned = true;
            Debug.Log(creature.name + " is spawned");
        }
    }
}