using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


public class NodeWindow {
	public Rect window;
	public string name;
	Node node;
	public NodeWindow (Rect newRect, Node newNode) {
		window = newRect;
		node = newNode;
		name = "AbstractNode";
	}
	public NodeWindow (Rect newRect, Node newNode, string newName) {
		window = newRect;
		node = newNode;
		name = newName;
	}
}

public class NodeEditor : EditorWindow {

	List<NodeWindow> nodeWindows = new List<NodeWindow>();
	List<int> windowsToAttach = new List<int>();
	List<int> attachedWindows = new List<int>();
	List<Node> nodeClass = new List<Node>();

	int nodesNum;

	float panX = 0;
	float panY = 0;

	public void SetValues(ref NodeGraph j1) {
		//		j1.nodes = new List<Node> (100);
		//		j1.nodes.Capacity = 100;
		//		j1.nodes.Insert(0, new Node(new StartNode()));
		//		foreach (Node n in j1.nodes) {
		//			nodeWindows.Add(new NodeWindow(new Rect(10, 10, 100, 100)));
		//		}
	}
	public void OpenWindow(ref List<Node> foundNodes, string newTitle) {
		titleContent = new GUIContent(newTitle); //"Node Editor: " + 
		NodeTypes.FetchNodes (); //Could be called static once on creation of this obj

		nodeWindows = new List<NodeWindow>();
		nodeClass = foundNodes;
		int i = 1;
		foreach (Node thisNode in foundNodes) {
			System.Type nodeType = thisNode.node.GetType();
			nodeWindows.Add(new NodeWindow(new Rect(15*i, 15*i, 100, 100), new Node(nodeType), nodeType.Name));
			//nodeWindows.Add(new NodeWindow(new Rect(15*i, 15*i, 100, 100), thisNode, thisNode.node.nodeName));
			i++;
		}
	}
	void OnGUI() {

		//EditorGUILayout.BeginScrollView ();

		//EditorGUILayout.LabelField(Heigh (4000), Width (4000));

		GUI.BeginGroup (new Rect(panX, panY, 100000, 100000));

		if (windowsToAttach.Count == 2) {
			attachedWindows.Add(windowsToAttach[0]);
			attachedWindows.Add(windowsToAttach[1]);
			windowsToAttach = new List<int>();
		}

		//		if (attachedWindows.Count >= 2) {
		//			for (int i = 0; i < attachedWindows.Count; i += 2) {
		//				DrawNodeCurve(nodeWindows.window[attachedWindows[i]], windows[attachedWindows[i + 1]]);
		//			}
		//		}

		Handles.BeginGUI ();
		//Handles.DrawBezier ();
		Handles.EndGUI ();

		BeginWindows();

		if (Application.isEditor && !EditorApplication.isPlayingOrWillChangePlaymode) {
			if (NodeTypes.nodesTypes == null) {
				NodeTypes.FetchNodes ();
			}
			foreach (System.Type node in NodeTypes.nodesTypes) {
				if (GUILayout.Button("Create " + node.Name)) { //if it's not hidden BaseNode type
					nodeWindows.Add(new NodeWindow(new Rect(80, 10, 100, 100), new Node(node), node.Name));
					nodeClass.Add (new Node(node));
					//MonoBehaviour.print ((new Node (node)).node.GetType ());
				}
			}
		}

		for (int i = 0; i < nodeWindows.Count; i++) {
			nodeWindows[i].window = GUI.Window(i, nodeWindows[i].window, DrawNodeWindow, nodeWindows[i].name); //Only store it it wasn't deleted???
			//nodeWindows[i].window = GUILayout.Window(i, nodeWindows[i].window, DrawNodeWindow, nodeWindows[i].name);
		}

		EndWindows();

		GUI.EndGroup ();

//		if (GUI.RepeatButton (new Rect (15, 5, 20, 20), "^")) {
//			panY -= 1;
//			Repaint ();
//		}

		//EditorGUILayout.EndScrollView ();
	}

	void DrawNodeWindow(int id) {
		EditorGUILayout.BeginHorizontal ();
		//foreach (Node thisNode in nodeClass) {
		//	foreach (System.Type thisType in thisNode.node.inParameters) {
		foreach (System.Type thisType in nodeClass[id].node.inParameters) {
			EditorGUILayout.ObjectField(thisType.Name, new UnityEngine.Object(), thisType, true);
		}
		//}
		EditorGUILayout.EndHorizontal ();

		if (GUILayout.Button("Attach")) {
			windowsToAttach.Add(id);
		}
		else if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Delete) {
			//Delete window
			nodeWindows.RemoveAt(id);
			nodeClass.RemoveAt(id);
			Repaint();
			return;
		}

		GUI.DragWindow();
	}

	void DrawNodeCurve(Rect start, Rect end) {
		Vector3 startPos = new Vector3(start.x + start.width, start.y + start.height / 2, 0);
		Vector3 endPos = new Vector3(end.x, end.y + end.height / 2, 0);
		Vector3 startTan = startPos + Vector3.right * 50;
		Vector3 endTan = endPos + Vector3.left * 50;
		Color shadowCol = new Color(0, 0, 0, 0.06f);

		for (int i = 0; i < 3; i++) {// Draw a shadow
			Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
		}

		Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
	}
}


