using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Script.Entity.Character;
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

        public List<CreatureController> InGameCharacters;

        public void StartCircle()
        {
            CurrentCreature = Pop();
            CalculateOrder();
        }
        
        public void AddCreature(CreatureController creatureController)
        {
            InGameCharacters.Add(creatureController);
            CalculateOrder();
        }
        
        public void RemoveCreature(CreatureController creatureController)
        {
            InGameCharacters.Remove(creatureController);
            CalculateOrder();
        }

        public void CalculateOrder()
        {
            InGameCharacters.Sort(CompareCharactersBySpeed);
        }

        private static int CompareCharactersBySpeed(CreatureController chrX, CreatureController chrY)
        {
            if (chrX == null)
                return chrY == null ? 0 : -1;
            return chrY == null ? 1 : chrX.speed.CompareTo(chrY.speed);
        }

        public void NextCharacter()
        {
            Push(CurrentCreature);
            CurrentCreature = Pop();
        }
        
        private void Push(CreatureController creature)
        {
            InGameCharacters.Add(creature);
        }

        private CreatureController Pop()
        {
            var tmpChr = InGameCharacters[0];
            InGameCharacters.Remove(tmpChr);
            return tmpChr;
        }
    }
}