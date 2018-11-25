using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Stat : MonoBehaviour {

	private Image _content;
	public float MaxValue { get; set; }
	[SerializeField] private float _lerpSpeed;
	[SerializeField] private Text _statValue;
	private float _currentValue;
	private float _currentFill;
	public float CurrentValue 
	{
		get { return _currentValue; }
		set
		{
			if (value > MaxValue)
			{
				_currentValue = MaxValue;
			}
			else if (value < 0)
			{
				_currentValue = 0;
			}
			else
			{
				_currentValue = value;
			}

			_currentFill = _currentValue / MaxValue;

			if (_statValue != null)
			{
				_statValue.text = CurrentValue + "/" + MaxValue;
			}
		} 
	}
	
	// Use this for initialization
	void Start ()
	{
		_content = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (_currentFill != _content.fillAmount)
		{
			_content.fillAmount = Mathf.Lerp(_content.fillAmount, _currentFill, Time.deltaTime * _lerpSpeed);
		}
	}

	public void Initialize(float currentValue, float maxValue)
	{
		if (_content == null)
		{
			_content = GetComponent<Image>();
		}
		MaxValue = maxValue;
		CurrentValue = currentValue;
		_content.fillAmount = CurrentValue / MaxValue;
	}
}
