using System.Collections.Generic;
using Script.Character;

namespace Script.Systems
{
    public class CharacterOrder
    {
        public List<CharacterController> InGameCharacters;
        public CharacterController CurrentCharacter;


        public void Push(CharacterController character)
        {
            InGameCharacters.Add(character);
        }

        public CharacterController Pop()
        {
            CharacterController tmpChr = InGameCharacters[0];
            InGameCharacters.Remove(tmpChr);
            return tmpChr;
        }

        public void CalculateOrder()
        {
            InGameCharacters.Sort(CompareCharactersBySpeed);
        }

        private static int CompareCharactersBySpeed(CharacterController chrX, CharacterController chrY)
        {
            if (chrX == null)
                return chrY == null ? 0 : -1;
            return chrY == null ? 1 : chrX.characterStats.speed.CompareTo(chrY.characterStats.speed);
        }

        public void NextCharacter()
        {
            Push(CurrentCharacter);
            CurrentCharacter = Pop();
        }
    }
}