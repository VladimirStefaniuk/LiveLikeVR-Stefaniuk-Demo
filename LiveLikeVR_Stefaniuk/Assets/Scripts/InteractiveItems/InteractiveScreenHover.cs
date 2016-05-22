using UnityEngine;
using System.Collections;

/// <summary>
/// Interactive screen hover.
/// Make some FX features, when user looks at  attached object.
/// </summary>
[RequireComponent(typeof(InteractiveItem))]
public class InteractiveScreenHover : MonoBehaviour {
 
	[SerializeField] private ParticleSystem m_ParticleSystem;
 

	private InteractiveItem m_InteractiveItem;
	private Renderer m_ObjectRenderer;
	private bool isHovered = false;
	private float NextFX = 0;					// timeout before next fx splash
 

	void Start () 
	{
		m_ObjectRenderer = GetComponent<Renderer>();
		m_InteractiveItem = GetComponent<InteractiveItem>();
		m_InteractiveItem.OnOver += () => isHovered = true;
		m_InteractiveItem.OnOut += OnHoverOut;
	}


	void Update () {
		if(isHovered)
		{
			// make transavtion 1 sec. from gray to white
			float fxTime =  Time.timeSinceLevelLoad % 1;
			m_ObjectRenderer.material.color = Color.Lerp(Color.gray, new Color(2,2,2,1), fxTime);
			if(fxTime > 0.9f && NextFX < Time.timeSinceLevelLoad) {
				NextFX = Time.timeSinceLevelLoad + 0.5f; 
				m_ParticleSystem.Play(); 
			}
		}
	}


	void OnHoverOut()
	{
		isHovered = false;
		m_ObjectRenderer.material.color = Color.white;
		m_ParticleSystem.Stop();
	}

}
