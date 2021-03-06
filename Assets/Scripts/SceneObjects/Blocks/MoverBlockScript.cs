﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverBlockScript : BaseBlockScript
{


	public Vector3 moveDirection = Vector3.forward;
	public float distancePerTick = 1.0f;


	public new void Awake() {
		base.Awake();
		blockType = BlockTypes.instance.MOVER_BLOCK;
	}

	public new void Start() {
		base.Start();
	}

	public override void AsyncEvalInteractions() {
		Vector3 moveVector = (blockState.rotation * moveDirection) * distancePerTick;
		blockStateMutation.AddMoveVector(moveVector);
	}

	public override void PlayerFKeyInteraction(PlayerToBlockMessage message) {
		base.PlayerFKeyInteraction(message);
	}

	// IMPLEMENTATION METHODS

}
