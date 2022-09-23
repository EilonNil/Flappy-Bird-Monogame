using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.DirectWrite;

namespace FlappyBird
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        //importing the other classes
        Collision c = new Collision(); //checking for colission
        MovePillars moveP = new MovePillars(); //used to move the pillars when needed
        static Random rand1 = new Random();
        Pillar[] pillars = new Pillar[4]; // array of the pillars
        MoveBird mov = new MoveBird(); //used to move the bird

        //general
        SpriteFont font; //font used to display text
        bool startGame = false; //used to start the game after the player pressed spacebar
        float times = 0f; //time elapsed
        int speed = 2;
        int score = 0;
        bool end = false;
        float timeSinceEnd = 0f;

        // bird
        Rectangle posB;
        Texture2D bird;


        // top pillars
        Rectangle posTP1 = new Rectangle(800, rand1.Next(30,270) - 400, 107, 400); //posiion of top pillar #1
        Rectangle posTP2 = new Rectangle(800, rand1.Next(30, 270) - 400, 107, 400); //posiion of top pillar #2
        Pillar topP1;
        Pillar topP2;


        // bottom pillars
        Rectangle posBP1 = new Rectangle(800, 0, 107, 400); //posiion of bottom pillar #1 (height will be adjusted)
        Rectangle posBP2 = new Rectangle(800, 0, 107, 400); //posiion of bottom pillar #2 (height will be adjusted)
        Pillar btmP1;
        Pillar btmP2;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {   
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //bird
            posB = new Rectangle(50, 300, 70, 50);
            bird = Content.Load<Texture2D>("birdie");

            //top pillars
            topP1 = new Pillar(posTP1, Content.Load<Texture2D>("pipetop"));
            topP2 = new Pillar(posTP2, Content.Load<Texture2D>("pipetop"));

            //bottom pillars
            btmP1 = new Pillar(posBP1, Content.Load<Texture2D>("pipebtm"));
            btmP2 = new Pillar(posBP2, Content.Load<Texture2D>("pipebtm"));
            btmP1.pos.Y = topP1.pos.Y + 580; //adjusting the height to always be the same distance from the top pillar
            btmP2.pos.Y = topP2.pos.Y + 580;

            //moving the pillars into an array
            pillars[0] = topP1;
            pillars[1] = topP2;
            pillars[2] = btmP1;
            pillars[3] = btmP2;

            //loading a font to display scoreboard
            font = Content.Load<SpriteFont>("File");

            
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && startGame == true)
            {
                mov.moveBird(ref posB);
            }
            //save the time elapsed in a variable. the use is to know when the second set of pillars should start
            times += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (end)
            {
                timeSinceEnd += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if (timeSinceEnd > 2000)
                Exit();
            Draw(gameTime);
            base.Update(gameTime);
            
        }

        protected override async void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //drawing the bird and pillars
            _spriteBatch.Begin();
            _spriteBatch.Draw(bird, posB, Color.White);
            for (int i = 0; i < 4; i++)
            {
                _spriteBatch.Draw(pillars[i].tp, pillars[i].pos, Color.White);
            }
            //display score
            _spriteBatch.DrawString(font, "Score: " + score, new Vector2(600, 0), Color.White);

            if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space) && !startGame) //starting the game
            {
                startGame = true;
                times = 0;
            }
            if (startGame)
            {
                //the bird always keeps on falling down
                posB.Y += 2;
                _spriteBatch.Draw(bird, posB, Color.White);

                //pillars are moving
                moveP.move(ref topP1, ref btmP1, ref speed, ref _spriteBatch);

                //display score (done here too so it stays above the pillars)
                _spriteBatch.DrawString(font, "Score: " + score, new Vector2(600, 0), Color.White);



                if (times > 2000)
                {
                    moveP.move(ref topP2, ref btmP2, ref speed, ref _spriteBatch);

                    //display score (done here too so it stays above the pillars)
                    _spriteBatch.DrawString(font, "Score: " + score, new Vector2(600, 0), Color.White);
                     
                }

                for (int i = 0; i < 4; i++)
                {
                    if (c.isCollide(posB, pillars[i].pos)) 
                    {
                        c.handleCollision(ref end, ref pillars, ref posB, ref _spriteBatch, ref score);
                        
                    }
                }

                if (topP1.pos.X < -200)
                { //sending the pillars back to the start
                    moveP.sendBack(ref topP1, ref btmP1, ref score, ref speed);
                }

                if (topP2.pos.X < -200)
                { //sending the pillars back to the start
                    moveP.sendBack(ref topP2, ref btmP2, ref score, ref speed);
                }

            } else { //showing insturctions to start the game only when it hasn't begun yet
                _spriteBatch.DrawString(font, "Press the spacebar to start", new Vector2(200, 170), Color.White);

            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}