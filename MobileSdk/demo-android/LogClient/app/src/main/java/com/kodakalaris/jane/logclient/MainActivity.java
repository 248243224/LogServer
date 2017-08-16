package com.kodakalaris.jane.logclient;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import java.text.SimpleDateFormat;

public class MainActivity extends AppCompatActivity {

    EditText log;
    Button bt_send;
    TextView tv;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        bt_send = (Button) findViewById(R.id.bt_send);
        log = (EditText) findViewById(R.id.et_log);
        tv = (TextView) findViewById(R.id.log_text);

        bt_send.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                String logContent = log.getText().toString();
                String logType="0";
                SimpleDateFormat sDateFormat = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
                String logDate = sDateFormat.format(new java.util.Date());
                String deviceName="KJIO-205546";

                if (sendLog(deviceName,logContent,logDate,logType) == 1) {
                    tv.append("send log: " + logContent + "\n");
                } else
                    tv.append("send log failed \n");
            }
        });
        tv.append("connecting server:10.229.18.134 \n");
        if (0 == usbConnect()) {
            if (0 == wifiConnect("10.229.18.134", "15556"))
                tv.append("connecting server failed \n");
            else
                tv.append("connecting server succeed \n");
        }
        else
            tv.append("connecting server succeed \n");
    }

    /**
     * A native method that is implemented by the 'native-lib' native library,
     * which is packaged with this application.
     */
    public native int sendLog(String device,String log,String date,String type);

    public native int usbConnect();

    public native  int wifiConnect(String ip,String port);

    public native void disConnect();

    // Used to load the 'native-lib' library on application startup.
    static {
        System.loadLibrary("native-lib");
    }
}
