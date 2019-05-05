using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverBlockScript : BaseBlockScript {
    
    public new void Start() {
        base.Start();
    }

	public override void EvaluateAtTick() {
        MoveBlockForward();
    }

    public override void PlayerFKeyInteraction(PlayerToBlockMessage message) {
		base.PlayerFKeyInteraction(message);
	}

    // IMPLEMENTATION METHODS

    private void MoveBlockForward() {
        BlockManager bm = BlockManager.instance;
        // check for block where about to move
        Vector3 amountToMove = transform.rotation * Vector3.forward;
        Vector3 positionToMove = transform.position + amountToMove;
        if(!bm.BlockExists(positionToMove)) {
            // unset on manager
            bm.UnsetBlock(gameObject);
            // move with transform
            transform.Translate(amountToMove, Space.World);
            // set on manager
            bm.SetBlock(gameObject);
        }
    }

}
