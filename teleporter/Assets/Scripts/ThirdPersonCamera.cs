using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThirdPersonCamera : MonoBehaviour 
{

	public float sensitivity;
	public float smoothing;
	public float followDistance;

	Vector2 mouseLook;
	Vector2 smoothV;

	GameObject player;

	void Start(){
		player = this.transform.parent.gameObject;
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update(){
		Vector2 mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
		mouseMovement = new Vector2(mouseMovement.x * sensitivity * smoothing, mouseMovement.y * sensitivity * smoothing);

		smoothV.x = Mathf.Lerp(smoothV.x, mouseMovement.x, 1f/smoothing);
		smoothV.y = Mathf.Lerp(smoothV.y, mouseMovement.y, 1f/smoothing);
		mouseLook += smoothV;
		mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

		transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
		player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);
		transform.localPosition = transform.localRotation * new Vector3(1f, 1.5f, -followDistance);
	}


}
