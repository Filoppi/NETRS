using UnityEngine;
using System.Collections;

public class RotatingInteractiveObject : InteractiveObject {

	public float time2 = 5;
	private float timer2 = -1;
	public float rotationSpeed = 2.8f;
	private float currentRotationSpeed = 2.8f;

	void Update () {
		if (timer2 > 0) {
			timer2 -= Time.deltaTime;
			if (timer2 <= 0) {
				transform.rotation.eulerAngles.Set (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 180f);
			} else {
				transform.Rotate(0, 0, currentRotationSpeed * Time.deltaTime);
				currentRotationSpeed = rotationSpeed * Mathf.Sqrt(timer2 / time2);
			}
		}
	}

	public override bool Use (bool calledByOtherInteractiveObject)
	{
		bool usedResult = base.Use (calledByOtherInteractiveObject);
		if (usedResult) {
			timer2 = time2;
			currentRotationSpeed = rotationSpeed;
		}
		return usedResult;
	}
}
