using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BulletCollision : MonoBehaviourPun, IPunObservable
{
    
   public PhotonViewProxy proxy;
    void OnTriggerEnter2D(Collider2D col)
    {



        if (proxy && proxy.photonView.Owner != photonView.Owner)
        {
            PhotonNetwork.Destroy(this.gameObject);
            Debug.Log("hit other player");

        }

        

    
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }
}
