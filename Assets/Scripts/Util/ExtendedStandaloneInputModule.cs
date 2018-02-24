using UnityEngine.EventSystems;
using UnityEngine;

namespace Maskirovka.Utility
{
	public class ExtendedStandaloneInputModule : StandaloneInputModule
	{
		private static ExtendedStandaloneInputModule _instance;
			
		protected override void Awake()
		{
			base.Awake();
			_instance = this;
		}

		public static PointerEventData GetPointerEventData(int pointerId = 0)
		{
			PointerEventData eventData;
			_instance.GetPointerData(pointerId, out eventData, true);
			return eventData;
		}

		public static bool OnUI(params int[] pointerId)
		{
			PointerEventData eventData = GetPointerEventData(pointerId[0]);
			bool touch = (eventData.pointerEnter != null && eventData.pointerEnter.layer == 5) || ( eventData.pointerPress != null && eventData.pointerPress.layer == 5 );
			
			for(int i = 1; i < pointerId.Length; ++i)
			{
				eventData = GetPointerEventData(pointerId[i]); 	
				touch = touch || (eventData.pointerEnter != null && eventData.pointerEnter.layer == 5) || ( eventData.pointerPress != null && eventData.pointerPress.layer == 5 );
			}
			
			#if UNITY_EDITOR
			eventData = GetPointerEventData(-1); 
			touch = touch || (eventData.pointerEnter != null && eventData.pointerEnter.layer == 5) || ( eventData.pointerPress != null && eventData.pointerPress.layer == 5 );
			#endif

			return touch;
		}
	}
}