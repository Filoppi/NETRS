using UnityEngine;
using System;
using System.Reflection;

[System.Serializable]
public class StartNode : BaseNode {

	public override string nodeName { get { return "StartNode"; } }

	protected override void Execute() {
		base.Execute ();
		End ();
	}

	public override System.Type[] inParameters { get ; protected set; }// = new System.Type[1] {typeof(GameObject)};
	//public override System.Type[] outParameters { get { } protected set { } } = System.Type[0];

	public StartNode() {
		linkedMethod = typeof(InteractiveObject).GetMethod ("NewM1");
		ParameterInfo[] parametersInfo = linkedMethod != null ? linkedMethod.GetParameters () : new ParameterInfo[0];
		inParameters = new System.Type[parametersInfo.Length];
		//foreach (ParameterInfo thisParameterInfo in parametersInfo) {
		for (int i = 0; i < parametersInfo.Length; i++) {
			inParameters.SetValue (parametersInfo[i].ParameterType, i);
		}
	}

	protected override void Update() {
		base.Update ();
		//if (isRunning) {
			End ();
		//}
		GameObject.FindGameObjectWithTag ("Stairs").transform.position += new Vector3 (1, 1, 1);
	}
}
