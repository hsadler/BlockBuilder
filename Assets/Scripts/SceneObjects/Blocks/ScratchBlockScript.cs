using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchBlockScript : BaseBlockScript
{

	// THIS BLOCK IS FOR TESTING, DO WHATEVER YOU WANT WITH IT


	public new void Awake() {
		base.Awake();
		blockType = BlockTypes.instance.SCRATCH_BLOCK;
	}

	public new void Start() {
		base.Start();
	}


}
