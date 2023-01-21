using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

using ParticleGame.Game.Entity;
using ParticleGame.Game.Entity.Particle;
using ParticleGame.Game.Properties;

namespace ParticleGame.Game.State
{
    [Serializable]
    public sealed class GameState : IState
    {
        private readonly List<IGameEntity> Entitites;
        private readonly List<IGameEntity> EntititesToAdd;

        public GameState()
        {
            this.Entitites = new List<IGameEntity>();
            this.EntititesToAdd = new List<IGameEntity>();
        }

        public GameState(List<IGameEntity> entitites, List<IGameEntity> entititesToAdd)
        {
            this.Entitites = entitites;
            this.EntititesToAdd = entititesToAdd;
            this.Init();
        }

        // Required by serialization mechanism
        public GameState(SerializationInfo info, StreamingContext context)
        {
            this.Entitites = (List<IGameEntity>)info.GetValue("Entitites", typeof(List<IGameEntity>));
            this.EntititesToAdd = (List<IGameEntity>)info.GetValue("EntititesToAdd", typeof(List<IGameEntity>));
        }

        public int GetNumberOfEntities()
        {
            return this.Entitites.Count;
        }

        public void Init()
        {
            for (uint i = 0; i < GameProperties.ParticlesPerView; i += 4)
            {
                this.Entitites.Add(Proton.Random());
                this.Entitites.Add(Neutron.Random());
                this.Entitites.Add(Electron.Random());
                this.Entitites.Add(Photon.Random());
            }

            foreach (IGameEntity entity in this.Entitites)
            {
                entity.Init();
            }

            GlobalState.CurrentlySelectedEntity = (Entity.Entity) this.Entitites[0];

            Debug.WriteLine($"Game initialized with number of: {this.Entitites.Count} particles");
        }

        public void Tick()
        {
            if (GlobalState.Paused)
            {
                return;
            }

            foreach (IGameEntity entity in Entitites)
            {
                entity.Tick();
            }

            // add new entities
            if (this.EntititesToAdd.Count > 0 && 
                this.Entitites.Count < GameProperties.ParticlesPerView)
            {
                int i = 0;
                while (i < this.EntititesToAdd.Count)
                {
                    this.Entitites.Add(this.EntititesToAdd[i]);
                    this.EntititesToAdd[i].Init();
                    this.EntititesToAdd.RemoveAt(i);
                    i++;
                }
            }

            // remove entities
            for (int i = this.Entitites.Count - 1; i >= 0; i--)
            {
                IGameEntity gameEntity = this.Entitites[i];
                if (gameEntity.IsToBeRemoved())
                {
                    // Queue new Entity of same type as deleted
                    this.EntititesToAdd.Add(QueueNewEntity(gameEntity.GetType()));
                    this.Entitites.RemoveAt(i);
                    gameEntity.Destroy();
                }
            }
        }

        public void Update()
        {
            foreach (IGameEntity Entity in Entitites)
            {
                Entity.Update();
            }
        }

        public void Render()
        {
            foreach (IGameEntity Entity in Entitites)
            {
                Entity.Render();
            }
        }

        public void Destroy()
        {
            // TODO: save state
            /*
            if (!GameProperties.DevMode)
            {
                this.Serialize();
            }
            */
            foreach (IGameEntity Entity in Entitites)
            {
                Entity.Destroy();
            }
            Entitites.Clear();
            EntititesToAdd.Clear();
        }

        private IGameEntity QueueNewEntity(Type entityType)
        {
            IGameEntity entity;
            if (entityType == typeof(Proton))
            {
                entity = Proton.Random();
            }
            else if (entityType == typeof(Neutron))
            {
                entity = Neutron.Random();
            }
            else if (entityType == typeof(Electron))
            {
                entity = Electron.Random();
            }
            else if (entityType == typeof(Photon))
            {
                entity = Photon.Random();
            } 
            else
            {
                throw new UnsupportedEntityType(entityType == null ? "null" : entityType.ToString());
            }

            return entity;
        }

        public StateType GetCurrentStateType()
        {
            return StateType.IN_GAME;
        }

        public void Serialize()
        {
            Serializer.Serializer.Serialize(this, "");
        }

        public void Deserialize()
        {
            var state = Serializer.Serializer.Deserialize("");
            Debug.WriteLine(state);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Entitites", this.Entitites, typeof(List<IGameEntity>));
            info.AddValue("EntititesToAdd", this.EntititesToAdd, typeof(List<IGameEntity>));
        }

    }
}
