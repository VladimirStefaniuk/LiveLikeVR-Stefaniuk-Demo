using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Clickable billboard class.
/// Firstly shows tutorial then show random strings from words[] and random HSV color
/// </summary>
[RequireComponent(typeof(InteractiveItem))]
public class InteractiveScreenClicker : MonoBehaviour {

	[SerializeField] private Text m_Text; 		// Screen overlay text
	[SerializeField] private string[] words;	// Uses for random phrases

	private InteractiveItem m_InteractableItem;
	private Renderer m_ObjectRenderer;
	private IEnumerator m_TutorialRoutine;

	void Awake()
	{
		m_InteractableItem = GetComponent<InteractiveItem>();
		m_InteractableItem.OnClick += OnScreenClick;

		m_ObjectRenderer = GetComponent<Renderer>();
		m_Text.text = "Click on me!"; 
		m_TutorialRoutine = OnClick(); 
	}

	void OnScreenClick()
	{
		if(!m_TutorialRoutine.MoveNext() && words.Length > 0) 
		{
			m_Text.text = words[Random.Range(0, words.Length - 1)];
			m_ObjectRenderer.material.color = Random.ColorHSV();
		}
	}

	IEnumerator OnClick()
	{  
		m_Text.text = "Again ...";
		m_ObjectRenderer.material.color = Color.Lerp(Color.white, Color.green, 0.25f);

		yield return null;

		m_Text.text = "Cool! I am clickable!";
		m_ObjectRenderer.material.color = Color.Lerp(Color.white, Color.green, 0.5f);

		yield return null;

		m_Text.text = "Yeaah!";
		m_ObjectRenderer.material.color = Color.Lerp(Color.white, Color.green, 0.75f);

		yield return null;

		m_Text.text = "You are amazing!";
		m_ObjectRenderer.material.color = Color.Lerp(Color.white, Color.green, 1.0f);

		yield return null;
	}
}
