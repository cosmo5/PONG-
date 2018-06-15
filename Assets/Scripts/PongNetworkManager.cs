using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;

public class PongNetworkManager : NetworkManagerBehavior
{
    public GameObject spawnPos1;
    public GameObject spawnPos2;

    public static PongNetworkManager instance;
    private void Start()
    {
        if (instance == null)
            instance = this;
    }
    protected override void NetworkStart()
    {
        base.NetworkStart();

        if(networkObject.IsServer)
        {
            networkObject.SpawnPos1 = spawnPos1.transform.position;
            networkObject.SpawnPos2 = spawnPos2.transform.position;
            MainThreadManager.Run(() =>
            {
                MovableObjectBehavior go = NetworkManager.Instance.InstantiateMovableObject(0, position: networkObject.SpawnPos1);
                go.networkObject.position = networkObject.SpawnPos1;

                MovableObjectBehavior obj = NetworkManager.Instance.InstantiateMovableObject(1, position: Vector3.zero);
                obj.networkObject.position = Vector3.zero;
            });

            NetworkManager.Instance.Networker.playerAccepted += NewPlayer;
        }
    }
    private void NewPlayer(BeardedManStudios.Forge.Networking.NetworkingPlayer player, BeardedManStudios.Forge.Networking.NetWorker sender)
    {
        MainThreadManager.Run(() =>
        {
            MovableObjectBehavior go = NetworkManager.Instance.InstantiateMovableObject(0, position: networkObject.SpawnPos2);
            go.networkObject.playerID = player.NetworkId;
            go.networkObject.position = networkObject.SpawnPos2;
            networkObject.BothSpawned = true;
        });
      
    }
}
