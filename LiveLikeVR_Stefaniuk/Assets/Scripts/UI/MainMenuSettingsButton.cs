using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class MainMenuSettingsButton : MonoBehaviour {

	[SerializeField] private CrosshairRaycaster.CrosshairMode m_CrosshairMode;

	public void onClick()
	{
		FindObjectOfType<MainUI>().SetCrosshire(m_CrosshairMode);
	}
}
