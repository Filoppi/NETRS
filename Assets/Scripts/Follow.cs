using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {
	public Transform target;
	public bool keepZ = true;
	private float initialZ = -10;
	public Vector3 offset = new Vector3(0f, 0f, 0f);

	void Start()
	{
		initialZ = transform.position.z;
	}

	private void LateUpdate()
	{
		if (target) {
			Vector3 newPosition = target.position + offset;
			if (keepZ)
				newPosition.z = initialZ;
			transform.position = newPosition;
		}
	}
}
