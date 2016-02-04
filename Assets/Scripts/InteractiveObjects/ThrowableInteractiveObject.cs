using UnityEngine;
using System.Collections;

public class ThrowableInteractiveObject : InteractiveObject {
	public override bool Use (bool calledByOtherInteractiveObject)
	{
		bool usedResult = base.Use (calledByOtherInteractiveObject);
		if (usedResult) {
			Drop ();
			float xForce = transform.lossyScale.x >= 0f ? 30f : -30f;
			rigidBody2D.AddForce (new Vector3 (xForce, 3f, 0f));
		}
		return usedResult;
	}
}
