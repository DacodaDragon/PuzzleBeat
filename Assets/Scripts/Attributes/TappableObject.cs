using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TouchListener))]
public class TappableObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<TouchListener>().onTouchTap += OnTap;
	}

    private void OnTap()
    {
        Destroy(gameObject);
    }
}
