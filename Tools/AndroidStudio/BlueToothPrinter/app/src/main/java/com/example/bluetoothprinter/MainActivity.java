package com.example.bluetoothprinter;

import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.OutputStream;
import java.io.UnsupportedEncodingException;
import java.util.HashMap;
import java.util.Set;

import com.example.bluetoothprinter.BlueToothService.OnReceiveDataHandleEvent;
import com.google.zxing.BarcodeFormat;
import com.google.zxing.EncodeHintType;
import com.google.zxing.MultiFormatWriter;
import com.google.zxing.WriterException;
import com.google.zxing.common.BitMatrix;
import android.net.Uri;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.provider.MediaStore;
import android.app.Activity;
import android.app.ProgressDialog;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.content.DialogInterface;
import android.content.Intent;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.graphics.Bitmap.CompressFormat;
import android.graphics.BitmapFactory;
import android.graphics.Matrix;
import android.util.Log;
import android.view.Menu;
import android.view.View;
import android.view.ViewGroup;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

//zkc.bluetooth.api

public class MainActivity extends Activity {

	private BlueToothService mBTService = null;
	private String tag = "MainActivity";
	private static final int REQUEST_EX = 1;
	public static final int MESSAGE_STATE_CHANGE = 1;
	public static final int MESSAGE_READ = 2;
	public static final int MESSAGE_WRITE = 3;
	public static final int MESSAGE_DEVICE_NAME = 4;
	public static final int MESSAGE_TOAST = 5;
	private Button checkButton;
	private Button controlButton;
	private Button bt_matches;// 配对蓝牙
	private ListView deviceList;// 设备列表
	private ArrayAdapter<String> mPairedDevicesArrayAdapter = null;// 已配对
	private ArrayAdapter<String> mNewDevicesArrayAdapter = null;// 新搜索列表
	private BluetoothAdapter mBluetoothAdapter = null;
	private Set<BluetoothDevice> devices;
	private Button bt_scan;// 扫描设备
	public Handler handler = null;
	public Handler mhandler;
	private ProgressDialog progressDialog = null;
	private EditText edit;
	private ViewGroup vg;
	private LinearLayout layout;
	private LinearLayout layoutscan;
	private Button bt_print;// 文字打印
	private Button bt_image;// 图片打印
	private Button bt_order;// 指令打印
	private Button bt_openpic;// 打开图片目录
	private ImageView iv;// 显示的图片
	private Button bt_2d;// 生成二维码
	private Button bt_bar;// 生成条形码
	private String picPath = "";// 打开图片保存的路径
	private Bitmap btMap = null;// 缓存图片
	private TextView tv_status;
	private Button bt_disconnect;
	private Thread tv_update;
	private boolean tvFlag = true;
	private Thread bt_update = null;
	private boolean updateflag = true;
	private Button nbt_img;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
		layout = (LinearLayout) View.inflate(MainActivity.this,
				R.layout.edittext, null);
		iv = (ImageView) findViewById(R.id.iv_test);
		deviceList = (ListView) findViewById(R.id.lv_device);
		vg = (ViewGroup) deviceList.getParent();
		edit = (EditText) layout.findViewById(R.id.et_input);
		edit.setFocusable(false);
		layout.removeAllViews();
		vg.addView(edit, new LinearLayout.LayoutParams(
				LinearLayout.LayoutParams.MATCH_PARENT,
				LinearLayout.LayoutParams.MATCH_PARENT));

		bt_print = (Button) findViewById(R.id.bt_print);
		layoutscan = (LinearLayout) findViewById(R.id.layoutscan);
		layoutscan.setVisibility(View.GONE);

