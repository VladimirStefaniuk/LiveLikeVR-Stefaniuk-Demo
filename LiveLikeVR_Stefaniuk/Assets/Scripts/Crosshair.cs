using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Chrosshair.
/// Class control position and rotation of the crosshair in the space.
/// </summary>
[RequireComponent (typeof(Image))]
public class Crosshair : MonoBehaviour
{
	[SerializeField] private float m_DistanceFromCamera = 10;
	private Image m_Graphic; 									// Chrosshair image

	private Transform m_ChrosshairTransform;
	private Transform m_CameraTransform;

	private Vector3 m_DefaultScale; 							// Since the scale of the reticle changes, the original scale needs to be stored.
	private Quaternion m_DefaultRotation; 						// Used to store the original rotation of the reticle.

	public bool IsGlobalSpace;


	void Awake ()
	{  
		m_Graphic = GetComponent<Image> ();
		if (m_Graphic == null)
		{
			Debug.LogError ("Crosshair.cs Can not find attached graphic. It was deleted of wrong editor settings. Please try to reassign script in editor.");
		} 
		else 
		{
			m_ChrosshairTransform = m_Graphic.transform; 
			m_DefaultScale = m_ChrosshairTransform.transform.localScale;
		}
		m_DefaultRotation = Quaternion.identity; 
		m_CameraTransform = SceneLinks.Instance.MainCamera.transform;
	}


	// WORLD SPACE POSITION
	// draw on the fixed distance m_DistanceFromCamera from the camera
	public void SetPosition ()
	{ 
		m_ChrosshairTransform.position = m_CameraTransform.position + m_CameraTransform.forward * m_DistanceFromCamera;
		m_ChrosshairTransform.localRotation = m_DefaultRotation; 
	}


	// DYNAMIC RENDER 
	// Draw on the surface of the objects. 
	// Use Shader/UIOverlay.shader for rendering or equal shader
	public void SetPosition (RaycastHit Hit)
	{   
		m_ChrosshairTransform.position = Hit.point;
		m_ChrosshairTransform.rotation = Quaternion.FromToRotation (Vector3.forward, Hit.normal); 
	}

} 