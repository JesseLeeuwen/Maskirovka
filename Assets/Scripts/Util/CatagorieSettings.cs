using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Maskirovka.Utility
{
	[CreateAssetMenu(menuName = "catagorie settings")]
	public class CatagorieSettings : ScriptableObject
	{
		[System.Serializable]
		public struct CatagorieSetting
		{
			public Catagorie catagorie;
			public Color color;
			public Sprite iconLeft;
			public Sprite iconRight;
			public string[] keywordsLeft;
			public string[] keywordsRight;
		}
		[SerializeField]
		private CatagorieSetting[] colors;
		private static CatagorieSettings instance;

		public void Init()
		{
			instance = this;
		}

		public static Color GetColor(Catagorie catagorie)
		{
			return instance.colors.SingleOrDefault( x => x.catagorie == catagorie ).color;
		}

		public static Sprite GetIconLeft(Catagorie catagorie)
		{
			return instance.colors.SingleOrDefault( x => x.catagorie == catagorie ).iconLeft;
		}

		public static Sprite GetIconRight(Catagorie catagorie)
		{
			return instance.colors.SingleOrDefault( x => x.catagorie == catagorie ).iconRight;
		}

		public static string[] GetKeywordsLeft(Catagorie catagorie)
		{
			return instance.colors.SingleOrDefault( x => x.catagorie == catagorie ).keywordsLeft;
		}

		public static string[] GetKeywordsRight(Catagorie catagorie)
		{
			return instance.colors.SingleOrDefault( x => x.catagorie == catagorie ).keywordsRight;
		}
	}
}