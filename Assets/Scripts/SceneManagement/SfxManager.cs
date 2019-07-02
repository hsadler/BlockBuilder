
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using UnityEngine;

public class SfxManager : MonoBehaviour
{

	// REGISTRY FOR SFX


    public AudioSource blockPlaceSfx;
    public AudioSource blockDestroySfx;
    public AudioSource footstepsSfx;


	// the static reference to the singleton instance
	public static SfxManager instance { get; private set; }

	void Awake() {
		// singleton pattern
		if(instance == null) {
			instance = this;
		} else {
			Destroy(gameObject);
		}
	}

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        // footstepsSfx.Play();
    }


}
