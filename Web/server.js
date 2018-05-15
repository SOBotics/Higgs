const express = require('express');
const app = express();

const oneWeek = 7 * 24 * 60 * 60 * 1000;

app.use(express.static(__dirname + '/dist', { maxAge: oneWeek }));

app.use('/*', function(req, res) {
  res.sendFile(__dirname + '/dist/index.html');
})

app.listen(process.env.PORT || 5555);