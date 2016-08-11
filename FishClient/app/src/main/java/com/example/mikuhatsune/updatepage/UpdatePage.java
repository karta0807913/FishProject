package com.example.mikuhatsune.updatepage;

import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.net.wifi.WifiManager;
import android.os.SystemClock;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;

public class UpdatePage extends AppCompatActivity {

    static final String HOST = "192.168.0.19";
    static final int PORT = 12233;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_update_page);
        Button selectButton = (Button) findViewById(R.id.SelectButton);
        Button cameraButton = (Button) findViewById(R.id.CameraButton);

        selectButton.setOnClickListener(new Button.OnClickListener() {
            @Override
            public void onClick(View v) {
                selectFile("image/*", 0);
            }
        });
        cameraButton.setOnClickListener(new Button.OnClickListener(){
            @Override
            public void onClick(View v) {
                Intent intent = new Intent("android.media.action.IMAGE_CAPTURE");
                startActivityForResult(intent, 1);
            }
        });

        /**  /
        SearchServer searchServer = new SearchServer(getApplicationContext());
        x xx = new x();
        searchServer.send("nope", xx);
        /**/

    }
    class x implements SearchServer.OnReceiveListener{

        @Override
        public void onReceive(String data) {
            System.out.print(data);
        }

        @Override
        public void onFail(String data) {

        }
    }

    public void selectFile(String types, int flag)
    {
        Intent getImage = new Intent(Intent.ACTION_GET_CONTENT);
        getImage.addCategory(Intent.CATEGORY_OPENABLE);
        getImage.setType(types);
        startActivityForResult(getImage, flag);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data)
    {
        super.onActivityResult(requestCode, resultCode, data);
        ImageView img = (ImageView) findViewById(R.id.imageView);

        if (resultCode == RESULT_OK) {
            if(data.getAction().equals("inline-data")) {
                Bitmap bmp = (Bitmap)data.getExtras().get("data");
                (new UpdateClass(getApplicationContext(), HOST, PORT, bmp)).start();
                img.setImageBitmap(bmp);
            } else {
                // 取得檔案的 Uri
                Uri uri = data.getData();
                if (uri != null) {
                    String path;
                    path = UpdateClass.getPathFromUri(getApplicationContext(), uri);
                    img.setImageBitmap(BitmapFactory.decodeFile(path));
                    (new UpdateClass(getApplicationContext(), HOST, PORT, path)).start();
                }
            }
        }
    }
}
