namespace ParticleGame.Game.Screen.Game
{
    public class GameScreen : IScreen
    {
        private readonly DebugScreen DebugScreen;
        private readonly ParticlePropertiesScreen ParticlePropertiesScreen;
        private readonly PauseScreen PauseScreen;

        public GameScreen()
        {
            this.DebugScreen = new DebugScreen();
            this.ParticlePropertiesScreen = new ParticlePropertiesScreen();
            this.PauseScreen = new PauseScreen();
        }

        public void Init()
        {
            this.DebugScreen.Init();
            this.ParticlePropertiesScreen.Init();
            this.PauseScreen.Init();
        }

        public void Tick()
        {
            this.DebugScreen.Tick();            
            this.ParticlePropertiesScreen.Tick();
            this.PauseScreen.Tick();
        }

        public void Update()
        {
            this.DebugScreen.Update();
            this.ParticlePropertiesScreen.Update();
            this.PauseScreen.Update();
        }

        public void Render()
        {
            this.DebugScreen.Render();
            this.ParticlePropertiesScreen.Render();
            this.PauseScreen.Render();
        }

        public void Destroy()
        {
            this.DebugScreen.Destroy();
            this.ParticlePropertiesScreen.Destroy();
            this.PauseScreen.Destroy();
        }

    }
}
