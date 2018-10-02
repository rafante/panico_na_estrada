using System.Collections.Generic;
using UnityEngine;

namespace LocalisationAndTranslation
{
	[CreateAssetMenu(fileName = "LocalisationManager", menuName = "Localisation/Localisation Manager", order = 1)]
	public class LocalisationManager : ScriptableObject
	{
		/// <summary> List of entries. </summary>
		[SerializeField]
		public List<EntryImage> entriesImage = new List<EntryImage> ();

		/// <summary> List of entries. </summary>
		[SerializeField]
		public List<EntryAudio> entriesAudio = new List<EntryAudio> ();

		/// <summary> Check if entry already exists. </summary>
		/// <returns><c>true</c>, if key exists, <c>false</c> otherwise.</returns>
		/// <param name="key">Key identifiable.</param>
		public bool ContainsKeyImage (string key)
		{
			return FindEntryImage (key) >= 0;
		}

		/// <summary> Check if entry already exists. </summary>
		/// <returns><c>true</c>, if key exists, <c>false</c> otherwise.</returns>
		/// <param name="key">Key identifiable.</param>
		public bool ContainsKeyAudio (string key)
		{
			return FindEntryAudio (key) >= 0;
		}

		/// <summary> Add the specified entry. </summary>
		/// <param name="entry">Entry.</param>
		public void Add (EntryImage entry)
		{
			Insert (entry, true);
		}

		/// <summary> Add the specified entry with a given index. </summary>
		/// <param name="entry">Entry.</param>
		/// <param name="index">Index.</param>
		public void Add (EntryImage entry, int index)
		{
			Insert (entry, index);
		}

		/// <summary> Add the specified entry. </summary>
		/// <param name="entry">Entry.</param>
		public void Add (EntryAudio entry)
		{
			Insert (entry, true);
		}

		/// <summary> Add the specified entry with a given index. </summary>
		/// <param name="entry">Entry.</param>
		/// <param name="index">Index.</param>
		public void Add (EntryAudio entry, int index)
		{
			Insert (entry, index);
		}

		/// <summary> Refresh the specified entry. </summary>
		/// <param name="entry">Entry.</param>
		public void Refresh (EntryImage entry)
		{
			Insert (entry, false);
		}

		/// <summary> Refresh the specified entry. </summary>
		/// <param name="entry">Entry.</param>
		public void Refresh (EntryAudio entry)
		{
			Insert (entry, false);
		}

		/// <summary> Move an Entry from an old Index to an new Index. </summary>
		/// <param name="oldIndex">Old index.</param>
		/// <param name="newIndex">New index.</param>
		public void MoveImage (int oldIndex, int newIndex)
		{
			EntryImage entry = entriesImage[oldIndex];
			RemoveImageAt (oldIndex);
			Insert (entry, newIndex);
		}

		/// <summary> Move an Entry from an old Index to an new Index. </summary>
		/// <param name="oldIndex">Old index.</param>
		/// <param name="newIndex">New index.</param>
		public void MoveAudio (int oldIndex, int newIndex)
		{
			EntryAudio entry = entriesAudio[oldIndex];
			RemoveAudioAt (oldIndex);
			Insert (entry, newIndex);
		}

		/// <summary> Insert the specified entry to the list. </summary>
		/// <param name="entry">Entry.</param>
		/// <param name="add">If set to <c>true</c> add or override.</param>
		private void Insert (EntryImage entry, bool add)
		{
			int i = FindEntryImage (entry.key);

			if (i >= 0)
			{
				if (add)
					return;

				entriesImage[i] = entry;
			}

			if (add)
			{
				entriesImage.Add (entry);
			}
		}

		/// <summary> Insert the specified entry to the list. </summary>
		/// <param name="entry">Entry.</param>
		/// <param name="add">If set to <c>true</c> add or override.</param>
		private void Insert (EntryAudio entry, bool add)
		{
			int i = FindEntryAudio (entry.key);

			if (i >= 0)
			{
				if (add)
					return;

				entriesAudio[i] = entry;
			}

			if (add)
			{
				entriesAudio.Add (entry);
			}
		}

		/// <summary> Insert the specified entry with a given index. </summary>
		/// <param name="entry">Entry.</param>
		/// <param name="index">Index.</param>
		private void Insert (EntryImage entry, int index)
		{
			if (index > entriesImage.Count)
				return;

			entriesImage.Insert (index, entry);
		}

		/// <summary> Insert the specified entry with a given index. </summary>
		/// <param name="entry">Entry.</param>
		/// <param name="index">Index.</param>
		private void Insert (EntryAudio entry, int index)
		{
			if (index > entriesAudio.Count)
				return;

			entriesAudio.Insert (index, entry);
		}

		/// <summary> Finds the entry with a given key </summary>
		/// <returns>The entry.</returns>
		/// <param name="key">Key identifiable.</param>
		private int FindEntryImage (string key)
		{
			for (int i = 0; i < entriesImage.Count; i++)
			{
				if (entriesImage[i].key.Equals (key))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary> Finds the entry with a given key </summary>
		/// <returns>The entry.</returns>
		/// <param name="key">Key identifiable.</param>
		private int FindEntryAudio (string key)
		{
			for (int i = 0; i < entriesAudio.Count; i++)
			{
				if (entriesAudio[i].key.Equals (key))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary> Remove the specified entry. </summary>
		/// <param name="entry">Entry.</param>
		public void Remove (EntryImage entry)
		{
			entriesImage.Remove (entry);
		}

		/// <summary> Remove the specified entry. </summary>
		/// <param name="entry">Entry.</param>
		public void Remove (EntryAudio entry)
		{
			entriesAudio.Remove (entry);
		}

		/// <summary> Remove an entry with a given index. </summary>
		/// <param name="index">Index.</param>
		public void RemoveImageAt (int index)
		{
			entriesImage.RemoveAt (index);
		}

		/// <summary> Remove an entry with a given index. </summary>
		/// <param name="index">Index.</param>
		public void RemoveAudioAt (int index)
		{
			entriesAudio.RemoveAt (index);
		}

		/// <summary> Remove an entry witha given key. </summary>
		/// <param name="key">Key.</param>
		public void RemoveImageWithKey (string key)
		{
			int i = FindEntryImage (key);

			if (i >= 0)
			{
				RemoveImageAt (i);
			}
		}

		/// <summary> Remove an entry witha given key. </summary>
		/// <param name="key">Key.</param>
		public void RemoveAudioWithKey (string key)
		{
			int i = FindEntryAudio (key);

			if (i >= 0)
			{
				RemoveAudioAt (i);
			}
		}

		/// <summary> Gets the sprite from a entry with a given key and given language. </summary>
		/// <returns>The sprite.</returns>
		/// <param name="key">Key.</param>
		public Sprite GetLocalisedSprite (string key, string language)
		{
			int i = FindEntryImage (key);

			if (i >= 0)
			{
				return entriesImage[i].GetLocalisation (language);
			}
			Debug.LogWarningFormat ("Localisation not found for Key '{0}' on '{1}'.", key, language);
			return null;
		}

		/// <summary> Gets the clip from a entry with a given key and given language. </summary>
		/// <returns>The clip.</returns>
		/// <param name="key">Key.</param>
		public AudioClip GetLocalisedAudioClip (string key, string language)
		{
			int i = FindEntryAudio (key);

			if (i >= 0)
			{
				return entriesAudio[i].GetLocalisation (language);
			}
			Debug.LogWarningFormat ("Localisation not found for Key '{0}' on '{1}'.", key, language);
			return null;
		}
	}
}
