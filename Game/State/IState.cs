using System.Runtime.Serialization;

namespace ParticleGame.Game.State
{
    public interface IState : IGameObjectLifecycle, ISerializable
    {

        public StateType GetCurrentStateType();

        public void Serialize();

        public void Deserialize();

    }
}
