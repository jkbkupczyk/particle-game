using ParticleGame.Game.Screen;

namespace ParticleGame.Game.State
{
    public static class GlobalState
    {
        private static long EntityId;

        public static Entity.Entity CurrentlySelectedEntity { get; set; }

        public static IState CurrentState;

        public static StateType CurrentStateType
        {
            get { return CurrentState.GetCurrentStateType(); }
        }

        public static IScreen CurrentScreen;

        public static bool Paused { get; set; }
        public static int Frames { get; set; }
        public static int Ticks { get; set; }

        static GlobalState()
        {
            EntityId = 1L;

            CurrentState = null;
            CurrentScreen = new MainMenuScreen();
            Paused = false;
            Frames = 0;
            Ticks = 0;
        }

        public static long GetNextEntityId()
        {
            return EntityId++;
        }

        public static void Init()
        {
            CurrentState?.Init();
            CurrentScreen?.Init();
        }

        public static void Tick()
        {
            CurrentState?.Tick();
            CurrentScreen?.Tick();
        }

        public static void Update()
        {
            CurrentState?.Update();
            CurrentScreen?.Update();
        }

        public static void Render()
        {
            CurrentState?.Render();
            CurrentScreen?.Render();
        }

        public static void Destroy()
        {
            CurrentState?.Destroy();
            CurrentScreen?.Destroy();
        }

    }
}
