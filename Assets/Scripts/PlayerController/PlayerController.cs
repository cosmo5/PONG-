using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
public class PlayerController : PlayerControllerBehavior {

    bool isMoving = false;

    public uint playerId;
    public bool isLocalOwner;

    public float moveSpeed = 5;
    Rigidbody myrigi;

	// Use this for initialization
	void Start () {
        myrigi = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        isLocalOwner = networkObject.MyPlayerId == networkObject.ID;
        playerId = networkObject.ID;
        if (!isLocalOwner)
            return;

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");


        if(Mathf.Abs(inputX) > 0.01f || Mathf.Abs(inputY) > 0.01f)
        {
            Vector3 wantedVel = new Vector3(inputX, 0, inputY);
            wantedVel *= moveSpeed;
            myrigi.AddForce(wantedVel - myrigi.velocity, ForceMode.VelocityChange);
        }

	}
}
