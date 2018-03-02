using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public Texture titleTexture;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width-200, Screen.height-100, 200, 100), "by Austen Rozanski\nVersion 1.0");
        GUI.DrawTexture(new Rect((Screen.width-(Screen.width / 2))/2, 0.0f, Screen.width / 2, Screen.width / 2), titleTexture);
        if (GUI.Button(new Rect((Screen.width - 400) / 2, (Screen.height - 220), 400.0f, 200.0f), "Click to Start Game"))
            Application.LoadLevel(1);
    }
}
