using System.Collections.Generic;
using Script.Entity.Creature;
using Script.GameBoard;
using UnityEngine;

namespace Script.SubSystems
{
    public class CreatureManager : MonoBehaviour
    {
        public CreatureController creaturePrefab;
        public List<CreatureStats> inGameCreaturesStats;
        public int numberOfTeams;

        public List<CreatureController> InGameCreatures { get; private set; }

        public CreatureController CurrentCreature { get; private set; }

        private bool _isCircleStart = false;

        public void Awake()
        {
            InGameCreatures = new List<CreatureController>();
            foreach (var creatureStat in inGameCreaturesStats)
            {
                var creature = Instantiate(creaturePrefab);
                creature.CreatureStats = creatureStat;
                creature.name = creatureStat.name;
                InGameCreatures.Add(creature);
            }
        }


        public void AddCreature(CreatureController creatureController)
        {
            InGameCreatures.Add(creatureController);
            CalculateOrder();
        }

        public void RemoveCreature(CreatureController creatureController)
        {
            InGameCreatures.Remove(creatureController);
            CalculateOrder();
        }

        public void CalculateOrder()
        {
            if (InGameCreatures.Count > 1)
                InGameCreatures.Sort(CompareCharactersBySpeed);
        }

        private static int CompareCharactersBySpeed(CreatureController chrX, CreatureController chrY)
        {
            return chrX.CreatureStats.speed.CompareTo(chrY.CreatureStats.speed);
        }

        public void NextCharacter()
        {
            if(_isCircleStart)Push(CurrentCreature);
            CurrentCreature = Pop();
            _isCircleStart = true;
        }
        private void Push(CreatureController creature)
        {
            InGameCreatures.Add(creature);
        }

        private CreatureController Pop()
        {
            var tmpChr = InGameCreatures[0];
            InGameCreatures.Remove(tmpChr);
            return tmpChr;
        }

        public List<CreatureController> GETCreaturesInTeam(int teamNumber)
        {
            var creaturesInTeam = new List<CreatureController>();
            foreach (var creature in InGameCreatures)
                if (creature.CreatureStats.team == teamNumber)
                    creaturesInTeam.Add(creature);
            return creaturesInTeam;
        }
    }
}