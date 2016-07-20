package com.example.mikuhatsune.updatepage;

import android.content.Context;
import android.content.CursorLoader;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.net.Uri;
import android.os.Build;
import android.provider.DocumentsContract;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.net.Socket;

public class UpdateClass extends Thread {

    //final Activity
    final String HOST;
    final int PORT;
    final String path;
    final Context context;
    final static float deviation = 1f;

    public UpdateClass(Context context, String host, int port, String path)
    {
        HOST = host;
        PORT = port;
        this.path = path;
        this.context = context;
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

    private boolean updateBmp(Bitmap bmp, String HOST, int PORT)
    {
        if(bmp == null || HOST == null || PORT < 1)
            return false;
        //getBitmapByteArray(cleanBackground(bmp));
        try {
            ServerConnect mSocket = new ServerConnect(HOST, PORT);
            while(!mSocket.connect()){
                sleep(500);
            }
            mSocket.writeData(getBitmapByteArray(cleanBackground(bmp)));
            mSocket.close();
        } catch (Exception e) {
            return false;
        }
        return true;
    }

    public static String getPathFromUri(final Context context, final Uri uri)
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

    private static Uri checkUri(final Context context, final Uri uri)
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

    private static String getFilePathFromUri(final Context context, Uri uri)
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

    private static Bitmap cleanBackground(Bitmap sourceBmp) {
        int pixels[] = getPixels(sourceBmp);
        return cleanBackground(pixels, sourceBmp.getWidth(),
                sourceBmp.getHeight(), sourceBmp.getConfig());
    }

    private static Bitmap cleanBackground(int[] pixels,
                                   int width, int height, Bitmap.Config config)
    {

        boolean[] binar = binarization(pixels);
        //binar = imageContour(binar, width,height);

        for(int index = 0; index < pixels.length; ++index){
            if(binar[index]) {
                pixels[index] = Color.alpha(255);
            }
        }
        return Bitmap.createBitmap(pixels, width, height, config);
    }

    private static int[] getPixels(Bitmap bmp)
    {
        int pixels[] = new int [bmp.getHeight() * bmp.getWidth()];
        bmp.getPixels(pixels, 0, bmp.getWidth(), 0, 0, bmp.getWidth(), bmp.getHeight());
        return pixels;
    }

    private static boolean[] binarization(int[] pixels)
    {
        boolean[] bool = new boolean[pixels.length];
        int[] bucket = new int [256];
        float average = 0;
        for(int pixel : pixels){
            int weight = computeWeight(pixel);
            average += weight;
            ++bucket[weight];
        }

        int max = 0;
        for(int index = 0; index < bucket.length; ++index){
            if(bucket[index] > bucket[max]) {
                max = index;
            }
        }

        int argement;
        if(bucket[max] < average){
            argement = 1;
        }else if (bucket[max] > average){
            argement = -1;
        }else{
            argement = 0;
        }

        average /= pixels.length;

        average += (max - average) *
                ((bucket[max] / (float)pixels.length) * deviation) * argement;

        if(average > 240)
            average = 240;

        for(int index = 0; index < pixels.length; ++index){
            bool[index] = computeWeight(pixels[index]) > average;
        }

        return bool;
    }

    private static int computeWeight(int color){
        int counter = Color.red(color);
        counter += Color.green(color);
        counter += Color.blue(color);
        counter += (255 - Color.alpha(color)) * 3;
        counter /= 3;
        if(counter > 255)
            counter = 255;
        return counter;
    }

    private static boolean[] imageContour(boolean[] bina, int width, int height)
    {
        boolean bool[] = new boolean[bina.length];
        for(int y = 0; y < height - 1; ++y){
            for(int x = 0; x < width - 1; ++x){
                bool[y * width + x] =!(bina [y * width + x] ^ bina[y * width + x + 1] |
                                       bina [y * width + x] ^ bina[(y + 1) * width + x]);
            }
        }
        return bool;
    }
}
