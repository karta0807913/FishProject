var net = require('net');

var HOST = '127.0.0.1';
var PORT = 12233;
var sock = null;
net.createServer(function(socket){
	if(sock == null){
		sock = socket;
	}
	socket.on('data', function(data){
		sock.write(data);
		//socket.write("LOL\r\n");
		console.log('some data');
	});
	socket.on('close', function(){
		console.log('client closed');
		if(sock == socket){
			sock = null;
		}else{
			sock.write(';');
			console.log('END');
		}
	});
	socket.on('error', function(error){
		console.log(error.toString());
	});
	console.log('someone on here');
}).listen(12233);