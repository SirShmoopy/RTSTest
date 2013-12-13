﻿using UnityEngine;
using System.Collections;

public class CameraOperator : MonoBehaviour {

	public Texture2D selectionHighlight = null;
	public static Rect selection = new Rect(0, 0, 0, 0);
	private Vector3 startClick = -Vector3.one;
	
	// Update is called once per frame
	void Update () {
		CheckCamera();
	}

	private void CheckCamera() {
		if(Input.GetMouseButtonDown(0))
			startClick = Input.mousePosition;
		else if(Input.GetMouseButtonUp(0))
			startClick = -Vector3.one;
	
		if(Input.GetMouseButton(0)) {
			selection = new Rect(startClick.x, InvertMouse(startClick.y), Input.mousePosition.x - startClick.x, InvertMouse (Input.mousePosition.y) - InvertMouse(startClick.y));
			if(selection.width < 0) {
				selection.x += selection.width;
				selection.width = -selection.width;
			}
			if(selection.height < 0) {
				selection.y += selection.height;
				selection.height = -selection.height;
			}
		}
	}

	void OnGUI() {
		if(startClick != -Vector3.one) {
			GUI.color = new Color(1, 1, 1, 0.5f);
			GUI.DrawTexture(selection, selectionHighlight);
		}
	}

	public static float InvertMouse(float y) {
		return Screen.height - y;
	}
}
