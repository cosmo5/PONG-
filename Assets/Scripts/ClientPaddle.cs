using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using System;

public class ClientPaddle : MovableObjectBehavior
{
    float inputY = 0;
    float moveSpeed = 5f;

    float deadZone = 0.05f;
    public override void MovePosition(RpcArgs args)
    {
        transform.position = (Vector3)args.Args[0];
    }

    private void Update()
    {
        if (networkObject.IsServer)
        {
            return;
        }
        inputY = Input.GetAxis("Vertical");

        
    }
}
