using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Maskirovka.UI
{
	public class ToggleButtonText : MonoBehaviour 
	{
		private Text text;
		private bool toggle;
		[SerializeField]
		private string[] textOptions;

		void Awake()
		{
			text = GetComponentInChildren<Text>();
		}

		public void ToggleText(  )
		{
			toggle = !toggle;
			text.text = textOptions[System.Convert.ToInt16( toggle )];
		}
	}
}