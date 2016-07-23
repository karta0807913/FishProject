package com.example.mikuhatsune.updatepage;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.Socket;

public class ServerConnect {
    private String HOST;
    private int PORT;
    private Socket socket;
    private InputStream inputStream;
    private OutputStream outputStream;
    private static final byte[] keyWords;
    static
    {
        keyWords = new byte[2];
        keyWords[0] = 'l';
        keyWords[1] = 'k';
    }

    public ServerConnect(String HOST, int PORT)
    {
        this.HOST = HOST;
        this.PORT = PORT;
        socket = null;
        inputStream = null;
        outputStream = null;
    }

    public void writeData(byte[] bytes) throws IOException
    {
        if(outputStream != null)
            outputStream.write(bytes);
    }

    public boolean connect() throws IOException
    {
        socket = new Socket(HOST, PORT);
        outputStream = socket.getOutputStream();
        inputStream = socket.getInputStream();

        char a = (char)inputStream.read();
        int b = (char)inputStream.read();
        if(a == 'a' && b == 'l'){
            outputStream.write(keyWords);
            if(!socket.isClosed())
                return true;
        }else
        socket = null;
        outputStream = null;
        inputStream = null;
        return false;
    }

    public void close() throws IOException
    {
        if(!socket.isClosed()){
            socket.close();
            inputStream.close();
            outputStream.close();
        }
        socket = null;
        outputStream = null;
        inputStream = null;
    }
}

