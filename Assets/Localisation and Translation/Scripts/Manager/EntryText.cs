using System.Collections.Generic;
using UnityEngine;

namespace LocalisationAndTranslation
{
	[System.Serializable]
	public class EntryText
	{
		/// <summary> Key identifiable. </summary>
		[SerializeField]
		public string key;
		/// <summary> List of translated values. </summary>
		[SerializeField]
		public List<Translation> Translations;
		/// <summary> If entry should not be translated. </summary>
		[SerializeField]
		public bool untranslatable;

		/// <summary> If entry contains translation for given language. </summary>
		/// <returns><c>true</c>, if translation exists, <c>false</c> otherwise.</returns>
		/// <param name="language">language.</param>
		public bool ContainsKey (string language)
		{
			return FindTranslation (language) >= 0;
		}

		/// <summary> Add new translation for given Language. </summary>
		/// <param name="language">Language.</param>
		public void Add (string language)
		{
			Translation translation = new Translation ();
			translation.language = language;
			translation.translation = "";
			Translations.Add (translation);
		}

		/// <summary> Removes Translation with given index. </summary>
		/// <param name="index">Index.</param>
		public void Remove (int index)
		{
			Translations.RemoveAt (index);
		}

		/// <summary> Removes Translation with given language. </summary>
		/// <param name="language">Language.</param>
		public void Remove (string language)
		{
			int i = FindTranslation (language);

			if (i >= 0)
			{
				Remove (i);
			}
		}

		/// <summary> Finds the index of translation with a given Language. </summary>
		/// <returns>The translation.</returns>
		/// <param name="key">Key.</param>
		private int FindTranslation (string key)
		{
			for (int i = 0; i < Translations.Count; i++)
			{
				if (Translations[i].language.Equals (key))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary> Gets translation of a given language. </summary>
		/// <returns>The string.</returns>
		/// <param name="language">Language.</param>
		public string GetTranslation (string language)
		{
			int i;

			if (untranslatable)
			{
				i = 0;
			}
			else
			{
				i = FindTranslation (language);
			}

			if (i >= 0)
			{
				return Translations[i].translation;
			}
			return string.Empty;
		}

		/// <summary> Gets the index of a given language. </summary>
		/// <returns>The index.</returns>
		/// <param name="language">Language.</param>
		public int GetIndex (string language)
		{
			return FindTranslation (language);
		}
	}
}
