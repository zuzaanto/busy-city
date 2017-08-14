# busy-city
For the program to work, you need to get your own API Key from here:https://developers.google.com/maps/documentation/directions/start#get-a-key and paste it into the program.cs 84th line.
You also may need to change the directory of the txt file exported, also in program.cs.
The program starts by asking the user for longitude and latitude (I am not sure if I didn't mix the labels up, sorry).
After typing longitude and latitude, you have to close the window of the Form.
Then the longer you let the program run, the more background data it shall export to txt file.
The simulation is not exactly real-time - its velocity depends on your computer's parameters, internet connection etc.
For cleariness purposes, I decided to export just the citizens' status and positions in every interation of the simulation.
In one iteration, a citizen moves ca. 10 meters, and a citizen on a scooter moves about 10x faster.
If any issues should come up, please do contact me.

instructions:

there are 10 citizens, and 20 scooters on the square map, height=width=20km.
to initialize it, please insert longitude and latitude of the center of preferred location, preferably with many streets and paths etc.
then close the window.
the simulation runs for 1000iterations for every citizen by default.


legend:

there are 7 cases of citizen-scooter interaction:

[cases of walking:]

1st case:
citizen has no map, so he:
-finds the closest scooter
-gets the directions from google api to it
-makes his first move in the direction of Google's first step (walking paths are suggested by google)

2nd case:
citizen knows where to go, but isn't there yet.
he makes his moves and checks which google step he is on (column 3 in txt file).

3rd case:
-citizen is on his last step - he makes it, and then finds out that the scooter he was aiming for is already taken.
-he decides to find another one.
-he has to get new google directions for that.

4th case:
-citizen is on his last step - he makes it, and this time the scooter is free to take!
-he starts driving it, and throws away the map he was using (or the google route).

[cases of driving:]

5th case:
citizen is on scooter, he has no map. He wants to go to his destination.
he gets directions from google maps. 
he makes his first move, so does his scooter - their velocity (jump) is bigger than in walking mode. they also use driving roads, not walking paths as before. they avoid highways.

6th case:
citizen is on scooter. he knows where he's going.
he makes his progress, and checks the google step he is on.

7th case:
citizen is finally in his destination.
he leaves his scooter free to take there.
his status is back to 'walking'.
his map is thrown, and new destination is ordered for him (randomly).
he is told to wait there 100 iterations.


