using System.Collections.Generic;
using Script.GameBoard.Tile;
using UnityEngine;

namespace Script.GameBoard
{
    public class BoardController : MonoBehaviour
    {
        public TileController tilePrefab;
        public float offset = 0.1f;

        private readonly List<List<TileController>> _tileMatrix = new List<List<TileController>>();

        public void Populate(IsSelectedDelegate selectionMethod)
        {
            for (var i = 0; i < 4; i++)
            {
                _tileMatrix.Add(new List<TileController>());
                for (var j = 0; j < 4; j++)
                {
                    var tile = Instantiate(tilePrefab, Vector3.zero, Quaternion.identity);
                    var position = new Vector3(
                        i * tile.MyTileRender.size.x + i * offset,
                        transform.position.y,
                        j * tile.MyTileRender.size.z + j * offset);
                    tile.transform.position = position;
                    _tileMatrix[i].Add(tile);
                    tile.name = "Tile " + i + " " + j;
                    tile.OnSelect += selectionMethod;
                }
            }
        }
    }
}