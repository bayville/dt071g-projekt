# Scoreboard
En matchklocka för ishockey, byggd med C#/.NET. Har full funktionalitet för matchtid, perioder, periodpauser, time-out, powerbreak och utvisningar. 
Använder sig av UDP-multicast för att skicka data över nätverket, detta innebär att man kan ha en reciever som tar emot klockans status alltså en scoreboard. Applikationen är byggd för att man enkelt ska kunna lägga till och ta bort olika moduler i koden.

## Videopresentation
[https://youtu.be/EoJSBM--oSA](https://youtu.be/EoJSBM--oSA)

## Todo
- Grafiskt gränsnitt för klockan
- En reciever/scoreboard

## Screenshot GUI-skiss
![GUI Screenshot](https://github.com/bayville/dt071g-projekt/blob/main/gui.png)



## Multicast-settings
- UdpClient: IPAdress.Any
- Multicast IP-Adress: 239.0.0.1
- Port: 5000
