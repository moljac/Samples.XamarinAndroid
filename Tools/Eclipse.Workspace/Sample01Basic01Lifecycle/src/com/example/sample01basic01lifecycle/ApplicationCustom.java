package com.example.sample01basic01lifecycle;

import android.app.*;
import android.content.*;
import android.content.res.Configuration;
import android.os.Bundle;
import android.util.Log;


public class ApplicationCustom extends Application{

	//=================================================================================
	// Error is just to distinguish it in LogCat 8)

    @Override
    public void onCreate() {
        super.onCreate();

        ApplicationCustom.context = getApplicationContext();
        
        Log.e("LIFECYCLE = ", this.getClass().toString() + ".onCreate");
        return;
    }
    
    @Override
    public void onTerminate() {
        super.onTerminate();

        
        Log.e("LIFECYCLE = ", this.getClass().toString() + ".onTerminate");
        return;
    }
    
    
    @Override
    public void onConfigurationChanged(Configuration newConfig) {
        super.onTerminate();

        
        Log.e("LIFECYCLE = ", this.getClass().toString() + ".onConfigurationChanged");
        return;
    }
    
    @Override
    public void onLowMemory() {
        super.onLowMemory();

        
        Log.e("LIFECYCLE = ", this.getClass().toString() + ".onLowMemory");
        return;
    }
	//=================================================================================
	
	
	
	
	
    private static Context context;

    public static Context getAppContext() {
        return ApplicationCustom.context;
    }
}
