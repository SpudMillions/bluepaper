﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

	private static UIManager instance;

	public static UIManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<UIManager>();
			}

			return instance;
		}
	}
	
	[SerializeField] private Button[] actionButtons;

	private KeyCode action1, action2, action3;

	[SerializeField] private GameObject targetFrame;

	[SerializeField] Image portraitFrame;

	[SerializeField] private Stat healthStat;
	// Use this for initialization
	void Start ()
	{
		healthStat = targetFrame.GetComponentInChildren<Stat>();
		action1 = KeyCode.Alpha1;
		action2 = KeyCode.Alpha2;
		action3 = KeyCode.Alpha3;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(action1))
		{
			ActionButtonOnClick(0);
		}

		if (Input.GetKeyDown(action2))
		{
			ActionButtonOnClick(1);
		}

		if (Input.GetKeyDown(action3))
		{
			ActionButtonOnClick(2);
		}
	}

	private void ActionButtonOnClick(int buttonIndex)
	{
		actionButtons[buttonIndex].onClick.Invoke();
	}

	public void ShowTargetFrame(NPC target)
	{
		targetFrame.SetActive(true);
		healthStat.Initialize(target.MyHealth.CurrentValue, target.MyHealth.MaxValue);
		//if want to implement character portraits
		//portraitFrame.sprite = target.Portrait;
		target.healthChanged += new HealthChanged(UpdateTargetFrame);
		target.characterRemoved += new CharacterRemoved(HideTargetFrame);
	}

	public void HideTargetFrame()
	{
		targetFrame.SetActive(false);
	}

	public void UpdateTargetFrame(float health)
	{
		healthStat.CurrentValue = health;
	}
}
