using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorBlockScript : BaseBlockScript {


    public float rotationPerTick = 90.0f;


    public new void Start() {
        base.Start();
    }

	public override void EvaluateAtTick() {
        RotateBlock(Vector3.up, rotationPerTick);
    }

    public override void PlayerFKeyInteraction(PlayerToBlockMessage message) {
		base.PlayerFKeyInteraction(message);
	}

    // IMPLEMENTATION METHODS

}
