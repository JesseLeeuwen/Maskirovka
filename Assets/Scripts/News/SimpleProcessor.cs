using UnityEngine;

namespace Maskirovka.News
{
    [CreateAssetMenu(menuName = "simple processor")]
	public class SimpleProcessor : NewsProcessor
	{
		public override bool ProccesNews(News news)
		{
			float value = Random.value * 100;
			if( value < news.chanceOfSucces )
			{		
				foreach(Neighbour Neighbour in news.country.neighbours)
				{
					Neighbour.neighbour.UpdateRepu( news.country, news.value, news.catagorie );
				}

			}
			return value < news.chanceOfSucces;
		}
	}
}