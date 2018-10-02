using UnityEngine;

namespace LocalisationAndTranslation
{
	[System.Serializable]
	public class LocalisationAudio
	{
		/// <summary> The language. </summary>
		[SerializeField]
		public string language;
		/// <summary> The sprite. </summary>
		[SerializeField]
		public AudioClip clip;

		public LocalisationAudio ()
		{

		}

		public LocalisationAudio (string language, AudioClip clip)
		{
			this.language = language;
			this.clip = clip;
		}
	}
}
