﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace com.xamarin.example.actionbar.tabs
{
    public class FragmentSettings : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here

            return;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.fragment_settings, container, false);

            return view;

            //return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }

}
