using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlainBlockScript : BaseBlockScript
{


	public new void Awake() {
		base.Awake();
		blockType = BlockTypes.instance.PLAIN_BLOCK;
	}

	public new void Start() {
		base.Start();
	}


}
