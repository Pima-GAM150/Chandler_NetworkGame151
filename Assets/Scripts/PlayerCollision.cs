using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviourPun, IPunObservable
{
    public NetworkedObjects networkedObjects;
    public int currentHealth;
    int amount = 1;
    public int maxHealth = 3;
    public Slider playerOneHealth;
    public PhotonViewProxy proxy;
    public static PlayerCollision find;
    public int p1score
    {
        get { return PlayerPrefs.GetInt("p1score", 0); ; }
        set {
            PlayerPrefs.SetInt("p1score", value);
        }
    }

    public int p2score
    {
        get { return PlayerPrefs.GetInt("p2score", 0); ; }
        set
        {
            PlayerPrefs.SetInt("p2score", value);
        }
    }

    void Awake()
    {
        UI.manager.p1scoreText.text = p1score.ToString();
        UI.manager.p2scoreText.text = p2score.ToString();
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
    void Update() {


    }


    void OnTriggerEnter2D(Collider2D col)
    {

        // print("player hit " + col.gameObject.name);


        proxy = col.GetComponent<PhotonViewProxy>();

        if (proxy && proxy.photonView.Owner != this.photonView.Owner)
        {
            photonView.RPC("takeDamage", RpcTarget.All, amount);

        }

    }
    [PunRPC]
    public void takeDamage(int amount) {

        currentHealth -= amount;
        playerOneHealth.value = currentHealth;
    
        //Debug.Log("Took damage of " + amount + " so health was modified to " + currentHealth);
        if (currentHealth <= 0) { //if a player is dead

            if( PhotonNetwork.IsMasterClient )
            {
                if (order == 0) p2score++;
                else p1score++;
                photonView.RPC("SetScoreText", RpcTarget.All, p1score, p2score);

                if (SceneManager.GetSceneByName("Game").isLoaded)
                {
                    PhotonNetwork.LoadLevel("Arena2");
                }
                else if (SceneManager.GetSceneByName("Arena2").isLoaded)
                {
                    // photonView.RPC("SetScoreText", RpcTarget.All, p1score, p2score);
                    PhotonNetwork.LoadLevel("Game");
                }
            }

            
        }
    }

    

    [PunRPC] 
    public void SetScoreText( int p1Score,  int p2Score)
    {
        this.p1score = p1Score;
        this.p2score = p2Score;

        UI.manager.p1scoreText.text = p1score.ToString();
        UI.manager.p2scoreText.text = p2score.ToString();

        /*
        if (PhotonNetwork.IsMasterClient)
        {
            if (photonView.IsMine)
            {
                p2score++;
                p2scoreText.text = p2score.ToString();
                Debug.Log("1");
         
            }
            else
            {
                p1score++;
                p1scoreText.text = p1score.ToString();
                Debug.Log("2");
            }
        }
        else
        {
            if (photonView.IsMine)
            { 
                p1score++;
                p1scoreText.text = p1score.ToString();
                Debug.Log("3");

            }
            else
            {
                p2score++;
                p2scoreText.text = p2score.ToString();
                Debug.Log("4");
            }
        }
        */
    }
          
        




        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }

    public int order { get { return GetComponent<PlayerColor>().order; } }
}
