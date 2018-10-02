using UnityEngine;
using LocalisationAndTranslation;

public interface ILocalisedObject
{
	string Key { get; set; }

	EntryType Type { get; }

	GameObject gameObject { get; }

}

