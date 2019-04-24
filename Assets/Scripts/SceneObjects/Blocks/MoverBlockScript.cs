using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverBlockScript : BaseBlockScript
{
    
    public new void Start() {
        base.Start();
    }

    public new void PlayerFKeyInteraction(PlayerToBlockMessage message) {
		base.PlayerFKeyInteraction(message);
        MoveBlockForward();
	}

    // IMPLEMENTATION METHODS

    private void MoveBlockForward() {
        Debug.Log("Moving mover block forward...");
    }

}
