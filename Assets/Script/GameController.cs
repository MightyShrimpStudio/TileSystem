using System;
using Script.GameBoard;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    public class GameController : MonoBehaviour
    {

        public BoardController boardControllerPrefab;

        public event DeSelectDelegate OnDeSelectAll;
        
        private BoardController _boardController;
        
        private void Start()
        {
            BoardController boardController = Instantiate(boardControllerPrefab, transform.position, Quaternion.identity);
            boardController.name = "BoardController";
        }
    }

    public delegate void DeSelectDelegate();
}