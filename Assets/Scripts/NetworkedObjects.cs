using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

using Photon.Pun;

public class NetworkedObjects : MonoBehaviour
{
    // the boundaries of the world
    public BoxCollider world;
    public GameObject Player;
    public GameObject bullet;
    public Transform shootPoint;
    public PlayerCollision playerCollision;
    public PlayerMovement playerMove;
    public PlayerColor playerColor;
    public float coolDownTime;
    private float nextFiretime;
    public Canvas canvas;
    


    // keep track of all the players in the game
    [HideInInspector] public List<PhotonView> players = new List<PhotonView>();
    [HideInInspector] public List<PhotonView> bullets = new List<PhotonView>();
    public List<Slider> healthBars = new List<Slider>();
    public static NetworkedObjects find;
    private static Canvas canvasInstance;

    int seed; // only matters on the master client

    // singleton assignment
    void Awake()
    {
   
       /* DontDestroyOnLoad(canvas);
        if (canvasInstance == null)
        {
            canvasInstance = this.canvas;
        }
        else
        {
            Destroy(canvasInstance);
        }*/

        find = this;
  
    }

    void Start()
    {
    

        if (PhotonNetwork.IsMasterClient)
        {
            // try to create a truly random number to use as your starting random seed
            seed = DateTime.Now.Millisecond + System.Threading.Thread.CurrentThread.GetHashCode();
        }

        // when the game starts on this client, instantiate a player from a named prefab in the resources folder
        float xRange = UnityEngine.Random.Range(-world.bounds.extents.x, world.bounds.extents.x);
        float yRange = UnityEngine.Random.Range(-world.bounds.extents.y, world.bounds.extents.y);

        Vector3 spawnPos = world.bounds.center + new Vector3(xRange, yRange, 0f);
       PhotonNetwork.Instantiate("Player", spawnPos, Quaternion.identity, 0);

        PhotonNetwork.AutomaticallySyncScene = true;
       
      

    }
    void Update()
    {
        netFire();
    }

    public bool netFire() {
       
            bool bulletCreated = false;
        Vector2 playerPos = players[0].GetComponent<PlayerMovement>().appearance.position;
        Vector2 mousePos = Input.mousePosition;
        Vector2 screenPos = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));
        Quaternion q = Quaternion.FromToRotation(Vector2.up, screenPos - playerPos);
        if (Input.GetMouseButtonDown(0) && Input.GetMouseButton(1) )
        {
            if (transform.localScale.x < 0 && bulletCreated == false && nextFiretime < Time.time)
            {
                nextFiretime = Time.time + coolDownTime;
                bullet = PhotonNetwork.Instantiate("Bullet",
                new Vector2(playerPos.x, playerPos.y), q);
            }
            else if (nextFiretime < Time.time)
            {
                nextFiretime = Time.time + coolDownTime;
                bullet = PhotonNetwork.Instantiate("Bullet",
               new Vector2(playerPos.x, playerPos.y), q);
            }
            bulletCreated = true;
        }
        return bulletCreated;
    }



    public void AddPlayer(PhotonView player, GameObject Player)
    {
        // add a player to the list of all tracked players
        players.Add(player);
        ChangeParent();

        foreach (PhotonView players in players)
        {
            Player.GetComponentInChildren<SpriteRenderer>().enabled = false;

             //playerMove.photonView.RPC("MakeSliderVisible", RpcTarget.All, false);
            

        }
       
        // only the "server" has authority over which color the player should be and its seed
        if (PhotonNetwork.IsMasterClient)
        {
            player.RPC("SetColor", RpcTarget.AllBuffered, players.Count - 1);
           
        }
    }

    public void AddBullet(PhotonView bullet, GameObject bulletGameObj)
    {
        bullets.Add(bullet);
  
    }
    public void ChangeParent()
    {

        for (int p = 0; p < players.Count; p++)
        {
            players[p].transform.SetParent(canvas.transform);
        }
    }

}