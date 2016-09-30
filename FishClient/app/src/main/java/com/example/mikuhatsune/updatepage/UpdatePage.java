package com.example.mikuhatsune.updatepage;

import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.net.wifi.WifiManager;
import android.os.Environment;
import android.os.SystemClock;
import android.provider.MediaStore;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;

public class UpdatePage extends AppCompatActivity {

    static final String HOST = "192.168.56.1";
    static final int PORT = 12233;
    static final int SELECT_PHOTO = 1;
    static final int TAKE_PICTURE = 2;

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
                selectFile("image/*", SELECT_PHOTO);
            }
        });
        cameraButton.setOnClickListener(new Button.OnClickListener(){
            @Override
            public void onClick(View v) {
                Intent intent = new Intent("android.media.action.IMAGE_CAPTURE");
                startActivityForResult(intent, TAKE_PICTURE);
            }
        });

    }

    public void selectFile(String types, int flag )
    {
        Intent photoPickerIntent = new Intent(Intent.ACTION_PICK);
        photoPickerIntent.setType(types);
        startActivityForResult(photoPickerIntent, flag);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data)
    {
        super.onActivityResult(requestCode, resultCode, data);
        ImageView img = (ImageView) findViewById(R.id.imageView);

        if (resultCode == RESULT_OK) {
            switch (requestCode){
                case SELECT_PHOTO:
                    Bitmap bmp = SELECT_PHOTO(data.getData());
                    img.setImageBitmap(bmp);
                    (new UpdateClass(getApplicationContext(), HOST, PORT, bmp)).start();
                    break;
                case TAKE_PICTURE:
                    bmp = (Bitmap)data.getExtras().get("data");
                    (new UpdateClass(getApplicationContext(), HOST, PORT, bmp)).start();
                    img.setImageBitmap(bmp);
                    break;

            }
        }
    }

    private Bitmap SELECT_PHOTO(Uri selectedImage){
        String[] filePathColumn = {MediaStore.Images.Media.DATA};

        Cursor cursor = getContentResolver().query(
                selectedImage, filePathColumn, null, null, null);

        assert cursor != null;
        cursor.moveToFirst();

        int columnIndex = cursor.getColumnIndex(filePathColumn[0]);
        String filePath = cursor.getString(columnIndex);
        cursor.close();

        return BitmapFactory.decodeFile(filePath);
    }
}
