using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoadSpowner : MonoBehaviour {

    [SerializeField]
    private GameObject centerEye;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.rotation = Quaternion.Euler(0, centerEye.transform.rotation.y, 0);
	}
}
