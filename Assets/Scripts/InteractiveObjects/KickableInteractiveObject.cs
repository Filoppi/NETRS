using UnityEngine;
using System.Collections;

public class KickableInteractiveObject : InteractiveObject {
	bool isFlying = false;
	bool isFalling = false;
	public GameObject xLeftLimitObj;
	public GameObject yDownLimitObj;

	void Update()
	{
		if (isFalling) {
			transform.position += new Vector3 (0f, -0.19f);
			if (transform.position.y <= yDownLimitObj.transform.position.y) {
				transform.position = yDownLimitObj.transform.position;
				isFalling = false;
				Invoke ("Arrived", 3);
			}
		}
		else if (isFlying) {
			transform.position += new Vector3 (-0.23f, 0.18f);
			if (transform.position.x <= xLeftLimitObj.transform.position.x) {
				transform.position = new Vector2 (xLeftLimitObj.transform.position.x, transform.position.y);
				isFlying = false;
				isFalling = true;
			}
		}
	}

	public void Arrived()
	{
		//yield return new WaitForSeconds (3.f);
		CameraManager.instance.ChangeCharacter(0);
	}

	public override bool Use (bool calledByOtherInteractiveObject)
	{
		bool usedResult = base.Use (calledByOtherInteractiveObject);
		if (usedResult) {
			isFlying = true;
		}
		return usedResult;
	}
}
