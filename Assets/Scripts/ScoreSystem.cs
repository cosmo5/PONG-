using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using UnityEngine.UI;
using BeardedManStudios.Forge.Networking;
using System;

public class ScoreSystem : ScoreSystemBehavior {


    public Text player1, player2;

	// Use this for initialization
	void Start () {
    }

   
    public void PlayerScored(int playerIndex)
    {
        if (!networkObject.IsServer)
            return;

        if (playerIndex == 0)
            networkObject.Player1Score++;
        else
            networkObject.Playaer2Score++;

        //networkObject.SendRpc(RPC_SCORE_CHANGED, Receivers.AllBuffered);
    }

    //public override void ScoreChanged(RpcArgs args) 
    //{
    //    player1.text = networkObject.Player1Score.ToString();
    //    player2.text = networkObject.Playaer2Score.ToString();
    //}

}
