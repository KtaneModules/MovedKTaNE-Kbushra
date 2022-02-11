using System.Collections.Generic;
using UnityEngine;
using KModkit;
using Newtonsoft.Json;
using System.Linq;
using System.Text.RegularExpressions;
using Rnd = UnityEngine.Random;

public class SimpleModuleScript : MonoBehaviour {

	public KMAudio audio;
	public KMBombInfo info;
	public KMBombModule module;
	public KMSelectable[] movingButton;
	public KMSelectable[] pressableModule;
	public GameObject movingButtonObject;
	static int ModuleIdCounter = 1;
	int ModuleId;

	public float xpos;
	public float zpos;
	public float ypos;

	public AudioSource correct;

	bool _isSolved = false;
	bool incorrect = false;

	void Awake()
	{
		ModuleId = ModuleIdCounter++;

		foreach (KMSelectable button in movingButton)
		{
			KMSelectable pressedButton = button;
			button.OnInteract += delegate () { movingButtonStuff(pressedButton); return false; };
		}

		foreach (KMSelectable button in pressableModule)
		{
			KMSelectable pressedButton = button;
			button.OnInteract += delegate () { ModulePressing(pressedButton); return false; };
		}
	}

	void Start()
	{
		xpos = Random.Range (5f / 10f, -5f / 10f);
		zpos = Random.Range (2f / 10f, -2f / 10f);
		ypos = Random.Range (0f, 2f / 10f);

		movingButtonObject.transform.position = new Vector3 (xpos, ypos, zpos);
	}

	void movingButtonStuff(KMSelectable pressedButton)
	{
		GetComponent<KMAudio>().PlayGameSoundAtTransformWithRef(KMSoundOverride.SoundEffect.ButtonPress, transform);
		int buttonPosition = new int();
		for(int i = 0; i < movingButton.Length; i++)
		{
			if (pressedButton == movingButton[i])
			{
				buttonPosition = i;
				break;
			}
		}

		if (_isSolved == false) 
		{
			switch (buttonPosition) 
			{
			case 0:
				Log ("You did it!");
				break;
			}
			if (incorrect) 
			{
				module.HandleStrike ();
				incorrect = false;
			}
			else
			{
				correct.Play ();
				module.HandlePass();
				_isSolved = true;
			}
		}
	}

	void ModulePressing(KMSelectable pressedButton)
	{
		GetComponent<KMAudio>().PlayGameSoundAtTransformWithRef(KMSoundOverride.SoundEffect.ButtonPress, transform);
		int buttonPosition = new int();
		for(int i = 0; i < movingButton.Length; i++)
		{
			if (pressedButton == movingButton[i])
			{
				buttonPosition = i;
				break;
			}
		}

		if (_isSolved == false) 
		{
			switch (buttonPosition) 
			{
			case 0:
				Log ("Ok cool.");
				break;
			}
			if (incorrect) 
			{
				module.HandleStrike ();
				incorrect = false;
			}
			else
			{
				correct.Play ();
				module.HandlePass();
				_isSolved = true;
			}
		}
	}


	void Log(string message)
	{
		Debug.LogFormat("[Moved #{0}] {1}", ModuleId, message);
	}
}
