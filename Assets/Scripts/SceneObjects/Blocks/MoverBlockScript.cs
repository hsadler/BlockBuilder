using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverBlockScript : BaseBlockScript {


    public float movePerTick = 1.0f;


    public new void Start() {
        base.Start();
    }

	public override void EvaluateAtTick() {
        MoveBlock(Vector3.forward, movePerTick, SceneConfig.instance.tickDurationSeconds);
    }

    public override void PlayerFKeyInteraction(PlayerToBlockMessage message) {
		base.PlayerFKeyInteraction(message);
	}

    // IMPLEMENTATION METHODS

}
