using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBlockScript : MonoBehaviour
{


	public Block selfBlock;
	public BlockState blockState;
	public BlockStateMutation blockStateMutation;
	public float ghostBlockColorAlpha = 0.5f;


	// CODE EXAMPLE: how to handle instance variable overrides
	// (fields exposed via properties)
	// overridden by subclasses
	private bool acceptsPower = false;
	public virtual bool AcceptsPower { get { return acceptsPower; } }


	private Coroutine moveCoroutine;
	private Coroutine rotateCoroutine;


	protected IDictionary<string, Vector3> sensorTagToDirectionVector3 =
		new Dictionary<string, Vector3>()
	{
		{ "TopSensor", new Vector3(0, 1, 0) },
		{ "LeftSensor", new Vector3(-1, 0, 0) },
		{ "RightSensor", new Vector3(1, 0, 0) },
		{ "FrontSensor", new Vector3(0, 0, 1) },
		{ "BackSensor", new Vector3(0, 0, -1) },
		{ "BottomSensor", new Vector3(0, -1, 0) }
	};


	public void Awake() {
		blockState = new BlockState(transform.position, transform.rotation);
		blockStateMutation = new BlockStateMutation();
	}

	public void Start() {}

	public void SetSelfBlock(Block block) {
		selfBlock = block;
	}

	// overridden by subclasses
	public virtual void OnPlacement() {}
	public virtual void EvaluateAtTick() {}
	public virtual void PowerOn() {}
	public virtual void PowerOff() {}

	public void CommitMutationsAtTick() {
		if(blockStateMutation.dirty || AcceptsPower) {
			MoveBlock(
				blockStateMutation.GetCombinedMoveVectors(),
				SceneConfig.instance.tickDurationSeconds
			);
			RotateBlock(
				blockStateMutation.GetCombinedRotations(),
				SceneConfig.instance.tickDurationSeconds
			);
			if(blockStateMutation.power > 0) {
				PowerOn();
			} else {
				PowerOff();
			}
			blockStateMutation.Init();
		}
	}

	public void MoveBlock(
		Vector3 moveVector,
		float duration
	) {
		BlockManager bm = BlockManager.instance;
		// make sure we start at discrete position
		transform.position = blockState.position;
		// calculate what position to move to
		Vector3 newPosition = blockState.position + moveVector;
		// print("curr Pos: " + transform.position.ToString());
		// print("new Pos: " + newPosition.ToString());
		if(!bm.BlockExists(newPosition)) {
			// attempt to unset block on manager
			bool unsetStatus = bm.UnsetBlock(selfBlock);
			if(unsetStatus) {
				blockState.position = newPosition;
				// set block on manager
				bm.SetBlock(selfBlock);
				// stop previous coroutine
				if(moveCoroutine != null) {
					StopCoroutine(moveCoroutine);
				}
				// lerp move with new coroutine
				moveCoroutine = StartCoroutine(
					MoveBlockOverTime(
						transform.position,
						newPosition,
						duration
					)
				);
			} else {
				Debug.Log("unable to unset block at position: " +
					blockState.position.ToString());
			}
		}
	}

	private IEnumerator MoveBlockOverTime(
		Vector3 startPostion,
		Vector3 endPosition,
		float duration
	) {
		float timeCounter = 0;
		// while still time left
		while(timeCounter < duration) {
			timeCounter += Time.deltaTime;
			Vector3 newLerpPos = Vector3.Lerp(
				startPostion,
				endPosition,
				timeCounter / duration
			);
			// print("setting new lerp position: " + newLerpPos.ToString());
			transform.position = newLerpPos;
			yield return null;
		}
	}

	public void RotateBlock(Quaternion addRotation, float duration) {
		// make sure we start at discrete rotation
		transform.rotation = blockState.rotation;
		// calculate what rotation to rotate to
		Quaternion newRotation = blockState.rotation * addRotation;
		// print("curr Rotation: " + transform.rotation.eulerAngles.ToString());
		// print("new Rotation: " + newRotation.eulerAngles.ToString());
		blockState.rotation = newRotation;
		if(rotateCoroutine != null) {
			StopCoroutine(rotateCoroutine);
		}
		rotateCoroutine = StartCoroutine(
			RotateBlockOverTime(
				transform.rotation,
				newRotation,
				duration
			)
		);
	}

	private IEnumerator RotateBlockOverTime(
		Quaternion startRotation,
		Quaternion endRotation,
		float duration
	) {
		float timeCounter = 0;
		// while still time left
		while(timeCounter < duration) {
			timeCounter += Time.deltaTime;
			Quaternion newLerpQuaternion = Quaternion.Lerp(
				startRotation,
				endRotation,
				timeCounter / duration
			);
			// print("setting new lerp rotation: " +
			// 	newLerpQuaternion.eulerAngles.ToString());
			transform.rotation = newLerpQuaternion;
			yield return null;
		}
	}

	public void DestroyBlock() {
		BlockManager.instance.UnsetBlock(selfBlock);
		Destroy(gameObject);
	}

	public void DisableInteractions() {
		DisableAllColliders();
	}

	public void EnableInteractions() {
		EnableAllColliders();
	}

	public void AddNewBlockAsNeighbor(GameObject blockPrefab) {
		EnvironmentGeneration.instance.CreateBlock(
			blockPrefab,
			BlockManager.instance.ghostBlock.transform.position,
			BlockManager.instance.ghostBlock.transform.rotation
		);
	}

	// GHOST BLOCK METHODS

	private void SetGhostBlockAsNeighbor(Vector3 direction) {
		Vector3 ghostBlockPosition = transform.position + direction;
		BlockManager.instance.UpdateGhostBlockPosition(ghostBlockPosition);
		BlockManager.instance.ActivateGhostBlock();
	}

	public void TransformToGhostBlock() {
		DisableAllColliders();
		SetAllMaterialColorAlphas(ghostBlockColorAlpha);
	}

	// PLAYER INTERACTION METHODS

	public void PlayerRayHitInteraction(PlayerToBlockMessage message) {
		// get object hit data
		GameObject objectHit = message.objectHit;
		if(sensorTagToDirectionVector3.ContainsKey(objectHit.tag)) {
			// CODE EXAMPLE: get sensor direction vector offset by rotation
			Vector3 direction = transform.rotation * sensorTagToDirectionVector3[objectHit.tag];
			SetGhostBlockAsNeighbor(direction);
		}
	}

	public void PlayerLeftClickInteraction(PlayerToBlockMessage message) {
		DestroyBlock();
	}

	public void PlayerRightClickInteraction(PlayerToBlockMessage message) {
		// get player data
		GameObject player = message.player;
		GameObject blockPrefab =
			player.GetComponent<PlayerInventoryScript>().currentSelected;
		AddNewBlockAsNeighbor(blockPrefab);
	}

	public virtual void PlayerFKeyInteraction(PlayerToBlockMessage message) {}

	// IMPLEMENTATION METHODS

	protected void DisableAllColliders() {
		foreach (Collider col in GetComponentsInChildren<Collider>()) {
			col.enabled = false;
		}
	}

	protected void EnableAllColliders() {
		foreach (Collider col in GetComponentsInChildren<Collider>()) {
			col.enabled = true;
		}
	}

	protected void SetAllMaterialColorAlphas(float colorAlpha) {
		foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>()) {
			Color c = mr.material.color;
			c.a = colorAlpha;
			mr.material.color = c;
		}
	}

}
