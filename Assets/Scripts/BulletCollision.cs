using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BulletCollision : MonoBehaviourPun, IPunObservable
{
    
   public PhotonViewProxy proxy;
    void OnTriggerEnter2D(Collider2D col)
    {
        print( "Bullet hit " + col.gameObject.name );
        // col = GetComponentInChildren<Collider2D>();
       proxy  = col.GetComponent<PhotonViewProxy>();

        if (proxy && proxy.photonView.Owner != this.photonView.Owner)
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
