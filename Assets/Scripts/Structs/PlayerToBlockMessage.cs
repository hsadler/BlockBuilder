using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerToBlockMessage
{

    public GameObject objectHit;
    public GameObject player;

    public PlayerToBlockMessage(GameObject player, GameObject objectHit) {
        this.player = player;
        this.objectHit = objectHit;
    }

}
