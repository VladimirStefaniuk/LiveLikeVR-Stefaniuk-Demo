using UnityEngine;
using System.Collections;

/// <summary>
/// Rotate cubes by default, change it rotation direction and color
/// when user double clicks on it
/// </summary>
[RequireComponent(typeof(InteractiveItem))]
public class InteractiveCube : MonoBehaviour {

	[SerializeField] private float RotationSpeed = 20;
	InteractiveItem m_InteractiveItem;
	private Renderer m_Renderer;


	void Start()
	{
		m_InteractiveItem = GetComponent<InteractiveItem>();
		m_InteractiveItem.OnDoubleClick += InverseRotation;
		m_Renderer = GetComponent<Renderer>();
	}


	void Update () 
	{
		transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * RotationSpeed);
	}


	void InverseRotation()
	{ 
		RotationSpeed = -RotationSpeed;
		m_Renderer.material.color = RotationSpeed > 0 ? Color.white : Color.black;
	}
}
