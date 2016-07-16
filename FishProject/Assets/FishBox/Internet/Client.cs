
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;
using System.IO;

public class Client : MonoBehaviour
{

    private const string IP = "127.0.0.1";
    private const int Port = 12233;

    private Socket _clientSocket = new Socket(AddressFamily.InterNetwork,
        SocketType.Stream, ProtocolType.Tcp);
    private byte[] _recieveBuffer = new byte[12288];
    private byte[] _picBuffer = new byte[10485760];
    private const char END_SYNBOL = ';';
    private int picSize = 0;


    public CreateFish createFish;

    private const string dirPath = "";
    private const string picType = "";

    // Use this for initialization
    void Start()
    {
        createFish = gameObject.GetComponent<CreateFish>();
        //Connect 
        try
        {
            _clientSocket.Connect(new IPEndPoint(IPAddress.Parse(IP), Port));
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
        //start listening
        //we dont need to sent something to server
        _clientSocket.BeginReceive(_recieveBuffer, 0, _recieveBuffer.Length,
            SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
    }

    //get the package
    private void ReceiveCallback(IAsyncResult AR)
    {
        int recieved = _clientSocket.EndReceive(AR);

        if (recieved <= 0)
            return;

        Buffer.BlockCopy(_recieveBuffer, 0, _picBuffer, picSize, recieved);
        picSize += recieved;
        Debug.Log((char)_picBuffer[picSize - 1]);
        if ((char)_picBuffer[picSize - 1] == END_SYNBOL)
        {
            byte[] recData = new byte[picSize - 1];
            Buffer.BlockCopy(_picBuffer, 0, recData, 0, picSize - 1);
            savePic(recData);
            picSize = 0;
        }
        _clientSocket.BeginReceive(_recieveBuffer, 0, _recieveBuffer.Length,
            SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
    }

    //save byte to file
    private void savePic(byte[] data)
    {
        string picName = hashName_MD5(data);
        string path = dirPath + picName + picType;
        if (File.Exists(path))
        {
            Debug.Log("This Pic Exist");
        }
        else
        {
            File.WriteAllBytes(path, clearnBackground(data));
            createFish.createFish(picName);
        }
    }

    //hash a name
    private string hashName_MD5(byte[] data)
    {
        System.Security.Cryptography.MD5CryptoServiceProvider md5 =
            new System.Security.Cryptography.MD5CryptoServiceProvider();

        byte[] hashBytes = md5.ComputeHash(data);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }

    private byte[] clearnBackground(byte[] sourceData)
    {
        return sourceData;
    }

    ~Client()
    {
        _clientSocket.Close();
    }

}