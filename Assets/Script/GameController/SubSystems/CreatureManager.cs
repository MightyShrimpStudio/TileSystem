using System;
using System.Collections.Generic;
using Script.Entity.Creature;
using UnityEngine;

namespace Script.GameController.SubSystems
{
    public class CreatureManager : MonoBehaviour
    {
        public CreatureController creaturePrefab;
        public List<CreatureStats> inGameCreaturesStats;
        public int numberOfTeams;
        
        public CreatureController CurrentCreature { get; private set; }

        private List<CreatureController> _inGameCreatures;
        private bool _isCircleStart;

        private void Awake()
        {
            _inGameCreatures = new List<CreatureController>();
            foreach (var creatureStat in inGameCreaturesStats)
            {
                var creature = Instantiate(creaturePrefab);
                creature.CreatureStats = creatureStat;
                creature.name = creatureStat.name;
                _inGameCreatures.Add(creature);
            }
        }

        private int CompareCharactersBySpeed(CreatureController chrX, CreatureController chrY)
        {
            return chrX.CreatureStats.speed.CompareTo(chrY.CreatureStats.speed);
        }

        private void Push(CreatureController creature)
        {
            _inGameCreatures.Add(creature);
        }

        private CreatureController Pop()
        {
            var tmpChr = _inGameCreatures[0];
            _inGameCreatures.Remove(tmpChr);
            return tmpChr;
        }

        public void AddCreature(CreatureController creatureController)
        {
            _inGameCreatures.Add(creatureController);
            CalculateOrder();
        }

        public void RemoveCreature(CreatureController creatureController)
        {
            _inGameCreatures.Remove(creatureController);
            CalculateOrder();
        }

        public void CalculateOrder()
        {
            if (_inGameCreatures.Count > 1)
                _inGameCreatures.Sort(CompareCharactersBySpeed);
        }

        public void NextCreature()
        {
            if(_isCircleStart)Push(CurrentCreature);
            CurrentCreature = Pop();
            _isCircleStart = true;
        }

        public List<CreatureController> GETCreaturesInTeam(int teamNumber)
        {
            var creaturesInTeam = new List<CreatureController>();
            foreach (var creature in _inGameCreatures)
                if (creature.CreatureStats.team == teamNumber)
                    creaturesInTeam.Add(creature);
            return creaturesInTeam;
        }
    }
}