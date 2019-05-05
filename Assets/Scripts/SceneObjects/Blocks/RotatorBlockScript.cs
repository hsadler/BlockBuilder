using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorBlockScript : BaseBlockScript {
    
    public new void Start() {
        base.Start();
    }

	public override void EvaluateAtTick() {
        RotateBlock();
    }

    public override void PlayerFKeyInteraction(PlayerToBlockMessage message) {
		base.PlayerFKeyInteraction(message);
	}

    // IMPLEMENTATION METHODS

    private void RotateBlock() {
         Vector3 addRotation = Vector3.up * 90.0f;

        // TODO: figure out Lerp later with coroutine

        // NAIVE SOLUTION: snap to new rotation
        transform.Rotate(addRotation, Space.Self); 
    }

}
