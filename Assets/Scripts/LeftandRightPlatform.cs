using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class LeftandRightPlatform : MonoBehaviourPun
{
    private float useSpeed;
    public float directionSpeed = 9.0f;
    float origX;
    public float distance = 10.0f;

    // Use this for initialization
    void Start()
    {
        origX = transform.position.x;
        useSpeed = -directionSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        photonView.RPC("leftandright", RpcTarget.All);
    }

    [PunRPC]
    public void leftandright() {
        if (origX - transform.position.x > distance)
        {
            useSpeed = directionSpeed; //flip direction
        }
        else if (origX - transform.position.x < -distance)
        {
            useSpeed = -directionSpeed; //flip direction
        }
        transform.Translate(useSpeed * Time.deltaTime, 0, 0);

    }
}
