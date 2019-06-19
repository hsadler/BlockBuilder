using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorBlockScript : BaseBlockScript
{


	public float degreesPerTick = 90.0f;


	public new void Awake() {
		base.Awake();
		blockType = BlockTypes.instance.ROTATOR_BLOCK;
	}

	public new void Start() {
		base.Start();
	}

	public override void AsyncEvalInteractions() {
		Vector3 localAxis = Vector3.up;
		Quaternion r = Quaternion.Euler(localAxis * degreesPerTick);
		blockStateMutation.AddRotation(r);
	}

	public override void PlayerFKeyInteraction(PlayerToBlockMessage message) {
		base.PlayerFKeyInteraction(message);
	}

	// IMPLEMENTATION METHODS
	

}
