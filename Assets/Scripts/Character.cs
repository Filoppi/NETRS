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

[DisallowMultipleComponent]
public class Character : MonoBehaviour {
	public System.Type whichType;
	public int id = -1;
	public bool isActive = false;
	public float speed = 8f; //To move???
	bool isMoving = false;
	public bool isAnimating = true;
	public int animationPhase = 0;

	protected Rigidbody2D rb2d;
	public Animator animator;

	public virtual void Awake () {
		rb2d = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
	}

	public virtual void Action(int number) {
	}
	public static void prpra() {
		print("FUCK AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA ");
	}
}
