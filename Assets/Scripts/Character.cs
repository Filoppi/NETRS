using UnityEngine;
using System.Collections;

public enum state { steady, controlled, selfControlled, action }
public enum behaviour { idle, patrol }
public enum actions {
	goTo,
	follow,
	triggerAnimation, //Can finish upon animation end, or times executed, or time...
	waitFor, //Don't change the previous character state
}

public class Character : MonoBehaviour {
	public int id = -1;
	public bool isActive = false;
	public float speed = 8f; //To move???
	bool isMoving = false;
	public bool isAnimating = true;
	public int animationPhase = 0;

	protected Rigidbody2D rigidbody2D;
	public Animator animator;

	void Awake () {
		rigidbody2D = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
	}

	public virtual void Action(int number) {
	}
}
