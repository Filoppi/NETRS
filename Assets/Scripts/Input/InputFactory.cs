using UnityEngine;
using System.Collections;

public class InputFactory 
{
	public static InputBase GetInput(InputManager.eInputSource eInputType)
	{
		InputBase oInputImplementation = null;
	//	bool _forceTouch = false;
//#if DEBUG
//		_forceTouch = true;
//#endif
		switch(eInputType)
		{
            case InputManager.eInputSource.PLAYER:
			oInputImplementation = new InputPlayerKeyboard();
			break;
		case InputManager.eInputSource.AI:
			Debug.LogWarning("AI input not yet available");
			//oInputImplementation = new InputAi();
			break;
		case InputManager.eInputSource.NETWORK:
			Debug.LogWarning("Network input not yet available");
			//oInputImplementation = new InputNetwork();
			break;
		case InputManager.eInputSource.REPLAY:
			Debug.LogWarning("REPLAY input not yet available");
			//oInputImplementation = new InputReplay();
			break;
		};

		if(oInputImplementation == null)
		{
			Debug.LogError("Input implementation not available!");
		}

		return oInputImplementation;
	}
}
