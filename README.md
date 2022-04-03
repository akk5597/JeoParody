# JeoParody
Submission for PackHacks 2022

## Inspiration
Jeopardy! game show has been a way to relax for millions of households since 1964. Every single person who has had a television has known of Jeopardy! and has dreamed of playing it. We bring it to you with local multiplayer to keep that nostalgic feeling of playing retro games with your buddies!

## What it does
JeoParody! is a game that essentially follows as a Jeopardy! game. There are 4 players who each have a specific key on the keyboard for a buzzer. The current player can select a question and then the first person to press the buzzer can answer it. You get points for getting it right but also lose them if you get it wrong!

## How we built it
The game has been built using the Unity Engine. The questions are preset. We built a WebGL project which is hosted on Netlify at: https://leafy-trifle-f117d5.netlify.app/

## Challenges we ran into
Initially we started out by making a Django server with each player on a single screen but we soon realized we would have reinvent the wheel in writing a lot of the processing that can be easily delegated to a game engine. Hence, we chose Unity to create the game.

## Accomplishments that we're proud of
We thought that the game would be fairly easy to make but soon ran into different data access issues. Switching out the entire plan within 24 hours is an achievement we are proud of!

## What we learned
Never not plan and design a software before beginning to write code.

## What's next for JeoParody!
Dependent on the feedback, we might expand it to online multiplayer!

