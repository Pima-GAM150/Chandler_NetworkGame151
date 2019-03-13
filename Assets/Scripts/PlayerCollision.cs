using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerCollision : MonoBehaviourPun, IPunObservable
{


    int amount;
    public int playerHealth;
    
    public static PlayerCollision find;

    void Awake()
    {
        find = this;

    }

    void OnTriggerEnter2D(Collider2D col)
    {

        takeDamage(amount);
        Debug.Log("player hit");
    }
    void OnTriggerExit2D(Collider2D col) {
    
    }
    public void takeDamage(int amount) {
        playerHealth -= amount;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }
}
