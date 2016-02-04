using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
		} else if (Input.GetKey (KeyCode.Return) || Input.GetKey (KeyCode.KeypadEnter) || Input.GetKey (KeyCode.Space) || Input.GetKey (KeyCode.JoystickButton0)) {
			SceneManager.LoadScene("Game");
		} else if (Input.GetKey(KeyCode.JoystickButton3) || Input.GetKey(KeyCode.F)) {
			{
				if (Screen.fullScreen) Screen.SetResolution(1280, 720, false);
				else {
					Resolution maxRes = new Resolution();
					maxRes.width = 1280;
					maxRes.height = 720;
					float maxX = 0;
					foreach (Resolution vec in Screen.resolutions) {
						if (vec.width > maxX) {
							maxX = vec.width;
							maxRes = vec;
						}
					}
					Screen.SetResolution(maxRes.width, maxRes.height, true);
				}
			}
		}
	}
}