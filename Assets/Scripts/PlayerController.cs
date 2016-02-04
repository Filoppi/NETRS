using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public static PlayerController instance;
	public InputManager m_oInputManager;

	public Character[] Characters;
	public Player[] Players;
	public Player currentPlayer;
	private int playerIndex = -1;
	
	void Awake()
	{
		if (instance == null) {
			instance = this;

			Characters = FindObjectsOfType(typeof(Character)) as Character[];
			Character[] tempCharacters = new Character[Characters.Length];
			foreach (Character thisCharacter in Characters) {
				tempCharacters[thisCharacter.id] = thisCharacter;
			}
			//tempCharacters.CopyTo (Characters, 0);
			Characters = tempCharacters;
		}
		if(m_oInputManager == null)
		{
			Debug.LogWarning ("No Input manager linked");
			
			m_oInputManager = FindObjectOfType<InputManager>();
			
			if(m_oInputManager == null)
			{
				//laod from hierarchy
				Debug.LogError ("No InputManager in the scene");
				return;
			}
		}
	}

	void Start () {
	}
	void Update () {
	}

	public void LockPlayers() {
		foreach (Player thisPlayer in Players) {
			thisPlayer.isControlled = false;
		}
	}

	public void DisablePlayers() {
		foreach (Player thisPlayer in Players) {
			thisPlayer.isControlled = false;
			thisPlayer.isActive = false;
		}
		currentPlayer = null;
	}
	public void EnablePlayers() {
		foreach (Player thisPlayer in Players) {
			thisPlayer.isActive = true;
		}
	}

	public void TakeControlOfCharacter(int newId) {
		EnablePlayers ();
		Player newCurrentPlayer = Players[newId];
		if (CameraManager.instance.ChangeCharacter(newCurrentPlayer.id)) {
			if (currentPlayer) {
				currentPlayer.isControlled = false; //Disables old Player
			}
			currentPlayer = newCurrentPlayer;
			currentPlayer.isControlled = true;
			playerIndex = newId;
		}
	}

	public void DetachFromCharacter() {
		currentPlayer.isControlled = false;
		currentPlayer = null;
	}
	
	public void SwitchCharacter() {
		//if (currentPlayer) {
		//	playerIndex = currentPlayer.id;
		//}
		int newPlayerIndex = playerIndex;
		newPlayerIndex++;
		if (newPlayerIndex >= Players.Length) {
			newPlayerIndex = 0;
		}
		Player newCurrentPlayer = Players[newPlayerIndex];
		if (CameraManager.instance.ChangeCharacter (newCurrentPlayer.id)) {
			if (currentPlayer) currentPlayer.isControlled = false; //Old Player
			currentPlayer = newCurrentPlayer;
			currentPlayer.isControlled = false;
			playerIndex = newPlayerIndex;
		}
	}
	
	private void OnEnable()
	{
		if(m_oInputManager == null)
		{
			m_oInputManager = GameObject.FindObjectOfType<InputManager>();
			
			if(m_oInputManager == null)
			{
				Debug.LogError("No InputManager in the scene");
				return;
			}
		}
		
		switch(m_eInputSource)
		{
		case InputManager.eInputSource.PLAYER:
			m_oInputManager.ChangeInput(m_eInputSource);
			break;
		default:
			m_oInputManager.ChangeInput(m_eInputSource);
			break;
		}
		
		if(m_oInputManager != null)
		{
			m_oInputManager.OnPrimaryPressed += ActionPrimaryPressed;
			m_oInputManager.OnSecondaryPressed += ActionSecondaryPressed;
			m_oInputManager.OnMoveRightPressed += ActionMoveRightPressed;
			m_oInputManager.OnMoveUpPressed += ActionMoveUpPressed;
			m_oInputManager.OnSwitchPlayerPressed+= ActionSwitchPlayerPressed;
			//m_oInputManager.OnUseObjectPressed += ActionUseObject;
			m_oInputManager.OnDropObjectPressed += ActionDropObject;
			m_oInputManager.OnInfoPressed += ActionInfoPressed;
		}
	}
	
	private void OnDisable()
	{
		if(m_oInputManager != null)
		{
			m_oInputManager.OnPrimaryPressed -= ActionPrimaryPressed;
			m_oInputManager.OnSecondaryPressed -= ActionSecondaryPressed;
			m_oInputManager.OnMoveRightPressed -= ActionMoveRightPressed;
			m_oInputManager.OnMoveUpPressed -= ActionMoveUpPressed;
			m_oInputManager.OnSwitchPlayerPressed-= ActionSwitchPlayerPressed;
			//m_oInputManager.OnUseObjectPressed -= ActionUseObject;
			m_oInputManager.OnDropObjectPressed -= ActionDropObject;
			m_oInputManager.OnInfoPressed += ActionInfoPressed;
		}
	}

	private void ActionPrimaryPressed () //X
	{
		if (currentPlayer && currentPlayer.isControlled) currentPlayer.Action ();
		CameraManager.instance.ButtonXDown ();
	}
	private void ActionSecondaryPressed () //A
	{
		CameraManager.instance.ButtonADown ();
	}
	private void ActionMoveRightPressed (float alpha)
	{
		if (currentPlayer) currentPlayer.MoveRight(alpha);
	}
	private void ActionMoveUpPressed (float alpha)
	{
		if (currentPlayer) currentPlayer.MoveUp(alpha);
	}
	private void ActionSwitchPlayerPressed ()
	{
		if (currentPlayer && currentPlayer.isControlled && !CameraManager.instance.isTranslating)
			SwitchCharacter();
	}
	private void ActionUseObject () //Unused
	{
	}
	private void ActionDropObject ()
	{
		if (currentPlayer && currentPlayer.isControlled) currentPlayer.Drop();
	}
	private void ActionInfoPressed ()
	{
		if (currentPlayer && currentPlayer.isControlled && !CameraManager.instance.isTranslating) {
			LevelsManager.instance.ToggleGuide ();
		}
	}
	[SerializeField]
	private InputManager.eInputSource m_eInputSource = InputManager.eInputSource.PLAYER;	
}