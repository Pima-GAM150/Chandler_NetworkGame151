using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerCollision : MonoBehaviourPun, IPunObservable
{
 

    void OnCollisionEnter2D(Collision2D col) {

        Debug.Log("player hit");

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }
}
