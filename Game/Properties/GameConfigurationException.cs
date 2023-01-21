using System;

namespace ParticleGame.Game.Properties
{
    public class GameConfigurationException : SystemException
    {
        public GameConfigurationException() : base("Error occured while parsing App.config")
        {
        }

        public GameConfigurationException(string message) : base(message)
        {
        }

        public GameConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
