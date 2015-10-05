using UnityEngine;
using System.Collections;

public enum GameMode {

	idle, playing, levelEnd

}

public class MissionDemolition : MonoBehaviour {

	static public MissionDemolition S;

	public GameObject[] castles;
	public GUIText gtLevel;
	public GUIText gtScore;

	private int level;
	private int maxLevel;
	private int shotsTaken;
	private GameObject castle;
	private GameMode mode = GameMode.idle;
	private string showing = "Slingshot";
	static private GameObject view;

	void Start(){

		S = this;

		level = 0;
		maxLevel = castles.Length;
		StartLevel ();

	}

	void StartLevel(){

		if ( castle != null ) Destroy ( castle );

		GameObject[] gos = GameObject.FindGameObjectsWithTag ("Shot");
		foreach (GameObject pTemp in gos) Destroy (pTemp);

		castle = Instantiate (castles [level]) as GameObject;
		shotsTaken = 0;

		SwitchView ("Both");
		ShotLine.S.Clear ();

		GoalBehavior.goalMet = false;

		ShowGT ();

		mode = GameMode.playing;

	}

	void ShowGT(){

		gtLevel.text = "Level: " + (level + 1) + " of " + maxLevel;
		gtScore.text = "Shots taken: " + shotsTaken;

	}

	void Update(){

		ShowGT ();

		if ( mode == GameMode.playing && GoalBehavior.goalMet ){

			mode = GameMode.levelEnd;
			SwitchView("Both");
			Invoke ("NextLevel", 2f);

		}

	}

	void NextLevel(){

		level++;
		if (level == maxLevel) level = 0;
		StartLevel ();

	}

	void OnGUI(){

		Rect buttonRect = new Rect ((Screen.width / 2) - 50, 10, 100, 24);

		switch (showing) {
		case "Slingshot":
			if( GUI.Button( buttonRect, "Show Castle" ) ) SwitchView("Castle");
			break;

		case "Castle":
			if( GUI.Button( buttonRect, "Show Both" ) ) SwitchView("Both");
			break;

		case "Both":
			if( GUI.Button( buttonRect, "Show Slingshot" ) ) SwitchView("Slingshot");
			break;
		}

	}

	static public void SwitchView( string eView ){

		S.showing = eView;
		switch (S.showing) {
		case "Slingshot":
			FollowCam.S.poi = null;
			break;
			
		case "Castle":
			FollowCam.S.poi = S.castle;
			break;
			
		case "Both":
			view = GameObject.Find("ViewBoth");
			FollowCam.S.poi = view;
			break;
		}

	}

	public static void ShotFired(){

		S.shotsTaken++;

	}

}
