using System.Collections.Generic;
using Script.Entity.Character;
using Script.GameBoard;
using UnityEngine;

namespace Script.SubSystems
{
    public class CreatureManager : MonoBehaviour
    {
        public List<CreatureController> inGameCreatures;

        public CreatureController CurrentCreature { get; private set; }
        public int NumberOfTeams { get; set; }

        public void StartCircle(BoardController bc)
        {
            foreach (var creature in inGameCreatures)
            {
                Instantiate(creature);
            }
            CurrentCreature = Pop();
            CalculateOrder();
        }

        public void AddCreature(CreatureController creatureController)
        {
            inGameCreatures.Add(creatureController);
            CalculateOrder();
        }

        public void RemoveCreature(CreatureController creatureController)
        {
            inGameCreatures.Remove(creatureController);
            CalculateOrder();
        }

        private void CalculateOrder()
        {
            if (inGameCreatures.Count > 1)
                inGameCreatures.Sort(CompareCharactersBySpeed);
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
            inGameCreatures.Add(creature);
        }

        private CreatureController Pop()
        {
            var tmpChr = inGameCreatures[0];
            inGameCreatures.Remove(tmpChr);
            return tmpChr;
        }

        public List<CreatureController> GETCreaturesInTeam(int teamNumber)
        {
            List<CreatureController> creaturesInTeam = new List<CreatureController>();
            foreach (var creature in inGameCreatures)
            {
                if (creature.creatureStats.team == teamNumber)
                {
                    creaturesInTeam.Add(creature);
                }
            }
            return creaturesInTeam;
        }
    }
}