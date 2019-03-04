using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class Bullet : MonoBehaviourPun, IPunInstantiateMagicCallback
{
    // when this player is instantiated on the network
    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
       
        NetworkedObjects.find.AddBullet(this.photonView, this.gameObject); // when the player is created, add it to a list of all players on the singleton

    }
}