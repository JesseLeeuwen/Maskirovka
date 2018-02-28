using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Maskirovka.News
{
	public abstract class NewsProcessor : ScriptableObject 
	{
		public virtual bool ProccesNews( News news )
		{
			return false;
		}
	}
}