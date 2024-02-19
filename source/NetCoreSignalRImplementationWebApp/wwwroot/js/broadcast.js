var connection = new signalR.HubConnectionBuilder()
    .withUrl("/broadcastHub")
    .build();

connection.on("ReceiveMessage", function (message) {
    var messages = document.getElementById("messages");
    var messageParagraph = document.createElement("p");
    messageParagraph.innerText = "Received message: " + message;
    messages.appendChild(messageParagraph);
});

document.getElementById("startButton").addEventListener("click", function () {
    connection.start()
        .then(function () {
            connection.invoke("SendMessage");
        })
        .catch(function (err) {
            return console.error(err.toString());
        });
});
