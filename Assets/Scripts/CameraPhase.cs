using UnityEngine;
using System.Collections;

[System.Serializable]
public class CameraStep {
	public Sprite sprite;
	public CameraStepSkipButtons isSkippableWith = new CameraStepSkipButtons();
	public float time = -1; //if negative, the next camera step will have to be manually triggered, otherwise if it is >= 0, that is the time the camera is going to stay onthis step
	public bool canOnlySkipAfterTime = false;
	public int cameraFov = -1;
	public bool startGameplay = false; //temp //if true: the gameplay (and Levels Manager starts)
	public bool isLastStep = false; //if true: the game ends

	public int talkingCharacter = -1; //Temp
	public string dialogue = ""; //Temp
}

[System.Serializable]
public class CameraChoiceStep { //If true: this will have the same id as another camera step, but will be the alternative option
	public CameraStep X;
	public CameraStep A;
	public CameraStep B;
	public CameraStep Y;
}

public class CameraPhase : MonoBehaviour {
	public int fromId = -1; //order
	public int toId = -1; //order
	public CameraChoiceStep[] steps;
}