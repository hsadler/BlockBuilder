﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToBlockMessage
{

	public GameObject objectHit;
	public GameObject player;

	public PlayerToBlockMessage(GameObject player, GameObject objectHit) {
		this.player = player;
		this.objectHit = objectHit;
	}

}
