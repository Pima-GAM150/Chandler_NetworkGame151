using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BulletCollision : MonoBehaviourPun, IPunObservable
{
    public GameObject bullet;
    void OnTriggerEnter2D(Collider2D col)
    {
        PhotonViewProxy proxy = col.GetComponent<PhotonViewProxy>();
        if (proxy && proxy.photonView.Owner != this.photonView.Owner)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }

        

        // Debug.Log("bullet hit");
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }
}
