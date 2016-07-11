package com.example.mikuhatsune.updatepage;

import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;

import java.io.ByteArrayOutputStream;

public class UpdatePage extends AppCompatActivity {

    static final String HOST = "192.168.56.1";
    static final int PORT = 12233;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_update_page);
        Button updateButton = (Button) findViewById(R.id.updateButton);
        updateButton.setOnClickListener(new Button.OnClickListener() {
            @Override
            public void onClick(View v) {
                selectFile("image/*", 0);
            }
        });
    }

    public void selectFile(String types, int flag){
        Intent getImage = new Intent(Intent.ACTION_GET_CONTENT);
        getImage.addCategory(Intent.CATEGORY_OPENABLE);
        getImage.setType(types);
        startActivityForResult(getImage, flag);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data)
    {
        super.onActivityResult(requestCode, resultCode, data);

        if (resultCode == RESULT_OK) {
            // 取得檔案的 Uri
            Uri uri = data.getData();
            if (uri != null) {
                String path;
                ImageView img = (ImageView)findViewById(R.id.imageView);
                path = UpdateClass.getPathFromUri(getApplicationContext(), uri);
                img.setImageBitmap(BitmapFactory.decodeFile(path));
                (new UpdateClass(getApplicationContext(), HOST, PORT, path)).start();
            }
        }
    }

    public byte[] getBytesFromBitmap(Bitmap bitmap)
    {
        ByteArrayOutputStream stream = new ByteArrayOutputStream();
        bitmap.compress(Bitmap.CompressFormat.JPEG, 70, stream);
        return stream.toByteArray();
    }
}
