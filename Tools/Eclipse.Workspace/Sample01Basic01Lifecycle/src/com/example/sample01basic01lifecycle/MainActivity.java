package com.example.sample01basic01lifecycle;


public class MainActivity extends 
		ActionBarActivity 
		//Activity
	{

	Button buttonNavigationSimple = null;
	Button buttonNavigationComplex = null;
	
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

                if (savedInstanceState == null) {
            getSupportFragmentManager().beginTransaction()
                    .add(R.id.container, new PlaceholderFragment())
                    .commit();
        }
        
    	buttonNavigationSimple = (Button) findViewById(R.id.buttonNavigationSimple);
    	buttonNavigationComplex = (Button) findViewById(R.id.buttonNavigationComplex);

    	buttonNavigationSimple.setOnClickListener(onClickListenerSimple);
    	buttonNavigationComplex.setOnClickListener(onClickListenerComplex);
    	
    	Log.e("LIFECYCLE = ", this.getClass().toString() + ".onCreate");
        return;
    }

	//=================================================================================
    // Navigation part
	//=================================================================================
    String Data;
    
    private void navigateComplex() {

    	Log.e("NAVIGATION = ", this.getClass().toString() + ".navigateComplex");
    	
    	
        Intent intent = new Intent(this, this.getClass());
        EditText editText = (EditText) findViewById(R.id.editTextDataToSend);
        String message = editText.getText().toString();
        intent.putExtra(Data, message);
        startActivity(intent);
   	
    	return;
    }
    
    
    private OnClickListener onClickListenerSimple = new OnClickListener() {
        @Override
        public void onClick(final View v) {
        	Log.e("NAVIGATION = ", this.getClass().toString() + ".onClick");
        	
            }
    }; 
    
    private OnClickListener onClickListenerComplex = new OnClickListener() {
        @Override
        public void onClick(final View v) {
        	Log.e("NAVIGATION = ", this.getClass().toString() + ".onClick");
        	
            }
    };

    //=================================================================================
    // LifeCycle part
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
            View rootView = inflater.inflate(R.layout.fragment_main, container, false);
            return rootView;
        }
    }

}
