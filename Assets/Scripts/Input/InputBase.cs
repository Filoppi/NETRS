using UnityEngine;
using System.Collections;
using System;

public class InputBase
{
	public virtual void Init()
	{
	}

	public virtual void InputUpdate()
	{
	}

	protected void InternalPrimaryKeyDetected()
	{
		if(m_actPrimaryKeyCallback != null)
		{
			m_actPrimaryKeyCallback();
		}
	}

	protected void InternalSecondaryKeyDetected()
	{
		if(m_actSecondaryKeyCallback != null)
		{
			m_actSecondaryKeyCallback();
		}
	}

	protected void InternalSwitchPlayerDetected()
	{
		if(m_actSwitchPlayerCallback != null)
		{
			m_actSwitchPlayerCallback();
		}
	}
	protected void InternalMoveLeftDetected()
	{
		if(m_actMoveLeftCallback != null)
		{
			m_actMoveLeftCallback();
		}
	}
	protected void InternalMoveRightDetected(float alpha)
	{
		if(m_actMoveRightCallback != null)
		{
			m_actMoveRightCallback(alpha);
		}
	}
	protected void InternalMoveUpDetected(float alpha)
	{
		if(m_actMoveUpCallback != null)
		{
			m_actMoveUpCallback(alpha);
		}
	}

	protected void InternalMoveDownDetected()
	{
		if(m_actMoveDownCallback != null)
		{
			m_actMoveDownCallback();
		}
	}
	protected void InternalGetObjectDetected()
	{
		if(m_actGetObjectCallback != null)
		{
			m_actGetObjectCallback();
		}
	}
	protected void InternalUseObjectDetected()
	{
		if(m_actUseObjectCallback != null)
		{
			m_actUseObjectCallback();
		}
	}
	protected void InternalDropObjectDetected()
	{
		if(m_actDropObjectCallback != null)
		{
			m_actDropObjectCallback();
		}
	}
	protected void InternalInfoDetected()
	{
		if(m_actInfoCallback != null)
		{
			m_actInfoCallback();
		}
	}
	public void Activate(Action actPrimaryKeyCallback, Action actSecondaryKeyCallback,Action actSwitchPlayerCallback,Action actMoveLeftCallback,Action<float> actMoveRightCallback,Action<float> actMoveUpCallback,Action actMoveDownCallback,Action actGetObjectCallback,Action actUseObjectCallback,Action actDropObjectCallback,Action actInfoCallback)
	{
		m_actPrimaryKeyCallback     = actPrimaryKeyCallback;
		m_actSecondaryKeyCallback   = actSecondaryKeyCallback;
		m_actSwitchPlayerCallback   = actSwitchPlayerCallback;
		m_actMoveLeftCallback       = actMoveLeftCallback;
		m_actMoveRightCallback      = actMoveRightCallback;
		m_actMoveUpCallback         = actMoveUpCallback;
		m_actMoveDownCallback       = actMoveDownCallback;
		m_actGetObjectCallback      = actGetObjectCallback;
		m_actUseObjectCallback      = actUseObjectCallback;
		m_actDropObjectCallback     = actDropObjectCallback;
		m_actInfoCallback           = actInfoCallback;
		m_bActive = true;
	}
	
	public void Deactivate()
	{
		m_actPrimaryKeyCallback    = null;
		m_actSecondaryKeyCallback  = null;
		m_actSwitchPlayerCallback  = null;
		m_actMoveLeftCallback      = null;
		m_actMoveRightCallback     = null;
		m_actMoveUpCallback        = null;
		m_actMoveDownCallback      = null;
		m_actGetObjectCallback     = null;
		m_actUseObjectCallback     = null;
		m_actDropObjectCallback    = null;
		m_actInfoCallback    = null;
		m_bActive = false;
	}

	//VARS
	private bool m_bActive = false;

	protected event Action m_actPrimaryKeyCallback     = null;
	protected event Action m_actSecondaryKeyCallback   = null;
	protected event Action m_actSwitchPlayerCallback   = null;
	protected event Action m_actMoveLeftCallback       = null;
	protected event Action<float> m_actMoveRightCallback      = null;
	protected event Action<float> m_actMoveUpCallback         = null;
	protected event Action m_actMoveDownCallback       = null;
	protected event Action m_actGetObjectCallback      = null;
	protected event Action m_actUseObjectCallback      = null;
	protected event Action m_actDropObjectCallback = null;
	protected event Action m_actInfoCallback = null;

}
