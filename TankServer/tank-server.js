var io = require('socket.io')({
	transports: ['websocket'],
});

io.attach(4567);

var players = {};

io.on('connection', function (socket) {

	socket.on("PlayerStartedCommand", (data) => {
		players[data.PlayerId] = data;
		socket.broadcast.emit('PlayerStartedEvent', data);
	})

	socket.on("PlayerMovedCommand", (data) => {
		players[data.PlayerId] = data;
		socket.broadcast.emit('PlayerMovedEvent', data);
	})

	socket.on("GetPlayersQuery", (data) => {

		var list = [];
		for (var key in players) {
			// check if the property/key is defined in the object itself, not in parent
			if (players.hasOwnProperty(key)) {
				list.push(players[key]);
			}
		}

		var GetPlayersResponse = {};
		GetPlayersResponse.Players = list;
		socket.emit('GetPlayersResponse', GetPlayersResponse);
	})

})
