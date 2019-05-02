using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RotatingPlatform : MonoBehaviourPun
{

    public float rotSpeed;
    public float maxRotSpeed = 100f;
    public float currentRotation = 0f;

    void Update()
    {
        photonView.RPC("rotating", RpcTarget.All);
    }

    [PunRPC]
    public void rotating() {
        //Rotates on the y axis
        transform.Rotate(new Vector3(0f, 0f, currentRotation * Time.deltaTime));
        //sets speed of rotation
        currentRotation = Mathf.Min(currentRotation + Time.deltaTime * rotSpeed, maxRotSpeed);
    }

}