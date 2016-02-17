using UnityEngine;
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

public static class NodeTypes
{
	//public static Dictionary<BaseNode, NodeData> nodes;
	public static List<System.Type> nodesTypes;

	public static void FetchNodes() 
	{
		//nodes = new Dictionary<BaseNode, NodeData> ();
		nodesTypes = new List<System.Type>(0);

		List<Assembly> scriptAssemblies = AppDomain.CurrentDomain.GetAssemblies ().Where ((Assembly assembly) => assembly.FullName.Contains ("Assembly")).ToList ();
		if (!scriptAssemblies.Contains (Assembly.GetExecutingAssembly ()))
			scriptAssemblies.Add (Assembly.GetExecutingAssembly ());
		foreach (Assembly assembly in scriptAssemblies) //To rename???
		{
			foreach (Type type in assembly.GetTypes ().Where (T => T.IsClass && !T.IsAbstract && T.IsSubclassOf (typeof (BaseNode)) )) 
			{
				nodesTypes.Add (type);

				//object[] nodeAttributes = type.GetCustomAttributes (typeof (NodeAttribute), false);
				//NodeAttribute attr = nodeAttributes [0] as NodeAttribute;
				//if (attr == null || !attr.hide)
				{
					//BaseNode node = ScriptableObject.CreateInstance (type.Name) as BaseNode; // Create a 'raw' instance (not setup using the appropriate Create function)
					//node = node.Create (Vector2.zero); // From that, call the appropriate Create Method to init the previously 'raw' instance
					//nodes.Add (node, new NodeData (attr == null? node.name : attr.contextText));
				}
			}
		}
	}

//	public static NodeData getNodeData (BaseNode node)
//	{
//		return nodes [getDefaultNode (node.GetID)];
//	}
//
//	public static BaseNode getDefaultNode (string nodeID)
//	{
//		return nodes.Keys.Single<BaseNode> ((BaseNode node) => node.GetID == nodeID);
//	}
//	public static T getDefaultNode<T> () where T : BaseNode
//	{
//		return nodes.Keys.Single<BaseNode> ((BaseNode node) => node.GetType () == typeof (T)) as T;
//	}
}

//public struct NodeData 
//{
//	public string adress;
//
//	public NodeData (string name) 
//	{
//		adress = name;
//	}
//}
//
//public class NodeAttribute : Attribute 
//{
//	public bool hide = false; //{ get; private set; }
//	public string contextText { get; private set; }
//
//	public NodeAttribute (bool HideNode, string ReplacedContextText) 
//	{
//		//hide = HideNode;
//		contextText = ReplacedContextText;
//	}
//}