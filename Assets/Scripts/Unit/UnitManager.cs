using UnityEngine;
using System.Collections;

public class UnitManager : MonoBehaviour {

	public bool selected = false;
	public float speed = 10f;
	public float collisionOffset;
	
	private Vector3 targetPosition;
	private Quaternion targetRotation;
	private bool moveNow = false;

	void Start() {
		targetPosition = rigidbody.position;
	}

	void Update () {
		selectionManagement();
	}
	void FixedUpdate() {

		movementManagement();
	}

	void selectionManagement() {
		if(renderer.isVisible && Input.GetMouseButton(0)) {
			Vector3 camPos = Camera.main.WorldToScreenPoint(rigidbody.position);
			camPos.y = CameraOperator.InvertMouse(camPos.y);
			selected = CameraOperator.selection.Contains(camPos);
		}
		
		if(selected)
			transform.GetChild(0).light.intensity = 0.25f;
		else 
			transform.GetChild(0).light.intensity = 0f;

		if(selected && Input.GetKeyDown("mouse 1"))
			moveNow = true;
	}

	void movementManagement() {
		if(moveNow) {
			moveNow = false;
			Plane unitPlane = new Plane(Vector3.up, rigidbody.position);
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			float hitdist = 0.0f;
			
			if(unitPlane.Raycast(ray, out hitdist)) {
				Vector3 targetPoint = ray.GetPoint(hitdist);
				targetPosition = ray.GetPoint(hitdist);
				targetRotation = Quaternion.LookRotation(targetPoint - rigidbody.position);
				rigidbody.rotation = targetRotation;
			}

		}
		rigidbody.position = Vector3.MoveTowards(rigidbody.position, targetPosition, speed * Time.fixedDeltaTime);
	}

	void OnCollisionEnter(Collision collision) {
		if(Vector3.Distance(rigidbody.position, targetPosition) <= collisionOffset)
			targetPosition = rigidbody.position;
	}

}
