package com.example.listviewcustomadapter;

import java.util.ArrayList;

import android.support.v7.app.ActionBarActivity;
import android.support.v7.app.ActionBar;
import android.support.v4.app.Fragment;
import android.app.Activity;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ListView;
import android.os.Build;

public class MainActivity extends 
			//ActionBarActivity 
			Activity
{

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);

		/*
		 * not using fragments
		 *
		if (savedInstanceState == null) {
			getSupportFragmentManager().beginTransaction()
					.add(R.id.container, new PlaceholderFragment()).commit();
		}
		*/
		
	    String[] values = new String[] { "SPA", "IAMU", "MAUI", "APS" };

	    final ListView listview = (ListView) findViewById(R.id.listview);
	    
	    final ArrayList<String> list = new ArrayList<String>();
	    for (int i = 0; i < values.length; ++i) {
	      list.add(values[i]);
	    }

	    final CustomAdapter adapter = 
	    		new CustomAdapter
	    			(
	    					this
	    			//, android.R.layout.simple_list_item_1
	    			, values
	    			);
	        listview.setAdapter(adapter);
	
	        listview.setOnItemClickListener(new AdapterView.OnItemClickListener() {

	            @Override
	            public void onItemClick(AdapterView<?> parent, final View view,
	                int position, long id) {
	              final String item = (String) parent.getItemAtPosition(position);
	              view
	              	.animate()	// API level 12
	              	.setDuration(2000).alpha(0)
	                  .withEndAction // API leve 16
	                  (new Runnable() {
	                    @Override
	                    public void run() {
	                      list.remove(item);
	                      adapter.notifyDataSetChanged();
	                      view.setAlpha(1);
	                    }
	                  });
	            }

	          });
	
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
