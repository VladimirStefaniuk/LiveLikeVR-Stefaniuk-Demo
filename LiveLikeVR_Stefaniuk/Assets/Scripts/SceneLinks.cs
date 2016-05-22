using UnityEngine;
using System.Collections;

/// <summary>
/// Scene links.
/// This class contain links to the most usualy used objects on the scene.
/// </summary>
public class SceneLinks : MonoBehaviour {

	readonly string TAG = "SceneLinks";

	#region "Singleton"
	private static SceneLinks _instance;
	public static SceneLinks Instance 
	{
		get {
			if(_instance == null)
			{
				_instance = FindObjectOfType<SceneLinks>();
				if(_instance == null)
				{
					Debug.LogError("Missing SceneLinks.cs on scene");
				}
			}
			return _instance;
		}
		private set {
			_instance = value;
		}
	}
	#endregion
 
	#region "Links"

	[SerializeField] private Camera m_MainCamera;
	public Camera MainCamera { get { return m_MainCamera; } }

	#endregion "Links"

	void Awake () 
	{
		_instance = this;
		if(MainCamera == null)
			Debug.LogWarning(TAG + " please assign MainCamera propperty.");
	} 
}
