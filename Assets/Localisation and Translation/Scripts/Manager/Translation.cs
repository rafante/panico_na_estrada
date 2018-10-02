using UnityEngine;

namespace LocalisationAndTranslation
{
	[System.Serializable]
	public class Translation
	{
		/// <summary> The language. </summary>
		[SerializeField]
		public string language;
		/// <summary> The translation. </summary>
		[SerializeField]
		public string translation;
	}
}
