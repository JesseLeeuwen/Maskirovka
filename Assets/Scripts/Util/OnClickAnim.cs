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
		private SpriteRenderer render;
		private Color currentColor;

		void Start()
		{
			defaultPosition = transform.position;
			render = GetComponent<SpriteRenderer>();
			currentColor = render.color;
		}

		IEnumerator ToPosition(float target, int arg)
		{
			while( current != target )
			{				
				current = Mathf.MoveTowards( current, target, Time.deltaTime * speed);
				render.sortingOrder = 2 + arg;
				transform.position = defaultPosition + Vector3.up * current;
				yield return null;
			}
			if( arg == 0 )
				render.sortingOrder = 1;
		}		

		public void Select(object arg)
		{
			if(routine != null)
				StopCoroutine(routine);
			
			routine = StartCoroutine(ToPosition( offset * (int)arg, (int)arg ));
		}

		public void SetColor( Color newColor)
		{
			currentColor = newColor;
		}

		void OnMouseOver(){
			render.color= currentColor * new Color(.9f,.9f,.9f);
		}
		
		void OnMouseExit(){
			render.color= currentColor;
		}
	}
}