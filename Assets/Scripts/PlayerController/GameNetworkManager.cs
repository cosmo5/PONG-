using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
public class GameNetworkManager : GameControllerBehavior {

    protected override void NetworkStart()
    {
        base.NetworkStart();

        if (!networkObject.IsServer)
            return;

        MainThreadManager.Run(() => 
        {
            PlayerControllerBehavior go = NetworkManager.Instance.InstantiatePlayerController(0, position: Vector3.zero + UnityEngine.Random.insideUnitSphere * 10);
            go.networkObject.position = go.transform.position;
        });

        networkObject.Networker.playerAccepted += NewPlayer;

    }

    private void NewPlayer(BeardedManStudios.Forge.Networking.NetworkingPlayer player, BeardedManStudios.Forge.Networking.NetWorker sender)
    {
   MainThreadManager.Run(() => 
        {
            PlayerControllerBehavior go = NetworkManager.Instance.InstantiatePlayerController(0, position: Vector3.zero + UnityEngine.Random.insideUnitSphere * 10);
            go.networkObject.position = go.transform.position;
            go.networkObject.ID = player.NetworkId;
        });
    }
}
