//Copyright 2009 Derek Duban
//This file is part of the Bimixual Animation Library.
//
//Bimixual Animation is free software: you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.
//
//Bimixual Animation is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License
//along with Bimixual Animation Library.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Animation;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace TestApplication
{
    /// <summary>
    /// Paint and processs
    /// </summary>
    public partial class TestForm : Form
    {
        DrawManager drawManager;			// Does all the drawing for the form
        FpsTimer fpsTimer;					// Controls timing of drawing
        AnimationScript animationScript;	// Orderly control of drawing

        /// <summary>
        /// Setup the drawing of the form
        /// </summary>
        public TestForm()
        {
            InitializeComponent();
            this.ClientSize = new Size( 800, 600 );
            Control c = new Control();
            c.Size = new Size(800, 600);
            c.Location = new Point(0, 0);
            Controls.Add(c);

            fpsTimer = new FpsTimer( 48 );	// frames per second

            LoopControl.SetAction( c, this.Go );
            LoopControl.FpsTimer = fpsTimer;
            drawManager = new DrawManager( c, fpsTimer );
            animationScript = new AnimationScript( drawManager );

            c.MouseClick += delegate
            {
                animationScript.Click();
            };
        }

        private void Go()
        {
            animationScript.Orchestrate();
            drawManager.DrawGame();
        }
    }

    class AnimationScript
    {
        private class Background : Drawable
        {
            public void Draw( Graphics p_g )
            {
                try
                {
                    p_g.FillRectangle( Brushes.AliceBlue, p_g.ClipBounds );
                }
                catch ( Exception e )
                {
                    System.Console.WriteLine( e.Message );
                    throw e;
                }
            }
        }

        private class TextBlock : Drawable
        {
            public Bitmap bmp; // because drawing strings is waaaaayyyy to slow
            public Rectangle rect;

            public TextBlock( Rectangle p_rect, Brush p_brush, string p_text )
            {
                try
                {
                    rect = p_rect;
                    bmp = new Bitmap( rect.Width + 1, rect.Height + 1 );

                    Graphics g = Graphics.FromImage( bmp );
                    g.FillRectangle( p_brush, new Rectangle( 0, 0, rect.Width, rect.Height ) );
                    g.DrawRectangle( Pens.Black, new Rectangle( 0, 0, rect.Width, rect.Height ) );

                    Rectangle textRect = new Rectangle( 0, 0, rect.Width, rect.Height );
                    textRect.Inflate( -5, -5 );
                    StringFormat sf = new StringFormat( StringFormatFlags.FitBlackBox );
                    g.DrawString( p_text, new Font( FontFamily.GenericMonospace, 10, GraphicsUnit.Pixel ), Brushes.Black, textRect, sf );
                }
                catch ( Exception e )
                {
                    System.Console.WriteLine( e.Message );
                    throw e;
                }
            }

            public void Draw( Graphics p_g )
            {
                try
                {
                    p_g.DrawImage( bmp, rect );
                }
                catch ( Exception e )
                {
                    System.Console.WriteLine( e.Message );
                    throw e;
                }
            }
        }

        DrawManager drawManager;
        Stopwatch stopwatch;
        int page;	// what page we are on
        int nextPage;	// what page we should turn to at next call to Orchestrate. If 0, do nothing
        SpriteManager spriteManager;
        List<Bitmap> horses;
        bool allowClick;
        private TextBlock textBlock1;
        private TextBlock textBlock2;
        private TextBlock textBlock3;
        RevolutionSprite horseSprite;
        Bitmap bmpFireball;

        public AnimationScript( DrawManager p_dm )
        {
            try
            {
                spriteManager = new SpriteManager( p_dm.FpsTimer );
                drawManager = p_dm;
                stopwatch = new Stopwatch();
                stopwatch.Reset();

                Assembly myAssembly = Assembly.GetExecutingAssembly();
                horses = new List<Bitmap>();
                horses.Add( new Bitmap( myAssembly.GetManifestResourceStream( "TestApplication.Resources.horse1.png" ) ) );
                horses.Add( new Bitmap( myAssembly.GetManifestResourceStream( "TestApplication.Resources.horse2.png" ) ) );
                horses.Add( new Bitmap( myAssembly.GetManifestResourceStream( "TestApplication.Resources.horse3.png" ) ) );
                horses.Add( new Bitmap( myAssembly.GetManifestResourceStream( "TestApplication.Resources.horse4.png" ) ) );

                horses.Add( new Bitmap( myAssembly.GetManifestResourceStream( "TestApplication.Resources.horse5.png" ) ) );
                horses.Add( new Bitmap( myAssembly.GetManifestResourceStream( "TestApplication.Resources.horse6.png" ) ) );
                horses.Add( new Bitmap( myAssembly.GetManifestResourceStream( "TestApplication.Resources.horse7.png" ) ) );
                horses.Add( new Bitmap( myAssembly.GetManifestResourceStream( "TestApplication.Resources.horse8.png" ) ) );

                horses.Add( new Bitmap( myAssembly.GetManifestResourceStream( "TestApplication.Resources.horse9.png" ) ) );
                horses.Add( new Bitmap( myAssembly.GetManifestResourceStream( "TestApplication.Resources.horse10.png" ) ) );
                horses.Add( new Bitmap( myAssembly.GetManifestResourceStream( "TestApplication.Resources.horse11.png" ) ) );
                horses.Add( new Bitmap( myAssembly.GetManifestResourceStream( "TestApplication.Resources.horse12.png" ) ) );

                bmpFireball = new Bitmap( myAssembly.GetManifestResourceStream( "TestApplication.Resources.fireball.png" ) );
            }
            catch ( Exception e )
            {
                System.Console.WriteLine( e.Message );
                throw e;
            }
        }

        public void Click()
        {
            if ( allowClick )//if (page != 5)// && page != 8 && page != 9)
            {
                nextPage = page + 1;
                Orchestrate();
            }
        }

        public void Orchestrate()
        {
            if ( !stopwatch.IsRunning )
            {
                Page1();
                page = 1;
                nextPage = 0;
                stopwatch.Start();

                allowClick = true;

                return;
            }


            if ( nextPage == 18 )
            {
                Page18();
                page = 18;
                nextPage = 0;
                stopwatch.Reset();
                stopwatch.Start();

                allowClick = true;

                return;
            }


            if ( nextPage == 17 )
            {
                Page17();
                page = 17;
                nextPage = 0;
                stopwatch.Reset();
                stopwatch.Start();

                allowClick = true;

                return;
            }

            if ( nextPage == 16 )
            {
                Page16();
                page = 16;
                nextPage = 0;
                stopwatch.Reset();
                stopwatch.Start();

                allowClick = true;

                return;
            }

            if ( nextPage == 15 )
            {
                Page15();
                page = 15;
                nextPage = 0;
                stopwatch.Reset();
                stopwatch.Start();

                allowClick = true;

                return;
            }

            if ( nextPage == 14 )
            {
                Page14RestartHorse();
                page = 14;
                nextPage = 0;
                stopwatch.Reset();
                stopwatch.Start();

                allowClick = true;

                return;
            }

            if ( nextPage == 13 )
            {
                Page13RunAwayHorse();
                page = 13;
                nextPage = 0;
                stopwatch.Reset();
                stopwatch.Start();

                allowClick = true;

                return;
            }

            if ( nextPage == 12 )
            {
                Page12FlippingHorse();
                page = 12;
                nextPage = 0;
                stopwatch.Reset();
                stopwatch.Start();

                allowClick = true;

                return;
            }

            if ( nextPage == 11 )
            {
                Page11TranslatedHorse();
                page = 11;
                nextPage = 0;
                stopwatch.Reset();
                stopwatch.Start();

                allowClick = true;

                return;
            }

            if ( ( page == 9 || page == 8 ) && stopwatch.ElapsedMilliseconds > 1000 )
            {
                Page10WhyOffCenter();
                page = 10;
                nextPage = 0;
                stopwatch.Reset();
                stopwatch.Start();

                allowClick = true;

                return;
            }

            if ( nextPage == 9 ) // only trigged by sprite event
            {
                Page9HorseStandCentered();
                page = 9;
                nextPage = 0;
                stopwatch.Reset();
                stopwatch.Start();

                allowClick = false;

                return;
            }

            if ( nextPage == 8 ) // only trigged by sprite event
            {
                Page8LetsStart();
                page = 8;
                nextPage = 0;
                stopwatch.Reset();
                stopwatch.Start();

                allowClick = false;

                return;
            }

            if ( nextPage == 7 ) // || (page == 6 && stopwatch.ElapsedMilliseconds > 2000))
            {
                Page7MoveHorseDown();
                page = 7;
                nextPage = 0;
                stopwatch.Reset();
                stopwatch.Start();

                allowClick = false;

                return;
            }

            if ( nextPage == 6 ) // || (page == 5 && stopwatch.ElapsedMilliseconds > 2000))
            {
                Page6();
                page = 6;
                nextPage = 0;
                stopwatch.Reset();
                stopwatch.Start();

                allowClick = true;

                return;
            }

            if ( nextPage == 5 ) // MUST be called by horse event from page 4  || (page == 4 && stopwatch.ElapsedMilliseconds > 1000))
            {
                Page5();
                page = 5;
                nextPage = 0;
                stopwatch.Reset();
                stopwatch.Start();

                allowClick = true;

                return;
            }
            if ( nextPage == 4 ) // || (page == 3 && stopwatch.ElapsedMilliseconds > 4000))
            {
                Page4();
                page = 4;
                nextPage = 0;
                stopwatch.Reset();
                stopwatch.Start();

                allowClick = false;

                return;
            }

            if ( nextPage == 3 ) // || (page == 2 && stopwatch.ElapsedMilliseconds > 4000))
            {
                Page3();

                page = 3;
                nextPage = 0;
                stopwatch.Reset();
                stopwatch.Start();

                allowClick = false;

                return;
            }


            if ( nextPage == 2 ) // || (page == 1 && stopwatch.ElapsedMilliseconds > 4000))
            {

                Page2();

                page = 2;
                nextPage = 0;
                stopwatch.Reset();
                stopwatch.Start();

                allowClick = true;

                return;
            }

        }

        private void Page1()
        {
            drawManager.AddDrawable( new Background() );
            drawManager.AddDrawable( spriteManager );

            horseSprite = new RevolutionSprite( new Point( 300, 300 ), new Point( 400, 300 ), 3d );
            horseSprite.Bitmap = horses[0];
            FlipBook fb = new FlipBook( horses.GetRange( 0, 11 ), 0.15d );
            fb.Loop = true;
            horseSprite.AddAlteration( fb );
            horseSprite.AddAlteration( new Rotation( 180d ) );
            horseSprite.AddAlteration( new Oriented( new Point( 400, 300 ) ) );
            horseSprite.AddAlteration( new Translation( new Size( -75, -110 ) ) );

            spriteManager.AddObject( horseSprite );

            string str =
                "These animation classes are based on scripted " +
                "overlays and sprites.\n\n" +
                "A form, like this one, is overlaid with any number " +
                "of 'Drawables'. Drawables are any class that " +
                "implement the Drawable interface.\n\n" +
                "The first Drawable for this form is the background. " +
                "The next Drawable is the SpriteManager (controlling the horse) followed by " +
                "this text box.\n\n" +
                "Click mouse...";

            textBlock1 = new TextBlock( new Rectangle( 20, 20, 300, 160 ), Brushes.White, str );

            drawManager.AddDrawable( textBlock1 );
        }

        private void Page2()
        {
            string str =
                "The SpriteManager accepts Sprites to its list of " +
                "objects to manage. Each Sprite accepts Alterations " +
                "which change the way the sprite is displayed. " +
                "More than one alteration can be applied to a sprite.\n\n" +
                "Sprite are positioned using coordinate mapping.\n" +
                "Alterations can alter the sprite bitmap or the Graphics " +
                "onwhich the sprite is drawn. This means matrix transformations " +
                "can be applied to sprites.\n\n" +
                "Click mouse...";
            textBlock2 = new TextBlock( new Rectangle( 480, 20, 300, 190 ), Brushes.White, str );
            drawManager.AddDrawable( textBlock2 );
        }

        private void Page3()
        {
            textBlock3 = new TextBlock( new Rectangle( 170, 118, 170, 55 ), Brushes.White, "Let's re-construct the horse" ); // >>>\n\nClick mouse...");	

            StraightLineSprite sls = new StraightLineSprite(
               500, new Point( 380, 20 ), new Point( 181, 118 ) );
            sls.eventOnCompletion += delegate( object sender, AnimationFinishedEventArgs e )
            {
                nextPage = 4;
            };
            sls.Bitmap = textBlock3.bmp;

            drawManager.RemoveDrawable( textBlock1 );
            drawManager.RemoveDrawable( textBlock2 );
            spriteManager.AddObject( sls );
        }

        private void Page4()
        {
            drawManager.AddDrawable( textBlock3 );

            horseSprite.eventOnCompletion += delegate( object sender, AnimationFinishedEventArgs e )
            {
                nextPage = 5;
            };
            horseSprite.StopAt( 88.0d );
        }

        //RevolutionSprite horseSprite;
        //private void AddRevolvingHorse()
        //{
        //    horseSprite = new RevolutionSprite(new Point(300, 300), new Point(400, 300), 3d);
        //    horseSprite.Bitmap = horses[0];
        //    FlipBook fb = new FlipBook(horses, 0.15d);
        //    fb.Loop = true;
        //    horseSprite.AddAlteration(fb);
        //    horseSprite.AddAlteration(new Rotation(180d));
        //    horseSprite.AddAlteration(new Oriented(new Point(400, 300)));
        //    horseSprite.AddAlteration(new Translation(new Size(-75, -110)));

        //    spriteManager.AddObject(horseSprite);

        //}

        private void Page5()
        {
            drawManager.RemoveDrawable( textBlock3 );
            textBlock3 = new TextBlock( new Rectangle( 181, 118, 170, 55 ), Brushes.White, "Let's re-construct the horse >>>\n\nClick mouse..." );
            drawManager.AddDrawable( textBlock3 );

            JustSitThereSprite js = new JustSitThereSprite( horseSprite.Location );
            js.AddAlteration( new Translation( new Size( -75, -110 ) ) );
            js.Bitmap = horses[11];
            spriteManager.AddObject( js );
        }

        private void Page6()
        {
            drawManager.RemoveDrawable( textBlock3 );
            textBlock3 = new TextBlock( new Rectangle( 170, 118, 170, 70 ), Brushes.White, "Actually, let's move him to the center first.\n\nClick mouse..." );
            drawManager.AddDrawable( textBlock3 );
        }

        JustSitThereSprite standingHorse;
        private void Page7MoveHorseDown()
        {
            // move horse and textblock down to center
            drawManager.RemoveDrawable( textBlock3 );
            textBlock3 = new TextBlock( new Rectangle( 181, 118, 160, 25 ), Brushes.White, "Down, down, down ..." );
            StraightLineSprite box = new StraightLineSprite( 200, new Point( 181, 118 ), new Point( 181, 280 ) );
            StraightLineSprite horse = new StraightLineSprite( 200, horseSprite.Location, new Point( horseSprite.Location.X, 300 ) );
            box.Bitmap = textBlock3.bmp;
            horse.Bitmap = horses[11];
            spriteManager.Stop();
            spriteManager.AddObject( box );
            spriteManager.AddObject( horse );

            // when the each arrive at center, make them sit there
            box.eventOnCompletion += delegate( object sender, AnimationFinishedEventArgs e )
            {
                nextPage = 8;
            };
            horse.eventOnCompletion += delegate( object sender, AnimationFinishedEventArgs e )
            {
                nextPage = 9;
            };

        }

        private void Page8LetsStart()
        {
            textBlock3 = new TextBlock( new Rectangle( 181, 280, 160, 25 ), Brushes.White, "Let's start" );
            drawManager.AddDrawable( textBlock3 );
        }

        private void Page9HorseStandCentered()
        {
            standingHorse = new JustSitThereSprite( new Point( 400, 300 ) );
            standingHorse.Bitmap = horses[11];
            spriteManager.AddObject( standingHorse );
        }

        private void Page10WhyOffCenter()
        {
            drawManager.RemoveDrawable( textBlock3 );
            string str =
               "Why is the horse down there? " +
               "Because the bitmap corner is drawn at the sprite location.\n\n" +
               "To center the horse, apply a Translation alteration.\n\n" +
               "Click your mouse ...";
            textBlock3 = new TextBlock( new Rectangle( 81, 280, 260, 100 ), Brushes.White, str );
            drawManager.AddDrawable( textBlock3 );
        }

        private void Page11TranslatedHorse()
        {
            drawManager.RemoveDrawable( textBlock3 );
            standingHorse.AddAlteration( new Translation( new Size( -75, -68 ) ) );
            string str =
               "Ok, now we have the horse centered.\n" +
               "Next we will apply a Flipbook alteration using a number of " +
               "horse images.\n\n" +
               "Click your mouse ...";
            textBlock3 = new TextBlock( new Rectangle( 81, 280, 260, 85 ), Brushes.White, str );
            drawManager.AddDrawable( textBlock3 );
        }

        private void Page12FlippingHorse()
        {
            drawManager.RemoveDrawable( textBlock3 );

            FlipBook fp = new FlipBook( horses.GetRange( 0, 11 ), 0.15d );
            fp.Loop = true;
            standingHorse.AddAlteration( fp );
            spriteManager.AddObject( standingHorse );

            string str =
               "Nice. But he doesn't seem to be getting anywhere..\n\n" +
               "Let's move him.\n\n" +
               "Click your mouse ...";
            textBlock3 = new TextBlock( new Rectangle( 81, 280, 260, 85 ), Brushes.White, str );
            drawManager.AddDrawable( textBlock3 );
        }

        private void Page13RunAwayHorse()
        {
            drawManager.RemoveDrawable( textBlock3 );

            standingHorse.Stop();
            StraightLineSprite sls = new StraightLineSprite( 1200, new Point( 400, 300 ), new Point( -160, 300 ) );
            FlipBook fp = new FlipBook( horses.GetRange( 0, 11 ), 0.15d );
            fp.Loop = true;
            sls.AddAlteration( new Translation( new Size( -75, -68 ) ) );
            sls.AddAlteration( fp );
            spriteManager.AddObject( sls );
            string str =
               "Nice. But he doesn't seem to be getting anywhere..\n\n" +
               "Let's move him.\n\n" +
               "Click your mouse ...";

            textBlock3 = new TextBlock( new Rectangle( 81, 280, 260, 85 ), Brushes.White, str );

            RevolutionSprite rs = new RevolutionSprite( new Point( 300, 300 ), new Point( 400, 300 ), -500 );
            rs.StopAt( 0.0d );
            rs.Bitmap = textBlock3.bmp;
            rs.AddAlteration( new Rotate( 720 ) );
            rs.AddAlteration( new Translation( new Size( -80, -30 ) ) );
            rs.eventOnCompletion += delegate
            {
                nextPage = 14;
            };
            spriteManager.AddObject( rs );

        }

        private void Page14RestartHorse()
        {
            string str =
            "Whoa! didn't think that through.\n" +
            "Let's revolve him around the center instead.\n\n" +
            "Click your mouse for take 2 ...";

            textBlock3 = new TextBlock( new Rectangle( 460, 270, 260, 85 ), Brushes.White, str );

            drawManager.AddDrawable( textBlock3 );
        }

        private void Page15()
        {
            string str =
            "That's better except that he is in orbit rather than running " +
         " running upon the circle. We'll add another alteration.\n\n" +
            "Click your mouse...";

            horseSprite = new RevolutionSprite( new Point( 300, 300 ), new Point( 400, 300 ), 3.0d );
            FlipBook fp = new FlipBook( horses.GetRange( 0, 11 ), 0.1d );
            fp.Loop = true;
            horseSprite.AddAlteration( fp );
            horseSprite.AddAlteration( new Translation( new Size( -75, -68 ) ) );
            spriteManager.AddObject( horseSprite );

            drawManager.RemoveDrawable( textBlock3 );
            textBlock3 = new TextBlock( new Rectangle( 460, 270, 260, 85 ), Brushes.White, str );
            drawManager.AddDrawable( textBlock3 );
        }

        private void Page16()
        {
            horseSprite.Stop();
            horseSprite = new RevolutionSprite( new Point( 300, 300 ), new Point( 400, 300 ), 3d );
            horseSprite.Bitmap = horses[0];
            FlipBook fb = new FlipBook( horses.GetRange( 0, 11 ), 0.15d );
            fb.Loop = true;
            horseSprite.AddAlteration( fb );
            //horseSprite.AddAlteration(new Rotation(180d));
            horseSprite.AddAlteration( new Oriented( new Point( 400, 300 ) ) );
            horseSprite.AddAlteration( new Translation( new Size( -75, -110 ) ) );

            spriteManager.AddObject( horseSprite );

            string str =
            "There we go. We just applyed an 'Oriented' alteration that " +
         "will point the top of the sprite always to the given point. " +
         "But now he is upside-down since it points the 'top' of the " +
         "sprite. So we gotta rotate him.\n\nClick mouse...";

            drawManager.RemoveDrawable( textBlock3 );
            textBlock3 = new TextBlock( new Rectangle( 460, 270, 260, 130 ), Brushes.White, str );
            drawManager.AddDrawable( textBlock3 );
        }

        private void Page17()
        {
            horseSprite.Stop();
            horseSprite = new RevolutionSprite( new Point( 300, 300 ), new Point( 400, 300 ), 3d );
            horseSprite.Bitmap = horses[0];
            FlipBook fb = new FlipBook( horses.GetRange( 0, 11 ), 0.15d );
            fb.Loop = true;
            horseSprite.AddAlteration( fb );
            horseSprite.AddAlteration( new Rotation( 180d ) );
            horseSprite.AddAlteration( new Oriented( new Point( 400, 300 ) ) );
            horseSprite.AddAlteration( new Translation( new Size( -75, -110 ) ) );

            spriteManager.AddObject( horseSprite );

            string str =
            "Now he is rotated and that also got him running in the right,  " +
         "direction.\n\nClick mouse...";

            drawManager.RemoveDrawable( textBlock3 );
            textBlock3 = new TextBlock( new Rectangle( 460, 270, 260, 60 ), Brushes.White, str );
            drawManager.AddDrawable( textBlock3 );
        }

        private void Page18()
        {
            Point start = new Point( 460, 270 );
            string path =
               "400,186 40,121 85,262 " +
               "130,403 153,499 271,532 " +
               "390,366 676,331 633,475 " +
               "589,519 726,431 735,400 " +
               "744,169 763,-33 639,34 " +
               "514,102 541,178 600,250 " +
               "658,322 759,427 555,342 " +
               "351,256 447,151 429,103 " +
               "411,55 358,-66 274,45 " +
               "190,156 502,315 367,346 " +
               "232,378 181,286 186,195";


            string str =
            "Oh yeah. You can also follow Bezier curves." +
         "\n\nThe end (of this demo.)";

            drawManager.RemoveDrawable( textBlock3 );
            textBlock3 = new TextBlock( new Rectangle( 460, 270, 260, 60 ), Brushes.White, str );
            BezierSprite bs = new BezierSprite( start, path, 0.005d, -1 );
            bs.Bitmap = textBlock3.bmp;
            bs.SetSpeed( 50 );
            spriteManager.AddObject( bs );
        }
    }
}
