using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlockScript : BaseBlockScript
{


	private bool acceptsPower = true;
	public override bool AcceptsPower { get { return acceptsPower; } }
	public bool lightOn = false;
	public float colorAlpha = 0.7f;


	private Light blockLight;


	public new void Start() {
		base.Start();
		blockLight = GetComponentInChildren<Light>();
		TurnOffBlockLight();
	}

	public override void OnPlacement() {
		SetAllMaterialColorAlphas(colorAlpha);
	}

	// NOT USING
	// public override void BeforeEvaluateAtTick() {
	// 	TurnOffBlockLight();
	// }

	public override void EvaluateAtTick() {}

	public override void PlayerFKeyInteraction(PlayerToBlockMessage message) {
		base.PlayerFKeyInteraction(message);
	}

	public override void PowerOn() {
		TurnOnBlockLight();
	}

	public override void PowerOff() {
		TurnOffBlockLight();
	}

	public bool GetBlockLightStatus() {
		return lightOn;
	}

	public void TurnOnBlockLight() {
		lightOn = true;
		blockLight.enabled = true;
	}

	public void TurnOffBlockLight() {
		lightOn = false;
		blockLight.enabled = false;
	}

	public void ToggleBlockLight() {
		lightOn = !lightOn;
		blockLight.enabled = lightOn;
	}

	// IMPLEMENTATION METHODS

}
