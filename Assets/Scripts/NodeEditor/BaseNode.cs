using UnityEngine;
using System;
using System.Reflection;

public enum nodeExecutionType {immediate, wait, asymmetrical}
//public enum nodeLinkType {get exec, get value, set (actions, wait...), branch (switch, sequencer)}
//public enum nodeReturnType { finish, branch (o, 1, 2 ???), tempFinish (sequence (o, 1, 2 ???)), keepAsAsync, wait}

[System.Serializable]
public class BaseNode { //abstract
	
	public virtual string nodeName { get { return "BaseNode"; } }
	public virtual int outputs { get { return 0; } }
	public int[] outputNodesId = new int[0];

	public virtual nodeExecutionType executionType  { get { return nodeExecutionType.immediate; } }

	public virtual System.Type[] inParameters { get { return new System.Type[0]; } protected set { } }// = System.Type[0]; //new System.Type[] {typeof(float), typeof(Vector2)};
	public virtual System.Type[] outParameters { get { return new System.Type[0]; } protected set { } }// = System.Type[0];

	protected MethodInfo linkedMethod;

	public BaseNode() {
	}

	public virtual bool hasInputNode { get { return false; } }

	//public static linkedMethod;
	//private static int classId = 0; //Needed???

	//public static NodesRuntimeManager linkedNodesManager
	//link to next nodes???
	//links to previous nodes (needed???)
	//links to every node linked to every input parameters (needed???) (otherwise you can write the value yourself???)
	//links to every node linked to every output parameters (needed???)
	//Could also delete the object when is not running anymore???

	public System.Object[] values; //Should be System.Object[]???
	public int id = -1; //protected???

	public void Begin() {
		//Broadcast(TurnOn)
		Execute ();
		if (executionType != nodeExecutionType.wait) {
			End ();
		}
	}

	protected virtual void Execute() {}

	protected virtual void Update() {}

	protected void End () {
		//Broadcast(TurnOff)
		if (executionType != nodeExecutionType.asymmetrical) {
			//if no existing links left???
			//Destroy (this); //Component, not the gameObject
			NodeRuntimeManager.instance.NextNode();
		}
	}
}
