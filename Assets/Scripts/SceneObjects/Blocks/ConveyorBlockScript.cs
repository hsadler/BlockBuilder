using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBlockScript : BaseBlockScript {
    
    public new void Start() {
        base.Start();
    }

	public override void EvaluateAtTick() {}

    public override void PlayerFKeyInteraction(PlayerToBlockMessage message) {
		base.PlayerFKeyInteraction(message);
	}

    // IMPLEMENTATION METHODS

    private void MoveNeighbor() {
        // TODO: stub
    }

}
