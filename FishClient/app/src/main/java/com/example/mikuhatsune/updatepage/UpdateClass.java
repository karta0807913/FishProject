package com.example.mikuhatsune.updatepage;

import android.content.Context;
import android.content.CursorLoader;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.os.Build;
import android.provider.DocumentsContract;
import android.provider.MediaStore;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.net.Socket;

public class UpdateClass extends Thread {

    //final Activity
    final String HOST;
    final int PORT;
    String path;
    Context context;
    public UpdateClass(Context context, String host, int port, String path)
    {
        HOST = host;
        PORT = port;
        this.path = path;
        this.context = context;
    }

    public void setPath(String path)
    {
        this.path = path;
    }

    @Override
    public void run()
    {
        super.run();
        updateBmp(image2Bitmap(path), HOST, PORT);
    }

    private Bitmap image2Bitmap(String path)
    {
        if(path == null)
            return null;
        Bitmap bmp = null;
        File imageFile = new File(path);
        try {
            FileInputStream fis = new FileInputStream(imageFile);
            bmp = BitmapFactory.decodeStream(fis);
            fis.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
        return bmp;
    }

    private static byte[] getBitmapByteArray(Bitmap bmp)
    {
        if(bmp == null)
            return null;
        byte[] byteArray;
        try {
            ByteArrayOutputStream stream = new ByteArrayOutputStream();
            bmp.compress(Bitmap.CompressFormat.PNG, 100, stream);
            byteArray = stream.toByteArray();
            stream.close();
        } catch (IOException e) {
            byteArray = null;
        }
        return  byteArray;
    }

    private static boolean updateBmp(Bitmap bmp, String HOST, int PORT)
    {
        if(bmp == null || HOST == null || PORT < 1)
            return false;
        try {
            Socket socket = new Socket(HOST, PORT);
            socket.getOutputStream().write(getBitmapByteArray(bmp));
            socket.close();
        } catch (IOException e) {
            return false;
        }
        return true;
    }

    public static String getPathFromUri(Context context, final Uri uri)
    {
        final String scheme = uri.getScheme();
        String path = null;
        if ("content".equals(scheme)) {
            // 內容URI
            path = getFilePathFromUri(context, uri);
        } else if ("file".equals(scheme)) {
            // 檔案URI
            path = uri.getPath();
        }
        return path;
    }

    private static Uri checkUri(Context context, Uri uri)
    {
        final boolean isKitKat = Build.VERSION.SDK_INT >= Build.VERSION_CODES.KITKAT;
        if(isKitKat && DocumentsContract.isDocumentUri(context, uri)){
            if(uri.toString().contains("%3A")){
                final String[] split = uri.toString().split("%3A");
                return Uri.parse(split[0] + ":" + split[1]);
            }
        }
        return uri;
    }

    private static String getFilePathFromUri(Context context, Uri uri)
    {
        uri = checkUri(context, uri);
        CursorLoader cursorLoader= new CursorLoader(
                context,
                uri,
                new String[]{ "_data" },
                null,
                null,
                null
        );
        String returnStr = "";
        Cursor cursor = cursorLoader.loadInBackground();
        if(cursor.getCount()!=0){
            cursor.moveToFirst();
            returnStr = cursor.getString(cursor.getColumnIndexOrThrow("_data"));
            cursor.close();
        }
        return returnStr;
    }
}
