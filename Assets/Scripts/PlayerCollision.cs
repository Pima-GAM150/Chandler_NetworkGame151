﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class PlayerCollision : MonoBehaviourPun, IPunObservable
{

    public int currentHealth;
    int amount = 1;
    public int maxHealth = 3;
    public Slider playerOneHealth;
  public  PhotonViewProxy proxy;
    public static PlayerCollision find;

    void Awake()
    {

        find = this;
    }
    void Start() {
        if (photonView.IsMine)
        {
            playerOneHealth = GameObject.Find("PlayerOneHealth").GetComponent<Slider>();
        }

            
        currentHealth = maxHealth;
        playerOneHealth.value = currentHealth;
       
    }
    

    void OnTriggerEnter2D(Collider2D col)
    {
      
        print("player hit " + col.gameObject.name);
      

        proxy = col.GetComponent<PhotonViewProxy>();
        
        if (proxy && proxy.photonView.Owner != this.photonView.Owner)
        {
            photonView.RPC("takeDamage", RpcTarget.All, amount);
            Debug.Log("player takes damage hit");

            
        }


        
    }
    [PunRPC]
    public void takeDamage(int amount) {
           
                currentHealth -= amount;
        playerOneHealth.value = currentHealth;
                Debug.Log("Took damage of " + amount + " so health was modified to " + currentHealth);
        if (currentHealth <= 0) { //if a player is dead

            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.LoadLevel("Arena2");

            

        }
         }

 



    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }
}
