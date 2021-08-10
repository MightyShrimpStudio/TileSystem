using System.Collections.Generic;
using Script.Entity.Character;

namespace Script.SubSystems
{
    public class CharacterOrder
    {
        public List<CreatureController> InGameCharacters;
        public CreatureController CurrentCreature;


        public void Push(CreatureController creature)
        {
            InGameCharacters.Add(creature);
        }

        public CreatureController Pop()
        {
            CreatureController tmpChr = InGameCharacters[0];
            InGameCharacters.Remove(tmpChr);
            return tmpChr;
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
    }
}