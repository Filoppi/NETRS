using UnityEngine;
using System.Collections;

[System.Serializable]
public class Randomness {
	public string name;
	public int action;
}

[System.Serializable]
public class Randomness2 {
	public Randomness name;
	public Randomness action;
}

public class InteractiveObject : MonoBehaviour {
	public Randomness2 asdasd; //Unused
	public bool isUsable = true; //Unused
	public bool isPickable = false;
	private bool isPicked = false;
	public bool useOnce = true;
	public bool used = false; //if true, it is unusable
	public int plotOrder = -1;
	public bool canOnlyBeUsedWhenInRightOrder = true;
	public bool isLastObjectOfStage = false;
	public bool nextRitualPhase = false;
	public bool storyGoesAheadIfFailed = false;
	public float actionLenght = 0;
	public float actionRadius = 3.2f;
	public InteractiveObject linkedObject;
	public bool canBeUsedWithoutLinkedObject = true;
	public InteractiveObject nextObject;
	public int characterAnimationPhase = -1; //if -1 then the character arm just rotates when picking
	public Character linkedCharacter;
	public Animator linkedCharacterAnimator; //Temp
	public int linkedCharacterAction = -1;
	public int linkedCharacterAnimation = -1;
	//Link to char hand
	public AudioClip useSound;
	public AudioClip hitSound;
	protected AudioSource audioSource;
	private SpriteRenderer button;
	//Does it has to be canBeUsedAtThisTime to be used
	public bool looseCredibility = false;
	public int credibilityToLoose = 1;
	public string gameName = "";
	public string actionName = "Use"; //Temp
	public string positiveDescription = ""; //Temp
	public string negativeDescription = ""; //Temp
	public float helpHeight = 10;

	protected Rigidbody2D rigidBody2D;

	protected float timer = -1;

	void Awake() {
		LevelsManager.interactiveObjects.Add(this);
		rigidBody2D = GetComponent<Rigidbody2D> ();
		audioSource = gameObject.AddComponent<AudioSource>() as AudioSource;
	}
	void Start () {
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (hitSound) {
			audioSource.clip = hitSound;
			audioSource.Play ();
		}
		//if (coll.gameObject.tag == "Character") {
		//	ShowHelp(true);
		//}
	}

	void OnCollisionExit2D(Collision2D coll) {
		//if (coll.gameObject.tag == "Character") {
		//	ShowHelp(false);
		//}
	}

	public void ShowHelp(bool newEnabled = true)
	{
		if (newEnabled && !button) {
			button = gameObject.AddComponent<SpriteRenderer> () as SpriteRenderer;
			button.sprite = LevelsManager.instance.XButton;
			Vector3 newVec = transform.position;
			button.transform.position = new Vector3 (newVec.x, newVec.y + helpHeight, newVec.z);
		} else if (!newEnabled && button) {
			Destroy (button);
		}
		//GetComponent<GUIText>().enabled = newEnabled;
	}

	void Update () {
		if (timer > 0) {
			timer -= Time.deltaTime;
			if (timer <= 0) {
				timer = -1;
				EndAction();
			}
		}
	}

	public void Drop() {
		if (isPicked) {
			//Find MainCharacter and call drop if they are holding this object???
			transform.parent = null;
			if (rigidBody2D) rigidBody2D.isKinematic = false;
			isPicked = false;
		}
	}

	public void Pick() {
		if (!isPicked) {
			if (rigidBody2D) {
				//Vector3 newV = rigidBody2D.velocity;
				rigidBody2D.isKinematic = true;
				//rigidBody2D.AddForce(transform.lossyScale.x * 1305f);
			}
			isPicked = true;
			GetComponent<SpriteRenderer>().sortingLayerName = "InteractiveObjects";
		}
	}

	public int Action()
	{
		ShowHelp (false);
		if (isPickable && !isPicked) {
			Pick();
			return 0;
		}
		bool useResult = Use(false);
		if (useResult == false)
			return -1;
		else
			return 1;
	}

	public virtual bool Use(bool calledByOtherInteractiveObject) //To call base.Use in childs
	{
		if (!LevelsManager.instance.UseObject(this) || (useOnce && used) || (!calledByOtherInteractiveObject && !canBeUsedWithoutLinkedObject)) {
			LevelsManager.instance.PlayForbiddenSound();
			return false;
		}
		bool doAction = false;
		if (linkedObject) {
			if ((linkedObject.transform.position - transform.position).magnitude < actionRadius) { //Temp
				linkedObject.Use(true);
				doAction = true;
			} else if (canBeUsedWithoutLinkedObject) {
				doAction = true;
			}
		} else {
			doAction = true;
		}
		if (doAction) {
			used = true;
			if (useSound) {
				audioSource.clip = useSound;
				audioSource.Play ();
			}
			if (characterAnimationPhase >= 0) {
				PlayerController.instance.currentPlayer.animator.SetInteger ("AnimState", characterAnimationPhase);
				PlayerController.instance.currentPlayer.isControlled = false;
				//GetMainCharacter and start charAnimation and block controls
			}
			if (actionLenght <= 0) {
				timer = -1;
				EndAction ();
			} else {
				timer = actionLenght;
			}
			return true;
		}
		LevelsManager.instance.PlayForbiddenSound();
		return false;
	}

	public virtual void EndAction()
	{
		if (linkedCharacter) {
			if (linkedCharacterAction >= 0)	linkedCharacter.Action (linkedCharacterAction);
			if (linkedCharacterAnimation >= 0) linkedCharacterAnimator.SetInteger ("AnimState", linkedCharacterAnimation);
			CameraManager.instance.ChangeCharacter(linkedCharacter.id);
		}
		if (nextObject) {
			nextObject.Use(true);
		}
		if (characterAnimationPhase >= 0) {
			PlayerController.instance.currentPlayer.animator.SetInteger ("AnimState", 0);
			PlayerController.instance.currentPlayer.isControlled = true;
			//GetMainCharacter and reset animation and control
		}
	}
}
