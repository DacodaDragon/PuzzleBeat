using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFps : MonoBehaviour {

	void Update () {
        GetComponent<Text>().text = (1 / Time.deltaTime).ToString();
	}
}
