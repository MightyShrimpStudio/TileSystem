using Script.Entity.Character;

namespace Script.Entity
{
    public interface IEntity
    {
        public bool IsApproachable();
        public void Approaching(CreatureController approachingCreature);
    }
}