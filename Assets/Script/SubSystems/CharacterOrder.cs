using System.Collections.Generic;
using Script.Entity.Character;
using Script.GameBoard;
using UnityEngine;

namespace Script.SubSystems
{
    public class CharacterOrder : MonoBehaviour
    {
        public CreatureController CurrentCreature
        {
            get;
            private set;
        }

        public List<CreatureController> inGameCharacters;

        public void StartCircle(BoardController bc)
        {
            inGameCharacters[0].Move(bc.TileMatrix[0][0]);
            inGameCharacters[1].Move(bc.TileMatrix[1][1]);
            CurrentCreature = Pop();
            CalculateOrder();
        }
        
        public void AddCreature(CreatureController creatureController)
        {
            inGameCharacters.Add(creatureController);
            CalculateOrder();
        }
        
        public void RemoveCreature(CreatureController creatureController)
        {
            inGameCharacters.Remove(creatureController);
            CalculateOrder();
        }

        private void CalculateOrder()
        {
            if(inGameCharacters.Count > 1)
                inGameCharacters.Sort(CompareCharactersBySpeed);
        }

        private static int CompareCharactersBySpeed(CreatureController chrX, CreatureController chrY)
        {
            return chrX.creatureStats.speed.CompareTo(chrY.creatureStats.speed);
        }

        public void NextCharacter()
        {
            Push(CurrentCreature);
            CurrentCreature = Pop();
        }
        
        private void Push(CreatureController creature)
        {
            inGameCharacters.Add(creature);
        }

        private CreatureController Pop()
        {
            var tmpChr = inGameCharacters[0];
            inGameCharacters.Remove(tmpChr);
            return tmpChr;
        }
    }
}