using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlockScript : BaseBlockScript {
    
    public new void Start() {
        base.Start();
    }

	public override void EvaluateAtTick() {
        ToggleBlockLight();
    }

    public override void PlayerFKeyInteraction(PlayerToBlockMessage message) {
		base.PlayerFKeyInteraction(message);
	}

    // IMPLEMENTATION METHODS

    private void ToggleBlockLight() {
        // TODO: implement
    }

}
