using UnityEngine;
using System.Collections;

public class ToggleSpriteInteractiveObject : InteractiveObject {

	public SpriteRenderer SpriteToToggle;
	public float time2 = 5;
	private float timer2 = -1;

	void Start () {
		SpriteToToggle.enabled = false;
	}

	void Update () {
		if (timer2 > 0) {
			timer2 -= Time.deltaTime;
			SpriteToToggle.transform.localScale *= Random.Range (0.98f, 1.02f);
			if (timer2 <= 0) {
				SpriteToToggle.enabled = false;
			}
		}
	}

	public override bool Use (bool calledByOtherInteractiveObject)
	{
		bool usedResult = base.Use (calledByOtherInteractiveObject);
		if (usedResult) {
			timer2 = time2;
			SpriteToToggle.enabled = true;
		}
		return usedResult;
	}
}
