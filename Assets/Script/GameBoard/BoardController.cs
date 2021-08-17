using System;
using System.Collections.Generic;
using Script.GameBoard.Tile;
using Script.ScriptableObjects;
using UnityEngine;

namespace Script.GameBoard
{
    public class BoardController : MonoBehaviour
    {
        public TileController tilePrefab;
        public float offset = 0.1f;
        public GameBoardStats gameBoardStats;

        public readonly List<List<TileController>> TileMatrix = new List<List<TileController>>();

        public void Populate(IsSelectedDelegate selectionMethod)
        {
            for (var i = 0; i < gameBoardStats.size.x; i++)
            {
                TileMatrix.Add(new List<TileController>());
                for (var j = 0; j < gameBoardStats.size.y; j++)
                {
                    var tile = Instantiate(tilePrefab, Vector3.zero, Quaternion.identity);
                    var position = new Vector3(
                        i * tile.MyTileRender.size.x + i * offset,
                        transform.position.y,
                        j * tile.MyTileRender.size.z + j * offset);
                    tile.transform.position = position;
                    TileMatrix[i].Add(tile);
                    tile.name = "Tile " + i + " " + j;
                    tile.OnSelect += selectionMethod;
                }
            }

            CalculateNeighbours();
        }

        public List<TileController> GETSpawnAreaByTeam(int teamNumber)
        {
            List<TileController> spawnAreaByTeam = new List<TileController>();

            Vector4 spawnZone = gameBoardStats.SpawnAreas[teamNumber];

            for (int i = (int) spawnZone.x; i <= spawnZone.z; i++)
            {
                for (int j = (int) spawnZone.y; j <= spawnZone.w; j++)
                {
                    spawnAreaByTeam.Add(TileMatrix[i][j]);
                }
            }
            
            return spawnAreaByTeam;
        }
        
        private void CalculateNeighbours()
        {
            for (var i = 0; i < gameBoardStats.size.x; i++)
            for (var j = 0; j < gameBoardStats.size.y; j++)
            {
                TileMatrix[i][j].neighbours.Add(TileMatrix[(int) ((i + 1) % gameBoardStats.size.x)][(int) (j % gameBoardStats.size.y)]);
                TileMatrix[i][j].neighbours.Add(TileMatrix[(int) (Math.Abs(i - 1) % gameBoardStats.size.x)][(int) (j % gameBoardStats.size.y)]);
                TileMatrix[i][j].neighbours.Add(TileMatrix[(int) (i % gameBoardStats.size.x)][(int) (Math.Abs(j - 1) % gameBoardStats.size.y)]);
                TileMatrix[i][j].neighbours.Add(TileMatrix[(int) (i % gameBoardStats.size.x)][(int) ((j + 1) % gameBoardStats.size.y)]);
            }
        }
    }
}