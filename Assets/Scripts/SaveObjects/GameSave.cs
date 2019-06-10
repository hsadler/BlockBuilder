using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSave
{

	// SERIALIZABLE DATA STRUCTURE FOR GAME SAVE


	public PlayerStruct player;
	public List<BlockStruct> blocks = new List<BlockStruct>();


	public GameSave() {}

	public void SetPlayer(PlayerStruct player) {
		this.player = player;
	}

	public void AddBlock(BlockStruct block) {
		this.blocks.Add(block);
	}


}
