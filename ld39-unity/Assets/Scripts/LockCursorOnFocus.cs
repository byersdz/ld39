using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCursorOnFocus : MonoBehaviour {

	void OnApplicationFocus(bool hasFocus)
	{
		if ( hasFocus )
		{
			Cursor.lockState = CursorLockMode.Locked;

		}
	}

}
