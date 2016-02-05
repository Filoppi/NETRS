using UnityEngine;
using System.Collections;

public class Strolling : MonoBehaviour {

	public float speed = .5f;
	
	private Rigidbody2D rigidbody2D;

	// Use this for initialization
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody2D.velocity = new Vector2 (transform.localScale.x, 0) * -speed;
	}
}