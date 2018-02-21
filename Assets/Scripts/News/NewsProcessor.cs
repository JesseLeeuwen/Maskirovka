using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Maskirovka.News
{

	public abstract class NewsProcessor : ScriptableObject 
	{
		public virtual void ProccesNews( News news )
		{

		}
	}

	[CreateAssetMenu(menuName = "simple processor")]
	public class SimpleProcessor : NewsProcessor
	{
		public override void ProccesNews(News news)
		{

		}
	}
}