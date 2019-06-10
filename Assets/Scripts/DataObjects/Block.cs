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

	public BlockStruct ToBlockStruct() {
		Vector3Struct positionStruct = new Vector3Struct(gameObject.transform.position);
		Vector3Struct rotationStruct = new Vector3Struct(gameObject.transform.rotation.eulerAngles);
		return new BlockStruct(
			script.blockType,
			positionStruct,
			rotationStruct
		);
	}


}
