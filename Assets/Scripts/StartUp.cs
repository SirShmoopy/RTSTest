using UnityEngine;
using System.Collections;

public class StartUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject[] obj = GameObject.FindGameObjectsWithTag(Tags.GeneralUnit);
		for(int i = 0; i < obj.Length; i++)
			obj[i].renderer.material.color = Color.blue;
	}

}
