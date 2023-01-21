namespace ParticleGame.Game
{
    public interface IGameObjectLifecycle
    {

        void Init();

        void Tick();

        void Update();

        void Render();

        void Destroy();

    }
}
