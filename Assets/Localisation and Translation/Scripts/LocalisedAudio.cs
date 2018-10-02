using UnityEngine;
using LocalisationAndTranslation;

[RequireComponent (typeof (AudioSource))]
[AddComponentMenu ("UI/Localised AudioSource")]
public class LocalisedAudio : MonoBehaviour, ILocalisedObject
{
	/// <summary> Entry key Identifiable. </summary>
	[Tooltip ("Key of AudioSource to be localised. Must be unique.")]
	[SerializeField]
	private string key;

	private EntryType type = EntryType.Audio;

	void Start ()
	{
		GUITranslator.Instance.localisedObjectsInScene.Add (this);
	}

	/// <summary>
	/// Sets clip of AudioSource Component
	/// </summary>
	/// <param name="sprite"></param>
	public void Set (AudioClip clip)
	{
		AudioSource audioSource = GetComponent<AudioSource> ();

		if (audioSource == null)
		{
			Debug.LogWarningFormat ("LocalisedObject '{0}' has no AudioSource component associated.", gameObject.name);
			return;
		}

		audioSource.clip = clip;
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
