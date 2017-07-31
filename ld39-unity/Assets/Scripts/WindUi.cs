using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindUi : MonoBehaviour 
{
	public ToyManager toyManager;
	public int toyIndex = 0;
	public Image windImage;
	public Image selectedImage;
	public Image windBgImage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {

		windBgImage.enabled = false;
		selectedImage.enabled = false;
		windImage.fillAmount = 0;

		if ( toyManager != null )
		{
			if ( toyManager.toys != null && toyIndex < toyManager.toys.Count && toyManager.toys[toyIndex] != null )
			{
				windBgImage.enabled = true;
				windImage.fillAmount = toyManager.toys[toyIndex].knob.power;

				if ( toyManager.selectedToyIndex == toyIndex )
				{
					selectedImage.enabled = true;
				}
				else
				{
					selectedImage.enabled = false;
				}
			}
		}
		
	}
}
