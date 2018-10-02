using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LocalisationAndTranslation
{
	public class LocalisationEditor : EditorWindow
	{
		/// <summary> Minimum Window width </summary>
		private const float WINDOW_MIN_WIDTH = 800f;
		/// <summary> Minimum Window height </summary>
		private const float WINDOW_MIN_HEIGHT = 600f;
		/// <summary> Path to Language Manager </summary>
		private const string path = "Assets/Localisation And Translation/Resources/";

		string extraPanelDisplay;
		private string key;

		private int index;
		private int newIndex;
		private int listSize;
		private List<bool> triggers = new List<bool> ();

		bool extraPanel;
		bool newEntry;
		bool moveEntry;

		Vector2 scrollPos_entryList;

		public int indexToolbar = 0;
		public string[] toolbarStrings = new string[] { "Localised Image", "Localised Audio" };

		LanguageManager langManager;
		LocalisationManager locManager;

		[MenuItem ("Tools/Localisation/Localisation Editor", false, 3)]
		public static void GetWindow ()
		{
			LocalisationEditor editor = GetWindow<LocalisationEditor> ("Localisations", true);
			editor.minSize = new Vector2 (WINDOW_MIN_WIDTH, WINDOW_MIN_HEIGHT);
		}

		void OnEnable ()
		{
			hideFlags = HideFlags.HideAndDontSave;

			langManager = (LanguageManager)AssetDatabase.LoadAssetAtPath (path + "LanguageManager.asset", typeof (LanguageManager));
			if (langManager == null)
			{
				Debug.LogWarning ("File not found. Creating new Language Manager...");
				langManager = CreateResourceAssets.CreateLanguageManager ();
			}

			locManager = (LocalisationManager)AssetDatabase.LoadAssetAtPath (path + "LocalisationManager.asset", typeof (LocalisationManager));
			if (locManager == null)
			{
				Debug.LogWarning ("File not found. Creating new Localisation Manager...");
				locManager = CreateResourceAssets.CreateLocalisationManager ();
			}

			index = 0;
			key = string.Empty;
		}

		private void OnGUI ()
		{
			EditorGUILayout.BeginVertical ("Box");

			EditorGUILayout.BeginHorizontal ();
			indexToolbar = GUILayout.Toolbar (indexToolbar, toolbarStrings);
			EditorGUILayout.EndHorizontal ();

			if (indexToolbar == 0)
			{
				SetTriggerList ();

				ManageLocalisedObjects ();
			}

			if (indexToolbar == 1)
			{
				SetTriggerList ();

				ManageLocalisedObjects ();
			}

			EditorGUILayout.EndVertical ();
		}

		private void ManageLocalisedObjects ()
		{
			//Separates left and right panel
			EditorGUILayout.BeginHorizontal ();

			DrawEntryList ();

			DrawEntryEditor ();

			//Closes Top panel with entry buttons
			EditorGUILayout.EndHorizontal ();
		}

		//Draws Left Panel with list of entries
		private void DrawEntryList ()
		{
			EditorGUILayout.BeginVertical ("Box", GUILayout.MaxWidth (250f), GUILayout.ExpandHeight (true));

			EditorGUILayout.Space ();

			if (GUILayout.Button ("Save List"))
			{
				EditorGUI.FocusTextInControl (null);
				Save ();
			}

			DrawListOfEntries ();

			//Close Left Panel with entry list
			EditorGUILayout.EndVertical ();
		}

		private void DrawListOfEntries ()
		{
			listSize = (indexToolbar == 0) ? locManager.entriesImage.Count : locManager.entriesAudio.Count;

			if (listSize > 0)
			{
				EditorGUILayout.LabelField (string.Format ("{0} found: {1}.", toolbarStrings[indexToolbar], listSize));

				EditorGUILayout.Space ();

				scrollPos_entryList = EditorGUILayout.BeginScrollView (scrollPos_entryList, GUILayout.Width (0), GUILayout.Height (WINDOW_MIN_HEIGHT - 150));

				for (int i = 0; i < listSize; i++)
				{
					string _key = (indexToolbar == 0) ? locManager.entriesImage[i].key : locManager.entriesAudio[i].key;

					string keyName = string.Format ("[{0}] - {1}", i, _key);
					
					triggers[i] = EditorGUI.Foldout (EditorGUILayout.GetControlRect (), triggers[i], keyName, true);

					if (triggers[i])
					{
						triggers[index] = false;
						index = i;
						key = (indexToolbar == 0) ? locManager.entriesImage[i].key : locManager.entriesAudio[i].key;
					}
				}
				EditorGUILayout.EndScrollView ();
			}
			else
			{
				EditorGUILayout.LabelField ("No Entries Found...");
			}
		}

		private void DrawEntryEditor ()
		{
			//Draws Right Panel with entry options
			EditorGUILayout.BeginVertical (GUILayout.ExpandWidth (true), GUILayout.ExpandHeight (true));

			EditorGUILayout.Space ();

			DrawEntryButtons ();

			if (extraPanel)
				DrawExtraPanel ();

			EditorGUILayout.Space ();

			DrawNavigationButtons ();

			DrawEntryPanel ();

			//close right panel with entry options
			EditorGUILayout.EndVertical ();
		}

		private void DrawEntryButtons ()
		{
			//Draws Top panel with entry buttons
			EditorGUILayout.BeginHorizontal (GUILayout.ExpandWidth (false));

			GUI.color = Color.green;
			if (GUILayout.Button ("New Entry"))
			{
				EditorGUI.FocusTextInControl (null);

				CreateNewEntry ();

				Save ();
			}

			GUI.color = Color.white;
			EditorGUILayout.Space ();

			if (GUILayout.Button ("New Entry at"))
			{
				EditorGUI.FocusTextInControl (null);
				extraPanel = !extraPanel;
				newEntry = !newEntry;
				extraPanelDisplay = "Create new Entry on : ";
			}

			EditorGUILayout.Space ();

			if (GUILayout.Button ("Move Entry"))
			{
				EditorGUI.FocusTextInControl (null);
				extraPanel = !extraPanel;
				moveEntry = !moveEntry;
				extraPanelDisplay = string.Format ("Move {0} from {1} to: ", key, index);
			}

			EditorGUILayout.Space ();

			GUI.color = Color.red;
			if (GUILayout.Button ("Delete Entry") && listSize > 0)
			{
				EditorGUI.FocusTextInControl (null);

				if (EditorUtility.DisplayDialog ("Delete Entry?",
					"Are you sure you want to delete " + key + "?", "Yes", "No"))
				{
					if (indexToolbar == 0)
						locManager.RemoveImageAt (index);
					else
						locManager.RemoveAudioAt (index);

					listSize = (indexToolbar == 0) ? locManager.entriesImage.Count : locManager.entriesAudio.Count;

					index = Mathf.Clamp (index, 0, listSize - 1);
					Debug.Log ("Item deleted.");
					SetTriggerList ();

					Save ();
				}
			}

			GUI.color = Color.white;

			//Closes Top panel with entry buttons
			EditorGUILayout.EndHorizontal ();
		}

		private void DrawExtraPanel ()
		{
			EditorGUILayout.Space ();
			//Opens extra panel
			EditorGUILayout.BeginHorizontal ("Box");

			EditorGUILayout.Space ();

			EditorGUILayout.LabelField (extraPanelDisplay);

			newIndex = EditorGUILayout.IntField (newIndex, GUILayout.MaxWidth (50f), GUILayout.ExpandWidth (false));

			if (newIndex < 0)
				newIndex = 0;

			if (newIndex > listSize)
				newIndex = listSize;

			EditorGUILayout.Space ();

			if (GUILayout.Button ("Confirm", GUILayout.ExpandWidth (false)) && listSize > 0)
			{
				EditorGUI.FocusTextInControl (null);

				if (newIndex >= 0 || newIndex <= listSize)
				{
					if (newEntry)
						CreateNewEntry (true);

					if (moveEntry)
					{
						if(indexToolbar == 0)
							locManager.MoveImage (index, newIndex);
						else
							locManager.MoveAudio (index, newIndex);
					}
				}

				index = newIndex;

				extraPanel = false;
				newEntry = false;
				moveEntry = false;

				Save ();
			}

			EditorGUILayout.Space ();

			if (GUILayout.Button ("Cancel", GUILayout.ExpandWidth (false)) && listSize > 0)
			{
				EditorGUI.FocusTextInControl (null);
				extraPanel = false;
				newEntry = false;
				moveEntry = false;
			}
			EditorGUILayout.Space ();

			//Closes extra panel
			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.Space ();
		}

		private void DrawNavigationButtons ()
		{
			int oldIndex = index;

			EditorGUILayout.BeginHorizontal (GUILayout.ExpandWidth (true));
			if (GUILayout.Button ("First") && listSize > 0)
			{
				EditorGUI.FocusTextInControl (null);
				index = 0;
			}

			if (GUILayout.Button ("Prev 10") && listSize > 0)
			{
				EditorGUI.FocusTextInControl (null);
				index -= 10;
				if (index < 0)
					index = 0;
			}

			if (GUILayout.Button ("Prev") && listSize > 0)
			{
				EditorGUI.FocusTextInControl (null);
				index--;
				if (index < 0)
					index = 0;
			}

			if (GUILayout.Button ("Next") && listSize > 0)
			{
				EditorGUI.FocusTextInControl (null);
				index++;
				if (index >= listSize)
					index = listSize - 1;
			}

			if (GUILayout.Button ("Next 10") && listSize > 0)
			{
				EditorGUI.FocusTextInControl (null);
				index += 10;
				if (index >= listSize)
					index = listSize - 1;
			}

			if (GUILayout.Button ("Last") && listSize > 0)
			{
				EditorGUI.FocusTextInControl (null);
				index = listSize - 1;
			}

			if (oldIndex != index)
				key = (indexToolbar == 0) ? locManager.entriesImage[index].key : locManager.entriesAudio[index].key;

			//Close Mid panel with navigation Buttons
			EditorGUILayout.EndHorizontal ();
		}

		private void DrawEntryPanel ()
		{
			//Draws Entry Box
			EditorGUILayout.BeginVertical ();
			if (listSize > 0)
			{
				EditorGUILayout.LabelField (string.Format ("Localisation {0} out of {1} found.", index + 1, listSize));

				if (indexToolbar == 0)
				{
					locManager.entriesImage[index].key = EditorGUILayout.TextField ("key:", locManager.entriesImage[index].key);

					for (int i = 0; i < langManager.languages.Count; i++)
					{
						string display = string.Format ("{0}: ", langManager.languages[i].name);

						if (!locManager.entriesImage[index].ContainsKey (langManager.languages[i].name))
						{
							locManager.entriesImage[index].Add (langManager.languages[i].name);
						}

						int localisationIndex = locManager.entriesImage[index].GetIndex (langManager.languages[i].name);
						locManager.entriesImage[index].Localisations[localisationIndex].sprite = (Sprite)EditorGUILayout.ObjectField (display, locManager.entriesImage[index].Localisations[localisationIndex].sprite, typeof (Sprite), false);
					}
				}
				else
				{
					locManager.entriesAudio[index].key = EditorGUILayout.TextField ("key:", locManager.entriesAudio[index].key);

					for (int i = 0; i < langManager.languages.Count; i++)
					{
						string display = string.Format ("{0}: ", langManager.languages[i].name);

						if (!locManager.entriesAudio[index].ContainsKey (langManager.languages[i].name))
						{
							locManager.entriesAudio[index].Add (langManager.languages[i].name);
						}

						int localisationIndex = locManager.entriesAudio[index].GetIndex (langManager.languages[i].name);
						locManager.entriesAudio[index].Localisations[localisationIndex].clip = (AudioClip)EditorGUILayout.ObjectField (display, locManager.entriesAudio[index].Localisations[localisationIndex].clip, typeof (AudioClip), false);
					}
				}
			}
			else
			{
				EditorGUILayout.LabelField ("No Localisations Found...");
			}
			//Close Entry Box
			EditorGUILayout.EndVertical ();
		}

		private void SetTriggerList ()
		{
			listSize = (indexToolbar == 0) ? locManager.entriesImage.Count : locManager.entriesAudio.Count;

			if (listSize == 0)
				return;

			triggers = new List<bool> (listSize);

			for (int i = 0; i < listSize; i++)
			{
				triggers.Add (false);
			}

			triggers[index] = true;
		}

		private void RefreshList ()
		{
			listSize = (indexToolbar == 0) ? locManager.entriesImage.Count : locManager.entriesAudio.Count;
		}

		private void CreateNewEntry (bool insertion = false)
		{
			if (indexToolbar == 0)
			{
				EntryImage entry = new EntryImage
				{
					key = "new entry",
					Localisations = new List<LocalisationImage> (),
				};

				foreach (Language language in langManager.languages)
				{
					entry.Add (language.name);
				}

				if (insertion)
				{
					locManager.Add (entry, newIndex);
					index = newIndex;
				}
				else
				{
					locManager.Add (entry);
					index = locManager.entriesImage.Count - 1;
				}
			}
			else
			{
				EntryAudio entry = new EntryAudio
				{
					key = "new entry",
					Localisations = new List<LocalisationAudio> (),
				};

				foreach (Language language in langManager.languages)
				{
					entry.Add (language.name);
				}

				if (insertion)
				{
					locManager.Add (entry, newIndex);
					index = newIndex;
				}
				else
				{
					locManager.Add (entry);
					index = locManager.entriesImage.Count - 1;
				}
			}

			listSize = (indexToolbar == 0) ? locManager.entriesImage.Count : locManager.entriesAudio.Count;

			SetTriggerList ();
		}

		private void Save ()
		{
			EditorUtility.SetDirty (locManager);
			AssetDatabase.SaveAssets ();
			AssetDatabase.Refresh ();
		}
	}
}


