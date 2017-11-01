using Android.App;
using Android.OS;
using Android.Util;

namespace com.xamarin.example.actionbar.tabs
{
    //[Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/ic_launcher")]
    [Activity(Label = "ActionBar Tabs Example", MainLauncher = true)]
    public class MainActivity : Activity
    {
        static readonly string Tag = "ActionBarTabsSupport";

        Fragment[] _fragments;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //enable navigation mode to support tab layout
            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            //adding Settings tab
            AddTab
                (
                    "Settings", 
                    -1, //Resource.Drawable.ic_tab_sessions, 
                    new FragmentSettings()
                );

            //adding Providers tab 
            AddTab
                (
                    "OAuth Providers", 
                    -1, //Resource.Drawable.ic_tab_speakers, 
                    new FragmentOAuthProviders()
                );

            //adding Providers tab 
            AddTab
                (
                    "OAuth Cases", 
                    -1, //Resource.Drawable.ic_tab_whats_on, 
                    new FragmentOAuthCases()
                );

            return;
        }

        /*
         * This method is used to create and add dynamic tab view
         * @Param,
         *  tabText: title to be displayed in tab
         *  iconResourceId: image/resource id
         *  fragment: fragment reference
         * 
        */
        void AddTab (string tabText, int iconResourceId, Fragment fragment)
        {
            var tab = this.ActionBar.NewTab ();            
            tab.SetText (tabText);

            if (iconResourceId > 0)
            {
                tab.SetIcon(iconResourceId);
            }

            fragment_selected = fragment;

            // must set event handler for replacing tabs tab
            tab.TabSelected += 
                TabOnTabSelected
                //delegate(object sender, ActionBar.TabEventArgs e) 
                //{
                //    e.FragmentTransaction.Replace(Resource.Id.fragmentContainer, fragment);
                //}
                ;

            this.ActionBar.AddTab (tab);
        }

        Fragment fragment_selected = null;

        void AddTabToActionBar(int labelResourceId, int iconResourceId)
        {
            ActionBar.Tab tab = ActionBar.NewTab()
                                         .SetText(labelResourceId)
                                         .SetIcon(iconResourceId);
            tab.TabSelected += TabOnTabSelected;
            ActionBar.AddTab(tab);

            return;
        }

        void TabOnTabSelected(object sender, ActionBar.TabEventArgs tabEventArgs)
        {
            ActionBar.Tab tab = (ActionBar.Tab)sender;

            Log.Debug(Tag, "The tab {0} has been selected.", tab.Text);
            //Fragment frag = _fragments[tab.Position];
            tabEventArgs
                .FragmentTransaction
                    .Replace
                        (
                            Resource.Id.fragmentContainer, 
                            fragment_selected
                        );

            return;
        }
    }
}
