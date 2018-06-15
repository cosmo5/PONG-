using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;

public class PongBall : MovableObjectBehavior
{
    Vector3 startMoveDir = Vector3.zero;
    public float ballSpeed = 2.5f;

    Rigidbody myrigi;
    private void Start()
    {
        startMoveDir = new Vector3(Random.value, Random.value, 0);
        myrigi = GetComponent<Rigidbody>();
    }

    public override void MovePosition(RpcArgs args)
    {
        // networkObject.position = args.GetNext<Vector3>();
        //transform.position = args.GetNext<Vector3>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (PongNetworkManager.instance.networkObject.BothSpawned == false)
            return;

        if (!networkObject.IsOwner || !networkObject.IsServer)
        {
            Debug.Log("NOT OWNER");
           transform.position = networkObject.position;
           return;
        }

        transform.rotation = Quaternion.LookRotation(startMoveDir, Vector3.up);
        Vector3 pos =  (transform.forward  * ballSpeed * Time.deltaTime);
        myrigi.AddForce(pos);
        //networkObject.SendRpc(RPC_MOVE_POSITION, Receivers.All, pos);
        networkObject.position = myrigi.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Wall")
        {
            startMoveDir = Vector3.Reflect(startMoveDir, collision.contacts[0].normal);
        }
    }
}
