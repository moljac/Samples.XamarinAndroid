
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kiosk.Sample.XamarinAndroid
{
	public partial class SharedPrefferencesUtilities
	{
		private static readonly string PREF_KIOSK_MODE = "pref_kiosk_mode";


	    public static bool IsKioskModeActive(Android.Content.Context context) 
	    {
	        Android.Content.ISharedPreferences sp = null;
	        sp = Android.Preferences.PreferenceManager.GetDefaultSharedPreferences(context);

	        return sp.GetBoolean(PREF_KIOSK_MODE, false);
	    }

	    public static void SetKioskModeActive(bool active, Android.Content.Context context)
	     {
	        Android.Content.ISharedPreferences sp = null;
	        sp = Android.Preferences.PreferenceManager.GetDefaultSharedPreferences(context);

	        sp.Edit().PutBoolean(PREF_KIOSK_MODE, active).Commit();

	        return;
		}
	}
}