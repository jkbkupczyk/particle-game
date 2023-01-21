using System.Windows.Media;

using ParticleGame.Util;
using ParticleGame.Game.Graphics;

namespace ParticleGame.Game.Entity
{
    public interface IGameEntity : IGameObjectLifecycle
    {
        long GetId();

        Vector2d GetCurrentPosition();

        void Interact(IGameEntity GameEntity);

        void Collide(IGameEntity GameEntity);

        void Move(int Dx, int Dy);

        bool IsToBeRemoved();

        Color GetColor();

        ShapeRenderable GetElement();
    }
}
