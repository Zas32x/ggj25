using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Credits: MonoBehaviour {
	public void close() {
		gameObject.SetActive(false);
	}

	public void open() {
		gameObject.SetActive(true);
	}
}
