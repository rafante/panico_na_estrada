using UnityEngine;
using UnityEngine.UI;
using LocalisationAndTranslation;

[RequireComponent (typeof (Text))]
[AddComponentMenu ("UI/Text Translatable")]
public class LocalisedText : MonoBehaviour, ILocalisedObject
{
	/// <summary> Entry key Identifiable. </summary>
	[Tooltip ("Key of text to be translated. Must be unique.")]
	[SerializeField]
	private string key;

	private EntryType type = EntryType.Text;

	void Start ()
	{
		GUITranslator.Instance.localisedObjectsInScene.Add (this);	
	}

	/// <summary>
	/// Sets content for Text Component
	/// </summary>
	/// <param name="content"></param>
	public void Set (string content)
	{
		Text text = gameObject.GetComponent<Text> ();

		if (text == null)
		{
			Debug.LogWarningFormat ("LocalisedObject '{0}' has no text component associated.", gameObject.name);
			return;
		}

		text.text = content;
	}

	public string Key
	{
		get
		{
			return key;
		}

		set
		{
			key = value;
		}
	}

	public EntryType Type
	{
		get
		{
			return type;
		}
	}
}
