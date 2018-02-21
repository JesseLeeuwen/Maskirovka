using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class NewsProcessor : ScriptableObject 
{
	public virtual void ProccesNews( News news )
	{

	}
}


public class SimpleProcessor : NewsProcessor
{
	public override void ProccesNews(News news)
	{
		
	}
}