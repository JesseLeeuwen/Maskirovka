using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActivity : MonoBehaviour {

	public void toggle(){
		gameObject.SetActive(!gameObject.activeInHierarchy);
	}
}
