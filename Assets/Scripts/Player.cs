using UnityEngine;
using System.Collections;

public class Player : Character {
	public bool isControlled = false;
	private bool canGoUp = false;

	public Vector2 maxVelocity = new Vector2(3, 5);
	public float climbingSpeed = 3.5f;
	private bool isClimbing = false; //temp
	public InteractiveObject linkedObjet;
	public float actionRadius = 3.2f;
	public GameObject playerArm;
	public GameObject HandSocket;
	private float armTargetRotation = 0f;
	public float armRotationSpeed = 2.36f;
	private bool onceActon = false;
		
	public PlayerController controller;
	
	public override void Awake(){
		base.Awake ();
		controller = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerController> () as PlayerController;
	}

	public void Action() {
		if (!linkedObjet) {
			InteractiveObject tempObj = null;
			InteractiveObject[] intObjs = FindObjectsOfType (typeof(InteractiveObject)) as InteractiveObject[];
			float minDist = 999999f;
			foreach (InteractiveObject intObj in intObjs) {
				float dist = (intObj.transform.position - transform.position).magnitude;
				if (dist < minDist) {
					minDist = dist;
					tempObj = intObj;
				}
			}
			if (minDist <= actionRadius && tempObj) {
				int result = tempObj.Action ();
				//if -1 play NO!! sound????
				if (result == 0) { //Start pick animation
					linkedObjet = tempObj;
					Vector3 tempScale = linkedObjet.transform.lossyScale;
					if (HandSocket)
						linkedObjet.transform.SetParent (HandSocket.transform);
					else
						linkedObjet.transform.SetParent (transform);
					linkedObjet.transform.localPosition = new Vector3 (0, 0, linkedObjet.transform.localPosition.z);
					linkedObjet.transform.localScale = new Vector3((1 / transform.lossyScale.x) * tempScale.x, (1 / transform.lossyScale.y) * tempScale.y, (1 / transform.lossyScale.z) * tempScale.z);
					linkedObjet.transform.localRotation = Quaternion.identity;
					onceActon = false;
					armTargetRotation = 90;
				} else if (result == 1) {
					onceActon = true;
					armTargetRotation = 90;//Use animation???
				}
			}
		} else {
			int result = linkedObjet.Action ();
			//if -1 play NO!! sound????
			//else use animation???
		}
	}

	public void Drop() {
		if (linkedObjet) {
			linkedObjet.Drop ();
		}
		linkedObjet = null;
		onceActon = false;
		armTargetRotation = 3;
	}
		
	void OnCollisionEnter2D(Collision2D target){
		if (target.gameObject.tag == "Stairs") {
			canGoUp = true;
			rb2d.gravityScale = 0;
		}
		else if (target.gameObject.tag == "StairsEnd") {
			canGoUp = false;
			rb2d.gravityScale = 12;
		}
	}

	void OnCollisionExit2D(Collision2D target) {
		//if (target.gameObject.tag == "Stairs") {
		//	canGoUp = false;
		//	rb2d.gravityScale = 12;
		//}
	}

	void Update () {
		if (playerArm && playerArm.transform.localRotation.eulerAngles.z != armTargetRotation) {
			if (playerArm.transform.localRotation.eulerAngles.z > armTargetRotation) {
				float newZ = Mathf.Max(armTargetRotation, playerArm.transform.localRotation.eulerAngles.z - armRotationSpeed);
				playerArm.transform.localRotation.eulerAngles.Set (playerArm.transform.localRotation.eulerAngles.x, playerArm.transform.localRotation.eulerAngles.y, newZ);
			}
			else {
				float newZ = Mathf.Min(armTargetRotation, playerArm.transform.localRotation.eulerAngles.z + armRotationSpeed);
				playerArm.transform.localRotation.eulerAngles.Set (playerArm.transform.localRotation.eulerAngles.x, playerArm.transform.localRotation.eulerAngles.y, newZ);
				if (onceActon && playerArm.transform.localRotation.eulerAngles.z > armTargetRotation) {
					onceActon = false;
					armTargetRotation = 3;
				}
			}
		}

		//var forceX = 0f;
		//var forceY = 0f;
		
		//var absVelX = Mathf.Abs (rb2d.velocity.x);
		//var absVelY = Mathf.Abs (rb2d.velocity.y);

//			if (absVelX < maxVelocity.x) {
//				
//				forceX = speed * controller.moving.x;
//
//				transform.localScale = new Vector3 (forceX > 0 ? 1 : -1, 1, 1);
//			}
//			animator.SetInteger ("AnimState", 1);
//		} else {
//			animator.SetInteger ("AnimState", 0);
//		}
		
//		if (controller.moving.y > 0) {
//			if (absVelY < maxVelocity.y) {
//				forceY = climbingSpeed * controller.moving.y;
//			}
//			animator.SetInteger ("AnimState", 2);
//		} else if (absVelY > 0) {
//			animator.SetInteger("AnimState", 3);
//		}
		
		//rb2d.AddForce (new Vector2 (forceX, forceY));
	}
	
	public void MoveRight(float alpha = 0) {
		if (!isControlled || isClimbing) alpha = 0;
		rb2d.AddForce (new Vector2(speed * alpha, 0));
		//rb2d.velocity = (new Vector2(speed * Time.deltaTime * 300 * alpha, 0));

		if (Mathf.Abs(alpha) < 0.13f) animator.SetInteger ("AnimState", 0);
		else animator.SetInteger ("AnimState", 1);
		animator.SetFloat ("PlaySpeed", Mathf.Abs(alpha) < 0.13f ? 0f : Mathf.Abs(alpha));

		if (alpha != 0 && (alpha > 0 == transform.localScale.x > 0)) {
			Vector3 newVec = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			transform.localScale = newVec;
		}
	}
	
	public void MoveUp(float alpha = 1) {
		if (!isControlled || !canGoUp) {
			isClimbing = false;
			rb2d.velocity = (new Vector2 (0, 0));
		} else {
			isClimbing = true;
			rb2d.velocity = (new Vector2 (0, climbingSpeed * alpha));
		}
	}
}
