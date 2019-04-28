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

	// synchronous assignment of color to the player from the "server"
	[PunRPC]
	public void SetColor( int order ) {
       
        currentColor = playerColors[ order ];
        order = 0;
        rend.color = currentColor;
       
	}
}