using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//

[System.Serializable]
public class Node {
	public BaseNode node { set; get; }
	public System.Type type = typeof(BaseNode); //Save as string??? //To update later (if needed)
	public int id = -1; //to make index and order list by index when starting
	//public System.Type[] inParameters;
	public System.Object[] inValues;
	public int[][,] inParametersLinks; //which params, (other node id, other node out param id)
	//public System.Type[] outParameters;
	public System.Object[] outValues;
	public int[][,] outParametersLinks; //which params, (other node id, other node in param id)
	public Node(System.Type nodeType) {
		type = nodeType;
		node = (BaseNode)System.Activator.CreateInstance (type);
		id = Random.Range (0, 10000);
		//this = new StartNode ();
		//node = (BaseNode)System.Convert.ChangeType (node, type);

		inValues = new System.Object[2];
		inValues [0] = 5;
	}
	public Node(BaseNode newNode) {
		BaseNode node = newNode;
		type = node.GetType();
		id = Random.Range (10001, 20000);

		inValues = new System.Object[3];
		inValues [0] = 7;
	}
	public void Init () {
		//inParameters = _node.inParameters;
		//outParameters = _node.outParameters;
	}
}

[System.Serializable]
public class NodeGraph {
	//[HideInInspector]
	public List<Node> nodes = new List<Node>(0); //Should be a dictionary of node and id
	//public System.Type[] currentParameters; //???
	public string name = ""; 
	//[System.NonSerialized]
	public NodeEditor linkedWindow;
}

[DisallowMultipleComponent]
public class NodeRuntimeManager : MonoBehaviour {
	
	public static NodeRuntimeManager instance; //Should be more than one... (no, implement the plot with an id and start)
	public List<NodeGraph> nodeGraph = new List<NodeGraph>(0);
	[HideInInspector] public int createdNodeGraphs = 0;
	//[ReadOnly][SerializeField]
	//public int[] outParametersLinksa;

	void Awake() {
		if (instance == null) {
			instance = this;
		}
	}

	void Start () {
		//execInstances [0].nodes [0].node;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartNodes() {

	}

	public void ExecuteNode(int id) {

	}

	public void NextNode() {
	}

	public void Stop() {
		//Destroy all nodes???
	}
}
