using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour 
{
	public delegate void InputPrimaryPressed();
	public delegate void InputSecondaryPressed();
	public delegate void InputSwitchPlayerCallback();
	public delegate void InputMoveLeftCallback();
	public delegate void InputMoveRightCallback(float alpha);
	public delegate void InputMoveUpCallback(float alpha);
	public delegate void InputMoveDownCallback();
	public delegate void InputGetObjectCallback();
	public delegate void InputUseObjectCallback();
	public delegate void InputDropObjectCallback();
	public delegate void InputInfoCallback();

	public event InputPrimaryPressed        OnPrimaryPressed;
	public event InputSecondaryPressed      OnSecondaryPressed;
	public event InputSwitchPlayerCallback  OnSwitchPlayerPressed;
	public event InputMoveLeftCallback      OnMoveLeftPressed;
	public event InputMoveRightCallback     OnMoveRightPressed;
	public event InputMoveUpCallback        OnMoveUpPressed;
	public event InputMoveDownCallback      OnMoveDownPressed;
	public event InputGetObjectCallback     OnGetObjectPressed;
	public event InputUseObjectCallback     OnUseObjectPressed;
	public event InputDropObjectCallback    OnDropObjectPressed;
	public event InputInfoCallback          OnInfoPressed;


	void Start()
	{
		InitInput();
	}
	
	void Update()
	{
		if(m_oInput != null)
		{
			m_oInput.InputUpdate();
		}
	}

	private void PrimaryDetecet()
	{
		if(OnPrimaryPressed != null)
		{
			OnPrimaryPressed();
		}
	}

	private void SecondaryDetected()
	{
		if(OnSecondaryPressed != null)
		{
			OnSecondaryPressed();
		}
	}
	private void SwitchPlayerDetected()
	{
		if(OnSwitchPlayerPressed != null)
		{
			OnSwitchPlayerPressed();
		}
	}
	private void MoveLeftDetected()
	{
		if(OnMoveLeftPressed != null)
		{
			OnMoveLeftPressed();
		}
	}
	private void MoveRightDetected(float alpha)
	{
		//if(OnMoveRightPressed != null)
		{
			OnMoveRightPressed(alpha);
		}
	}
	private void MoveUpDetected(float alpha)
	{
		//if(OnMoveUpPressed != null)
		{
			OnMoveUpPressed(alpha);
		}
	}
	private void MoveDownDetected()
	{
		if(OnMoveDownPressed != null)
		{
			OnMoveDownPressed();
		}
	}
	private void GetObjectDetected()
	{
		if(OnGetObjectPressed != null)
		{
			OnGetObjectPressed();
		}
	
	}
	private void UseObjectDetected()
	{
		if(OnUseObjectPressed != null)
		{
			OnUseObjectPressed();
		}
		
	}
	private void DropObjectDetected()
	{
		if(OnDropObjectPressed != null)
		{
			OnDropObjectPressed();
		}

	}
	private void InfoDetected()
	{
		if(OnInfoPressed != null)
		{
			OnInfoPressed();
		}
		
	}
	private void InitInput()
	{
		m_oInput = InputFactory.GetInput(m_eInputSource);
		
		if(m_oInput != null)
		{
			m_oInput.Init();

			m_oInput.Activate(PrimaryDetecet, SecondaryDetected,SwitchPlayerDetected,MoveLeftDetected,MoveRightDetected,MoveUpDetected,MoveDownDetected,GetObjectDetected,UseObjectDetected,DropObjectDetected,InfoDetected);
	
		}
	}

	public void ChangeInput(eInputSource eNewInputSource)
	{
		if(eNewInputSource != m_eInputSource)
		{
			if(m_oInput != null)
			{
				m_oInput.Deactivate();
				m_oInput = null;
			}

			m_eInputSource = eNewInputSource;

			InitInput(); 
		}
	}

	//VARS
	public enum eInputSource
	{
		PLAYER = 0,
		AI,
		REPLAY,
		NETWORK,
		COUNT
	}
	
	[SerializeField] private eInputSource m_eInputSource = eInputSource.PLAYER;

	private InputBase m_oInput;
}
