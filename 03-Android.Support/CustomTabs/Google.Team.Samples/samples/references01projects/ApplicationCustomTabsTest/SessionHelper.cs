// Copyright 2016 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace ApplicationCustomTabsTest
{

	//using Nullable = Android.Support.Annotation.Nullable;
	using CustomTabsSession = Android.Support.CustomTabs.CustomTabsSession;

	/// <summary>
	/// A class that keeps tracks of the current <seealso cref="CustomTabsSession"/> and helps other components of
	/// the app to get access to the current session.
	/// </summary>
	public class SessionHelper
	{
		private static System.WeakReference<CustomTabsSession> sCurrentSession;

		/// <returns> The current <seealso cref="CustomTabsSession"/> object. </returns>
		public static /*mc++ @Nullable*/ CustomTabsSession GetCurrentSession()
		{
            CustomTabsSession cts = null;
            sCurrentSession.TryGetTarget(out cts);

            return sCurrentSession == null ? null : cts;
		}

		/// <summary>
		/// Sets the current session to the given one. </summary>
		/// <param name="session"> The current session. </param>
		public static void SetCurrentSession(CustomTabsSession session)
		{
			sCurrentSession = new System.WeakReference<CustomTabsSession>(session);
		}

        public static CustomTabsSession CustomTabsSession
        {
            get 
            {
                CustomTabsSession cts = null;
                sCurrentSession.TryGetTarget(out cts);
                return cts; 
            }
            set
            {
                sCurrentSession.SetTarget(value);

                return;
            }
        }
    }

}