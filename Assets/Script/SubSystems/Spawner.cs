using System.Collections.Generic;
using Script.Entity.Character;
using Script.GameBoard.Tile;
using UnityEngine;

namespace Script.SubSystems
{
    public class Spawner : MonoBehaviour
    {
        public void SpawnCreatures(List<CreatureController> creatureList, List<TileController> tileList)
        {
            
        }

        public void SpawnCreature(CreatureController creature, TileController tile)
        {
            Instantiate(creature);
            creature.Move(tile);
        }
    }
}