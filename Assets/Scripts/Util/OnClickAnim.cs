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

		IEnumerator ToPosition(float target)
		{

			while( current != target )
			{
				
				current = Mathf.MoveTowards( current, target, Time.deltaTime * speed);
				if (current > 0 && target >= current){
				render.sortingOrder=2;
				}else{
					render.sortingOrder=1;
				}
				transform.position = defaultPosition + Vector3.up * current;
				yield return null;
			}
		}		

		public void Select(object arg)
		{
			
			if(routine != null)
				StopCoroutine(routine);
				
			routine = StartCoroutine(ToPosition( offset * (int)arg ));
		}

	void OnMouseOver(){
		render.color= currentColor * new Color(.9f,.9f,.9f);
	}
	    void OnMouseExit()
    {

		render.color= currentColor;
	}
	}
}