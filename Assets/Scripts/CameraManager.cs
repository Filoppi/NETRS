using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[System.Serializable]
public class CameraStepSkipButtons {
	public bool X = false;
	public bool A = false;
	public bool B = false;
	public bool Y = false;
}

public class CameraManager : MonoBehaviour {
	public static CameraManager instance;
	public int currentStep = 0;
	public int gameplayCameraFov = 7;
	public int cutsceneCameraFov = 10;
	private float timer = 0;
	private CameraStepSkipButtons isSkippableWith;
	public bool isTranslating = false;
	private bool canOnlySkipAfterTime = false;
	//bool isInGameplay = true; //Useless
	//Use this for initialization? //What????
	//private ArrayList characters = new ArrayList(); //-1 //To make array and int index of linked character (-1 when not linked)
	public int characterID = -1;
	private float zoomTimePassed = 0;
	public float changeCharacterTime = 0.8f;
	Character prevCharacter;
	Character character;
	private float cameraZ = -10;

	void Awake() {
		if (instance == null) {
			instance = this;
		}
		cameraZ = transform.position.z;
	}

	void Start () {
		FindStep(new CameraStepSkipButtons());
	}

	void Update () {
		if (!isTranslating) {
			if (timer > 0) {
				timer -= Time.deltaTime;
				if (timer <= 0 && !canOnlySkipAfterTime) {
					NextStep (new CameraStepSkipButtons());
				}
			}
		}
	}

	void LateUpdate () {
		if (isTranslating) {
			zoomTimePassed += Time.deltaTime;
			if (zoomTimePassed >= changeCharacterTime) {
				GetComponent<Camera> ().orthographicSize = gameplayCameraFov;
				zoomTimePassed = 0.0f;
				isTranslating = false;
				LevelsManager.instance.mistakesBar.enabled = true;
				if (character) {
					GetComponent<Follow>().target = character.transform;
					if (PlayerController.instance.currentPlayer && PlayerController.instance.currentPlayer.id == characterID) {
						PlayerController.instance.currentPlayer.isControlled = true;
					}
					//transform.localPosition = new Vector3 (0, 0, transform.localPosition.z);
					//transform.localScale = new Vector3(1 / character.transform.lossyScale.x, 1 / character.transform.lossyScale.y, 1 / character.transform.lossyScale.z);
				}
			} else {
				float fovAlpha = zoomTimePassed;
				if (zoomTimePassed > changeCharacterTime / 2) {
					fovAlpha = changeCharacterTime - zoomTimePassed;
				}

				GetComponent<Camera> ().orthographicSize = gameplayCameraFov + ((10 - gameplayCameraFov) * fovAlpha / (changeCharacterTime / 2));
				Vector3 newPosition = Vector3.Lerp(prevCharacter.transform.position, character.transform.position, zoomTimePassed / changeCharacterTime);
				newPosition.z = cameraZ;
				transform.position = newPosition;
			}
		}
	}

	public void ButtonXDown()
	{
		if (isSkippableWith.X && !isTranslating && (!canOnlySkipAfterTime || (canOnlySkipAfterTime && timer <= 0))) {
			NextStep(new CameraStepSkipButtons());
		}
	}

	public void ButtonADown()
	{
		if (isSkippableWith.A && !isTranslating && (!canOnlySkipAfterTime || (canOnlySkipAfterTime && timer <= 0))) {
			CameraStepSkipButtons newCameraStepSkipButtons = new CameraStepSkipButtons();
			newCameraStepSkipButtons.A = true;
			NextStep(newCameraStepSkipButtons);
		}
	}

	public void NextStep(CameraStepSkipButtons SkippedWith)
	{
		timer = -1;
		isSkippableWith = new CameraStepSkipButtons();
		isTranslating = false;

		//characters = null; //Uneeded
		characterID = -1;

		GetComponent<Follow>().target = null;

		//if (isInGameplay) {
		//	isInGameplay = false;
		//}
		currentStep++;
		FindStep(SkippedWith);
	}

	public bool ChangeCharacter(int characterId) //To convert playerId to Index
	{
		if (!isTranslating) {
			//Vector3 lastPosition = transform.position;
			GetComponent<Follow>().target = null;
			//transform.position = lastPosition;
			transform.localScale = new Vector3 (1, 1, 1);
			if (timer < 0) { //Avoids bugs
				characterID = characterId;
				isTranslating = true;
				zoomTimePassed = 0;
				LevelsManager.instance.mistakesBar.enabled = false;

				prevCharacter = character;
//				foreach (Character thisCharacter in characters) {
//					if (thisCharacter.id == characterID) {
//						character = thisCharacter;
//						break;
//					}
//				}
				character = PlayerController.instance.Characters[characterID];
				if (!prevCharacter || prevCharacter.id == character.id) {
					prevCharacter = character;
					GetComponent<Camera> ().orthographicSize = gameplayCameraFov;
					isTranslating = false;
					if (character) {
						if (PlayerController.instance.currentPlayer && PlayerController.instance.currentPlayer.id == characterID) {
							PlayerController.instance.currentPlayer.isControlled = true;
						}
						GetComponent<Follow>().target = character.transform;
						//transform.localPosition = new Vector3 (0, 0, transform.localPosition.z);
						//transform.localScale = new Vector3(1 / character.transform.lossyScale.x, 1 / character.transform.lossyScale.y, 1 / character.transform.lossyScale.z);
					}
				}
				return true;
			}
		}
		return false;
	}

	void FindStep(CameraStepSkipButtons SkippedWith)
	{
		timer = -1;
		isSkippableWith = new CameraStepSkipButtons();
		GetComponent<Camera>().orthographicSize = gameplayCameraFov;
		LevelsManager.instance.StartLevel();
		PlayerController.instance.TakeControlOfCharacter(0); //Set both as active (and then as stop)

		//CameraEdit
//		CameraPhase[] CameraPhases = FindObjectsOfType(typeof(CameraPhase)) as CameraPhase[];
//
//		foreach (CameraStep thisCameraStep in CameraPhases) {
//			if (thisCameraStep.id == currentStep && thisCameraStep.isSkippableWith == SkippedWith) {
//				if (thisCameraStep.isLastStep) {
//					Application.Quit();
//				} else {
//					Vector3 newPosition = thisCameraStep.transform.position;
//					newPosition.z = cameraZ;
//					transform.position = newPosition;
//
//					if (thisCameraStep.startGameplay) {
//						timer = -1;
//						isSkippableWith = new CameraStepSkipButtons();
//						if (thisCameraStep.cameraFov > 0) GetComponent<Camera>().orthographicSize = thisCameraStep.cameraFov;
//						else GetComponent<Camera>().orthographicSize = gameplayCameraFov;
//						LevelsManager.instance.StartLevel();
////						characters.Clear();
////						Character[] tempList = GameObject.FindObjectsOfType(typeof(Character)) as Character[];
////						foreach (Character thisObj in tempList) {
////							characters.Add (thisObj);
////						}
//						PlayerController.instance.TakeControlOfCharacter(0); //Set both as active (and then as stop)
//					} else {
//						timer = thisCameraStep.time;
//						isSkippableWith = thisCameraStep.isSkippableWith;
//						canOnlySkipAfterTime = thisCameraStep.canOnlySkipAfterTime;
//						if (thisCameraStep.cameraFov > 0) GetComponent<Camera>().orthographicSize = thisCameraStep.cameraFov;
//						else GetComponent<Camera>().orthographicSize = cutsceneCameraFov;
//					}
//				}
//				break;
//			}
//		}
	}
}
