using UnityEngine;
using System.Collections;

public class LookForward : MonoBehaviour {

	public Transform sightStart, sightEnd;
	
	private bool collision;

	// Update is called once per frame
	void Update () {
		collision = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Obstacle"));
		Debug.DrawLine(sightStart.position, sightEnd.position, Color.green);
	}
}
