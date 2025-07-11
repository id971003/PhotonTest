using UnityEngine.Scripting;

namespace Quantum.Asteroids
{
    [Preserve]
    public unsafe class ShipSpawnSystem : SystemSignalsOnly, ISignalOnPlayerAdded
    {
        //플레이어 추가시 실행 되는 코드
        public void OnPlayerAdded(Frame frame, PlayerRef player, bool firstTime)
        {
            {
                RuntimePlayer data = frame.GetPlayerData(player);

                // resolve the reference to the avatar prototype.
                var entityPrototypAsset = frame.FindAsset<EntityPrototype>(data.PlayerAvatar);

                // Create a new entity for the player based on the prototype.
                var shipEntity = frame.Create(entityPrototypAsset);

                // Create a PlayerLink component. Initialize it with the player. Add the component to the player entity.
                frame.Add(shipEntity, new PlayerLink { PlayerRef = player });
            }
        }
    }
}