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


    // keep track of all the players in the game
    [HideInInspector] public List<PhotonView> players = new List<PhotonView>();
    public List<PhotonView> bullets = new List<PhotonView>();

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
    }

    public void AddPlayer(PhotonView player, GameObject Player)
    {
        // add a player to the list of all tracked players
        players.Add(player);
        foreach (PhotonView players in players)
        {
            Player.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
        // only the "server" has authority over which color the player should be and its seed
        if (PhotonNetwork.IsMasterClient)
        {
         
        }
    }

    public void AddBullet(PhotonView bullet, GameObject bulletGameObj) {
        bullets.Add(bullet);
    }
}