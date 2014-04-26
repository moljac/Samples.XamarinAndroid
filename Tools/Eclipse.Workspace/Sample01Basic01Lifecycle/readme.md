# Android LifeCycle sample

## References

*	[http://developer.android.com/training/basics/activity-lifecycle/index.html]
(http://developer.android.com/training/basics/activity-lifecycle/index.html)


## Activity 

*	Activity
	*	States
		*	Created
		*	Started (running)
		*	Resumed	(running)
		*	Paused (partially Visible)
		*	Stopped (hidden)
			activity is partially obscured by another activity—the other activity 		
			that's in the foreground is semi-transparent or doesn't cover the entire 	
			screen. The paused activity does not receive user input and cannot execute 	
			any code.	
		*	Destroyed	
	*	Events
		*	onCreate 		
		*	onDestroy
		*	onStart 		
		*	onStop 		
		*	onPause 
		*	onResume 		


Code


	//=================================================================================
	// Error is just to distinguish it in LogCat 8)

    @Override
    protected void onDestroy() {
        super.onDestroy();
        
        Log.e("LIFECYCLE = ", this.getClass().toString() + ".onDestroyed");
        return;
    }
    
    @Override
    protected void onStart() {
        super.onStart();
        
        Log.e("LIFECYCLE = ", this.getClass().toString() + ".onStart");
        return;
    }
    
    @Override
    protected void onStop() {
        super.onStop();
        
        Log.e("LIFECYCLE = ", this.getClass().toString() + ".onStop");
        return;
    }

    
    @Override
    protected void onPause() {
        super.onPause();
        
        Log.e("LIFECYCLE = ", this.getClass().toString() + ".onPause");
        return;
    }
    
    @Override
    protected void onResume() {
        super.onResume();
        
        Log.e("LIFECYCLE = ", this.getClass().toString() + ".onResume");
        return;
    }
	//=================================================================================


## Application

*	to maintain global application state.
*	AndroidManifest.xml <application/>

ApplicationCutom.java

	public class ApplicationCutom extends Application{

	    private static Context context;

	    public void onCreate(){
	        super.onCreate();
	        MyApplication.context = getApplicationContext();
	    }

	    public static Context getAppContext() {
	        return MyApplication.context;
	    }
	}


AndroidManifest.xml


    <application
        android:allowBackup="true"
        android:icon="@drawable/ic_launcher"
        android:label="@string/app_name"
        android:theme="@style/AppTheme" 
        android:name="com.example.sample01basic01lifecycle.ApplicationCustom" <!-- Add  -->
        >
        <activity
            android:name="com.example.sample01basic01lifecycle.MainActivity"
            android:label="@string/app_name" >
            <intent-filter>
                <action android:name="android.intent.action.MAIN" />

                <category android:name="android.intent.category.LAUNCHER" />
            </intent-filter>
        </activity>
    </application>

