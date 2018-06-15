using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;

using System;

public class PaddleScript : MovableObjectBehavior
{
    float inputY = 0;
    float moveSpeed = 5f;

    float deadZone = 0.03f;

    public bool isLocalOwner;

    public uint myplayerid;

    public Material[] mats;
    
    public override void MovePosition(RpcArgs args)
    {
        transform.position = args.GetNext<Vector3>();
    }

    private void Update()
    {
        myplayerid = networkObject.playerID;
        isLocalOwner = networkObject.MyPlayerId == networkObject.playerID;

        inputY = Input.GetAxis("Vertical");
        //If we do not own this object we want to update it
        if(!isLocalOwner && !networkObject.IsServer)
        {
            transform.position = networkObject.position;
        }

        if(isLocalOwner)
        {
            Vector3 pos = transform.position + (Vector3.up * (inputY * Time.deltaTime * moveSpeed));
            networkObject.position = pos;
            networkObject.SendRpc(RPC_MOVE_POSITION, Receivers.All, pos);
        }

        if (!networkObject.IsServer)
        {
            return;
        }
    }

}
