using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerCollision : MonoBehaviourPun, IPunObservable
{


    public bool isTriggered = false;



    void OnTriggerEnter2D(Collider2D col)
    {

        isTriggered = true;
        Debug.Log("player hit");
    }
    void OnTriggerExit2D(Collider2D col) {
        isTriggered = false;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }
}
