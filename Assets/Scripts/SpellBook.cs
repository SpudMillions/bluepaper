using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class SpellBook : MonoBehaviour
{

	[SerializeField] private Spell[] spells;

	[SerializeField] private Image castingBar;

	[SerializeField] private Text spellName;

	[SerializeField] private Image spellIcon;

	[SerializeField] private Text spellCastTime;
	[SerializeField] private CanvasGroup canvasGroup;
	
	private Coroutine spellRoutine;

	private Coroutine fadeRoutine;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Spell CastSpell(int index)
	{
		castingBar.fillAmount = 0;
		castingBar.color = spells[index].BarColor;
		spellName.text = spells[index].Name;
		spellIcon.sprite = spells[index].Icon;

		spellRoutine = StartCoroutine(Progress(index));
		fadeRoutine = StartCoroutine(FadeBar());
		return spells[index];
	}

	private IEnumerator Progress(int index)
	{
		float timePassed = Time.deltaTime; //start time

		float rate = 1.0f / spells[index].CastTime;

		float progress = 0.0f; //how far we have gotten

		while (progress <= 1.0)
		{
			castingBar.fillAmount = Mathf.Lerp(0, 1, progress);
			progress += rate * Time.deltaTime;

			timePassed += Time.deltaTime;
			spellCastTime.text = (spells[index].CastTime - timePassed).ToString("F1"); //F2 only 1 digits

			if (spells[index].CastTime - timePassed < 0)
			{
				spellCastTime.text = "0.0";
			}
			
			yield return null;
		}
		
		StopCasting();
	}

	private IEnumerator FadeBar()
	{

		float rate = 1.0f / 0.25f;

		float progress = 0.0f;

		while (progress <= 1.0)
		{
			canvasGroup.alpha = Mathf.Lerp(0, 1, progress);
			progress += rate * Time.deltaTime;

			yield return null;
		}
	}
	
	public void StopCasting()
	{
		if (spellRoutine != null)
		{
			StopCoroutine(spellRoutine);
			spellRoutine = null;
		}

		if (fadeRoutine != null)
		{
			StopCoroutine(fadeRoutine);
			canvasGroup.alpha = 0;
			fadeRoutine = null;
		}
		
	}
}
