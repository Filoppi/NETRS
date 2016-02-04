using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour {

	public static LevelsManager instance;
	public Sprite XButton;
	public Sprite YButton;
	public Sprite AButton;
	public Sprite BButton;
	public int levelsNumber = 1;
	public int currentPuzzlePhase = 0;
	public int currentLevel = 0; //Public to Debug
	public int currentStep = 0; //Public to Debug
	public int puzzleAttempts = 2;
	protected int leftAttempts = 1;
	public AudioClip music;
	public AudioClip forbiddedSound;
	public AudioClip succeedSound;
	public AudioClip failSound;
	public AudioClip errorSound;
	public SpriteRenderer mistakesBar;
	public SpriteRenderer ritualGuide;
	private AudioSource audioSourceFX;
	private AudioSource audioSourceMusic;
	public static ArrayList interactiveObjects = new ArrayList();
	private float guideOriginalLocalY;
	private float guideTargetLocalY = 0;
	public bool canBePaused = true;
	private bool guideIsMoving = false;
	private bool guideIsGoingUp = false; //last static position
	public float guideSpeed = 40f;
	private float mistakeBarOriginalX = 1;
	//CurrentPhase (enum???)

	void Awake() {
		if (instance == null) {
			instance = this;
			audioSourceFX = gameObject.AddComponent<AudioSource>() as AudioSource;
			audioSourceMusic = gameObject.AddComponent<AudioSource>() as AudioSource;
		}
		mistakesBar.enabled = false;
		if (ritualGuide) {
			guideOriginalLocalY = ritualGuide.transform.localPosition.y;
		}
		leftAttempts = puzzleAttempts;
		mistakeBarOriginalX = mistakesBar.transform.lossyScale.x;
	}

	void Start () {
		if (music) {
			audioSourceMusic.loop = true;
			audioSourceMusic.clip = music;
			audioSourceMusic.Play ();
		}
	}
	void Update () {
		if (guideIsMoving) {
			if (guideIsGoingUp) {
				ritualGuide.transform.localPosition = new Vector3 (ritualGuide.transform.localPosition.x, ritualGuide.transform.localPosition.y + (guideSpeed * Time.deltaTime), ritualGuide.transform.localPosition.z);
				if (ritualGuide.transform.localPosition.y >= guideTargetLocalY) {
					ritualGuide.transform.localPosition = new Vector3 (ritualGuide.transform.localPosition.x, guideTargetLocalY, ritualGuide.transform.localPosition.z);
					guideIsMoving = false;
					PlayerController.instance.currentPlayer.isControlled = true;
				}
			} else {
				ritualGuide.transform.localPosition = new Vector3 (ritualGuide.transform.localPosition.x, ritualGuide.transform.localPosition.y - (guideSpeed * Time.deltaTime), ritualGuide.transform.localPosition.z);
				if (ritualGuide.transform.localPosition.y <= guideOriginalLocalY) {
					ritualGuide.transform.localPosition = new Vector3 (ritualGuide.transform.localPosition.x, guideOriginalLocalY, ritualGuide.transform.localPosition.z);
					guideIsMoving = false;
					PlayerController.instance.currentPlayer.isControlled = true;
				}
			}
		}
		//ritualGuide.transform.localScale = new Vector3((1 / transform.lossyScale.x) * ritualGuide.transform.lossyScale.x, (1 / transform.lossyScale.y) * ritualGuide.transform.lossyScale.y, (1 / transform.lossyScale.z) * ritualGuide.transform.lossyScale.z);
	}

	public bool UseObject(InteractiveObject usedObj)
	{
		if (usedObj.plotOrder == currentStep) {
			//Start cutscene??? (Disable character controls...)
			if (usedObj.isLastObjectOfStage) {
				if (succeedSound) {
					audioSourceFX.clip = succeedSound;
					audioSourceFX.Play ();
				}
				PlayerController.instance.DetachFromCharacter ();
				//Camera: Go to the centre of the house (Next step??? yes, exit with A)
				//Invoke("LoadMenu", 3f);
				PlayerController.instance.DisablePlayers ();
				CameraManager.instance.NextStep(new CameraStepSkipButtons());
			} else {
				currentStep++;
			}
			return true;
		} else if (usedObj.looseCredibility) {
			leftAttempts -= usedObj.credibilityToLoose;
			float newScale = Mathf.Max(0, leftAttempts) / puzzleAttempts;
			mistakesBar.transform.localScale = new Vector3(newScale * mistakeBarOriginalX, mistakesBar.transform.localScale.y, mistakesBar.transform.localScale.z);
			if (leftAttempts < 0) {
				if (failSound) {
					audioSourceFX.clip = failSound;
					audioSourceFX.Play ();
				}
				PlayerController.instance.DisablePlayers ();
				Invoke("LoadMenu", 3f);
			} else {
				if (errorSound) {
					audioSourceFX.clip = errorSound;
					audioSourceFX.Play ();
				}

				if (usedObj.storyGoesAheadIfFailed) {
					ArrayList newInteractiveObjects = new ArrayList();
					foreach (InteractiveObject thisIntObj in interactiveObjects) {
						if (thisIntObj.nextRitualPhase && thisIntObj.plotOrder >= currentPuzzlePhase) {
							newInteractiveObjects.Add(thisIntObj);
						}
					}
					int minID = 9999;
					foreach (InteractiveObject thisIntObj in newInteractiveObjects) {
						if (thisIntObj.plotOrder < minID) {
							minID = thisIntObj.plotOrder;
						}
					}
					currentPuzzlePhase = minID + 1;
				} else {
				//Go Back to last used player???
				}
			}
			return true;
		} else if (usedObj.canOnlyBeUsedWhenInRightOrder) {
			return false;
		}
		return true;
	}

	private void LoadMenu() {
		SceneManager.LoadScene("Menu");
	}

	public void PlayForbiddenSound() {
		if (forbiddedSound) {
			audioSourceFX.clip = forbiddedSound;
			audioSourceFX.Play ();
		}
	}

	public void StartLevel() {
		leftAttempts = puzzleAttempts;
		mistakesBar.enabled = true;
	}

	public void EndLevel() {
		mistakesBar.enabled = false;
		currentStep = 0;
		if (currentLevel == levelsNumber) {
			CameraManager.instance.NextStep(new CameraStepSkipButtons());
		} else {
			currentLevel++;
		}
	}

	public void ToggleGuide() {
		if (PlayerController.instance.currentPlayer && PlayerController.instance.currentPlayer.isControlled && !CameraManager.instance.isTranslating) {
			guideIsGoingUp = !guideIsGoingUp;
			guideIsMoving = true;
			PlayerController.instance.currentPlayer.isControlled = false;
		}
	}
}