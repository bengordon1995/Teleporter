using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	public float throwForce;

	private Rigidbody rigid;
	private bool throwTrigger;
	[SerializeField]
	private bool inHand;
	private bool returnTrigger;
	private bool teleportTrigger;

	public TrailRenderer trail;
	public Camera camera;
	public GameObject player;
	private Rigidbody playerRigid;

	// Use this for initialization
	void Start () {
		rigid = this.GetComponent<Rigidbody>();
		playerRigid = player.GetComponent<Rigidbody>();
		inHand = true;
		throwTrigger = false;
		returnTrigger = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) ){
			//if ball in hand, throw it
			if(inHand){
				throwTrigger = true;
			}
			//otherwise, return it to hand
			else{
				teleportTrigger = true;
			}
		}	

		if (Input.GetMouseButtonDown(1)){
			//if ball in flight, teleport to ball
			if(!inHand){
				returnTrigger = true;
			}

		}
	}

	void FixedUpdate(){ 
		Vector3 ballReturnPosition = player.transform.position + camera.transform.forward * 2f;
		if(throwTrigger){
			trail.emitting = true;
			rigid.isKinematic = false;
			rigid.AddForce(camera.transform.forward * throwForce);
			inHand = false;
			throwTrigger = false;
		}
		if(returnTrigger){
			trail.emitting = false;
			rigid.transform.position = ballReturnPosition;
			rigid.velocity = Vector3.zero;
			rigid.isKinematic = true;
			inHand = true;
			returnTrigger = false;
		}
		if(teleportTrigger){
			trail.emitting = false;
			player.transform.position = this.transform.position;
			playerRigid.velocity = Vector3.zero;
			rigid.transform.position = ballReturnPosition;
			rigid.velocity = Vector3.zero;
			rigid.isKinematic = true;
			inHand = true;
			teleportTrigger = false;
		}
		if(inHand){
			rigid.transform.position = ballReturnPosition;
		}
	}


}
