using UnityEngine;

namespace LocalisationAndTranslation
{
	[System.Serializable]
	public class Language
	{
		/// <summary> Name of Language. </summary>
		[SerializeField]
		public string name;
		/// <summary> Code of Language. </summary>
		[SerializeField]
		public string code;

		public Language () { }

		public Language (string name, string code)
		{
			this.name = name;
			this.code = code;
		}
	}
}
