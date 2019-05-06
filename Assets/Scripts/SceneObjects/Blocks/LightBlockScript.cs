using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlockScript : BaseBlockScript {

    
    public bool lightOn = false;
    public float colorAlpha = 0.7f;


    private Light blockLight;
    

    public new void Start() {
        base.Start();
        blockLight = GetComponentInChildren<Light>();
    }

    public override void OnPlacement() {
        SetAllMaterialColorAlphas(colorAlpha);
    }

	public override void EvaluateAtTick() {
        ToggleBlockLight();
    }

    public override void PlayerFKeyInteraction(PlayerToBlockMessage message) {
		base.PlayerFKeyInteraction(message);
	}

    // IMPLEMENTATION METHODS

    private void ToggleBlockLight() {
        lightOn = !lightOn;
        blockLight.enabled = lightOn;
    }

}
