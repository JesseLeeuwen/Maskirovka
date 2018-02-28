using System.Collections;
using UnityEngine;

namespace Maskirovka.Utility
{
	public class OnClickAnim : MonoBehaviour
	{		
		[SerializeField]
		private float offset;
		[SerializeField]
		private float speed;
		
		private Coroutine routine;
		private Vector3 defaultPosition;
		private float target;
		private float current;

		void Start()
		{
			defaultPosition = transform.position;
		}

		IEnumerator ToPosition(float target)
		{
			while( current != target )
			{
				current = Mathf.MoveTowards( current, target, Time.deltaTime * speed);
				transform.position = defaultPosition + Vector3.up * current;
				yield return null;
			}
		}		

		public void Select(object arg)
		{
			if(routine != null)
				StopCoroutine(routine);
				
			StartCoroutine(ToPosition( offset * (int)arg ));
		}		
	}
}