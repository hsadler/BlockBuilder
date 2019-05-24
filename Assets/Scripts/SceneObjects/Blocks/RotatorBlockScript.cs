using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorBlockScript : BaseBlockScript
{


    public float degreesPerTick = 90.0f;


    public new void Start() {
        base.Start();
    }

	public override void EvaluateAtTick() {
        Vector3 localAxis = Vector3.up;
        Quaternion r = Quaternion.Euler(localAxis * degreesPerTick);
        blockStateMutation.AddRotation(r);
    }

    public override void CommitMutationsAtTick() {
        RotateBlock(
            blockStateMutation.GetCombinedRotations(),
            SceneConfig.instance.tickDurationSeconds
        );
        blockStateMutation.Init();
    }

    public override void PlayerFKeyInteraction(PlayerToBlockMessage message) {
		base.PlayerFKeyInteraction(message);
	}

    // IMPLEMENTATION METHODS

}
