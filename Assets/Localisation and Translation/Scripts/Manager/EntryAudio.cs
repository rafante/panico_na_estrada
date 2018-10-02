using System.Collections.Generic;
using UnityEngine;

namespace LocalisationAndTranslation
{
	[System.Serializable]
	public class EntryAudio
	{
		/// <summary> Key identifiable. </summary>
		[SerializeField]
		public string key;
		/// <summary> List of localised values. </summary>
		[SerializeField]
		public List<LocalisationAudio> Localisations;

		/// <summary> If entry contains localisation for given language. </summary>
		/// <returns><c>true</c>, if localisation exists, <c>false</c> otherwise.</returns>
		/// <param name="language">language.</param>
		public bool ContainsKey (string language)
		{
			return FindLocalisation (language) >= 0;
		}

		/// <summary> Add new localisation for given Language. </summary>
		/// <param name="language">Language.</param>
		public void Add (string language)
		{
			LocalisationAudio localisation = new LocalisationAudio ();
			localisation.language = language;
			Localisations.Add (localisation);
		}

		/// <summary> Removes Localisation with given index. </summary>
		/// <param name="index">Index.</param>
		public void Remove (int index)
		{
			Localisations.RemoveAt (index);
		}

		/// <summary> Removes Localisation with given language. </summary>
		/// <param name="language">Language.</param>
		public void Remove (string language)
		{
			int i = FindLocalisation (language);

			if (i >= 0)
			{
				Remove (i);
			}
		}

		/// <summary> Finds the index of a localisation with a given Language. </summary>
		/// <returns>The translation.</returns>
		/// <param name="key">Key.</param>
		private int FindLocalisation (string key)
		{
			for (int i = 0; i < Localisations.Count; i++)
			{
				if (Localisations[i].language.Equals (key))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary> Gets localised clip of a given language. </summary>
		/// <returns>The string.</returns>
		/// <param name="language">Language.</param>
		public AudioClip GetLocalisation (string language)
		{
			int i = FindLocalisation (language);

			return Localisations[i].clip;
		}

		/// <summary> Gets the index of a given language. </summary>
		/// <returns>The index.</returns>
		/// <param name="language">Language.</param>
		public int GetIndex (string language)
		{
			return FindLocalisation (language);
		}
	}
}

