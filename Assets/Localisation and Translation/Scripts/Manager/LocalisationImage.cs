using UnityEngine;

namespace LocalisationAndTranslation
{
	[System.Serializable]
	public class LocalisationImage
	{
		/// <summary> The language. </summary>
		[SerializeField]
		public string language;
		/// <summary> The sprite. </summary>
		[SerializeField]
		public Sprite sprite;

		public LocalisationImage ()
		{

		}

		public LocalisationImage (string language, Sprite sprite)
		{
			this.language = language;
			this.sprite = sprite;
		}
	}
}
