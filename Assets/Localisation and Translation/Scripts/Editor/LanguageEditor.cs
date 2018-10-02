using UnityEngine;
using UnityEditor;

namespace LocalisationAndTranslation
{
	public class LanguageEditor : EditorWindow
	{
		/// <summary> Minimum Window width </summary>
		private const float WINDOW_MIN_WIDTH = 350f;
		/// <summary> Minimum Window height </summary>
		private const float WINDOW_MIN_HEIGHT = 400f;
		/// <summary> Path to Language Manager </summary>
		private const string path = "Assets/Localisation And Translation/Resources/LanguageManager.asset";

		private int index;
		private string extraPanelDisplay;
		private string currentLanguage;
		private string newName;
		private string newCode;
		private bool newLanguageOption;
		private bool modifyLanguageOption;
		private bool newLanguagePanel;

		Vector2 scrollPos;

		LanguageManager manager;

		[MenuItem ("Tools/Localisation/Language Editor", false, 1)]
		public static void GetWindow ()
		{
			LanguageEditor editor = GetWindow<LanguageEditor> ("Languages", true);
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
		}

		private void OnGUI ()
		{
			if (manager != null && manager.languages.Count > 0)
			{
				EditorGUILayout.Space ();

				GUI.color = Color.green;
				if (GUILayout.Button ("New Language"))
				{
					extraPanelDisplay = "Create new language";

					newLanguagePanel = !newLanguagePanel;
					newLanguageOption = !newLanguageOption;
				}
				GUI.color = Color.white;

				if (newLanguagePanel)
					DisplayNewLanguagePanel ();
				
				EditorGUILayout.Space ();

				DisplayMainLanguage ();

				EditorGUILayout.Space ();

				scrollPos = EditorGUILayout.BeginScrollView (scrollPos, GUILayout.Width (0), GUILayout.Height (WINDOW_MIN_HEIGHT - 20));

				for (int i = 1; i < manager.languages.Count; i++)
				{
					DisplayOptionalLanguage (i);
				}

				EditorGUILayout.EndScrollView ();

				EditorGUILayout.Space ();
			}
		}

		private void DisplayNewLanguagePanel ()
		{
			EditorGUILayout.LabelField (extraPanelDisplay);

			EditorGUILayout.BeginVertical ();

			newName = EditorGUILayout.TextField ("Name: ", newName);
			newCode = EditorGUILayout.TextField ("Code: ", newCode);

			EditorGUILayout.EndVertical ();

			EditorGUILayout.BeginHorizontal ();

			EditorGUILayout.Space ();

			if (GUILayout.Button ("Confirm", GUILayout.Width (100f)))
			{
				if (newLanguageOption)
					CreateNewLanguage ();

				if (modifyLanguageOption)
					ModifyLanguage ();

				ResetData ();

				SaveManager ();
			}

			EditorGUILayout.Space ();

			if (GUILayout.Button ("Cancel", GUILayout.Width (100f)))
			{
				ResetData ();
			}

			EditorGUILayout.Space ();

			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.Space ();
		}

		private void DisplayMainLanguage ()
		{
			EditorGUILayout.BeginHorizontal ("Box");

			EditorGUILayout.BeginVertical ();
			EditorGUILayout.LabelField (string.Format ("Main Language: {0}", manager.languages[0].name));
			EditorGUIUtility.labelWidth = 40f;
			EditorGUILayout.LabelField ("Code: ", manager.languages[0].code);
			EditorGUILayout.EndVertical ();

			if (GUILayout.Button ("Modify", GUILayout.MaxWidth (50f)))
			{
				currentLanguage = manager.languages[0].name;
				extraPanelDisplay = "Modify language: " + currentLanguage;

				index = 0;

				newLanguagePanel = !newLanguagePanel;
				modifyLanguageOption = !modifyLanguageOption;
			}

			EditorGUILayout.EndHorizontal ();
		}

		private void DisplayOptionalLanguage (int i)
		{
			EditorGUILayout.BeginHorizontal ("Box");

			EditorGUILayout.BeginVertical ();
			EditorGUILayout.LabelField (string.Format ("Optional Language: {0}", manager.languages[i].name));
			EditorGUIUtility.labelWidth = 40f;
			EditorGUILayout.LabelField ("Code: ", manager.languages[i].code);
			EditorGUILayout.EndVertical ();

			EditorGUILayout.Space ();

			if (GUILayout.Button ("Modify", GUILayout.MaxWidth (50f)))
			{
				currentLanguage = manager.languages[i].name;
				extraPanelDisplay = "Modify language: " + currentLanguage;

				index = i;

				newLanguagePanel = !newLanguagePanel;
				modifyLanguageOption = !modifyLanguageOption;
			}

			GUI.color = Color.red;
			if (GUILayout.Button ("Delete", GUILayout.MaxWidth (50f)))
			{
				if (EditorUtility.DisplayDialog ("Delete Language?",
					   "Are you sure you want to delete " + manager.languages[i].name + "?", "Yes", "No"))
				{

					foreach (EntryText entry in manager.entries)
					{
						entry.Remove (manager.languages[i].name);
					}

					manager.languages.RemoveAt (i);

					Debug.Log ("Language deleted.");

					SaveManager ();
				}
			}
			GUI.color = Color.white;

			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.Space ();
		}

		private void CreateNewLanguage ()
		{
			if (newName == null || newName == "")
			{
				Debug.LogWarning ("Please, write a name for new language.");
				return;
			}

			if (manager.ContainsLanguage (newName, newCode))
			{
				Debug.LogWarningFormat ("Language already exists in library: {0}", newName);
				return;
			}

			Language language = new Language
			{
				name = newName,
				code = newCode
			};

			manager.languages.Add (language);

			if (manager.entries.Count > 0)
			{
				foreach (EntryText entry in manager.entries)
				{
					entry.Add (newName);
				}
			}

			Debug.Log ("New Language created.");
		}

		private void ModifyLanguage ()
		{
			if (newName == null || newName == "")
			{
				Debug.LogWarning ("Please, write a name for new language.");
				return;
			}

			if (manager.ContainsKey (currentLanguage))
			{
				Debug.LogWarningFormat ("Language already exist in library: {0}", currentLanguage);
				return;
			}

			manager.languages[index].name = newName;
			manager.languages[index].code = newCode;

			if (manager.entries.Count > 0)
			{
				foreach (EntryText entry in manager.entries)
				{
					entry.Remove (manager.languages[index].name);
					entry.Add (newName);
				}
			}

			Debug.LogFormat ("Language modified to {0}.", newName);
		}

		private void ResetData ()
		{
			newName = "";
			newCode = "";

			newLanguageOption = false;
			modifyLanguageOption = false;
			newLanguagePanel = false;
		}

		private void SaveManager ()
		{
			EditorUtility.SetDirty (manager);
			AssetDatabase.SaveAssets ();
			AssetDatabase.Refresh ();
		}
	}
}
