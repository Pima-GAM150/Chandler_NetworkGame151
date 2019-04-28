using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviourPun, IPunObservable
{

    public int currentHealth;
    int amount = 1;
    public int maxHealth = 3;
    public Slider playerOneHealth;
    public PhotonViewProxy proxy;
    public static PlayerCollision find;
    int p1score;
    public TextMeshProUGUI p1scoreText;
    int p2score;
    public TextMeshProUGUI p2scoreText;

    void Awake()
    {
        p1score=0;
        p1scoreText.text = p1score.ToString();
        p2score=0;
        p2scoreText.text = p2score.ToString();
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

        // print("player hit " + col.gameObject.name);


        proxy = col.GetComponent<PhotonViewProxy>();

        if (proxy && proxy.photonView.Owner != this.photonView.Owner)
        {
            photonView.RPC("takeDamage", RpcTarget.All, amount);

            // Debug.Log("player takes damage hit");


        }

    }
    [PunRPC]
    public void takeDamage(int amount) {

        currentHealth -= amount;
        playerOneHealth.value = currentHealth;
        //Debug.Log("Took damage of " + amount + " so health was modified to " + currentHealth);
        if (currentHealth <= 0) { //if a player is dead

            if (PhotonNetwork.IsMasterClient)

                PhotonNetwork.LoadLevel("Arena2");

            photonView.RPC("SetScoreText", RpcTarget.All);

            if (SceneManager.GetSceneByName("Arena2").isLoaded)
            {
                if (currentHealth <= 0)
                { //if a player is dead

                    if (PhotonNetwork.IsMasterClient)

                        PhotonNetwork.LoadLevel("Game");

                    photonView.RPC("SetScoreText", RpcTarget.All);
                }
            }

        }
    }

    [PunRPC] 
    public void SetScoreText()
    {
        if (proxy && proxy.photonView.Owner != this.photonView.Owner)
        {
            p1score++;
            p1scoreText.text = p1score.ToString();
           //  Debug.Log(p1score + "player ones score");
        }
        else {
            p2score++;
            p2scoreText.text = p2score.ToString();
           // Debug.Log(p2score + "player twos score");
        }
    }




        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }
}
