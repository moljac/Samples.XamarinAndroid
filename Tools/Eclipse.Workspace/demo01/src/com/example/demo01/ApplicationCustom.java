package com.example.demo01;

import android.app.Application;
import android.content.res.Configuration;
import android.util.Log;

public class ApplicationCustom extends Application {

    @Override
    public void onCreate() {
    	super.onCreate();
    	
    	Log.e("LIFECYCLE = ", this.getClass().toString() + ".onCreate");
    	return;
    }
 
    
    @Override
    public void onTerminate() {
    	// TODO Auto-generated method stub
    	super.onTerminate();
    	
    	Log.e("LIFECYCLE = ", this.getClass().toString() + ".onTerminate");
    	return;
    }
    
    @Override
    public void onConfigurationChanged(Configuration newConfig) {
    	// TODO Auto-generated method stub
    	super.onConfigurationChanged(newConfig);
    	
    	Log.e("LIFECYCLE = ", this.getClass().toString() + ".onConfigurationChanged");
    	return;
    }
    
    @Override
    public void onLowMemory() {
    	// TODO Auto-generated method stub
    	super.onLowMemory();
    }
}
