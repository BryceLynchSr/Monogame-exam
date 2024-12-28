using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using SharpDX.DirectWrite;


namespace DouglasGame
{
    public class Game1 : Game
    {
        // Deklarear variabler för bilden katt som en Texture2D.
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D katt;

       
        // Deklarear variabler för katterna av som rektanglar.
        Rectangle grönKattRect;
        Rectangle rödKattRect;
        Rectangle rödKattRect2;
        // Deklarear min keyboard och döper den till tangentbord.
        KeyboardState tangentbord = Keyboard.GetState();
        // Deklarerar en lista av rektanglar för de röda katterna.
        List<Rectangle> rödaKatter = new List<Rectangle>();

        // DEklarerar min ljudeffekt som är applåder.
        SoundEffect applåder;
        // Deklarerar min spritefont för poängräkning.
        SpriteFont poäng;

        // antal röda katter från början.
        int startAntalRödaKatter = 1;
        // Deklarérar variabel av typen heltal för poängräknare och ger den värdet noll.
        int räknare = 0;
        // Deklarerar en position för poängen som en Vector 2.
        Vector2 poängPosition = new Vector2(700, 50);
       
        // Deklarerar en variabel av typen Random för mina slumptal.
        Random slump = new Random();
        // Deklarerar en variabel av typen heltal för ännu en räknare.
        int antal = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // musen synlig från början.
            IsMouseVisible = true;

           

            base.Initialize();
        }

        // Metod för att ladda upp mitt innehåll.
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // Laddar upp min kattbild
            katt = Content.Load<Texture2D>("kvadrat");
            // Laddar upp min ljudeffelt som är applåder.
            applåder = Content.Load<SoundEffect>("applause_y");
            //Laddar upp min spreitefont för poängräknaren.
            poäng = Content.Load<SpriteFont>("poäng");
            // Laddar upp en ny rektangel för min gröna katt
            grönKattRect = new Rectangle(200, 100, katt.Width, katt.Height);
            // Laddar upp rektangel för min röda katt.
            rödKattRect = new Rectangle (slump.Next(0, 750), slump.Next(0, 430), katt.Width, katt.Height);

           
            /*
            Rectangle rödKattRect2 = new Rectangle(nyKattX, nyKattY, katt.Width, katt.Height);
            */
            // Lägger till den nya katten  i listan för röda katter.

           
            rödaKatter.Add(rödKattRect);

        }
        // Metod som uppdaterar spelet 60 ggr i sekunden.
        protected override void Update(GameTime gameTime)
        {
           // hämtar information om status för mitt keyboard.
            tangentbord = Keyboard.GetState();

           

          // if-satser för att styra den gröna katten med piltangenter.
            if ( tangentbord.IsKeyDown(Keys.Up))
            {
                grönKattRect.Y -= 4;
            }

            if (tangentbord.IsKeyDown(Keys.Down))
            {
                grönKattRect.Y += 4;
            }

            if (tangentbord.IsKeyDown(Keys.Left))
            {
                grönKattRect.X -= 4;
            }

            if (tangentbord.IsKeyDown(Keys.Right))
            {
                grönKattRect.X += 4;
            }

            /*
            if (grönKattRect.Intersects(rödKattRect))
            {
                
                FörstaKattenTräffad();
                
            }
            */
            // Om den gröna katten träffar den röda anropas metoden FörstaKattenTräffad, ljudeffekten
            // applåder spelas och räknaren ökar med en poäng.
            if (grönKattRect.Intersects(rödKattRect))
            {
                FörstaKattenTräffad();
                räknare++;
  
            }

            base.Update(gameTime);
        }

        
        // Metod för vad som händer när den röda katten är träffad.
        void FörstaKattenTräffad()
        {

            
            // Slumpar fram ny x och y-position och skapar en ny rektangel för röd katt.
            int nyKattX = slump.Next(0, 800 - katt.Width);
            int nyKattY = slump.Next(0, 480 - katt.Height);
            Rectangle rödKattRect2 = new Rectangle(nyKattX, nyKattY, katt.Width, katt.Height);
            // Lägger till den nya katten  i listan för röda katter.
            rödaKatter.Add(rödKattRect2);
            rödKattRect = new Rectangle(nyKattX, nyKattY, katt.Width, katt.Height);


           

        }
        


        // Metod som ritar en ursprunglig grön och röd katt i spelfönstret.
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
           
            _spriteBatch.DrawString(poäng, räknare.ToString(),poängPosition, Color.Black);
            _spriteBatch.Draw(katt, grönKattRect, Color.Green);
            foreach (Rectangle kattRect in rödaKatter) {
                _spriteBatch.Draw(katt, kattRect, Color.Red);
            }
            /*
            _spriteBatch.Draw(katt, rödKattRect, Color.Red);
            */
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
