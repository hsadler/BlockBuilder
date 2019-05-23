﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverBlockScript : BaseBlockScript
{


    public Vector3 moveDirection = Vector3.forward;
    public float distancePerTick = 1.0f;


    public new void Start() {
        base.Start();
    }
    
    public override void EvaluateAtTick() {
        Vector3 moveVector = (transform.rotation * moveDirection) * distancePerTick; 
        blockStateMutation.moveVectors.Add(moveVector);
    }

    public override void CommitMutationsAtTick() {
        MoveBlock2(
            blockStateMutation.getCombinedMoveVectors(),
            SceneConfig.instance.tickDurationSeconds
        );
        blockStateMutation.Init();
    }

    public override void PlayerFKeyInteraction(PlayerToBlockMessage message) {
		base.PlayerFKeyInteraction(message);
	}

    // IMPLEMENTATION METHODS

}
