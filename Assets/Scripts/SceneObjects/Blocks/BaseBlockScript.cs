using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBlockScript : MonoBehaviour
{


	public string blockType;
	public Block selfBlock;
	public BlockState blockState;
	public BlockState onDeckBlockState;
	public BlockState lastBlockState;
	public BlockStateMutation blockStateMutation;
	public float ghostBlockColorAlpha = 0.5f;
	public Material fadeMaterial;

	
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
		blockType = BlockTypes.instance.BASE_BLOCK;
		blockState = new BlockState(transform.position, transform.rotation);
		onDeckBlockState = new BlockState(transform.position, transform.rotation);
		lastBlockState = new BlockState(transform.position, transform.rotation);
		blockStateMutation = new BlockStateMutation();
	}

	public void Start() {}

	public void SetSelfBlock(Block block) {
		selfBlock = block;
	}

	// overridden by subclasses
	public virtual void OnPlacement() {}
	public virtual void AsyncEvalInteractions() {}
	public virtual void PowerOn() {}
	public virtual void PowerOff() {}

	// currently handles position and rotation
	public void AsyncValidateMutations() {
		if(blockStateMutation.dirty) {
			// handle move mutation
			Vector3 moveVector = blockStateMutation.GetCombinedMoveVectors();
			// limit move length to 1
			moveVector = Functions.ApplyBoundsToVector3(moveVector, -1, 1);
			Vector3 newPosition = blockState.position + moveVector;
			// is the new position vacant?
			if(!BlockManager.instance.BlockExists(newPosition)) {
				// update block state to be committed
				onDeckBlockState.position = newPosition;
			}
			// handle rotation mutation
			Quaternion newRotation = blockState.rotation * blockStateMutation.GetCombinedRotations();
			onDeckBlockState.rotation = newRotation;
		}
	}

	// currently handles position and rotation
	public void AsyncCommitMutations() {
		if(blockStateMutation.dirty) {
			// unset on Block Manager dictionary
			bool unsetStatus = BlockManager.instance.UnsetBlock(selfBlock);
			if(unsetStatus) {
				blockState.position = onDeckBlockState.position;
				BlockManager.instance.SetBlock(selfBlock);
			}
			blockState.rotation = onDeckBlockState.rotation;
		}
	}

	public void CommitBlockStateToUI() {
		if(blockStateMutation.dirty || AcceptsPower) {
			MoveBlock(
				lastBlockState.position,
				blockState.position,
				SceneConfig.instance.tickDurationSeconds
			);
			lastBlockState.position = blockState.position;
			RotateBlock(
				lastBlockState.rotation,
				blockState.rotation,
				SceneConfig.instance.tickDurationSeconds
			);
			lastBlockState.rotation = blockState.rotation;
			if(blockStateMutation.power > 0) {
				PowerOn();
			} else {
				PowerOff();
			}
			blockStateMutation.Init();
		}
	}

	public void MoveBlock(
		Vector3 startPosition,
		Vector3 endPosition,
		float duration
	) {
		transform.position = startPosition;
		// stop previous coroutine
		if(moveCoroutine != null) {
			StopCoroutine(moveCoroutine);
		}
		// lerp move with new coroutine
		moveCoroutine = StartCoroutine(
			MoveBlockOverTime(
				startPosition,
				endPosition,
				duration
			)
		);
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
			transform.position = newLerpPos;
			yield return null;
		}
	}

	public void RotateBlock(
		Quaternion startRotation, 
		Quaternion endRotation, 
		float duration
	) {
		transform.rotation = startRotation;
		if(rotateCoroutine != null) {
			StopCoroutine(rotateCoroutine);
		}
		rotateCoroutine = StartCoroutine(
			RotateBlockOverTime(
				startRotation,
				endRotation,
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
		Player p = PlayerManager.instance.player;
		BlockManager.instance.CreateBlock(
			blockPrefab,
			p.inventoryScript.ghostBlockGO.transform.position,
			p.inventoryScript.ghostBlockGO.transform.rotation
		);
	}

	// GHOST BLOCK METHODS

	private void SetGhostBlockAsNeighbor(Vector3 direction) {
		Vector3 ghostBlockPosition = transform.position + direction;
		Player p = PlayerManager.instance.player;
		p.inventoryScript.UpdateGhostBlockPosition(ghostBlockPosition);
		p.inventoryScript.ActivateGhostBlock();
	}

	public void TransformToGhostBlock() {
		DisableAllColliders();
		Renderer r = GetComponent<Renderer>();
		r.material = fadeMaterial;
		Color c = r.material.color;
		c.a = ghostBlockColorAlpha;
		r.material.color = c;
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

	public virtual void PlayerFKeyInteraction(PlayerToBlockMessage message) {
		print("block coordinates: " + blockState.position.ToString());
	}

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
