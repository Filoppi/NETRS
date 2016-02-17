using UnityEngine;
using System.Collections;

public class Strolling : MonoBehaviour {

	public float speed = .5f;
	
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		rb2d.velocity = new Vector2 (transform.localScale.x, 0) * -speed;
	}
}