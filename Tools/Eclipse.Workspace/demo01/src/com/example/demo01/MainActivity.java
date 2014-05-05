package com.example.demo01;

import android.support.v7.app.ActionBarActivity;
import android.support.v7.app.ActionBar;
import android.support.v4.app.Fragment;
import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.os.Build;

public class MainActivity extends 
	//ActionBarActivity // no ActionBar
	Activity
	{

	Button buttonNavigationSimple = null;
	Button buttonNavigationComplex = null;
	EditText editTextDataToSend = null;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		//-----------------------------------------------
		// loading UI (layouts)
		setContentView(R.layout.activity_main);
		//-----------------------------------------------

		/*
		 * not using fragments!!
		if (savedInstanceState == null) {
			getSupportFragmentManager().beginTransaction()
					.add(R.id.container, new PlaceholderFragment()).commit();
		}
		*/
		
		//-----------------------------------------------
		// getting Widget objects
		buttonNavigationSimple = (Button) findViewById(R.id.buttonNavigateSimple);
		buttonNavigationComplex = (Button) findViewById(R.id.buttonNavigateComplex);
		editTextDataToSend = (EditText) findViewById(R.id.editTextDataToSend);
		//-----------------------------------------------
		

		// Touch/Click Event programmaticaly 
		// not using attribute in xml layout
		buttonNavigationComplex.setOnClickListener(new View.OnClickListener() {
		    @Override
		    public void onClick(View v) {
		    	navigateComplex(v);
		    }
		});
		
    	Log.e("LIFECYCLE = ", this.getClass().toString() + ".onCreate");

    	return;
	}

	
    public void navigateSimple(View v)
    {
    	// data_class_level = "Class";
		//-----------------------------------------------
    	Intent intent = new Intent(this, HelperActivity.class);    	
    	this.startActivity(intent);
		//-----------------------------------------------
    	
    	return;
    }
    
    public static String data_class_level = "";
    
    public void navigateComplex(View v)
    {
    	String data = 
    			//"data sent from main activity"
    			this.editTextDataToSend.getText().toString().concat(" - Intent");
    			;
    		
		//-----------------------------------------------
    	Intent intent = new Intent(this, HelperActivity.class);
    	intent.putExtra("Data", data);
    	intent.putExtra("Broj", 1.0);
    	this.startActivity(intent);
		//-----------------------------------------------
    	
    	return;
    }

	
    @Override
    protected void onDestroy() {
    	super.onDestroy();
    	
    	Log.e("LIFECYCLE = ", this.getClass().toString() + ".onDestroy");
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
    
	
	
	
	
	
	
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {

		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		// Handle action bar item clicks here. The action bar will
		// automatically handle clicks on the Home/Up button, so long
		// as you specify a parent activity in AndroidManifest.xml.
		int id = item.getItemId();
		if (id == R.id.action_settings) {
			return true;
		}
		return super.onOptionsItemSelected(item);
	}

	/**
	 * A placeholder fragment containing a simple view.
	 */
	public static class PlaceholderFragment extends Fragment {

		public PlaceholderFragment() {
		}

		@Override
		public View onCreateView(LayoutInflater inflater, ViewGroup container,
				Bundle savedInstanceState) {
			View rootView = inflater.inflate(R.layout.fragment_main, container,
					false);
			return rootView;
		}
	}

}
