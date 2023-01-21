using System;
using System.Windows.Media;

namespace ParticleGame.Util
{
    public static class ColorUtils
    {
        public static readonly Color Transparent = Color.FromArgb(255, 0, 0, 0);

        public static Color FromHex(uint number)
        {
            byte[] bytes = BitConverter.GetBytes(number);
            return Color.FromArgb(bytes[3], bytes[2], bytes[1], bytes[0]);
        }

    }
}
