using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	void Start() {
		print("Starting " + Time.time);
		//StartCoroutine(WaitAndPrint(1.0F));
		//InvokeRepeating("prpr", 2.0f, 1.0f);
	}

	void prpr(float arg1, string arg2) {
		print("WaitAndPrint " + Time.time + arg2 + arg1);
	}

	IEnumerator WaitAndPrint(float waitTime) {
		int asd = 10;
		while (asd > 0) {
			yield return new WaitForSeconds(waitTime);
			print("WaitAndPrint " + Time.time);
			asd--;
		}
	}

	void Update () {
	
	}

}
