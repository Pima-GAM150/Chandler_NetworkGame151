﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class PlayerColor : MonoBehaviourPun
{
	public Color[] playerColors;
    public Color currentColor { get; set; }

	public SpriteRenderer rend;
    public int order { get; set; }

	// synchronous assignment of color to the player from the "server"
	[PunRPC]
	public void SetColor( int order ) {
       
        currentColor = playerColors[ order ];
        this.order = order;
        rend.color = currentColor;
       
	}
}
