using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	public enum mother {trus, fals}

	void Start() {
		//print("Starting " + Time.time);
		//StartCoroutine(WaitAndPrint(1.0F));
		//InvokeRepeating("prpr", 2.0f, 1.0f);

		System.Reflection.Assembly assembly = System.Reflection.Assembly.GetAssembly(typeof(MonoBehaviour));
		//print("Class: " + assembly.FullName);
		System.Type[] types = assembly.GetTypes () ;
		foreach (System.Type thisT in types) {
			//print ("Name: " + thisT.FullName);
			//print ("Properties:");
			System.Reflection.PropertyInfo[] props = thisT.GetProperties ();
			foreach (System.Reflection.PropertyInfo prop in props) {
				//print (prop.Name);
			}
			//print ("Fields:");
			System.Reflection.FieldInfo[] fields = thisT.GetFields ();
			foreach (System.Reflection.FieldInfo field in fields) {
				//print (field.Name);
			}
			//print ("Methods:");
			System.Reflection.MethodInfo[] methods = thisT.GetMethods ();
			foreach (System.Reflection.MethodInfo method in methods) {
				//print (method.Name);
			}
		}

		//Timer newTimas = new gameObject.AddComponent<Timer> () as Timer; //new Timas ();
		var typeOf = typeof(Timer);
		var myMethod = typeOf.GetMethod ("prpra");
		GetType().GetMethod("prpra").Invoke(GetComponent<Timer>(), null);
		//typeof(Character).GetType ().GetMethod ("pasrpra").Invoke (null, null);
		//myMethod.Invoke(newTimas, null);
	}
	public static void prpra() {
		print("FUCK AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA ");
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
