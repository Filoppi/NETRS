using UnityEngine;
using System.Collections;
using System;

// This is a very big TAPULLO
// you should implement the proper input controller class for Joypad or ehatever you want!!!!
// now keyboard do all!!!
public class InputPlayerKeyboard : InputBase
{
	public override void InputUpdate()
	{
		base.InputUpdate();

		if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton2))
		{
			InternalPrimaryKeyDetected();
		}
		if(Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.JoystickButton0))
		{
			InternalSecondaryKeyDetected();
		}
		//Keep pressed
		if (Input.GetKey (KeyCode.D)) {
			InternalMoveRightDetected (1);
		} else if (Input.GetKey (KeyCode.A)) {
			InternalMoveRightDetected (-1);
		} else { //if (Input.GetAxisRaw ("Horizontal") != 0)
			InternalMoveRightDetected (Input.GetAxisRaw ("Horizontal"));
		}
		if (Input.GetKey (KeyCode.W)) {
			InternalMoveUpDetected (1);
		} else if (Input.GetKey (KeyCode.S)) {
			InternalMoveUpDetected (-1);
		} else { //if (Input.GetAxisRaw ("Vertical") != 0)
			InternalMoveUpDetected (Input.GetAxisRaw ("Vertical"));
		}
		if(Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.JoystickButton3))
		{
			InternalSwitchPlayerDetected();
		}
		if(Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.JoystickButton1))
		{
			InternalDropObjectDetected();
		}
		if(Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.JoystickButton6))
		{
			InternalInfoDetected();
		}
	}
}
