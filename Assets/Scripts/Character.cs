using UnityEngine;
using System.Collections;

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
