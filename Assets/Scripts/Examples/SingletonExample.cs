using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonExample : MonoBehaviour
{

	// the static reference to the singleton instance
	public static SingletonExample instance { get; private set; }

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		// singleton pattern
		if(instance == null) {
			instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}

}
