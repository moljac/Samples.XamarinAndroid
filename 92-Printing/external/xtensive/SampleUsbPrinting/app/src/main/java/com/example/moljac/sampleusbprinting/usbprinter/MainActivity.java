package com.example.moljac.sampleusbprinting;

import com.example.moljac.sampleusbprinting.PrintOrder;

import android.app.ActionBar;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.os.Bundle;
import android.util.Log;
import android.widget.Toast;

public class MainActivity extends Activity{
	//private ConfigAdapter datasource;
	
	protected void onCreate(Bundle icicle) {
		 super.onCreate(icicle);
	     setContentView(R.layout.activity_main);
	     //String msg = 'This is a test message';
	     PrintOrder printer = new PrintOrder();
	     printer.Print(this,"ddsasdsadasdasdsa");
	}
}
