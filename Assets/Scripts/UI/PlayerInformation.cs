using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInformation : MonoBehaviour
{
    
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = string.Format(
            "Player velocity magnitude: {0}", 
            player.GetComponent<Rigidbody>().velocity.magnitude
        );    

    }
}
