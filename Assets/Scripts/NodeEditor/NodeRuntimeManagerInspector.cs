using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(NodeRuntimeManager))]
public class NodeRuntimeManagerInspector : Editor {	

	public override void OnInspectorGUI() {

		NodeRuntimeManager nodeRuntimeManager = (NodeRuntimeManager)target;

		Color previousColor = GUI.color;

		GUI.color = Color.green;
		if (GUILayout.Button("Create new Node Graph")) {
			nodeRuntimeManager.nodeGraph.Add (new NodeGraph ());
			int i = nodeRuntimeManager.nodeGraph.Count - 1;
			nodeRuntimeManager.createdNodeGraphs++;
			nodeRuntimeManager.nodeGraph[i].name = "Graph " + nodeRuntimeManager.createdNodeGraphs;
			for (int k = 0; k < nodeRuntimeManager.nodeGraph.Count && k != i; k++) {
				if (nodeRuntimeManager.nodeGraph[k].linkedWindow != null)
					nodeRuntimeManager.nodeGraph[k].linkedWindow.Close ();
			}
			nodeRuntimeManager.nodeGraph[i].linkedWindow = EditorWindow.GetWindow<NodeEditor>();
			nodeRuntimeManager.nodeGraph[i].linkedWindow.OpenWindow(ref nodeRuntimeManager.nodeGraph[i].nodes, nodeRuntimeManager.nodeGraph[i].name);
		}

		for (int i = 0; i < nodeRuntimeManager.nodeGraph.Count; i++) {
			GUI.color = previousColor;
			if (GUILayout.Button("Open: " + nodeRuntimeManager.nodeGraph[i].name)) {
				for (int k = 0; k < nodeRuntimeManager.nodeGraph.Count && k != i; k++) {
					if (nodeRuntimeManager.nodeGraph[k].linkedWindow != null)
						nodeRuntimeManager.nodeGraph[k].linkedWindow.Close ();
				}
				nodeRuntimeManager.nodeGraph[i].linkedWindow = EditorWindow.GetWindow<NodeEditor>();
				nodeRuntimeManager.nodeGraph[i].linkedWindow.OpenWindow(ref nodeRuntimeManager.nodeGraph[i].nodes, nodeRuntimeManager.nodeGraph[i].name);
				break;
				//if (myScript.execInstances[0] == null)
				//myScript.execInstances.SetValue(new NodeGraph(), 0);
				//editor.SetValues (ref myScript.execInstances[0]);

				//Node id, node type, outputs types, outputs values, outputs links, inputs types, inputs values, inputs links
			}
			GUI.color = Color.red;
			//			GUISettings newSettings = new GUISettings ();
			//			newSettings.selectionColor = Color.blue;
			//			GUIStyle newStyle = new GUIStyle ();
			//			newStyle.fixedHeight = 25;
			if (GUILayout.Button ("Delete: " + nodeRuntimeManager.nodeGraph[i].name)) {
				if (nodeRuntimeManager.nodeGraph[i].linkedWindow != null)
					nodeRuntimeManager.nodeGraph[i].linkedWindow.Close ();
				nodeRuntimeManager.nodeGraph.RemoveAt (i);
				if (nodeRuntimeManager.nodeGraph.Count == 0)
					nodeRuntimeManager.createdNodeGraphs = 0;
				break;
			}
		}

		GUI.color = previousColor;

		DrawDefaultInspector ();
	}
}
