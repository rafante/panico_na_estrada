using UnityEngine;
using UnityEditor;

namespace LocalisationAndTranslation
{
	public class CreateResourceAssets
	{
		private const string path = "Assets/Localisation And Translation/Resources/";

		public static LanguageManager CreateLanguageManager ()
		{
			LanguageManager languageManager = ScriptableObject.CreateInstance<LanguageManager> ();

			AssetDatabase.CreateAsset (languageManager, path + "LanguageManager.asset");
			AssetDatabase.SaveAssets ();

			languageManager.languages.Add (new Language ("English", "EN"));

			return languageManager;
		}

		public static LocalisationManager CreateLocalisationManager ()
		{
			LocalisationManager localisationManager = ScriptableObject.CreateInstance<LocalisationManager> ();

			AssetDatabase.CreateAsset (localisationManager, path + "LocalisationManager.asset");
			AssetDatabase.SaveAssets ();

			return localisationManager;
		}
	}
}


