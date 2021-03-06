﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletMovement : MonoBehaviourPun, IPunObservable
{
    public GameObject bullet;
    public Transform bulletTarget;
    Vector3 lastSyncedPos;
    int bulletSpeed = 100;
    bool isInstanciated;



    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;

        if (photonView.IsMine)
        {
            
            if (!isInstanciated)
            {
                StartCoroutine(waitForInstanciate()); 
                StopAllCoroutines();
                StartCoroutine(destroyBullet());
           
                isInstanciated = true;
            }
    
            
                fire();
            


        }

        

    }
    public void fire()
    { 
        GetComponent<Rigidbody2D>().AddForce(GetComponent<Rigidbody2D>().transform.up * bulletSpeed);
    }

    IEnumerator waitForInstanciate()
    {
       
        yield return new WaitUntil(() => NetworkedObjects.find.netFire());

    }
    IEnumerator destroyBullet() {
        yield return new WaitForSeconds(1);
        PhotonNetwork.Destroy(this.gameObject);
    }
  


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {

            // don't send redundant data, like an unchanged position, over the network
            if (lastSyncedPos != bulletTarget.position)
            {
                lastSyncedPos = bulletTarget.position;

                // since there is new position data, serialize it to the data stream
                stream.SendNext(bulletTarget.position);
            }
        }
        else
        {
            // receive data from the stream in *the same order* in which it was originally serialized
            bulletTarget.position = (Vector3)stream.ReceiveNext();
        }

    }

}
