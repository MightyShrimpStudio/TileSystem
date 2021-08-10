using System;
using System.Collections.Generic;
using Script.GameBoard.Tile;
using UnityEngine;
using UnityEngine.WSA;

namespace Script.GameBoard
{
    public class BoardController : MonoBehaviour
    {
        public TileController tilePrefab;
        public float offset = 0.1f;
        public Vector2 size = new Vector2(4, 4);

        public readonly List<List<TileController>> _tileMatrix = new List<List<TileController>>();

        public void Populate(IsSelectedDelegate selectionMethod)
        {
            for (var i = 0; i < size.x; i++)
            {
                _tileMatrix.Add(new List<TileController>());
                for (var j = 0; j < size.y; j++)
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
            CalculateNeighbours();
        }

        private void CalculateNeighbours()
        {
            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    _tileMatrix[i][j].neighbours.Add(_tileMatrix[(int) ((i+1)%size.x)][(int) ((j)%size.y)]);
                    _tileMatrix[i][j].neighbours.Add(_tileMatrix[(int) (Math.Abs(i-1)%size.x)][(int) ((j)%size.y)]);
                    _tileMatrix[i][j].neighbours.Add(_tileMatrix[(int) ((i)%size.x)][(int) (Math.Abs(j-1)%size.y)]);
                    _tileMatrix[i][j].neighbours.Add(_tileMatrix[(int) ((i)%size.x)][(int) ((j+1)%size.y)]);
                }
            }
        }
    }
}