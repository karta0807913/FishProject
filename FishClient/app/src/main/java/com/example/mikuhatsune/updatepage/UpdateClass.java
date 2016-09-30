package com.example.mikuhatsune.updatepage;

import android.app.Activity;
import android.content.Context;
import android.content.CursorLoader;
import android.content.pm.ActivityInfo;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.net.Uri;
import android.os.Build;
import android.provider.DocumentsContract;
import android.provider.MediaStore;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.net.Socket;

public class UpdateClass extends Thread {

    final static int BITMAP = 0;
    final static int INTENT = 1;

    Context context;
    String HOST;
    int PORT;
    String path;
    Bitmap bitmap;
    final static float deviation = 3.4f;
    int state;

    public UpdateClass(Context context, String host, int port, String path)
    {
        HOST = host;
        PORT = port;
        this.path = path;
        this.context = context;
        state = INTENT;
    }

    public UpdateClass(Context context, String host, int port, Bitmap bitmap){
        HOST = host;
        PORT = port;
        this.context = context;
        this.bitmap = bitmap;
        state = BITMAP;
    }

    @Override
    public void run()
    {
        super.run();
        if(state == INTENT)
            updateBmp(image2Bitmap(path), HOST, PORT);
        else if(state == BITMAP)
            updateBmp(bitmap, HOST, PORT);
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

    private static Bitmap cleanBackground(Bitmap sourceBmp)
    {
        int pixels[] = getPixels(sourceBmp);
        return cleanBackground(pixels, sourceBmp.getWidth(),
                sourceBmp.getHeight(), sourceBmp.getConfig());
    }

    private static Bitmap cleanBackground(int[] pixels,
                                   int width, int height, Bitmap.Config config)
    {

        boolean[] binar = binarization(pixels, width, height);
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

    private static boolean[] computeLocal(int[] pixels, boolean bool, int startX, int endX, int startY, int endY, int height){
        float average = 0;
        int m = (endX - startX) * (endY - startY);

        for(int column = startY; column < endY; ++column){
            for(int row = startX; row < endX; ++row){
                average = computeWeight(pixels[row + column * height]) / m;
            }
        }

        for(int column = startY; column < endY; ++column){
            for(int row = startX; row < endX; ++row){
                bool[row + column * height] = computeWeight(pixels[row + column * height]) > average;
            }
        }

        return bool;
    }

    private static boolean[] binarization(int[] pixels, int width, int height)
    {
        boolean[] bool = new boolean[pixels.length];
        final int xSize = 10;
        final int ySize = 10;

        int xLength = width / xSize;
        int yLength = height / ySize;

        for(int y = 0; y < yLength; ++y){
            for(int x = 0; x < xlength; ++x){
                computeLocal(pixels, bool, x * xSize, (x + 1) * xSize, y * ySize, (y + 1) * ySize, height);
            }
        }

        computeLocal(pixels, bool, xSize * xLenght, width, ySize * yLength, height, height);

        return bool;
    }

    private static int computeWeight(int color)
    {
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


    /*
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
    */