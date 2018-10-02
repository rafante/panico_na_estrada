using System.Collections.Generic;
using UnityEngine;

namespace LocalisationAndTranslation
{
	[CreateAssetMenu (fileName = "LanguageManager", menuName = "Localisation/Language Manager", order = 1)]
	public class LanguageManager : ScriptableObject
	{
		/// <summary> Additional Languages. </summary>
		[SerializeField]
		public List<Language> languages = new List<Language> ();
		/// <summary> List of entries. </summary>
		[SerializeField]
		public List<EntryText> entries = new List<EntryText> ();

		#region Language Functions

		public string GetCurrentLanguage (string code)
		{
			for (int i = 0; i < languages.Count; i++)
			{
				if (languages[i].code.Equals (code))
				{
					return languages[i].name;
				}
			}

			return string.Empty;
		}

		/// <summary>
		/// Check if language with given name exists.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public bool ContainsLanguage (string name)
		{
			return FindLanguageByName (name) >= 0;
		}

		public bool ContainsLanguage (string name, string code)
		{
			return (FindLanguageByName (name) >= 0) && (FindLanguageByCode (code) >= 0);
		}

		/// <summary>
		/// Find language with given name.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		private int FindLanguageByName (string name)
		{
			for (int i = 0; i < languages.Count; i++)
			{
				if (languages[i].name.Equals (name))
				{
					return i;
				}
			}
			return -1;
		}

		private int FindLanguageByCode (string code)
		{
			for (int i = 0; i < languages.Count; i++)
			{
				if (languages[i].code.Equals (code))
				{
					return i;
				}
			}
			return -1;
		}

		public Language GetLanguageByName (string name)
		{
			int index = FindLanguageByName (name);

			if (index >= 0)
				return languages[FindLanguageByName (name)];
			else
				return null;
		}

		public Language MainLanguage
		{
			get
			{
				return languages[0];
			}
		} 

		#endregion

		#region Entry Functions

		/// <summary> Check if entry already exists. </summary>
		/// <returns><c>true</c>, if key exists, <c>false</c> otherwise.</returns>
		/// <param name="key">Key identifiable.</param>
		public bool ContainsKey (string key)
		{
			return FindEntry (key) >= 0;
		}

		/// <summary> Add the specified entry. </summary>
		/// <param name="entry">Entry.</param>
		public void Add (EntryText entry)
		{
			Insert (entry, true);
		}

		/// <summary> Add the specified entry with a given index. </summary>
		/// <param name="entry">Entry.</param>
		/// <param name="index">Index.</param>
		public void Add (EntryText entry, int index)
		{
			Insert (entry, index);
		}

		/// <summary> Refresh the specified entry. </summary>
		/// <param name="entry">Entry.</param>
		public void Refresh (EntryText entry)
		{
			Insert (entry, false);
		}

		/// <summary> Move an Entry from an old Index to an new Index. </summary>
		/// <param name="oldIndex">Old index.</param>
		/// <param name="newIndex">New index.</param>
		public void Move (int oldIndex, int newIndex)
		{
			EntryText entry = entries[oldIndex];
			Remove (oldIndex);
			Insert (entry, newIndex);
		}

		/// <summary> Insert the specified entry to the list. </summary>
		/// <param name="entry">Entry.</param>
		/// <param name="add">If set to <c>true</c> add or override.</param>
		private void Insert (EntryText entry, bool add)
		{
			int i = FindEntry (entry.key);

			if (i >= 0)
			{
				if (add)
					return;

				entries[i] = entry;
			}

			if (add)
			{
				entries.Add (entry);
			}
		}

		/// <summary> Insert the specified entry with a given index. </summary>
		/// <param name="entry">Entry.</param>
		/// <param name="index">Index.</param>
		private void Insert (EntryText entry, int index)
		{
			if (index > entries.Count)
				return;

			entries.Insert (index, entry);
		}

		/// <summary> Finds the entry with a given key </summary>
		/// <returns>The entry.</returns>
		/// <param name="key">Key identifiable.</param>
		private int FindEntry (string key)
		{
			for (int i = 0; i < entries.Count; i++)
			{
				if (entries[i].key.Equals (key))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary> Remove the specified entry. </summary>
		/// <param name="entry">Entry.</param>
		public void Remove (EntryText entry)
		{
			entries.Remove (entry);
		}

		/// <summary> Remove an entry with a given index. </summary>
		/// <param name="index">Index.</param>
		public void Remove (int index)
		{
			entries.RemoveAt (index);
		}

		/// <summary> Remove an entry witha given key. </summary>
		/// <param name="key">Key.</param>
		public void Remove (string key)
		{
			int i = FindEntry (key);

			if (i >= 0)
			{
				Remove (i);
			}
		}

		/// <summary> Gets the string from a entry with a given key and given language. </summary>
		/// <returns>The string.</returns>
		/// <param name="key">Key.</param>
		public string GetTranslation (string key, string language)
		{
			int i = FindEntry (key);

			if (i >= 0)
			{
				//return entries [i].values [language];
				return entries[i].GetTranslation (language);
			}
			Debug.LogWarningFormat ("Translation not found for Key '{0}' on '{1}'.", key, language);
			return string.Empty;
		}

		#endregion
	}
}