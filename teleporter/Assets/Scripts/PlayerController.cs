using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float maxSpeed;
	public float acceleration;
	public float jumpImpulse;

	private Rigidbody rigid;
	private float strafe;
	private float forward;
	private bool jumpTrigger;

	public Camera camera;

	public  GameObject player;

	private Vector3 movement;

	public enum moveState{
		walk,
		crouch,
		fly
	}
	public moveState currentMoveState;

	// Use this for initialization
	void Start () {
		rigid = this.GetComponent<Rigidbody>();
		this.currentMoveState = moveState.walk;
	}
	
	// Update is called once per frame
	void Update () {
		strafe = Input.GetAxis("Horizontal");
		forward = Input.GetAxis("Vertical");
		if (Input.GetButton("Crouch")){
			currentMoveState = moveState.crouch;
		}
		else{
			currentMoveState = moveState.walk;
		}
	}

	void FixedUpdate(){
		if(this.currentMoveState == moveState.walk){
			if (isGrounded()){
				Vector3 targetVelocity = transform.TransformDirection(new Vector3(strafe, 0f, forward)) * maxSpeed;
				Vector3 velocityChange = targetVelocity - rigid.velocity;
				velocityChange.x = Mathf.Clamp(velocityChange.x, -acceleration, acceleration);
				velocityChange.z = Mathf.Clamp(velocityChange.z, -acceleration, acceleration);
				velocityChange.y = 0;
				rigid.AddForce(velocityChange, ForceMode.VelocityChange);

				if (Input.GetButton("Jump")){
					rigid.AddForce(new Vector3(0f, jumpImpulse, 0f), ForceMode.Impulse);
				}
			}
		}

		else if (this.currentMoveState == moveState.crouch){
				if (isGrounded()){
				Vector3 targetVelocity = transform.TransformDirection(new Vector3(strafe, 0f, forward)) * maxSpeed;
				Vector3 velocityChange = targetVelocity - rigid.velocity;
				velocityChange.x = Mathf.Clamp(velocityChange.x, -acceleration, acceleration);
				velocityChange.z = Mathf.Clamp(velocityChange.z, -acceleration, acceleration);
				velocityChange.y = 0;
				rigid.AddForce(velocityChange, ForceMode.VelocityChange);

				if (Input.GetButton("Jump")){
					rigid.AddForce(camera.transform.forward * jumpImpulse, ForceMode.Impulse);
				}
			}
		}
	}

	//assumes players pivot is at their feet, not center mass
	private bool isGrounded(){
		RaycastHit hit;
		return Physics.Raycast(this.transform.position, Vector3.down, out hit, 0.1f);
	}

}
