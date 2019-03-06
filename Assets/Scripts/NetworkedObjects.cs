using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Photon.Pun;

public class NetworkedObjects : MonoBehaviour
{
    // the boundaries of the world
    public BoxCollider world;
    public GameObject Player;
    public GameObject bullet;
    public Transform shootPoint;



    // keep track of all the players in the game
    [HideInInspector] public List<PhotonView> players = new List<PhotonView>();
    [HideInInspector] public List<PhotonView> bullets = new List<PhotonView>();

    public static NetworkedObjects find;
   

    int seed; // only matters on the master client

    // singleton assignment
    void Awake()
    {
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
        PhotonNetwork.Instantiate("Player", spawnPos + new Vector3(xRange, yRange, 0f), Quaternion.identity, 0);
    }
    void Update()
    {
        netFire();
        netFire2();
    }

    public bool netFire() {

        bool bulletCreated = false;
        Vector2 playerPos = players[0].GetComponent<PlayerMovement>().appearance.position;
        Vector2 mousePos = Input.mousePosition;
        Vector2 screenPos = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));
        Quaternion q = Quaternion.FromToRotation(Vector2.up, screenPos - playerPos);
        if (Input.GetMouseButtonDown(0) && Input.GetMouseButton(1))
        {
            if (transform.localScale.x < 0 && bulletCreated == false)
            {
                bullet = PhotonNetwork.Instantiate("Bullet",
                new Vector2(playerPos.x, playerPos.y), q);
            }
            else {
                bullet = PhotonNetwork.Instantiate("Bullet",
               new Vector2(playerPos.x, playerPos.y), q);
            }
            bulletCreated = true;
        }
        return bulletCreated;
    }

    public bool netFire2()
    {
        bool bulletCreated2 = false;
        Vector2 playerPos2 = players[1].GetComponent<PlayerMovement>().appearance.position;
        Vector2 mousePos2 = Input.mousePosition;
        Vector2 screenPos2 = Camera.main.ScreenToWorldPoint(new Vector2(mousePos2.x, mousePos2.y));
        Quaternion q2 = Quaternion.FromToRotation(Vector2.up, screenPos2 - playerPos2);
        if (players.Count > 1) {
          
            if (Input.GetMouseButtonDown(0) && Input.GetMouseButton(1))
            {
                if (transform.localScale.x < 0 && bulletCreated2 == false)
                {
                    bullet = PhotonNetwork.Instantiate("Bullet",
                    new Vector2(playerPos2.x, playerPos2.y), q2);
                }
                else
                {
                    bullet = PhotonNetwork.Instantiate("Bullet",
                   new Vector2(playerPos2.x, playerPos2.y), q2);
                }
                bulletCreated2 = true;
            }
            
        }
        return bulletCreated2;
    }


    public void AddPlayer(PhotonView player, GameObject Player)
    {
        // add a player to the list of all tracked players
        players.Add(player);

        // only the "server" has authority over which color the player should be and its seed
        if (PhotonNetwork.IsMasterClient)
        {
            foreach (PhotonView players in players)
            {
                Player.GetComponentInChildren<SpriteRenderer>().enabled = false;
            }
        }
    }

    public void AddBullet(PhotonView bullet, GameObject bulletGameObj)
    {
        bullets.Add(bullet);
  
    }


}