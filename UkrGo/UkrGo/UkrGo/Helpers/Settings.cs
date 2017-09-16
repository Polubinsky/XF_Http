// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace UkrGo.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		private const string PinCodeKey = "pincode_key";
		

		#endregion


		public static string PinCode
        {
			get
			{
				return AppSettings.GetValueOrDefault(PinCodeKey, string.Empty);
			}
			set
			{
				AppSettings.AddOrUpdateValue(PinCodeKey, value);
			}
		}

	}
}