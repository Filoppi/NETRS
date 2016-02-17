using UnityEngine;
using System.Collections;

public class ChangeDirection : MonoBehaviour {

	public bool disabled = false; //Needed???
	public Vector2 XMinAndMax;
	public float randomizedXToAdd = 0;
	private float currentRandomizedXToAdd = 0;
	//public float toAddWaitTime???

	//public float timer = 5.2f;

	void Start () {
		//StartCoroutine(ChangeFacingDirection());
		currentRandomizedXToAdd = Random.Range(-randomizedXToAdd, randomizedXToAdd);
	}

	void Update () {
		if (transform.position.x + currentRandomizedXToAdd > XMinAndMax.y && transform.localScale.x < 0) { //To invert to >=
			Vector3 newVec = transform.localScale;
			newVec.x = -newVec.x;
			transform.localScale = newVec;
			currentRandomizedXToAdd = Random.Range(-randomizedXToAdd, randomizedXToAdd);
		} else if (transform.position.x + currentRandomizedXToAdd < XMinAndMax.x && transform.localScale.x >= 0) { //To inverto to <
			Vector3 newVec = transform.localScale;
			newVec.x = -newVec.x;
			transform.localScale = newVec;
			currentRandomizedXToAdd = Random.Range(-randomizedXToAdd, randomizedXToAdd);
		}
	}
	
//	IEnumerator ChangeFacingDirection() {
//		yield return new WaitForSeconds(timer);
//		
//		// Change direction
//		Vector3 newVec = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
//		//newVec.x = -transform.localScale.x
//		transform.localScale = newVec;
//		StartCoroutine(ChangeFacingDirection());
//	}
}
