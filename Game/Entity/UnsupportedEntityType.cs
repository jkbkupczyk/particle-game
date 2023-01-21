using System;

namespace ParticleGame.Game.Entity
{
    public class UnsupportedEntityType : Exception
    {
        public UnsupportedEntityType(string entityTypeName) : base($"Entity of type {entityTypeName} is not supported!")
        {
        }
    }
}
