using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BulletCollision : MonoBehaviourPun, IPunObservable
{
    int bulletDamage = 1;
   public PlayerCollision playerCollision;
   public PhotonViewProxy proxy;
    void Awake()
    {

    }
    void OnTriggerEnter2D(Collider2D col)
    {
<<<<<<< HEAD
        print( "Bullet hit " + col.gameObject.name );
        // col = GetComponentInChildren<Collider2D>();
       proxy  = col.GetComponent<PhotonViewProxy>();
=======
>>>>>>> master



        if (proxy && proxy.photonView.Owner != photonView.Owner)
        {
           // PhotonNetwork.Destroy(this.gameObject);
            playerCollision.photonView.RPC( "takeDamage", RpcTarget.All , bulletDamage);
            Debug.Log("hit other player");

        }

        

    
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }
}
