using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace LocalisationAndTranslation
{
	public class LocalisedObjectEditor
	{
		/// <summary> Path to Language Manager </summary>
		private const string path = "Assets/Localisation And Translation/Resources/";

		[MenuItem ("Tools/Localisation/Language Manager", false, 51)]
		public static void CreateLanguageManager ()
		{
			LanguageManager manager = (LanguageManager)AssetDatabase.LoadAssetAtPath (path + "LanguageManager.asset", typeof (LanguageManager));
			if (manager == null)
			{
				manager = CreateResourceAssets.CreateLanguageManager ();
			}
			else
			{
				Debug.LogWarning ("LocalisationManager.asset already exists");
			}
		}

		[MenuItem ("Tools/Localisation/Localisation Manager", false, 52)]
		public static void CreateLocalisationManager ()
		{
			LocalisationManager manager = (LocalisationManager)AssetDatabase.LoadAssetAtPath (path + "LocalisationManager.asset", typeof (LocalisationManager));
			if (manager == null)
			{
				manager = CreateResourceAssets.CreateLocalisationManager ();
			}
			else
			{
				Debug.LogWarning ("LocalisationManager.asset already exists");
			}
		}

		[MenuItem ("Tools/Localisation/GUI Translator", false, 101)]
		public static void CreateGUITranslator ()
		{
			GUITranslator translator = new GameObject ("Gui Translator").AddComponent<GUITranslator> ();
			Selection.activeGameObject = translator.gameObject;

			Undo.RegisterCreatedObjectUndo (translator.gameObject, "Added GUITranslator to Scene");
		}

		[MenuItem ("Tools/Localisation/Localised Text", false, 151)]
		public static void AddLocalisedText ()
		{
			LocalisedText localisedObject = new GameObject ("Translatable Text").AddComponent<LocalisedText> ();
			Selection.activeGameObject = localisedObject.gameObject;

			AddToCanvas (localisedObject.transform);

			localisedObject.gameObject.AddComponent<Text> ();
			Undo.RegisterCreatedObjectUndo (localisedObject.gameObject, "Created a translatable Text");
		}

		[MenuItem ("Tools/Localisation/Localised Image", false, 152)]
		public static void AddLocalisedImage ()
		{
			LocalisedImage localisedObject = new GameObject ("Localised Image").AddComponent<LocalisedImage> ();
			Selection.activeGameObject = localisedObject.gameObject;

			AddToCanvas (localisedObject.transform);

			localisedObject.gameObject.AddComponent<Image> ();
			Undo.RegisterCreatedObjectUndo (localisedObject.gameObject, "Created a localised image.");
		}

		[MenuItem ("Tools/Localisation/Localised AudioSource", false, 153)]
		public static void AddLocalisedSound ()
		{
			LocalisedAudio localisedObject = new GameObject ("Localised AudioSource").AddComponent<LocalisedAudio> ();
			Selection.activeGameObject = localisedObject.gameObject;

			AddToCanvas (localisedObject.transform);

			localisedObject.gameObject.AddComponent<AudioSource> ();
			Undo.RegisterCreatedObjectUndo (localisedObject.gameObject, "Created a localised audio source.");
		}

		private static void AddToCanvas (Transform LocalisedObjectTransform)
		{
			Canvas canvas = Object.FindObjectOfType<Canvas> ();

			if (canvas == null)
			{
				canvas = new GameObject ("UI Canvas").AddComponent<Canvas> ();
				canvas.gameObject.AddComponent<CanvasScaler> ();
				canvas.gameObject.AddComponent<GraphicRaycaster> ();
			}

			LocalisedObjectTransform.SetParent (canvas.transform, false);
		}
	}
}
