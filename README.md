Samples.XamarinAndroid
======================

Samples.XamarinAndroid


https://www.google.hr/url?sa=t&rct=j&q=&esrc=s&source=web&cd=7&cad=rja&uact=8&ved=0CEAQFjAG&url=http%3A%2F%2Fblog.ostebaronen.dk%2F2013%2F02%2Fusing-dialogs-in-mono-for-android.html&ei=TuzkU5y6FMG57AaH_ID4Dw&usg=AFQjCNHMSogNz5R72ULmnkdalOEqIQ8avg&bvm=bv.72676100,d.ZGU




## ListViews and Adapters

*	[https://github.com/android/platform_frameworks_base/tree/master/core/res/res/layout](https://github.com/android/platform_frameworks_base/tree/master/core/res/res/layout)
*	[http://arteksoftware.com/androids-built-in-list-items/](http://arteksoftware.com/androids-built-in-list-items/)
*	[http://androidapi.xamarin.com/?link=T:Android.Widget.ArrayAdapter](http://androidapi.xamarin.com/?link=T:Android.Widget.ArrayAdapter)
http://developer.xamarin.com/guides/android/user_interface/working_with_listviews_and_adapters/part_2_-_populating_a_listview_with_data/

*	in Xamarin.Android
	*	id: Android.Resource.Layout.SimpleListItem1
	*	simple_list_item_1.xml . 
	
	<TextView
	  xmlns:android="http://schemas.android.com/apk/res/android"
	  android:id="@android:id/text1"
	  android:layout_width="match_parent"
	  android:layout_height="wrap_content"
	  android:textappearance="?android:attr/textAppearanceListItemSmall"
	  android:gravity="center_vertical"
	  android:paddingstart="?android:attr/listPreferredItemPaddingStart"
	  android:paddingend="?android:attr/listPreferredItemPaddingEnd"
	  android:minheight="?android:attr/listPreferredItemHeightSmall" 
	  />
	  
1.	ListActivity with ArrayAdapter<String>		
		public class Main : ListActivity 
		{
			protected override void OnCreate(Bundle bundle)
			{
			   base.OnCreate(bundle);
			   var items = new string[] { "Vegetables","Fruits","Flower Buds","Legumes","Bulbs","Tubers" };
			   ListAdapter = new ArrayAdapter<String>
											(
											  this
											, Android.Resource.Layout.SimpleListItem1
											, items
											);
			}
			
			protected override void OnListItemClick(ListView l, View v, int position, long id)
			{
			   var t = items[position];
			   Android.Widget.Toast.MakeText(this, t, Android.Widget.ToastLength.Short).Show();
			}
		}
2.	ListActivity with ArrayAdapter<ComplexClass>	
3.	Activity with ListView with ArrayAdapter<ComplexClass>		

			<?xml version="1.0" encoding="utf-8"?>
			<LinearLayout xmlns:android="http://schemas.android.com/apk/res/android"
				android:orientation="vertical"
				android:layout_width="match_parent"
				android:layout_height="match_parent">
				<ListView
					android:id="@+id/instructorListView"
					android:layout_width="match_parent"
					android:layout_height="wrap_content" />
			</LinearLayout>	
			
			public class MainActivity : Activity
			{
				protected override void OnCreate(Bundle bundle)
				{
					base.OnCreate(bundle);

					SetContentView(Resource.Layout.Main);

					ListView instructorList = FindViewById<ListView>(Resource.Id.instructorListView);
					instructorList.ItemClick += OnItemClick;

					instructorList.Adapter = new ArrayAdapter<Instructor>(this, Android.Resource.Layout.SimpleListItem1, InstructorData.Instructors);
				}

				void OnItemClick(object sender, AdapterView.ItemClickEventArgs e)
				{
					Instructor instructor = InstructorData.Instructors[e.Position];

					AlertDialog.Builder dialog = new AlertDialog.Builder(this);
					dialog.SetMessage(instructor.Name);
					dialog.SetNeutralButton("OK", delegate { });
					dialog.Show();
				}
			}
3.	ListView with Adapter
		public class Main : Activity 
		{
			protected override void OnCreate(Bundle bundle)
			{
			   base.OnCreate(bundle);
			   var items = new string[] { "Vegetables","Fruits","Flower Buds","Legumes","Bulbs","Tubers" };
			   ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, items);
			}
			
			
		}
		
		
		
1, 2 often in your class Activity OnCreate call, and call the button object has already been initialized.
Method 1: use the delegate
button.Click += delegate {
button.Text = string.Format ("{0} clicks!", count++);
};
Method 2: use Lamda expressions
button.Click += (sender, e) =>{
    button.Text = string.Format ("{0} clicks!", count++);

};
Method 3: use the interface
Make your class that implements the Activity interface
public class MainActivity : Activity,View.IOnClickListener
public class MainActivity : Activity,View.IOnClickListener
{
      ....
        public void OnClick(View v)
       {
              button.Text = string.Format ("{0} clicks!", count++);

       }
}
Click event Listener settings in your Activity class OnCreate

button.SetOnClickListener(this);



		
3.	CustomAdapters
4.	Optimization FastScrolling
5.



























			filterlistView
				//.SetOnItemClickListener(new IOnItemClickListenerAnonymousInnerClassHelper(this));
				.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => 
				{					}
				};
			}
		}
		
		
		
		private class IOnItemClickListenerAnonymousInnerClassHelper : AdapterView.IOnItemClickListener
		{
			private readonly Sif_Example_ImageFilter outerInstance;

			public IOnItemClickListenerAnonymousInnerClassHelper(Sif_Example_ImageFilter outerInstance)
			{
				this.outerInstance = outerInstance;
			}

			public override void OnItemClick(AdapterView parent, View view, int position, long id)
			{

				if (outerInstance.bmBackgroundBitmap == null)
				{
					Toast.MakeText(outerInstance.mContext, "Input image does not selected!, select Image firtsly", ToastLength.Short).Show();
					return;
				}

				outerInstance.mImageOperationIndex = position;
				outerInstance.mImageOperation = outerInstance.imageOperationByIndex[outerInstance.mImageOperationIndex];

				if (outerInstance.mImageOperation == SifImageFilter.FILTER_ORIGINAL)
				{
					outerInstance.mBackgroudnImageView.SetImageBitmap(outerInstance.bmBackgroundBitmap);
				}
				else
				{
					outerInstance.bmFilteredBitmap = outerInstance.bmBackgroundBitmap.Copy(Bitmap.Config.Argb8888, true);

					if (outerInstance.mImageOperation != SifImageFilter.FILTER_ORIGINAL)
					{
						outerInstance.bmFilteredBitmap = SifImageFilter.FilterImageCopy(outerInstance.bmBackgroundBitmap, outerInstance.mImageOperation, outerInstance.mImageOperationLevel);
						if (outerInstance.bmFilteredBitmap != null)
						{
							outerInstance.mBackgroudnImageView.SetImageBitmap(outerInstance.bmFilteredBitmap);
						}
						else
						{
							Toast.MakeText(outerInstance.mContext, "Fail to apply image filter", ToastLength.Long).Show();
						}
					}

					if (outerInstance.bmFilteredBitmap != null)
					{
						outerInstance.bmFilteredBitmap = outerInstance.bmFilteredBitmap.Copy(Bitmap.Config.Argb8888, false);
						SifImageFilter.SetImageTransparency(outerInstance.bmFilteredBitmap, outerInstance.mTransparency);
						outerInstance.mBackgroudnImageView.SetImageBitmap(outerInstance.bmFilteredBitmap);
					}
					// reset the transparency
					outerInstance.mTransparency = 0;
				}
			}
		}
		*/
		