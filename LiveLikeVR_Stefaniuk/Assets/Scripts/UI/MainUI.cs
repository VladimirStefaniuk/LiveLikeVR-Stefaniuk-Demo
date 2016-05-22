using UnityEngine;
using System.Collections;

/// <summary>
/// Main UI. 
/// - Open setting menu when press ESC
/// </summary>
public class MainUI : MonoBehaviour {


	[SerializeField] GameObject SettingMenu;
	private  UnityStandardAssets.Characters.FirstPerson.FirstPersonController m_FPS; 
	private CrosshairRaycaster m_ChrosshairRaycaster;
	private bool isPause = false;

	void Start()
	{
		SettingMenu.SetActive(false); // turn of on start
		m_FPS = FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
		m_ChrosshairRaycaster = FindObjectOfType<CrosshairRaycaster>();
	}



	void Update () {
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			TooglePause();
		}
	}


	void TooglePause()
	{
		isPause = !isPause;
		SettingMenu.SetActive(isPause);
		m_FPS.enabled = !isPause;
		Cursor.visible = isPause;
		if(!isPause)
			Cursor.lockState = CursorLockMode.Locked;
	}


	#region "Crosshire mode"

	public void SetCrosshire(CrosshairRaycaster.CrosshairMode mode)
	{ 
		m_ChrosshairRaycaster.SetCrossfireMode(mode);
		TooglePause();
	}

	#endregion "Crosshire mode"
 
}
