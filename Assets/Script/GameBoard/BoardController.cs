using System;
using System.Collections.Generic;
using Script.GameBoard.Tile;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.GameBoard
{
    public class BoardController : MonoBehaviour
    {

        public TileController tilePrefab;
        public float offset = 0.1f;
        
        private List<List<TileController>> _tileMatrix = new List<List<TileController>>();

        private void Start()
        {
            Populate();
        }

        private void Populate()
        {
            for (int i = 0; i < 4; i++)
            {
                _tileMatrix.Add(new List<TileController>());
                for (int j = 0; j < 4; j++)
                {
                    TileController tile = Instantiate(tilePrefab, Vector3.zero, Quaternion.identity);
                    Vector3 position = new Vector3(
                        i*tile.MyTileRender.size.x + i*offset,
                        transform.position.y,
                        j*tile.MyTileRender.size.z + j*offset);
                    tile.transform.position = position;
                    _tileMatrix[i].Add(tile);
                    tile.name = "Tile " + i + " " + j;
                }
            }
        }
    }
}