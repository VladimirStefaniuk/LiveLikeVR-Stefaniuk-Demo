using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Raycast from the Main Camera forward ray,
/// handle user input, and delegate events to the hited
/// InteractiveItems objects.
/// </summary>
public class CrosshairRaycaster : MonoBehaviour {

	public event Action<RaycastHit> OnRaycasthit;                  // This event is called every frame that the user's gaze is over a collider.

	public float RaycastDistance = 10;							   // How far into the scene the ray is cast.

	[SerializeField] private Crosshair m_Chrosshair;
	[SerializeField] private Camera m_Camera;


	private InteractiveItem m_CurrentInteractible;                //The current interactive item
	private InteractiveItem m_LastInteractible;                   //The last interactive item


	[SerializeField] private LayerMask m_ExclusionLayers;         // Layers to exclude from the raycast.
	private UserInput m_UserInput;						  // This object raice event on user input

	public enum CrosshairMode { AUTOMATIC = 0, GLOBAL = 1, OBJECTS = 2 };
	private CrosshairMode m_CrosshairCurrMode;

	void Awake ()
	{
		
		m_Camera = SceneLinks.Instance.MainCamera;

		if(m_Chrosshair == null)
		{
			m_Chrosshair = FindObjectOfType<Crosshair>();
		}

		m_UserInput = FindObjectOfType<UserInput>();

		if(m_UserInput != null) {
			m_UserInput.OnClick += HandleClick;
			m_UserInput.OnDoubleClick += HandleDoubleClick;
		}
		else
		{
			Debug.Log("Missing UserInput.cs on the scene");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		Ray ray = new Ray(m_Camera.transform.position, m_Camera.transform.forward);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, RaycastDistance, ~m_ExclusionLayers))
		{
			if(m_CrosshairCurrMode == CrosshairMode.AUTOMATIC || m_CrosshairCurrMode == CrosshairMode.OBJECTS) 
				m_Chrosshair.SetPosition(hit);
			else 	
				m_Chrosshair.SetPosition();

			InteractiveItem interactible = hit.collider.GetComponent<InteractiveItem>(); //attempt to get the VRInteractiveItem on the hit object
			m_CurrentInteractible = interactible;

			// If we hit an interactive item and it's not the same as the last interactive item, then call Over
			if (interactible && interactible != m_LastInteractible)
				interactible.Over(); 

			// Deactive the last interactive item 
			if (interactible != m_LastInteractible)
				DeactiveLastInteractible();

			m_LastInteractible = interactible;
 
			if (OnRaycasthit != null)
				OnRaycasthit(hit);
			
		}
		else 
		{
			// Nothing was hit, deactive the last interactive item.
			DeactiveLastInteractible();
			m_CurrentInteractible = null;
			// if we have not hited any object,
			// so we cann't draw at the object
			// either is the object mode is set
			m_Chrosshair.SetPosition();
		}
	}


	private void DeactiveLastInteractible()
	{
		if (m_LastInteractible == null)
			return;

		m_LastInteractible.Out();
		m_LastInteractible = null;
	}


	private void HandleUp()
	{
		if (m_CurrentInteractible != null)
			m_CurrentInteractible.Up();
	}


	private void HandleDown()
	{
		if (m_CurrentInteractible != null)
			m_CurrentInteractible.Down();
	}


	private void HandleClick()
	{
		if (m_CurrentInteractible != null)
			m_CurrentInteractible.Click();
	}


	private void HandleDoubleClick()
	{
		if (m_CurrentInteractible != null)
			m_CurrentInteractible.DoubleClick();

	}

	public void SetCrossfireMode(CrosshairMode mode)
	{
		m_CrosshairCurrMode = mode;
	}

}
