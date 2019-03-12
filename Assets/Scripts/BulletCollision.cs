using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BulletCollision : MonoBehaviourPun, IPunObservable
{



    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("bullet hit");

    }



    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }
}
