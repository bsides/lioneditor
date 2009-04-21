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
using Animation;
using NUnit.Fixtures;
using NUnit.Framework;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;

namespace DrawableTests
{
	[TestFixture]
	public class DrawableTests
	{
		Stopwatch sw;
		DrawManager dm;
		Form window;
		FpsTimer fpsTimer;
        SpriteManager am1, am2;

        Bitmap bmpButton;
        Bitmap bmpCard;
        Bitmap bmpFireball;
        Bitmap bmpMousePointer;
        Bitmap bmpNoX;
        Bitmap bmpNumberRow;
        Bitmap bmpUpArrow;

		int FPS = 36;
        //int SPEED = 175;

		public DrawableTests()
		{
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            bmpButton = new Bitmap(myAssembly.GetManifestResourceStream("Resources.btn-feedback.png"));
            bmpCard = new Bitmap(myAssembly.GetManifestResourceStream("Resources.dkl.png"));
            bmpFireball = new Bitmap(myAssembly.GetManifestResourceStream("Resources.fireball.png"));
            bmpMousePointer = new Bitmap(myAssembly.GetManifestResourceStream("Resources.mouse-pointer.png"));
            bmpNoX = new Bitmap(myAssembly.GetManifestResourceStream("Resources.no_x_small.png"));
            bmpNumberRow = new Bitmap(myAssembly.GetManifestResourceStream("Resources.trick-scores-2.png"));
            bmpUpArrow = new Bitmap(myAssembly.GetManifestResourceStream("Resources.uparrow.png"));

            window = new Form();
            window.ClientSize = new Size(800, 600);
            window.StartPosition = FormStartPosition.CenterScreen;
            window.BackColor = Color.White;

            fpsTimer = new FpsTimer(FPS);
			dm = new DrawManager(window, fpsTimer);
			sw = new Stopwatch();
            am1 = new SpriteManager(fpsTimer);
            am2 = new SpriteManager(fpsTimer);

			sw.Reset();
			sw.Start();

			window.Show();
		}

		class Background : Drawable
		{
			public void Draw(Graphics p_g)
			{
                p_g.FillPolygon(Brushes.AliceBlue, new Point[] { new Point(0, 0), new Point(399, 0), new Point(399, 299) });
                p_g.FillPolygon(Brushes.Bisque, new Point[] { new Point(399, 0), new Point(799, 0), new Point(399, 299) });
                p_g.FillPolygon(Brushes.CadetBlue, new Point[] { new Point(799, 0), new Point(799, 299), new Point(399, 299) });
                p_g.FillPolygon(Brushes.DarkBlue, new Point[] { new Point(399, 299), new Point(799, 299), new Point(799, 599) });
                p_g.FillPolygon(Brushes.Firebrick, new Point[] { new Point(399, 299), new Point(799, 599), new Point(399, 599) });
                p_g.FillPolygon(Brushes.Gainsboro, new Point[] { new Point(399, 299), new Point(399, 599), new Point(0, 599) });
                p_g.FillPolygon(Brushes.Honeydew, new Point[] { new Point(0, 299), new Point(399, 299), new Point(0, 599) });
                p_g.FillPolygon(Brushes.IndianRed, new Point[] { new Point(0, 0), new Point(399, 299), new Point(0, 299) });
                p_g.FillEllipse(Brushes.Khaki, new Rectangle(300, 200, 200, 200));
                p_g.FillEllipse(Brushes.Black, new Rectangle(399, 299, 2, 2));
			}
		}

        class ForeGround : Drawable
        {
            Region r;

            public ForeGround()
            {
                r = new Region(new Rectangle(200, 100, 400, 400));
                r.Exclude(new Rectangle(250, 150, 300, 300));
            }

            public void Draw(Graphics p_g)
            {
                p_g.FillRegion(Brushes.Green, r);
            }
        }
		
		[Test]
		public void Test1()
		{
			dm.AddDrawable(new Background());
            dm.AddDrawable(am1);
            dm.AddDrawable(new ForeGround());
            dm.AddDrawable(am2);

            RevolutionSprite ra1 = new RevolutionSprite(new Point(300, 300), new Point(400, 300), 2d, 5.0d);
            ra1.Bitmap = bmpNumberRow;
            ra1.AddAlteration(new Translation(new Size(-150, -17)));
            am1.AddObject(ra1);

            RevolutionSprite ra2 = new RevolutionSprite(new Point(200, 300), new Point(400, 300), -2d, 5.0d);
            ra2.AddAlteration(new Translation(new Size(-64, -64)));
            ra2.Bitmap = bmpFireball;
            am2.AddObject(ra2);

			RunDrawablesTest();
		}
	
		public void RunDrawablesTest()
		{
			while (sw.ElapsedMilliseconds < 6000)
			{
                dm.DrawGame();
			}
		}
	}
}
