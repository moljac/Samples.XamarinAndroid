package com.example.demo01;

import android.app.Activity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

public class HelperActivity extends Activity{

	   @Override
	    protected void onCreate(Bundle savedInstanceState) {

			//-----------------------------------------------
		   	super.onCreate(savedInstanceState);
			//-----------------------------------------------

		   	//-----------------------------------------------
			// loading UI (layouts)
	        setContentView(R.layout.activity_helper);
			//-----------------------------------------------
	        
			//-----------------------------------------------
			// getting Widget objects
	        Button buttonBack = (Button) findViewById(R.id.buttonBack);
	        TextView textViewDataFromClass = (TextView) findViewById(R.id.textViewDataFromClass);
	        TextView textViewDataFromIntent = (TextView) findViewById(R.id.textViewDataFromIntent);
			//-----------------------------------------------

	        //-----------------------------------------------
	        // extracting data from intent
	        String message1 = this.getIntent().getStringExtra("Data");
			//-----------------------------------------------
	        
			//-----------------------------------------------
	        // extracting data from class
	        String message2 = MainActivity.data_class_level;
			//-----------------------------------------------

			//-----------------------------------------------
			// working with Widget objects
	        if (message1 != null)
	        {
	        textViewDataFromClass.setText(message1);
	        }
	        textViewDataFromIntent.setText(message2);
			//-----------------------------------------------
	        
	    	Log.e("LIFECYCLE = ", this.getClass().toString() + ".onCreate");
	    	return;
	    }

	    
	   public void back(View v)
	    {
	    	this.finish();
	    }
}
