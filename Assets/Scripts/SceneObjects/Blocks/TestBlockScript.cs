using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlockScript : BaseBlockScript
{


	public new void Awake() {
		base.Awake();
		blockType = BlockTypes.instance.TEST_BLOCK;
	}

	public new void Start() {
		base.Start();
	}


}
