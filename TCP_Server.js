var net = require('net');

var HOST = '127.0.0.1';
var PORT = 12233;
var sock = null;
var lock = false;
var nullcode = 'ep';
var alivecode = 'al';
var buzycode = 'bz';
var unityproj = 'gt';
var dataend = 'ed';
var clientcode = 'lk';

net.createServer(function (socket) {

    if (sock == null) {
        socket.write(nullcode);
    } else {
        if (lock) {
            socket.write(buzycode);
        } else {
            socket.write(alivecode);
        }
    }

    socket.on('data', function (data) {
        // no unity
        if (sock == null) {
            console.log(data);
            if (data == unityproj) {
                if (sock == null) {
                    sock = socket;
                    console.log('is untiy');
                }
                else
                    socket.end();
            } else {
                socket.end();
            }
        } else {
            // have client
            if (lock) {
                sock.write(data);
                console.log('some data');
            } else {
                if (data == clientcode) {
                    if (lock == false) {
                        lock = true;
                        console.log('is client');
                    } else {
                        socket.end();
                    }
                } else {
                    socket.end();
                }
            }
        }
    });

	socket.on('close', function(){
		if(sock == socket){
			sock = null;
		} else {
		    if (lock) {
		        sock.write('\0');
		        console.log('data end');
		        lock = false;
		    }
		}
	});

	socket.on('error', function(error){
		console.log(error.toString());
	});

	console.log('someone connected');

}).listen(12233);

console.log("server is ready");