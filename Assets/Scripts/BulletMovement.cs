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
    public Vector2 direction;
    Vector3 lastSyncedPos;
    public int bulletSpeed = 5;
    public NetworkedObjects networkedObjects;
    public static BulletMovement find;
    public PhotonView bulletView;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(waitForInstanciate());
        fire();
        StopCoroutine(waitForInstanciate());
        
    }
    public void fire() {
        //Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        //transform.Translate(direction * 2);    
        //GetComponent<Rigidbody2D>().AddForce(direction * 200);

        Vector2 mousePos = Input.mousePosition;
        Vector2 screenPos = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));
        Vector2 pos = transform.position;
        Quaternion q = Quaternion.FromToRotation(Vector2.up, screenPos - pos);
        GetComponent<Rigidbody2D>().AddForce(GetComponent<Rigidbody2D>().transform.up * 5);

    }

    IEnumerator waitForInstanciate()
    {
       
        yield return new WaitUntil(() => networkedObjects.netFire());
        
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
