using System;
using System.Collections.Generic;
using Script.GameBoard.Tile;
using UnityEngine;

namespace Script.GameBoard
{
    public class BoardController : MonoBehaviour
    {
        public TileController tilePrefab;
        public float offset = 0.1f;
        public Vector2 size = new Vector2(4, 4);

        public readonly List<List<TileController>> TileMatrix = new List<List<TileController>>();
        public List<List<Vector2>> SpawnAreas = new List<List<Vector2>>();

        public void Populate(IsSelectedDelegate selectionMethod)
        {
            for (var i = 0; i < size.x; i++)
            {
                TileMatrix.Add(new List<TileController>());
                for (var j = 0; j < size.y; j++)
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

            List<Vector2> spawnZone = SpawnAreas[teamNumber];

            for (int i = (int) spawnZone[0].x; i <= spawnZone[1].x; i++)
            {
                for (int j = (int) spawnZone[0].y; j <= spawnZone[1].y; j++)
                {
                    spawnAreaByTeam.Add(TileMatrix[i][j]);
                }
            }
            
            return spawnAreaByTeam;
        }
        
        private void CalculateNeighbours()
        {
            for (var i = 0; i < size.x; i++)
            for (var j = 0; j < size.y; j++)
            {
                TileMatrix[i][j].neighbours.Add(TileMatrix[(int) ((i + 1) % size.x)][(int) (j % size.y)]);
                TileMatrix[i][j].neighbours.Add(TileMatrix[(int) (Math.Abs(i - 1) % size.x)][(int) (j % size.y)]);
                TileMatrix[i][j].neighbours.Add(TileMatrix[(int) (i % size.x)][(int) (Math.Abs(j - 1) % size.y)]);
                TileMatrix[i][j].neighbours.Add(TileMatrix[(int) (i % size.x)][(int) ((j + 1) % size.y)]);
            }
        }
    }
}