		bt_order = (Button) findViewById(R.id.bt_order);
		bt_order.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				if (mBTService.getState() != mBTService.STATE_CONNECTED) {
					Toast.makeText(
							MainActivity.this,
							MainActivity.this.getResources().getString(
									R.string.str_unconnected), 2000).show();
					return;
				}
				byte[] send = new byte[10];
				mBTService.SendOrder(send);
			}
		});
		bt_openpic = (Button) findViewById(R.id.bt_openpci);
		bt_openpic.setOnClickListener(new View.OnClickListener() {
			public void onClick(View v) {
				Intent intent = new Intent(
						Intent.ACTION_PICK,
						android.provider.MediaStore.Images.Media.EXTERNAL_CONTENT_URI);
				startActivityForResult(intent, REQUEST_EX);
			}
		});

		mhandler = new Handler() {
			public void handleMessage(Message msg) {
				super.handleMessage(msg);
				switch (msg.what) {
				case MESSAGE_STATE_CHANGE:// 蓝牙连接状态
					switch (msg.arg1) {
					case BlueToothService.STATE_CONNECTED:// 已经连接
						break;
					case BlueToothService.STATE_CONNECTING:// 正在连接
						break;
					case BlueToothService.STATE_LISTEN:
					case BlueToothService.STATE_NONE:
						break;
					case BlueToothService.SUCCESS_CONNECT:
						Toast.makeText(
								MainActivity.this,
								MainActivity.this.getResources().getString(
										R.string.str_succonnect), 2000).show();
						vg.getChildAt(0).setVisibility(View.GONE);
						vg.getChildAt(1).setVisibility(View.GONE);
						vg.getChildAt(2).setVisibility(View.VISIBLE);
						vg.getChildAt(2).setFocusable(true);
						vg.getChildAt(2).setFocusableInTouchMode(true);

						break;
					case BlueToothService.FAILED_CONNECT:
						Toast.makeText(
								MainActivity.this,
								MainActivity.this.getResources().getString(
										R.string.str_faileconnect), 2000)
								.show();
						vg.getChildAt(0).setVisibility(View.VISIBLE);
						vg.getChildAt(1).setVisibility(View.VISIBLE);
						vg.getChildAt(2).setVisibility(View.GONE);
						vg.getChildAt(2).setFocusable(false);
						break;
					case BlueToothService.LOSE_CONNECT:
						Toast.makeText(
								MainActivity.this,
								MainActivity.this.getResources().getString(
										R.string.str_lose), 2000).show();
						vg.getChildAt(0).setVisibility(View.VISIBLE);
						vg.getChildAt(1).setVisibility(View.VISIBLE);
						vg.getChildAt(2).setVisibility(View.GONE);
						vg.getChildAt(2).setFocusable(false);
						break;
					}
					break;
				case MESSAGE_READ:
					// sendFlag = false;//缓冲区已满
					break;
				case MESSAGE_WRITE:// 缓冲区未满
					// sendFlag = true;
					break;

				}
			}
		};

		bt_disconnect = (Button) findViewById(R.id.bt_disconnect);
		bt_disconnect.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				if (mBTService.getState() == mBTService.STATE_CONNECTED) {
					mBTService.DisConnected();
				}
			}
		});

		bt_2d = (Button) findViewById(R.id.bt_2d);

		bt_2d.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub

				if (mBTService.getState() != mBTService.STATE_CONNECTED) {
					Toast.makeText(
							MainActivity.this,
							MainActivity.this.getResources().getString(
									R.string.str_unconnected), 2000).show();
					return;
				}
				String message = edit.getText().toString();
				if (message.length() > 0) {

					try {
						message = new String(message.getBytes("utf8"));
					} catch (UnsupportedEncodingException e1) {
						// TODO Auto-generated catch block
						e1.printStackTrace();
					}
					btMap = BarcodeCreater.encode2dAsBitmap(message, 384, 384,
							2);
					BarcodeCreater.saveBitmap2file(btMap, "mypic1.JPEG");
					iv.setImageBitmap(btMap);

				}
			}
		});

		bt_bar = (Button) findViewById(R.id.bt_bar);
		bt_bar.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				if (mBTService.getState() != mBTService.STATE_CONNECTED) {
					Toast.makeText(
							MainActivity.this,
							MainActivity.this.getResources().getString(
									R.string.str_unconnected), 2000).show();
					return;
				}
				String message = edit.getText().toString();

				if (message.getBytes().length > message.length()) {
					Toast.makeText(
							MainActivity.this,
							MainActivity.this.getResources().getString(
									R.string.str_cannotcreatebar), 2000).show();
					return;
				}
				if (message.length() > 0) {

					btMap = BarcodeCreater.creatBarcode(MainActivity.this,
							message, 384, 120, true, 1);// 最后一位参数是条码格式
					iv.setImageBitmap(btMap);

				}

			}
		});
		mBTService = new BlueToothService(this, mhandler);// 创建对象的时候必须有一个是Handler类型
		// 点击检查是否有蓝牙设备
		checkButton = (Button) findViewById(R.id.bt_check);
		checkButton.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				if (mBTService.HasDevice()) {
					Toast.makeText(
							MainActivity.this,
							MainActivity.this.getResources().getString(
									R.string.str_devecehasblue), 2000).show();
				} else {
					Toast.makeText(
							MainActivity.this,
							MainActivity.this.getResources().getString(
									R.string.str_hasnodevice), 2000).show();
				}
			}
		});

		// 点击打开或者关闭蓝牙设备

		controlButton = (Button) findViewById(R.id.bt_openclose);
		if (mBTService.IsOpen()) {// 判断蓝牙是否打开
			controlButton.setText(MainActivity.this.getResources().getString(
					R.string.str_open));
		}
		controlButton.setOnClickListener(new OnClickListener() {
			public void onClick(View v) {
				// TODO Auto-generated method stub
				if (mBTService.IsOpen()) {// 判断蓝牙是否打开
					if (mBTService.getState() == mBTService.STATE_CONNECTED) {
						mBTService.DisConnected();
					}
					try {
						Thread.sleep(200);
					} catch (InterruptedException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
					mBTService.CloseDevice();
				} else {
					mBTService.OpenDevice();
				}

			}
		});

		// 更新按钮状态
		bt_update = new Thread() {
			public void run() {
				while (updateflag) {
					if (mBTService.IsOpen()) {// 判断蓝牙是否打开
						controlButton.post(new Runnable() {

							@Override
							public void run() {
								// TODO Auto-generated method stub
								controlButton.setText(MainActivity.this
										.getResources().getString(
												R.string.str_close));
							}
						});
					} else {
						controlButton.post(new Runnable() {

							@Override
							public void run() {
								// TODO Auto-generated method stub
								controlButton.setText(MainActivity.this
										.getResources().getString(
												R.string.str_open));
							}
						});

					}

				}
			}
		};

		bt_update.start();
		mPairedDevicesArrayAdapter = new ArrayAdapter<String>(this,
				R.layout.device_name);

		// 查看已配对蓝牙
		bt_matches = (Button) findViewById(R.id.bt_matches);
		bt_matches.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				if (!mBTService.IsOpen()) {
					mBTService.OpenDevice();
					return;
				}
				deviceList.setAdapter(mPairedDevicesArrayAdapter);
				mPairedDevicesArrayAdapter.clear();
				devices = mBTService.GetBondedDevice();
				if (devices.size() > 0) {

					for (BluetoothDevice device : devices) {
						mPairedDevicesArrayAdapter.add(device.getName() + "\n"
								+ device.getAddress());
					}
				} else {
					String noDevices = MainActivity.this.getResources()
							.getString(R.string.str_nomatched);
					mPairedDevicesArrayAdapter.add(noDevices);
				}
			}
		});

		mNewDevicesArrayAdapter = new ArrayAdapter<String>(this,
				R.layout.device_name);
		// 扫描所有区设备

		bt_scan = (Button) findViewById(R.id.bt_scan);
		bt_scan.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				// 先判断是否正在扫描

				if (!mBTService.IsOpen()) {// 判断蓝牙是否打开
					mBTService.OpenDevice();
					return;
				}
				if (mBTService.GetScanState() == mBTService.STATE_SCANING)
					return;
				vg.getChildAt(0).setVisibility(View.VISIBLE);
				vg.getChildAt(1).setVisibility(View.VISIBLE);
				layoutscan.setVisibility(View.VISIBLE);
				mNewDevicesArrayAdapter.clear();
				devices = mBTService.GetBondedDevice();

				if (devices.size() > 0) {

					for (BluetoothDevice device : devices) {
						mNewDevicesArrayAdapter.add(device.getName() + "\n"
								+ device.getAddress());
					}
				}
				deviceList.setAdapter(mNewDevicesArrayAdapter);
				new Thread() {
					public void run() {
						mBTService.ScanDevice();
					}
				}.start();

			}

		});

		mBTService.setOnReceive(new OnReceiveDataHandleEvent() {

			@Override
			public void OnReceive(BluetoothDevice device) {
				// TODO Auto-generated method stub
				if (device != null) {
					mNewDevicesArrayAdapter.add(device.getName() + "\n"
							+ device.getAddress());
				} else {
					Message msg = new Message();
					msg.what = 1;
					handler.sendMessage(msg);

				}
			}
		});
		deviceList.setOnItemClickListener(new OnItemClickListener() {

			@Override
			public void onItemClick(AdapterView<?> parent, View view,
					int position, long id) {
				// 获取蓝牙物理地址
				if (!mBTService.IsOpen()) {// 判断蓝牙是否打开
					mBTService.OpenDevice();
					return;
				}
				if (mBTService.GetScanState() == mBTService.STATE_SCANING) {
					Message msg = new Message();
					msg.what = 2;
					handler.sendMessage(msg);
				}
				if (mBTService.getState() == mBTService.STATE_CONNECTING) {
					return;
				}

				String info = ((TextView) view).getText().toString();
				String address = info.substring(info.length() - 17);
				mBTService.DisConnected();
				mBTService.ConnectToDevice(address);// 连接蓝牙
			}
		});

		bt_print.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				if (mBTService.getState() != mBTService.STATE_CONNECTED) {
					Toast.makeText(
							MainActivity.this,
							MainActivity.this.getResources().getString(
									R.string.str_unconnected), 2000).show();
					return;
				}
				String message = edit.getText().toString();
				byte[] bt = new byte[3];
				bt[0] = 27;
				bt[1] = 56;
				bt[2] = 2;// 1,2//设置字体大小
				mBTService.write(bt);
				mBTService.PrintCharacters(message);
			}
		});

		bt_image = (Button) findViewById(R.id.bt_image);
		bt_image.setOnClickListener(new OnClickListener() {
			public void onClick(View v) {
				if (mBTService.getState() != mBTService.STATE_CONNECTED) {
					Toast.makeText(
							MainActivity.this,
							MainActivity.this.getResources().getString(
									R.string.str_unconnected), 2000).show();
					return;
				}
				if (btMap != null) {
					Bitmap bitmapOrg = btMap;// BitmapFactory.decodeFile(picPath);
					int w = bitmapOrg.getWidth();
					int h = bitmapOrg.getHeight();
					mBTService.PrintImage(resizeImage(bitmapOrg, 384, h));
					return;
				}
			}
		});
		handler = new Handler() {
			@Override
			public void handleMessage(Message msg) {
				super.handleMessage(msg);
				switch (msg.what) {
				case 0:

					break;
				case 1:// 扫描完毕
						// progressDialog.cancel();
					mBTService.StopScan();
					layoutscan.setVisibility(View.GONE);
					Toast.makeText(
							MainActivity.this,
							MainActivity.this.getResources().getString(
									R.string.str_scanover), 2000).show();
					break;
				case 2:// 停止扫描

					layoutscan.setVisibility(View.GONE);
					break;
				}
			}
		};

		tv_status = (TextView) findViewById(R.id.tv_status);
		tv_update = new Thread() {
			public void run() {
				while (tvFlag) {
					try {
						Thread.sleep(200);
					} catch (InterruptedException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
					tv_status.post(new Runnable() {
						@Override
						public void run() {
							// TODO Auto-generated method stub
							if (mBTService != null) {
								if (mBTService.getState() == mBTService.STATE_CONNECTED) {
									tv_status.setText(MainActivity.this
											.getResources().getString(
													R.string.str_connected));
								} else if (mBTService.getState() == mBTService.STATE_CONNECTING) {
									tv_status.setText(MainActivity.this
											.getResources().getString(
													R.string.str_connecting));
								} else {
									tv_status.setText(MainActivity.this
											.getResources().getString(
													R.string.str_disconnected));
								}
							}
						}
					});
				}
			}
		};
		tv_update.start();

	}

	public static Bitmap resizeImage(Bitmap bitmap, int w, int h) {
		Bitmap BitmapOrg = bitmap;
		int width = BitmapOrg.getWidth();
		int height = BitmapOrg.getHeight();
		int newWidth = w;
		int newHeight = h;

		float scaleWidth = ((float) newWidth) / width;
		float scaleHeight = ((float) newHeight) / height;
		Matrix matrix = new Matrix();
		matrix.postScale(scaleWidth, scaleWidth);
		Bitmap resizedBitmap = Bitmap.createBitmap(BitmapOrg, 0, 0, width,
				height, matrix, true);
		return resizedBitmap;
	}

	private void sendMessage(String message) {
		// Check that we're actually connected before trying anything
		if (mBTService.getState() != BlueToothService.STATE_CONNECTED) {
			return;
		}
		if (message.length() > 0) {

			byte[] send;
			try {
				send = message.getBytes("GBK");
			} catch (UnsupportedEncodingException e) {
				send = message.getBytes();
			}
			mBTService.write(send);

		}
	}

	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		super.onActivityResult(requestCode, resultCode, data);
		if (requestCode == REQUEST_EX && resultCode == RESULT_OK
				&& null != data) {
			Uri selectedImage = data.getData();
			String[] filePathColumn = { MediaStore.Images.Media.DATA };
			Cursor cursor = getContentResolver().query(selectedImage,
					filePathColumn, null, null, null);
			cursor.moveToFirst();
			int columnIndex = cursor.getColumnIndex(filePathColumn[0]);
			String picturePath = cursor.getString(columnIndex);
			picPath = picturePath;
			iv.setImageURI(selectedImage);
			btMap = BitmapFactory.decodeFile(picPath);
			if (btMap.getHeight() > 384) {
				btMap = BitmapFactory.decodeFile(picPath);
				iv.setImageBitmap(resizeImage(btMap, 384, 384));

			}
			cursor.close();
		}

	}

	@Override
	protected void onDestroy() {
		// TODO Auto-generated method stub
		super.onDestroy();

	}

	@Override
	protected void onPause() {
		// TODO Auto-generated method stub
		super.onPause();
	}

	public void onBackPressed() {
		if (bt_update != null) {
			updateflag = false;
			bt_update = null;
		}
		if (tv_update != null) {
			tvFlag = false;
			tv_update = null;
		}
		try {
			Thread.sleep(200);
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		if (mBTService != null) {
			mBTService.DisConnected();
			mBTService = null;
		}
		try {
			Thread.sleep(1000);
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		System.exit(0);
	}

}
