const Discord = require("discord.js");
const client = new Discord.Client();
let prefix = "poly:";
var fs = require("fs");
var lelCountFile = fs.readFileSync("./variables.txt", "utf8");

//Glitch Uptime
const http = require('http');
const express = require('express');
const app = express();
app.get("/", (request, response) => {
  console.log(Date.now() + " Ping Received");
  response.sendStatus(200);
});
app.listen(process.env.PORT);
setInterval(() => {
  http.get(`http://${process.env.PROJECT_DOMAIN}.glitch.me/`);
}, 280000);


client.on("ready", () => {
  console.log("Bot started");
  var lelCount = parseFloat(lelCountFile, 10);
  const lel = client.users.get("384454726512672768");
  console.log(lel);
  console.log("Current LelCount:");
  console.log(lelCount)

});
client.on("message", (message) => {
    if (message.author.bot) return;
      if (message.isMentioned("384454726512672768") == true) {
        var lelCountFile = fs.readFileSync("./variables.txt", "utf8");
        var lelCount = parseFloat(lelCountFile, 10);
        var lelCount = lelCount + 1;
        console.log(lelCount);
        var newLel = lelCount.toString();
        fs.writeFile("./variables.txt", newLel, function (err) {
          if (err) return console.log(err);
        });
        var lelCountFile = fs.readFileSync("./variables.txt", "utf8");
        var lelCount = parseFloat(lelCountFile, 10);
      console.log("LEL");
      } 
      if (message.content.startsWith(prefix + "ping")) {
        message.channel.send("pong!");
      } 
      if (message.content.startsWith(prefix + "whoislel")) {
        let embed = new Discord.RichEmbed;
        embed.title = "LEL"
        embed.setColor(0x00ff00)
        embed.setThumbnail("https://cdn.discordapp.com/avatars/384454726512672768/70cd247a060bf1ebdbe4ba099b8eef32.png?size=2048")
        embed.setDescription("Our Current Lel is Reflectron#1288")
        message.channel.send(embed);
      } 
      if (message.content.startsWith(prefix + "help")) {
        message.channel.send("I'm a basic bot to count how many times @lel is pinged. Also I count Michael's daily complaints!");
      } 
      if (message.content.startsWith(prefix + "lel" || prefix + "lelcount")) {
        var lelCountFile = fs.readFileSync("./variables.txt", "utf8");
        var lelCount = parseFloat(lelCountFile, 10);
        message.channel.send(`Lel Indeed. We've LELd ${lelCount} times!`);
      } 
      

});
client.login(process.env.TOKEN);
