using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LocalisationAndTranslation
{
	public class TranslatorEditor : EditorWindow
	{
		/// <summary> Minimum Window width </summary>
		private const float WINDOW_MIN_WIDTH = 800f;
		/// <summary> Minimum Window height </summary>
		private const float WINDOW_MIN_HEIGHT = 600f;
		/// <summary> Path to Language Manager </summary>
		private const string path = "Assets/Localisation And Translation/Resources/LanguageManager.asset";

		string extraPanelDisplay;

		private int index;
		private int newIndex;
		private List<bool> triggers = new List<bool> ();

		bool extraPanel;
		bool newEntry;
		bool moveEntry;
		bool untranslatable;

		Vector2 scrollPos_entryList;

		LanguageManager manager;

		[MenuItem ("Tools/Localisation/Translation Editor", false, 2)]
		public static void GetWindow ()
		{
			TranslatorEditor editor = GetWindow<TranslatorEditor> ("Translations", true);
			editor.minSize = new Vector2 (WINDOW_MIN_WIDTH, WINDOW_MIN_HEIGHT);
		}

		void OnEnable ()
		{
			hideFlags = HideFlags.HideAndDontSave;

			manager = (LanguageManager)AssetDatabase.LoadAssetAtPath (path, typeof (LanguageManager));
			if (manager == null)
			{
				Debug.LogWarning ("File not found. Creating new Language Manager...");
				manager = CreateResourceAssets.CreateLanguageManager ();
			}
	
			index = 0;

			SetTriggerList ();
		}

		private void OnGUI ()
		{
			ManageTranslatedObjects ();
		}

		private void ManageTranslatedObjects ()
		{
			//Separates left and right panel
			EditorGUILayout.BeginHorizontal ("Box");

			DrawEntryList ();

			DrawEntryEditor ();

			//Closes window
			EditorGUILayout.EndHorizontal ();
		}

		private void DrawEntryList ()
		{
			EditorGUILayout.BeginVertical ("Box", GUILayout.MaxWidth (250f), GUILayout.ExpandHeight (true));

			EditorGUILayout.Space ();

			if (GUILayout.Button ("Save List"))
			{
				EditorGUI.FocusTextInControl (null);
				SaveManager ();
			}

			DrawListOfEntries ();

			//Close Left Panel with entry list
			EditorGUILayout.EndVertical ();
		}

		private void DrawListOfEntries ()
		{
			//Draws list of entries
			if (manager.entries.Count > 0)
			{
				EditorGUILayout.LabelField (string.Format ("Translations found: {0}.", manager.entries.Count));

				EditorGUILayout.Space ();

				scrollPos_entryList = EditorGUILayout.BeginScrollView (scrollPos_entryList, GUILayout.Width (0), GUILayout.Height (WINDOW_MIN_HEIGHT - 150));

				for (int i = 0; i < manager.entries.Count; i++)
				{
					string keyName = string.Format ("[{0}] - {1}", i, manager.entries[i].key);
					triggers[i] = EditorGUI.Foldout (EditorGUILayout.GetControlRect (), triggers[i], keyName, true);

					if ((triggers[i]))
					{
						triggers[index] = false;
						index = i;
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
			//Draws Right Panel with buttons and entry editor
			EditorGUILayout.BeginVertical (GUILayout.ExpandWidth (true), GUILayout.ExpandHeight (true));

			EditorGUILayout.Space ();

			DrawEntryButtons ();

			if (extraPanel)
				DrawExtraPanel ();

			EditorGUILayout.Space ();

			DrawNagivationButtons ();

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

				SaveManager ();
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
				extraPanelDisplay = string.Format ("Move {0} from {1} to: ", manager.entries[index].key, index);
			}

			EditorGUILayout.Space ();

			GUI.color = Color.red;
			if (GUILayout.Button ("Delete Entry") && manager.entries.Count > 0)
			{
				EditorGUI.FocusTextInControl (null);

				if (EditorUtility.DisplayDialog ("Delete Entry?",
					"Are you sure you want to delete " + manager.entries[index].key + "?", "Yes", "No"))
				{
					manager.Remove (index);
					index = Mathf.Clamp (index, 0, manager.entries.Count - 1);
					Debug.Log ("Item deleted.");
					SetTriggerList ();

					SaveManager ();
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

			if (newIndex > manager.entries.Count)
				newIndex = manager.entries.Count;

			EditorGUILayout.Space ();

			if (GUILayout.Button ("Confirm", GUILayout.ExpandWidth (true)) && manager.entries.Count > 0)
			{
				EditorGUI.FocusTextInControl (null);

				if (newIndex >= 0 || newIndex <= manager.entries.Count)
				{

					if (newEntry)
						CreateNewEntry (true);

					if (moveEntry)
						manager.Move (index, newIndex);

				}

				index = newIndex;

				extraPanel = false;
				newEntry = false;
				moveEntry = false;

				SaveManager ();
			}

			EditorGUILayout.Space ();

			if (GUILayout.Button ("Cancel", GUILayout.ExpandWidth (true)) && manager.entries.Count > 0)
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

		private void DrawNagivationButtons ()
		{
			//Draws Mid panel with navigation Buttons
			EditorGUILayout.BeginHorizontal (GUILayout.ExpandWidth (true));
			if (GUILayout.Button ("First") && manager.entries.Count > 0)
			{
				EditorGUI.FocusTextInControl (null);
				index = 0;
			}

			if (GUILayout.Button ("Prev 10") && manager.entries.Count > 0)
			{
				EditorGUI.FocusTextInControl (null);
				index -= 10;
				if (index < 0)
					index = 0;
			}

			if (GUILayout.Button ("Prev") && manager.entries.Count > 0)
			{
				EditorGUI.FocusTextInControl (null);
				index--;
				if (index < 0)
					index = 0;
			}

			if (GUILayout.Button ("Next") && manager.entries.Count > 0)
			{
				EditorGUI.FocusTextInControl (null);
				index++;
				if (index >= manager.entries.Count)
					index = manager.entries.Count - 1;
			}

			if (GUILayout.Button ("Next 10") && manager.entries.Count > 0)
			{
				EditorGUI.FocusTextInControl (null);
				index += 10;
				if (index >= manager.entries.Count)
					index = manager.entries.Count - 1;
			}

			if (GUILayout.Button ("Last") && manager.entries.Count > 0)
			{
				EditorGUI.FocusTextInControl (null);
				index = manager.entries.Count - 1;
			}
			//Close Mid panel with navigation Buttons
			EditorGUILayout.EndHorizontal ();
		}

		private void DrawEntryPanel ()
		{
			//Draws Entry Box
			EditorGUILayout.BeginVertical ();
			if (manager.entries.Count > 0)
			{
				EditorGUILayout.LabelField (string.Format ("Translation {0} out of {1} found.", index + 1, manager.entries.Count));
				manager.entries[index].key = EditorGUILayout.TextField ("key:", manager.entries[index].key);

				for (int i = 0; i < manager.languages.Count; i++)
				{
					if ((!manager.entries[index].untranslatable && i > 0) || i == 0)
					{
						string display = string.Format ("{0}: ", manager.languages[i].name);

						if (!manager.entries[index].ContainsKey (manager.languages[i].name))
						{
							manager.entries[index].Add (manager.languages[i].name);
						}

						int translationIndex = manager.entries[index].GetIndex (manager.languages[i].name);
						manager.entries[index].Translations[translationIndex].translation = EditorGUILayout.TextField (display, manager.entries[index].Translations[translationIndex].translation);
					}
				}

				manager.entries[index].untranslatable = EditorGUILayout.Toggle ("Untranslatable", manager.entries[index].untranslatable);
			}
			else
			{
				EditorGUILayout.LabelField ("No Translation Found...");
			}
			//Close Entry Box
			EditorGUILayout.EndVertical ();
		}

		private void SetTriggerList ()
		{
			if (manager.entries.Count == 0)
				return;

			triggers = new List<bool> (manager.entries.Count);

			for (int i = 0; i < manager.entries.Count; i++)
			{
				triggers.Add (false);
			}

			triggers[index] = true;
		}

		private void CreateNewEntry (bool insertion = false)
		{
			EntryText entry = new EntryText
			{
				key = "new entry",
				Translations = new List<Translation> ()
			};

			foreach (Language language in manager.languages)
			{
				entry.Add (language.name);
			}

			if (insertion)
			{
				manager.Add (entry, newIndex);
				index = newIndex;
			}
			else
			{
				manager.Add (entry);
				index = manager.entries.Count - 1;
			}

			SetTriggerList ();
		}

		private void SaveManager ()
		{
			EditorUtility.SetDirty (manager);
			AssetDatabase.SaveAssets ();
			AssetDatabase.Refresh ();
		}
	}
}
