using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;

public class PongBall : MovableObjectBehavior
{
    Vector3 startMoveDir = Vector3.zero;
    public float ballSpeed = 2.5f;
    public float maxBallSpeed = 6f;
    Rigidbody myrigi;
    ScoreSystem score;
    float timeSet = 0;
    private void Start()
    {
        startMoveDir = new Vector3(Random.Range(0f, 0.5f), 0, 0);

        if (Random.value > 0.5f)
            startMoveDir = -startMoveDir;

        score = FindObjectOfType<ScoreSystem>();
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

        if (myrigi.isKinematic)
        {
            if( Time.time - timeSet  > 2)
            {
                myrigi.isKinematic = false;
            }
            return;
        }

        if (!networkObject.IsOwner || !networkObject.IsServer)
        {
           myrigi.position = networkObject.position;
           return;
        }

        Vector3 velocity = startMoveDir.normalized * ballSpeed;

        myrigi.AddForce(velocity - myrigi.velocity, ForceMode.VelocityChange);
        networkObject.position = myrigi.position;


        if (ballSpeed > maxBallSpeed)
            ballSpeed = maxBallSpeed; 

        else if(ballSpeed != maxBallSpeed)
            ballSpeed += (Time.deltaTime / 100);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!networkObject.IsServer)
            Debug.Log("IM NO THE SERVER");

        if (collision.transform.tag == "Wall" || collision.transform.tag == "Paddle")
        {
            if (!networkObject.IsServer)
                return;

            startMoveDir = Vector3.Reflect(startMoveDir, collision.contacts[0].normal);

            ballSpeed += 0.1f;

            Vector3 velocity = startMoveDir.normalized * ballSpeed;

            myrigi.AddForce(velocity - myrigi.velocity, ForceMode.VelocityChange);
            networkObject.position = myrigi.position;


        }

        if (collision.transform.tag == "ScoreWall")
        {
            AddScore(transform.position.x);
            Restart();
        }
    }
    void AddScore(float x)
    {
        if(x > 0)
        {
            score.PlayerScored(0);
        }
        else if(x < 0)
        {
            score.PlayerScored(1);
        }
    }
    void Restart()
    {
        if (!networkObject.IsServer)
            return;

        myrigi.isKinematic = true;
        transform.position = Vector3.zero;

        networkObject.position = transform.position;
        startMoveDir = new Vector3(Random.Range(0f, 0.5f), Random.Range(0f, 0.5f), 0);

        if (Random.value > 0.5f)
            startMoveDir = -startMoveDir;

        timeSet = Time.time;
        ballSpeed = 2.5f;
    }
}
