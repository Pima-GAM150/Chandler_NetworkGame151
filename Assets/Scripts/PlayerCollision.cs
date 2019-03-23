using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerCollision : MonoBehaviourPun, IPunObservable
{


    int amount = 1;
    int maxHealth = 10;
    public int playerHealth;
  public  PhotonViewProxy proxy;
    public static PlayerCollision find;

    void Awake()
    {
         proxy = this.GetComponent<PhotonViewProxy>();
        find = this;
        playerHealth = maxHealth;
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        proxy = col.GetComponent<PhotonViewProxy>();
        if (proxy && proxy.photonView.Owner != this.photonView.Owner)
        {
           
                takeDamage(amount);
                Debug.Log("player hit");
            
        }
    }

    public void takeDamage(int amount) {
           
                this.playerHealth -= amount;
                Debug.Log("Took damage of " + amount + " so health was modified to " + playerHealth);
     
         }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }
}
