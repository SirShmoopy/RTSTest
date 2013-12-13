using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	
	public float scrollSpeed = 15f;
	public float scrollEdge = 0.01f;
	public float panSpeed = 10f;
	public float currentZoom = 0f;
	public float zoomSpeed = 1f;
	public float zoomRotation = 1f;
	public float maxZoom = 5f;

	private Vector2 zoomRange;	
	private Vector3 initPos;
	private Vector3 initRotation;

	void Start() {
		initPos = transform.position;
		initRotation = transform.eulerAngles;
		zoomRange = new Vector2(initPos.y, maxZoom);
	}

	void Update() {
		//Pan
		if(Input.GetKey("mouse 2")) {
			transform.Translate(Vector3.right * Time.deltaTime * panSpeed * (Input.mousePosition.x - Screen.width * 0.5f) / (Screen.width * 0.5f), Space.World);
			transform.Translate(Vector3.forward * Time.deltaTime * panSpeed * (Input.mousePosition.y - Screen.height * 0.5f)/(Screen.height * 0.5f), Space.World);
		} else {
			if(Input.GetKey("right") /**|| Input.mousePosition.x >= Screen.width * (1 - scrollEdge)*/) {
				transform.Translate(Vector3.right * Time.deltaTime * scrollSpeed, Space.World);
			} else if(Input.GetKey("left") /**|| Input.mousePosition.x <= Screen.width * scrollEdge*/) {
				transform.Translate(Vector3.right * Time.deltaTime * -scrollSpeed, Space.World);
			}

			if(Input.GetKey("up") /**|| Input.mousePosition.y >= Screen.height * (1 - scrollEdge)*/) {
				transform.Translate(Vector3.forward * Time.deltaTime * scrollSpeed, Space.World);
			} else if(Input.GetKey("down") /**|| Input.mousePosition.y <= Screen.height * scrollEdge*/) {
				transform.Translate(Vector3.forward * Time.deltaTime * -scrollSpeed, Space.World);
			}
		}

		//Zoom
		currentZoom -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 1000 * zoomSpeed;
		currentZoom = Mathf.Clamp(currentZoom, zoomRange.x, zoomRange.y);
		float yPos = transform.position.y - (transform.position.y - (initPos.y + currentZoom)) * 0.1f;
		float eAngles = transform.eulerAngles.x - (transform.eulerAngles.x - (initRotation.x + currentZoom * zoomRotation)) * 0.1f;

		transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
		transform.eulerAngles = new Vector3(eAngles, transform.eulerAngles.y, transform.eulerAngles.z);
	}

}