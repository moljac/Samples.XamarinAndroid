﻿// Copyright 2015 Google Inc. All Rights Reserved.
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

namespace XamarinAnroid.Support.CustomTabs.ChromiumUtilities
{

	using CustomTabsClient = Android.Support.CustomTabs.CustomTabsClient;

	/// <summary>
	/// Callback for events when connecting and disconnecting from Custom Tabs Service.
	/// </summary>
	public interface IServiceConnectionCallback
	{
		/// <summary>
		/// Called when the service is connected. </summary>
		/// <param name="client"> a CustomTabsClient </param>
		void OnServiceConnected(CustomTabsClient client);

		/// <summary>
		/// Called when the service is disconnected.
		/// </summary>
		void OnServiceDisconnected();
	}

}