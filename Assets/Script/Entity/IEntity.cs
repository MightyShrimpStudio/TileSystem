using Script.Entity.Character;

namespace Script.Entity
{
    public interface IEntity
    {
        bool IsApproachable();
        void Approaching(CreatureController approachingCreature);
    }
}