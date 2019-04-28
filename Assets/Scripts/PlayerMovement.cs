﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviourPun, IPunObservable
{
    public float speed;
    public GameObject Player;
    public Transform appearance;
    public Transform target;
    public Vector3 lastSyncedPos;

    void Awake() {
        photonView.RPC("MakeSliderVisible", RpcTarget.All, false);
    }

    void Update()
    {

        // if this client owns this view, then control its movement using the input axes
        if (photonView.IsMine)
        {
            float y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

            target.Translate(x, y, 0f);

            // keep the player within the world's bounds
            if (!NetworkedObjects.find.world.bounds.Contains(target.position))
            {
                target.position = NetworkedObjects.find.world.bounds.ClosestPoint(target.position);
            }

            // move the renderer for this player immediately to its ideal position
            appearance.position = target.position;

           if (Input.GetMouseButtonDown(1))
            {
                photonView.RPC("MakeVisible", RpcTarget.All, true);
                photonView.RPC("MakeSliderVisible", RpcTarget.All, true);
                //Player.GetComponentInChildren<SpriteRenderer>().enabled = true;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                photonView.RPC("MakeVisible", RpcTarget.All, false);
                photonView.RPC("MakeSliderVisible", RpcTarget.All, false);
                //Player.GetComponentInChildren<SpriteRenderer>().enabled = false;
            }
        }
        else
        {
            // interpolate from the renderer's current position to its ideal position
            appearance.position = Vector3.Lerp(appearance.position, target.position, speed * Time.deltaTime);

            //appearance.position = target.position; // for jerky but accurate movement
        }


    }

    [PunRPC]
    public void MakeVisible(bool isVisible) => Player.GetComponentInChildren<SpriteRenderer>().enabled = isVisible;
    [PunRPC]
    public void MakeSliderVisible(bool isVisible) => Player.GetComponentInChildren<Image>().enabled = isVisible;

    // read and write to a serialized data stream to send this object's position information
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(target.position);
        }
        else
        {
            // receive data from the stream in *the same order* in which it was originally serialized
            target.position = (Vector3)stream.ReceiveNext();
        }
    }
}
