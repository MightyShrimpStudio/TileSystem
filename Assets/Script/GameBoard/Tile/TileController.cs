using System.Collections.Generic;
using Script.Entity.Character;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.GameBoard.Tile
{
    [RequireComponent(typeof(TileRenderer))]
    public class TileController : MonoBehaviour
    {
        public List<TileController> neighbours = new();

        [FormerlySerializedAs("currentCharacter")]
        public CreatureController currentCreatureController;

        public TileRenderer MyTileRender { get; private set; }

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
    }
}