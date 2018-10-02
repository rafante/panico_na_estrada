using UnityEngine;
using UnityEngine.UI;
using LocalisationAndTranslation;

[RequireComponent (typeof (Image))]
[AddComponentMenu ("UI/Localised Image")]
public class LocalisedImage : MonoBehaviour, ILocalisedObject
{
	/// <summary> Entry key Identifiable. </summary>
	[Tooltip ("Key of image to be localised. Must be unique.")]
	[SerializeField]
	private string key;

	private EntryType type = EntryType.Image;

	void Start ()
	{
		GUITranslator.Instance.localisedObjectsInScene.Add (this);
	}

	/// <summary>
	/// Sets sprite of Image Component
	/// </summary>
	/// <param name="sprite"></param>
	public void Set (Sprite sprite)
	{
		Image image = GetComponent<Image> ();

		if (image == null)
		{
			Debug.LogWarningFormat ("LocalisedObject '{0}' has no image component associated.", gameObject.name);
			return;
		}

		image.sprite = sprite;
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
