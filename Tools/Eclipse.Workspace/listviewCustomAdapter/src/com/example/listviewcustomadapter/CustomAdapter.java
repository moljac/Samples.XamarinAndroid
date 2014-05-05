package com.example.listviewcustomadapter;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

public class CustomAdapter extends ArrayAdapter<String> {
  private final Context context;
  private final String[] values;

  public CustomAdapter(Context context, String[] values) {
    super(context, R.layout.listview_item, values);
    this.context = context;
    this.values = values;
  }

  @Override
  public View getView(int position, View convertView, ViewGroup parent) {
    LayoutInflater inflater = (LayoutInflater) context
        .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
    View rowView = inflater.inflate(R.layout.listview_item, parent, false);
    TextView textView = (TextView) rowView.findViewById(R.id.secondLine);
    ImageView imageView = (ImageView) rowView.findViewById(R.id.icon);
    textView.setText(values[position]);
    // change the icon for Windows and iPhone
    String s = values[position];

    
    return rowView;
  }
} 