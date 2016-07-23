package com.example.mikuhatsune.updatepage;

import java.io.IOException;
import java.net.InetSocketAddress;
import java.net.Socket;

final class FindHost {
    static final String subNet = "192.168";

    int ip3, ip4;
    int search3, search4;
    String result;
    final int PORT;
    final int timeout;

    public FindHost(int PORT)
    {
        this(1, 255, PORT);
    }

    public FindHost(int ip3, int ip4, int PORT)
    {
        this(ip3, ip4, PORT, 100);
    }

    public FindHost(int ip3, int ip4, int PORT, int timeout)
    {
        search3 = 0;
        search4 = 1;
        result = null;

        this.ip3 = ip3;
        this.ip4 = ip4;
        this.PORT = PORT;
        this.timeout = timeout;
    }

    void startSearch()
    {
        Worm worm = new Worm(subNet + "." + search3, search4, ip4);
        worm.start();
    }

    String getResult(){
        String tmp = null;
        if(result != null){
            tmp = result;
            if(search4 < ip4){
                Worm worm = new Worm(subNet + "." + search3, search4, ip4);
                worm.start();
            }else{
                search4 = 0;
                Worm worm = new Worm(subNet + "." + ++search3, search4, ip4);
                worm.start();
            }
            result = null;
        }
        return tmp;
    }

    class Worm extends Thread
    {
        final int start, end;
        final String subnet;

        public Worm(String subnet, int start, int end)
        {
            this.subnet = subnet;
            this.start = start;
            this.end = end;
        }

        @Override
        public void run()
        {
            super.run();

            for (int i = start; i < end; i++) {
                String host = subnet + "." + i;
                try {
                    Socket socket = new Socket();
                    socket.connect(new InetSocketAddress(host, PORT), timeout);
                    if (!socket.isClosed()) {
                        result = host;
                        search4 = i;
                        socket.close();
                        return;
                    }
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }
    }
}