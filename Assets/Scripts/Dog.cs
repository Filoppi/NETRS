using UnityEngine;
using System.Collections;

public class Dog : InteractiveObject {

	private float timer2 = -1;
	bool usedTimer3 = false;
	protected Animator animator;
	public float animLenght = 10f;

	void Start () {
		animator = GetComponent<Animator> ();
	}

	void Update () {
		if (timer2 > 0) {
			timer2 -= Time.deltaTime;
			if (timer2 <= 0) {
				if (!usedTimer3) {
					usedTimer3 = true;
					timer2 = animLenght;
					animator.SetInteger ("AnimState", 2);
					audioSource.Play();
					audioSource.loop = true;
				} else {
					usedTimer3 = false;
					timer2 = -1;
					animator.SetInteger ("AnimState", 0);
					audioSource.loop = false;
				}
			}
		}
	}

	public override bool Use (bool calledByOtherInteractiveObject)
	{
		bool usedResult = base.Use (calledByOtherInteractiveObject);
		if (usedResult) {
			if (timer2 < 0) {
				timer2 = 0.63f;
				animator.SetInteger ("AnimState", 1);
			}
		}
		return usedResult;
	}
}
