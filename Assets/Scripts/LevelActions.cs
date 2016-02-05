using UnityEngine;
using System.Collections;

[System.Serializable]
public class Action {
	public float preDelay = 0.0f; //From the start of this actions group or from the end of the previous one
	public float postDelay = 0.0f; //Needeed???
	public int actionNumber = -1;
	public InteractiveObject nextObject; //nextObject should tell when it's done doing the action so that we can move to the next object
	public Character nextCharacter; //linkedCharacter should tell when it's done doing the action so that we can move to the next character //The animation should be store inside the action
}

//Camera Manager
//Game Manager (Gameplay mode, pause, ...)
//Control Manager (and Player Controller) ???
//Level Manager (Plot? Puzzle Phases? FMS for each character with specified states???) (Specific stuff in level???)
//Actions Manager (Actions in plot???)

public class LevelActions : MonoBehaviour {
	public Action[] actions; //Unused
	private int currentAction = -1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
