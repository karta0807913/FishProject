using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net;

public class BroadCastReceiver : MonoBehaviour {
    static int listenPort = 48899;

    UdpClient listener = new UdpClient(listenPort);
    IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);

    // Use this for initialization
    void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        byte[] bytes = listener.Receive(ref groupEP);
        if(bytes != null)
        {
            Debug.Log(groupEP.ToString());
            Debug.Log(bytes);
        }
    }
}
