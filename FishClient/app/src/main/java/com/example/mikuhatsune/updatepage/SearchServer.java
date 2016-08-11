package com.example.mikuhatsune.updatepage;

import android.app.Activity;
import android.content.Context;
import android.net.wifi.WifiManager;

import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;
import java.net.MulticastSocket;

public class SearchServer{
    Context context;

    public SearchServer(Context cc)
    {
        context = cc;
    }

    interface OnReceiveListener
    {
        void onReceive(String data);
        void onFail(String data);
    }

    OnReceiveListener onReceiveListener;
    private MulticastSocket socket;
    private DatagramPacket packet;

    public void send(final String sData, final OnReceiveListener cc)
    {
        new Thread(new Runnable()
        {
            @Override
            public void run()
            {
                onReceiveListener = cc;
                //String datastring = data;//"HF-A11ASSISTHREAD";
                byte[] data = sData.getBytes();

                try
                {
                    WifiManager manager = (WifiManager) context.getSystemService(Context.WIFI_SERVICE);
                    WifiManager.MulticastLock lock= manager.createMulticastLock("localWifi");
                    lock.acquire();
                    socket = new MulticastSocket(35124);
                    //socket.joinGroup(InetAddress.getByName("0.0.0.0"));
                    socket.setBroadcast(true);
                    packet = new DatagramPacket(data, data.length, InetAddress.getByName("255.255.255.255"), 48899);
                    socket.send(packet);
                    //socket.setSoTimeout(2000);
                    socket.receive(packet);
                    lock.release();
                    final String s = new String(packet.getData(), 0, packet.getLength());
                    if (onReceiveListener != null)
                    {
                        ((Activity)context).runOnUiThread(new Runnable()
                        {
                            @Override
                            public void run()
                            {
                                onReceiveListener.onReceive(s);
                            }
                        });

                    }

                }
                catch (final Exception e)
                {
                    ((Activity)context).runOnUiThread(new Runnable()
                    {
                        @Override
                        public void run()
                        {
                            onReceiveListener.onFail(e.getMessage());
                        }
                    });

                }

            }
        }).start();

    }
}
