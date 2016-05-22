using UnityEngine;
using System;
using System.Collections;

public class UserInput : MonoBehaviour {

    //public event Action<SwipeDirection> OnSwipe;                // Called every frame passing in the swipe, including if there is no swipe.
    public event Action OnClick;                                // Called when Fire1 is released and it's not a double click.
    public event Action OnDown;                                 // Called when Fire1 is pressed.
    public event Action OnUp;                                   // Called when Fire1 is released.
    public event Action OnDoubleClick;                          // Called when a double click is detected.
    public event Action OnCancel;                               // Called when Cancel is pressed.
 
	[SerializeField] private float m_DoubleClickTime = 0.3f;    //The max time allowed between double clicks
	[SerializeField] private float m_SwipeWidth = 0.3f;         //The width of a swipe
	private float m_LastMouseUpTime;                            // The time when Fire1 was last released.
	public float DoubleClickTime{ get { return m_DoubleClickTime; } }


	// Update is called once per frame
	void Update ()
	{
		CheckInput();
	}

	void CheckInput()
	{
		// This if statement is to trigger events based on the information gathered before.
		if(Input.GetButtonUp ("Fire1"))
		{
			// If anything has subscribed to OnUp call it.
			if (OnUp != null)
				OnUp();

			// If the time between the last release of Fire1 and now is less
			// than the allowed double click time then it's a double click.
			if (Time.time - m_LastMouseUpTime < m_DoubleClickTime)
			{
				// If anything has subscribed to OnDoubleClick call it.
				if (OnDoubleClick != null)
					OnDoubleClick();
			}
			else
			{
				// If it's not a double click, it's a single click.
				// If anything has subscribed to OnClick call it.
				if (OnClick != null)
					OnClick();
			}

			// Record the time when Fire1 is released.
			m_LastMouseUpTime = Time.time;
		} 
	}

	private void OnDestroy()
	{
		// Ensure that all events are unsubscribed when this is destroyed.
		// OnSwipe = null;
		OnClick = null;
		OnDoubleClick = null;
		OnDown = null;
		OnUp = null;
	}

}
