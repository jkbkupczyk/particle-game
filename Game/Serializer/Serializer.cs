using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;

using ParticleGame.Game.Properties;
using ParticleGame.Game.State;

namespace ParticleGame.Game.Serializer
{
    public static class Serializer
    {
        public static void Serialize(GameState gameState, string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                filePath = GameProperties.SaveFilePath;
            }
            Debug.WriteLine($"Saving game in {filePath}...");
            using FileStream fs = new FileStream(filePath, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, gameState);
        }

        public static GameState Deserialize(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                filePath = GameProperties.SaveFilePath;
            }
            using FileStream fs = new FileStream(filePath, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            return (GameState)formatter.Deserialize(fs);
        }

    }
}
