using System.Configuration;
using System.Collections.Specialized;

using ParticleGame.Internal;

namespace ParticleGame.Game.Properties
{
    public static class GameProperties
    {
        private const string CONFIG_FILE = "App.config";
        private const string CONFIG_GROUP = "appSettings";

        private static readonly NameValueCollection Properties;

        /** Internal configuration */
        public static readonly AppProfile Profile;
        public static readonly bool DevMode;

        /** Game settings */
        public static readonly string WindowTitle;
        public static readonly int WindowWidth;
        public static readonly int WindowHeight;

        /** Game logic config */
        public static readonly uint ParticlesPerView;
        public static readonly int ParticleCriticalSizeValue;

        /** Others */
        public static readonly string SaveFilePath;

        static GameProperties()
        {
            Properties = ConfigurationManager.AppSettings;

            Profile = InitProfile();
            DevMode = Profile == AppProfile.DEV;

            WindowTitle = Get("WindowTitle");
            WindowWidth = int.Parse(Get("WindowWidth"));
            WindowHeight = int.Parse(Get("WindowHeight"));

            ParticlesPerView = uint.Parse(Get("ParticlesPerView"));
            ParticleCriticalSizeValue = int.Parse(Get("ParticleCriticalSizeValue"));

            SaveFilePath = Get("SaveFilePath");
        }

        private static string Get(in string Key)
        {
            if (string.IsNullOrEmpty(Key))
            {
                throw new GameConfigurationException($"Key: \"{Key}\" cannot be null or empty!");
            }
            string Property = Properties.Get(Key);
            if (Property == null)
            {
                throw new GameConfigurationException($"Could not find property with key \"{Key}\" within group \"{CONFIG_GROUP}\" in file \"{CONFIG_FILE}\"!");
            }

            return Property;
        }

        private static AppProfile InitProfile()
        {
            string mode = Get("Mode");

            if ("dev".Equals(mode))
            {
                return AppProfile.DEV;
            } 
            else if ("test".Equals(mode)) {
                return AppProfile.TEST;
            }

            return AppProfile.USER;
        }

    }
}
