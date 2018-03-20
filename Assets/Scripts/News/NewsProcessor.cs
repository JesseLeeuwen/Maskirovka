using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Maskirovka.UI;

namespace Maskirovka.News
{
	public abstract class NewsProcessor : ScriptableObject 
	{
		public virtual bool ProccesNews( NewsFeedItem news )
		{
			return false;
		}
	}
}