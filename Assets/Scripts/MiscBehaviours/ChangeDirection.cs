using UnityEngine;
using System.Collections;

public class ChangeDirection : MonoBehaviour {

	public float timeR = 5.2f;
	// Use this for initialization
	void Start () {
		StartCoroutine(ChangeFacingDirection());	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	IEnumerator ChangeFacingDirection() {
		yield return new WaitForSeconds(timeR);
		
		// Change direction
		Vector3 newVec = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		//newVec.x = -transform.localScale.x
		transform.localScale = newVec;
		StartCoroutine(ChangeFacingDirection());
	}
}
