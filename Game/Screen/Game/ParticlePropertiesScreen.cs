using System.Windows.Media;

using ParticleGame.Game.Graphics;
using ParticleGame.Game.Entity.Particle;
using ParticleGame.Game.State;
using ParticleGame.Game.Controls;
using ParticleGame.Util;

namespace ParticleGame.Game.Screen.Game
{
    public class ParticlePropertiesScreen : IScreen
    {
        private static readonly Color TEXT_COLOR = ColorUtils.FromHex(0xFF000000);
        private static readonly Color BG_COLOR = ColorUtils.FromHex(0x77A9A9A9);

        private const string PropertyData = " \n" +
                                            " Entity ID: {0}\t\n" +
                                            " Width: {1}, Height: {2}\t\n" +
                                            " Position: {3}\t\n" +
                                            " Motion vector: {4}\t\n" +
                                            " Type: {5}\t\n" +
                                            " Mass: {6}\t\n" +
                                            " Charge: {7}\t\n" +
                                            " Heat: {8}\t\n" +
                                            " Critical heat: {9}\t\n" +
                                            " Time to live: {10}\t\n";

        private SubatomicParticle Particle;
        private readonly TextRenderable Properties;

        private bool PropertiesVisible;

        public ParticlePropertiesScreen()
        {
            this.Properties = new TextRenderable("AAA", null, 22, ColorUtils.FromHex(0xFFFFFF00));
            this.PropertiesVisible = false;
        }

        public void Init()
        {
            this.Properties.SetVisible(this.PropertiesVisible);
            this.Properties.SetZIndex(int.MaxValue);
            this.Properties.SetColor(TEXT_COLOR, BG_COLOR);
            this.Properties.Init();
        }

        public void Tick()
        {
            if (KeyboardHandler.PROPERTIES_TOGGLE.PressedTick())
            {
                this.ToggleVisible();
            }
        }

        public void Update()
        {
            if (!object.ReferenceEquals(this.Particle, GlobalState.CurrentlySelectedEntity))
            {
                this.Particle = (SubatomicParticle) GlobalState.CurrentlySelectedEntity;
            }
        }

        public void Render()
        {
            this.Properties.SetVisible(this.PropertiesVisible);
            if (this.PropertiesVisible)
            {
                this.Properties.SetText(string.Format(PropertyData, this.GetProperties(this.Particle)));
                this.Properties.SetPosition(Renderer.Renderer.Width - (Properties.Width + 8), 0);
                this.Properties.Render();
            }
        }

        public void Destroy()
        {
            this.Properties.Destroy();
        }

        private void ToggleVisible()
        {
            this.PropertiesVisible = !this.PropertiesVisible;
        }

        private object[] GetProperties(SubatomicParticle particle)
        {
            return new object[]
            {
                particle.GetId(),
                particle.Width,
                particle.Height,
                particle.Position,
                particle.MotionVector,
                particle.GetType().Name,
                particle.Mass,
                particle.Charge.ToString(),
                particle.Heat,
                particle.CriticalHeat,
                particle.TimeToLive
            };
        }

    }
}
