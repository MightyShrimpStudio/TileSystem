using System.Collections.Generic;
using Script.Entity.Character;

namespace Script.SubSystems
{
    public class CharacterOrder
    {
        public CreatureController CurrentCreature;
        public List<CreatureController> InGameCharacters;

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
            return chrY == null ? 1 : chrX.creatureStats.speed.CompareTo(chrY.creatureStats.speed);
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