using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{


	public GameObject gameObject;
	public BaseBlockScript script;


	public Block(GameObject gameObject, BaseBlockScript script) {
		this.gameObject = gameObject;
		this.script = script;
	}

}
