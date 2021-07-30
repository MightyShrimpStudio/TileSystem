using System;
using System.Collections.Generic;
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

        public void Populate()
        {
            for (int i = 0; i < 4; i++)
            {
                _tileMatrix.Add(new List<TileController>());
                for (int j = 0; j < 4; j++)
                {

                    Vector3 position = new Vector3(
                        i*tilePrefab.size.x + i*offset,
                        transform.position.y,
                        j*tilePrefab.size.z + j*offset);
                    TileController tile = Instantiate(tilePrefab, position, Quaternion.identity);
                    _tileMatrix[i].Add(tile);
                    tile.name = "Tile " + i + " " + j;
                    tile.OnSelect += DeSelectAllExcept;
                }
            }
        }

        private void DeSelectAllExcept(String exception)
        {
            foreach (var tileRows in _tileMatrix)
            {
                foreach (var tile in tileRows)
                {
                    if(tile.name != exception) tile.DeSelect();
                }
            }
        }
        
    }
}