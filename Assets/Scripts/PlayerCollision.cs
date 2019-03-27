using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerCollision : MonoBehaviourPun, IPunObservable
{


    int amount = 1;
    int maxHealth = 20;
    public int playerHealth;
  public  PhotonViewProxy proxy;
    public static PlayerCollision find;

    void Awake()
    {

        find = this;
        playerHealth = maxHealth;
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        print("player hit " + col.gameObject.name);
        

        proxy = col.GetComponent<PhotonViewProxy>();
        
        if (proxy && proxy.photonView.Owner != this.photonView.Owner)
        {
            photonView.RPC("takeDamage", RpcTarget.All, amount);
            Debug.Log("player takes damage hit");
//
        }
    }
    [PunRPC]
    public void takeDamage(int amount) {
           
                playerHealth -= amount;
                Debug.Log("Took damage of " + amount + " so health was modified to " + playerHealth);
     
         }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }
}