//using UnityEngine;
//using UnityEditor;
//using System.Collections;
//using System.Collections.Generic;
//using System;
//using System.Reflection;
//using System.Text;
//
//public class NodeEditor : EditorWindow
//{
//	static NodeEditor instance;
//
//	public List<Rect> windows = new List<Rect>();
//
//	public int windowsCapacity = 7;
//	List<int> windowsToAttach = new List<int>();
//	List<int> attachedWindows = new List<int>();
//
//	public Rect _handleArea;
//	private bool _nodeOption, _options, _handleActive, _action;
//	private Texture2D _resizeHandle, _aaLine;
//	private GUIContent _icon;
//	private float _winMinX, _winMinY;
//	private int _mainwindowID;
//
//	[MenuItem("Window/Node Editor")]
//	static void Init()
//	{
//		instance = EditorWindow.GetWindow<NodeEditor>();
//		instance.title = "Node Editor";
//		instance.ShowNodes();
//	}
//
//	private void ShowNodes()
//	{
//		_winMinX = 100f;
//		_winMinY = 100f;
//
//		windows.Capacity = windowsCapacity;
//		for (int i = 0; i < windows.Capacity; ++i) { // Updates the Rect's when these are dragged
//			windows[i] = new Rect(30, 30, _winMinX, _winMinY);
//		}
//		//windows.Add(new Rect(30, 30, _winMinX, _winMinY));
//		//windows.Add(new Rect(30, 30, _winMinX, _winMinY));
//
//		_resizeHandle = AssetDatabase.LoadAssetAtPath("Assets/NodeEditor/Icons/ResizeHandle.png", typeof(Texture2D)) as Texture2D;
//		_aaLine = AssetDatabase.LoadAssetAtPath("Assets/NodeEditor/Icons/AA1x5.png", typeof(Texture2D)) as Texture2D;
//		_icon = new GUIContent(_resizeHandle);
//		_mainwindowID = GUIUtility.GetControlID(FocusType.Native); //grab primary editor window controlID
//	}
//
//	void OnGUI()
//	{
//		BeginWindows();
//		for (int i = 0; i < windows.Capacity; ++i) { // Updates the Rect's when these are dragged
//			windows[i] = GUI.Window(i, windows[i], DrawNodeWindow, "Window " + (i+1));
//		}
//		EndWindows();
//
////		for (int i = 0; i < windows.Capacity; ++i) { // Updates the Rect's when these are dragged
////			DrawNodeCurve (window1, window2);
////		}
//
//		GUILayout.BeginHorizontal(EditorStyles.toolbar);
//		_options = GUILayout.Toggle(_options, "Toggle Me", EditorStyles.toolbarButton);
//		GUILayout.FlexibleSpace();
//		GUILayout.EndHorizontal();
//
//		//if drag extends inner window bounds _handleActive remains true as event gets lost to parent window
//		if ((Event.current.rawType == EventType.MouseUp) && (GUIUtility.hotControl != _mainwindowID))
//		{
//			GUIUtility.hotControl = 0;
//		}
//	}
//
//	private void DrawNodeWindow(int id)
//	{
//		if (GUIUtility.hotControl == 0)  //mouseup event outside parent window?
//		{
//			_handleActive = false; //make sure handle is deactivated
//		}
//
//		float _cornerX = 0f;
//		float _cornerY = 0f;
//
//		_cornerX = windows[id].width;
//		_cornerY = windows[id].height;
//
//		//begin layout of contents
//		GUILayout.BeginArea(new Rect(1, 16, _cornerX - 3, _cornerY - 1));
//		GUILayout.BeginHorizontal(EditorStyles.toolbar);
//		_nodeOption = GUILayout.Toggle(_nodeOption, "Node Toggle", EditorStyles.toolbarButton);
//		GUILayout.FlexibleSpace();
//		GUILayout.EndHorizontal();
//		GUILayout.EndArea();
//
//		GUILayout.BeginArea(new Rect(1, _cornerY - 16, _cornerX - 3, 14));
//		GUILayout.BeginHorizontal(EditorStyles.toolbarTextField, GUILayout.ExpandWidth(true));
//		GUILayout.FlexibleSpace();
//
//		//grab corner area based on content reference
//		_handleArea = GUILayoutUtility.GetRect(_icon, GUIStyle.none);
//		GUI.DrawTexture(new Rect(_handleArea.xMin + 6, _handleArea.yMin - 3, 20, 20), _resizeHandle); //hacky placement
//		_action = (Event.current.type == EventType.MouseDown) || (Event.current.type == EventType.MouseDrag);
//		if (!_handleActive && _action)
//		{
//			if (_handleArea.Contains(Event.current.mousePosition, true))
//			{
//				_handleActive = true; //active when cursor is in contact area
//				GUIUtility.hotControl = GUIUtility.GetControlID(FocusType.Native); //set handle hot
//			}
//		}
//
//		EditorGUIUtility.AddCursorRect(_handleArea, MouseCursor.ResizeUpLeft);
//		GUILayout.EndHorizontal();
//		GUILayout.EndArea();
//
//		//resize window
//		if (_handleActive && (Event.current.type == EventType.MouseDrag))
//		{
//			ResizeNode(id, Event.current.delta.x, Event.current.delta.y);
//			Repaint();
//			Event.current.Use();
//		}
//
//		//enable drag for node
//		if (!_handleActive)
//		{
//			GUI.DragWindow();
//		}
//	}
//
//	private void ResizeNode(int id, float deltaX, float deltaY)
//	{
//		Rect window = windows [id];
//		if ((window.width + deltaX) > _winMinX) {
//			window.xMax += deltaX;
//		}
//		if ((window.height + deltaY) > _winMinY) {
//			window.yMax += deltaY;
//		}
//	}
//
//	void DrawNodeCurve(Rect start, Rect end)
//	{
//		Vector3 startPos = new Vector3(start.x + start.width, start.y + start.height / 2, 0);
//		Vector3 endPos = new Vector3(end.x, end.y + end.height / 2, 0);
//		Vector3 startTan = startPos + Vector3.right * 50;
//		Vector3 endTan = endPos + Vector3.left * 50;
//		Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, _aaLine, 1.5f);
//	}
//}