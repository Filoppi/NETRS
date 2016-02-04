using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public InputManager m_oInputManager;

	// Use this for initialization

	void Awake()
	{
		if(m_oInputManager == null)
		{
			Debug.LogWarning ("No Input manager linked");

			m_oInputManager = GameObject.FindObjectOfType<InputManager>();
			
			if(m_oInputManager == null)
			{
				//laod from hierarchy
				Debug.LogError ("No InputManager in the scene");
				return;
			}
			
		}
	}
	void Start () {
		string[] names = Input.GetJoystickNames();
		foreach(string s in names)
		{
			Debug.LogWarning (s);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
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
			m_oInputManager.OnMoveLeftPressed += ActionMoveLeftPressed;
			m_oInputManager.OnMoveRightPressed += ActionMoveRightPressed;
			m_oInputManager.OnMoveUpPressed += ActionMoveUpPressed;
			m_oInputManager.OnMoveDownPressed += ActionMoveDownPressed;
			m_oInputManager.OnSwitchPlayerPressed += ActionSwitchPlayerPressed;
			m_oInputManager.OnGetObjectPressed += ActionGetObject;
			m_oInputManager.OnUseObjectPressed += ActionUseObject;
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
			m_oInputManager.OnMoveLeftPressed -= ActionMoveLeftPressed;
			m_oInputManager.OnMoveRightPressed -= ActionMoveRightPressed;
			m_oInputManager.OnMoveUpPressed -= ActionMoveUpPressed;
			m_oInputManager.OnMoveDownPressed -= ActionMoveDownPressed;
			m_oInputManager.OnSwitchPlayerPressed-= ActionSwitchPlayerPressed;
			m_oInputManager.OnGetObjectPressed -= ActionGetObject;
			m_oInputManager.OnUseObjectPressed -= ActionUseObject;
			m_oInputManager.OnDropObjectPressed -= ActionDropObject;
			m_oInputManager.OnInfoPressed -= ActionInfoPressed;
		}
		
	}


	//actionlist!!!!
	private void ActionPrimaryPressed ()
	{
		//Debug.LogWarning("Called primary");
	}
	private void ActionSecondaryPressed ()
	{
		//Debug.LogWarning("Called secondary");
	}
	private void ActionMoveLeftPressed ()
	{
	}
	private void ActionMoveDownPressed ()
	{
	}
	private void ActionMoveRightPressed (float alpha) //from -1 to +1
	{
		//Debug.LogWarning("ActionMoveRight");
	}
	private void ActionMoveUpPressed (float alpha) //from -1 to +1
	{
		//Debug.LogWarning("ActionMoveUp");
	}
	private void ActionSwitchPlayerPressed ()
	{
		//Debug.LogWarning("ActionSwitchPlayer");
	}
	private void ActionGetObject ()
	{
		//Debug.LogWarning("ActionGetObject");
	}
	private void ActionUseObject ()
	{
		//Debug.LogWarning("ActionUseObject");
	}
	private void ActionDropObject ()
	{
		//Debug.LogWarning("ActionDropObject");
	}
	private void ActionInfoPressed ()
	{
		//Debug.LogWarning("ActionInfoPressed");
	}
	[SerializeField]
	private InputManager.eInputSource m_eInputSource = InputManager.eInputSource.PLAYER;
}
