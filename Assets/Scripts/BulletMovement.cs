using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletMovement : MonoBehaviourPun, IPunObservable
{
    public GameObject Player;
    public GameObject Bullet;
    public Transform bulletAppearance;
    public Transform bulletTarget;
    Vector3 lastSyncedPos;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  

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
