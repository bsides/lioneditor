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
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using NUnit.Framework;
using NUnit.Fixtures;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Diagnostics;
using Animation;
using System.Drawing.Drawing2D;

namespace AnimationTests
{
    [TestFixture]
    public class AnimationTests
    {
		private class Background : Drawable
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
		
        Bitmap bmpButton;
        Bitmap bmpCard;
        Bitmap bmpFireball;
        Bitmap bmpMousePointer;
        Bitmap bmpNoX;
        Bitmap bmpNumberRow;
        Bitmap bmpUpArrow;
        List<Bitmap> horses;
        Form window;
		DrawManager dm;
        SpriteManager am;
        FpsTimer fpsTimer;

		int FPS = 28;
        int SPEED = 175;

        [TestFixtureSetUp]
        public void Init()
        {
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            bmpButton = new Bitmap(myAssembly.GetManifestResourceStream("Resources.btn-feedback.png"));
            bmpCard = new Bitmap(myAssembly.GetManifestResourceStream("Resources.dkl.png"));
            bmpFireball = new Bitmap(myAssembly.GetManifestResourceStream("Resources.fireball.png"));
            bmpMousePointer = new Bitmap(myAssembly.GetManifestResourceStream("Resources.mouse-pointer.png"));
            bmpNoX = new Bitmap(myAssembly.GetManifestResourceStream("Resources.no_x_small.png"));
            bmpNumberRow = new Bitmap(myAssembly.GetManifestResourceStream("Resources.trick-scores-2.png"));
            bmpUpArrow = new Bitmap(myAssembly.GetManifestResourceStream("Resources.uparrow.png"));

            horses = new List<Bitmap>();
            horses.Add(new Bitmap(myAssembly.GetManifestResourceStream("Resources.horse1.png")));
            horses.Add(new Bitmap(myAssembly.GetManifestResourceStream("Resources.horse2.png")));
            horses.Add(new Bitmap(myAssembly.GetManifestResourceStream("Resources.horse3.png")));
            horses.Add(new Bitmap(myAssembly.GetManifestResourceStream("Resources.horse4.png")));

            horses.Add(new Bitmap(myAssembly.GetManifestResourceStream("Resources.horse5.png")));
            horses.Add(new Bitmap(myAssembly.GetManifestResourceStream("Resources.horse6.png")));
            horses.Add(new Bitmap(myAssembly.GetManifestResourceStream("Resources.horse7.png")));
            horses.Add(new Bitmap(myAssembly.GetManifestResourceStream("Resources.horse8.png")));
  
            horses.Add(new Bitmap(myAssembly.GetManifestResourceStream("Resources.horse9.png")));
            horses.Add(new Bitmap(myAssembly.GetManifestResourceStream("Resources.horse10.png")));
            horses.Add(new Bitmap(myAssembly.GetManifestResourceStream("Resources.horse11.png")));
            horses.Add(new Bitmap(myAssembly.GetManifestResourceStream("Resources.horse12.png")));
            
            window = new Form();
            window.ClientSize = new Size(800, 600);
            window.StartPosition = FormStartPosition.CenterScreen;
            window.BackColor = Color.White;

			fpsTimer = new FpsTimer(FPS);

			dm = new DrawManager(window, fpsTimer);
            am = new SpriteManager(fpsTimer);
			dm.AddDrawable(new Background());
			dm.AddDrawable(am);

			window.Show();
        }

        [Test]
        public void TestRotation()
        {
            JustSitThereSprite sla = new JustSitThereSprite(new Point(400, 300), 5);
            sla.Bitmap = this.bmpNumberRow;
            sla.AddAlteration(new Rotate(90));
            am.AddObject(sla);
			
            RunAnimationTest();
        }

        [Test]
        public void TestJustSitThere()
        {
            JustSitThereSprite sla1 = new JustSitThereSprite(new Point(400, 300), 5);
            sla1.Bitmap = this.bmpCard;
            sla1.AddAlteration(new RotateSpan(SpriteManager.FPS, 180));
            am.AddObject(sla1);

            RunAnimationTest();
        }

        [Test]
        public void TestJustSitThereTimed()
        {
            JustSitThereSprite sla1 = new JustSitThereSprite(new Point(400, 300), 5.0d);
            sla1.Bitmap = this.bmpCard;
            sla1.AddAlteration(new RotateSpan(SpriteManager.FPS, 180));
            am.AddObject(sla1);

            RunAnimationTest();
        }

		[Test]
        public void TestRotationSpan()
        {
            StraightLineSprite sla1 = new StraightLineSprite(150, new Point(400, 0), new Point(400, 500));
            sla1.Bitmap = this.bmpNumberRow;
            sla1.AddAlteration(new RotateSpan(sla1.Steps, 180));
            am.AddObject(sla1);

            StraightLineSprite sla2 = new StraightLineSprite(300, new Point(0, 299), new Point(500, 299));
            sla2.Bitmap = this.bmpButton;
            sla2.AddAlteration(new RotateSpan(sla2.Steps, 180));
            am.AddObject(sla2);

            RunAnimationTest();
        }

        [Test]
        public void TestStraightLineAnimation()
        {
//            StraightLineSprite sla = new StraightLineSprite(SPEED, new Point(0, 0), new Point(799, 599));
//            sla.Bitmap = this.bmpCard;
//            am.AddObject(sla);
//
//            StraightLineSprite sla2 = new StraightLineSprite(SPEED, new Point(799, 599), new Point(0,0));
//            sla2.Bitmap = this.bmpCard;
//            am.AddObject(sla2);
//
//            StraightLineSprite sla3 = new StraightLineSprite(SPEED, new Point(799,0), new Point(0, 599));
//            sla3.Bitmap = this.bmpCard;
//            am.AddObject(sla3);
//
//            StraightLineSprite sla4 = new StraightLineSprite(SPEED, new Point(0, 599), new Point(799, 0));
//            sla4.Bitmap = this.bmpCard;
//            am.AddObject(sla4);

			StraightLineSprite sla5 = new StraightLineSprite(100, new Point(0, 0), new Point(799, 0));
            sla5.Bitmap = this.bmpCard;
            am.AddObject(sla5);
			
            RunAnimationTest();
        }

        [Test]
        public void TestStraightLineAnimationTranslation()
        {
            StraightLineSprite sla = new StraightLineSprite(SPEED, new Point(799, 0), new Point(0, 599));
            sla.Bitmap = this.bmpCard;
            sla.AddAlteration(new Translation(new Size(-32,-45)));
            am.AddObject(sla);

            RunAnimationTest();
        }

        [Test]
        public void TestStraightLineAnimationRotation()
        {
            StraightLineSprite sla = new StraightLineSprite(230, new Point(799, 599), new Point(0, 0));
            sla.Bitmap = this.bmpCard;
            sla.AddAlteration(new Rotate(360));
            am.AddObject(sla);

            RunAnimationTest();
        }

        [Test]
        public void TestStraightLineAnimationTranslationRotation()
        {
            StraightLineSprite sla = new StraightLineSprite(250, new Point(799, 0), new Point(399, 299));
            sla.Bitmap = this.bmpCard;
            sla.AddAlteration(new Rotate(-360));
            sla.AddAlteration(new Translation(new Size(-32, -45)));
            am.AddObject(sla);

            RunAnimationTest();
        }

        [Test]
        public void TestOrientedObject()
        {
            StraightLineSprite sla = new StraightLineSprite(SPEED, new Point(0, 100), new Point(800, 500));
            sla.Bitmap = this.bmpUpArrow;
            sla.AddAlteration(new Oriented(new Point(399, 299))); // new Point(this.bmpUpArrow.Width/2,this.bmpUpArrow.Height/2)));
            sla.AddAlteration(new Translation(new Size(-26, 0)));
            am.AddObject(sla);

            StraightLineSprite sla1 = new StraightLineSprite(SPEED, new Point(600, 0), new Point(400, 500));
            sla1.Bitmap = this.bmpUpArrow;
            sla1.AddAlteration(new Oriented(new Point(399, 299))); // new Point(this.bmpUpArrow.Width/2,this.bmpUpArrow.Height/2)));
            sla1.AddAlteration(new Translation(new Size(-26, 0)));
            am.AddObject(sla1);

            RunAnimationTest();
        }

        //    StraightLineSprite sla2 = new StraightLineSprite(SPEED, new Point(799, 0), new Point(0, 599), new Point(26, 0));
        //    sla2.Bitmap = this.bmpUpArrow;
        //    sla2.AddAlteration(new Oriented(new Point(399, 299), new Point(26, 0))); // new Point(this.bmpUpArrow.Width/2,this.bmpUpArrow.Height/2)));
        //    am1.AddObject(sla2);

        //    StraightLineSprite sla3 = new StraightLineSprite(SPEED, new Point(799, 599), new Point(0, 0), new Point(26, 0));
        //    sla3.Bitmap = this.bmpUpArrow;
        //    sla3.AddAlteration(new Oriented(new Point(399, 299), new Point(26, 0))); // new Point(this.bmpUpArrow.Width/2,this.bmpUpArrow.Height/2)));
        //    am1.AddObject(sla3);

        //    StraightLineSprite sla4 = new StraightLineSprite(SPEED, new Point(0, 599), new Point(799, 0), new Point(26, 0));
        //    sla4.Bitmap = this.bmpUpArrow;
        //    sla4.AddAlteration(new Oriented(new Point(399, 299), new Point(26, 0))); // new Point(this.bmpUpArrow.Width/2,this.bmpUpArrow.Height/2)));
        //    am1.AddObject(sla4);
            
        //    RunAnimationTest();
        //}

        [Test]
        public void TestRevolutionAnimation()
        {
            RevolutionSprite byTime = new RevolutionSprite(new Point(300, 300), new Point(400, 300), 1.1d, 3.0d);
            byTime.Bitmap = this.bmpMousePointer;
            am.AddObject(byTime);

            RunAnimationTest();
        }

		[Test]
        public void TestRevolutionAnimationTimed()
        {
            RevolutionSprite byTime = new RevolutionSprite(new Point(299, 299), new Point(399, 299), 3d, 5.0d);
            byTime.Bitmap = this.bmpMousePointer;
            am.AddObject(byTime);

            RevolutionSprite bySpeed = new RevolutionSprite(new Point(299, 299), new Point(399, 299), -(int)150, 1.0d);
            bySpeed.Bitmap = this.bmpCard;
            am.AddObject(bySpeed);

            RunAnimationTest();
        }
		
        [Test]
        public void TestRevolutionWithOrient()
        {
            RevolutionSprite arrow = new RevolutionSprite(new Point(299, 299), new Point(399, 299), (int)150,5);
            arrow.Bitmap = this.bmpUpArrow;
            arrow.AddAlteration(new Oriented(new Point(399, 299)));
            arrow.AddAlteration(new Translation(new Size(-26,-103)));
            am.AddObject(arrow);

            RevolutionSprite arrow2 = new RevolutionSprite(new Point(499, 299), new Point(399, 299), (int)150,6);
            arrow2.Bitmap = this.bmpUpArrow;
            arrow2.AddAlteration(new Oriented(new Point(399, 299)));
            arrow2.AddAlteration(new Translation(new Size(-26, 0)));
            am.AddObject(arrow2);

            RunAnimationTest();
        }


        [Test]
        public void TestRevolutionWithRotation()
        {
            RevolutionSprite arrow = new RevolutionSprite(new Point(299, 299), new Point(399, 299), (int)150, 5);
            arrow.Bitmap = this.bmpCard;
            arrow.AddAlteration(new Rotate(360));
            arrow.AddAlteration(new Translation(new Size(-32, -45)));
            am.AddObject(arrow);

            RunAnimationTest();
        }

        [Test]
        public void TestMulti()
        {
            RevolutionSprite ra = new RevolutionSprite(new Point(300, 200), new Point(400, 300), 3d, 6);
            ra.Bitmap = bmpCard;
            ra.AddAlteration(new Rotate(720));
            ra.AddAlteration(new Translation(new Size(-32, -45)));
            am.AddObject(ra);

            StraightLineSprite sa = new StraightLineSprite(200, new Point(0, 0), new Point(799, 599));
            sa.Bitmap = bmpCard;
            sa.AddAlteration(new Translation(new Size(-32, -45)));
            am.AddObject(sa);

            RunAnimationTest();
        }

        [Test]
        public void TestMatrix()
        {
            JustSitThereSprite j = new JustSitThereSprite(new Point(0, 0), 5.0d);
            j.Bitmap = bmpButton;
			
			// the 2 transformations should be cummulative
            Matrix mTrans = new Matrix( 1.0f, 0.0f, 0.0f, 1.0f, 400.0f, 300.0f );
            j.AddAlteration(new MatrixAlteration(mTrans));
			Matrix mTrans2 = new Matrix( 1.0f, 0.0f, 0.0f, 1.0f, 200.0f, 200.0f );
            j.AddAlteration(new MatrixAlteration(mTrans2));

            am.AddObject(j);

            RunAnimationTest();
        }

        [Test]
        public void TestFlip()
        {
            JustSitThereSprite horizontal = new JustSitThereSprite(new Point(400, 100), 5.0d);
            horizontal.Bitmap = bmpButton;
            horizontal.AddAlteration(new Flip(Flip.FlipType.Horizontal));
			horizontal.AddAlteration(new Translation(new Size(-bmpButton.Width/2, -bmpButton.Height/2)));

            am.AddObject(horizontal);

            JustSitThereSprite vertical = new JustSitThereSprite(new Point(400, 200), 5.0d);
            vertical.Bitmap = bmpButton;
            vertical.AddAlteration(new Flip(Flip.FlipType.Vertical));
			vertical.AddAlteration(new Translation(new Size(-bmpButton.Width/2, -bmpButton.Height/2)));

            am.AddObject(vertical);

            JustSitThereSprite both = new JustSitThereSprite(new Point(400, 300), 5.0d);
            both.Bitmap = bmpButton;
            both.AddAlteration(new Flip(Flip.FlipType.Both));
			both.AddAlteration(new Translation(new Size(-bmpButton.Width/2, -bmpButton.Height/2)));

            am.AddObject(both);

            RunAnimationTest();
        }

        [Test]
        public void TestFlipBook()
        {
            StraightLineSprite s = new StraightLineSprite(200, new Point(600, 200), new Point(0, 200));
            FlipBook fb = new FlipBook(horses,0.15d);
            fb.Loop = true;
            s.AddAlteration(fb);
            am.AddObject(s);

            RevolutionSprite r = new RevolutionSprite(new Point(300, 300), new Point(400, 300), -130, 10.0d);
            r.Bitmap = horses[0];
            r.AddAlteration(fb);
            r.AddAlteration(new Rotation(180d));
            r.AddAlteration(new Oriented(new Point(400, 300)));
            r.AddAlteration(new Translation(new Size(-91, -110)));

            am.AddObject(r);
            
            RunAnimationTest();
        }

		[Test]
		public void TestBezierCalculations()
		{
			Point p1 = new Point(0, -12);
			Point p2 = new Point(5, 0);
			int d = (int)(BezierSprite.Hypot(p1, p2));
			Assert.That(d == 13);
			
			Point [] points = new Point[4];
			points[0] = new Point(0, 0);
			points[1] = new Point(0, 0);
			points[2] = new Point(12, 5);
			points[3] = new Point(12, 5);
			double result = BezierSprite.CalculateRatioSizeToPixelAccuracy(points, 1);
			Assert.That(BezierSprite.IsBetween(result, 0.089d, 1.011d));
			
			double cl = BezierSprite.CalculateCurveLength(points);
			Console.WriteLine("curve length1=" + cl);
			Assert.That(BezierSprite.IsBetween(cl, 13-1, 13+1));

		
			Point [] points2 = new Point[4];
			points2[0] = new Point(-10, 0);
			points2[1] = new Point(-5, 0);
			points2[2] = new Point(5, 0);
			points2[3] = new Point(10, 0);
			double cl2 = BezierSprite.CalculateCurveLength(points2);
			Console.WriteLine("curve length2=" + cl2);
			Assert.That(BezierSprite.IsBetween(cl2, 20-1, 20+1));

		
		}

        [Test]
        public void TestBezierSprite()
        {
			string strPoints1 =
				"175,235 196,192 217,216 " +
				"238,240 394,-12 502,16 " +
				"600,42 620,253 568,288 " +
				"515,323 535,328 515,323 " +
				"493,317 526,278 507,268 " +
				"494,261 464,264 454,273 " +
				"427,298 502,354 473,378 " +
				"464,385 436,386 425,380 " +
				"410,371 431,343 415,336 " +
				"364,314 257,233 225,376 " +
				"193,519 215,500 101,371 " +
				"35,295 40,383 96,314 " +
				"156,238 178,237 175,235";
 
				
//				"100,100 100,100 200,100 " +
//				"450,100 450,100 700,100";

//				"169,186 40,121 85,262 " +
//				"130,403 153,699 271,532 " +
//				"390,366 676,331 633,475 " +
//				"589,619 726,631 735,400 " +
//				"744,169 763,-33 639,34 " +
//				"514,102 541,178 600,250 " +
//				"658,322 759,427 555,342 " +
//				"351,256 447,151 429,103 " +
//				"411,55 358,-66 274,45 " +
//				"190,156 502,315 367,346 " +
//				"232,378 181,286 186,195";

            string strPoints2 = "0,100 400,100 400,100";
            string strPoints3 = "0,300 800,300 800,300";

            BezierSprite bs = new BezierSprite(new Point(0,100), strPoints1, 0.01d, 1);
			bs.Bitmap = bmpNoX;
			bs.SetSpeed(400);
//return;
			bs.AddAlteration(new Translation(new Size(-64, -64)));
            am.AddObject(bs);

            BezierSprite bs1 = new BezierSprite(new Point(0, 100), strPoints2, 0.01d, 1);
            bs1.Bitmap = bmpFireball;
			bs1.SetSpeed(100);
            bs1.AddAlteration(new Translation(new Size(-64, -64)));
            am.AddObject(bs1);

            BezierSprite bs2 = new BezierSprite(new Point(0, 300), strPoints3, 0.01d, 1);
			bs2.SetSpeed(50);
            bs2.Bitmap = bmpFireball;
            bs2.AddAlteration(new Translation(new Size(-64, -64)));
            am.AddObject(bs2);

//            StraightLineSprite sl = new StraightLineSprite(100, new Point(0, 200), new Point(800, 200));
//            sl.Bitmap = bmpCard;
//            sl.AddAlteration(new Translation(new Size(-32, -45)));
//            am.AddObject(sl);
long dt1 = DateTime.Now.Ticks;
            RunAnimationTest();
Console.WriteLine("time diff: " + (DateTime.Now.Ticks - dt1)/10000000);
        }
		
		[Test]
		public void TestBezierSpriteTangent()
		{
			string strPoints1 =
			"175,235 196,192 217,216 " +
			"238,240 394,-12 502,16 " +
			"600,42 620,253 568,288 " +
			"515,323 535,328 515,323 " +
			"493,317 526,278 507,268 " +
			"494,261 464,264 454,273 " +
			"427,298 502,354 473,378 " +
			"464,385 436,386 425,380 " +
			"410,371 431,343 415,336 " +
			"364,314 257,233 225,376 " +
			"193,519 215,500 101,371 " +
			"35,295 40,383 96,314 " +
			"156,238 178,237 175,235";
				
//			"40,442 123,394 178,435 " +
//			"234,475 291,544 334,504 " +
//			"378,463 490,400 525,402 " +
//			"559,403 654,405 684,421 " +
//			"714,438 742,462 742,462";

//			"54,370 316,229 397,252 " +
//            "544,268 699,432 759,310";

//            "73,285 141,414 141,414 " +
//            "141,414 174,472 217,505";

//			"150,100 450,100 500,500 " +
//          "450,550 150,550 100,500";
			
			// arrow following path
            BezierSprite bs1 = new BezierSprite(new Point(54,370), strPoints1, 0.01d, 1);
			bs1.Bitmap = bmpFireball;
			bs1.SetSpeed(50);
			bs1.AddAlteration(new Translation(new Size(-64,-64)));

			am.AddObject(bs1);

			// arrow following path
            BezierSprite bs2 = new BezierSprite(new Point(54,370), strPoints1, 0.01d);
			bs2.Bitmap = bmpUpArrow; // 54x103
			bs2.SetSpeed(50);
			bs2.AddAlteration(new RotateToTangent(bs2));
			bs2.AddAlteration(new Translation(new Size(-27,0)));

			am.AddObject(bs2);
			
            BezierSprite bs3 = new BezierSprite(new Point(54,370), strPoints1, 0.01d, 1);
			bs3.Bitmap = bmpUpArrow;
			bs3.SetSpeed(50);
			bs3.AddAlteration(new RotateToNormal(bs3));
			bs3.AddAlteration(new Translation(new Size(-27,0)));

			am.AddObject(bs3);

			RunAnimationTest();

		}

        [Test]
        public void TestBezierSpriteTimed()
        {
			string path = //"100,400 700,400, 700,400";
				"169,186 40,121 85,262 " +
				"130,403 153,699 271,532 " +
				"390,366 676,331 633,475 " +
				"589,619 726,631 735,400 " +
				"744,169 763,-33 639,34 " +
				"514,102 541,178 600,250 " +
				"658,322 759,427 555,342 " +
				"351,256 447,151 429,103 " +
				"411,55 358,-66 274,45 " +
				"190,156 502,315 367,346 " +
				"232,378 181,286 186,195";
			
			BezierSprite bs = new BezierSprite(new Point(30,30), path, 0.01d);
			bs.Bitmap = bmpFireball;
			bs.AddAlteration(new Translation(new Size(-64, -64)));
			//System.Console.WriteLine("TestBezierSpriteTimed ratio=" + bs.SetTimedRatio(10.0d));
			am.AddObject(bs);

//            StraightLineSprite sl = new StraightLineSprite(100, new Point(0, 200), new Point(800, 200));
//            sl.Bitmap = bmpCard;
//            sl.AddAlteration(new Translation(new Size(-32, -45)));
//            am.AddObject(sl);
//
			RunAnimationTest();
        }

//        [Test]
//        public void TestBezierSpriteSpeed()
//        {
//			throw new NotImplementedException("Thing to be tested here is known to be faulty or isn't done");
//			
//			string path = "100,0 700,0, 700,200";
//			
//			BezierSprite bs = new BezierSprite(new Point(100,200), path,0.1d,false);
//			bs.Bitmap = bmpFireball;
//			bs.SetSpeedRatio(100);
//			bs.AddAlteration(new Translation(new Size(-64, -64)));
//			am.AddObject(bs);
//
//			string path1 = "100,300 700,300, 700,300";
//
//			BezierSprite bs1 = new BezierSprite(new Point(100,300), path1,0.1d,false);
//			bs1.Bitmap = bmpFireball;
//			bs1.SetSpeedRatio(200);
//			bs1.AddAlteration(new Translation(new Size(-64, -64)));
//			am.AddObject(bs1);
//
//			string path2 = "100,400 700,400, 700,400";
//
//			BezierSprite bs2 = new BezierSprite(new Point(100,400), path2,0.1d,false);
//			bs2.Bitmap = bmpFireball;
//			bs2.SetSpeedRatio(300);
//			bs2.AddAlteration(new Translation(new Size(-64, -64)));
//			am.AddObject(bs2);
//
//			RunAnimationTest();
//		}

		[Test]
		public void TestText()
		{
			RevolutionSprite rs = new RevolutionSprite(new Point(200,300), new Point(400,300), 2.0d, 10.0d);
			rs.AddAlteration(new Rotate(720));
			Bitmap b = new Bitmap(100, 100);
			Graphics g = Graphics.FromImage(b);
			g.DrawString("Donkey Kong", new Font(FontFamily.GenericSerif, 17, GraphicsUnit.Pixel), Brushes.Black, new Rectangle(0,0,100,100));
			rs.Bitmap = b;
			am.AddObject(rs);

			RunAnimationTest();
		}

		[Test]
		public void TestTimer()
		{
			Stopwatch sw = new Stopwatch();
			FpsTimer f = new FpsTimer(24);
			double err = 0.0d;
			double startTime;
			double nextTime;

			f.Reset();

			double WAIT = 3000;
			int frameCount = 0;
			startTime = DateTime.Now.TimeOfDay.TotalMilliseconds;
			nextTime = startTime + 42; // 10 ms per frame
			sw.Start();
			f.Start();
			while(sw.ElapsedMilliseconds < WAIT)
			{
				if (f.Next())
				{
					// measure lateness
					double now = DateTime.Now.TimeOfDay.TotalMilliseconds;
					frameCount++;
					if (frameCount > 1) err += (now - nextTime);
					nextTime = now + 42;
				}
			}
			System.Console.WriteLine("  end (ms): " + DateTime.Now.TimeOfDay.TotalMilliseconds);
			System.Console.WriteLine("start (ms): " + startTime);
			System.Console.WriteLine("  err (ms): " + err);
			System.Console.WriteLine("frameCount: " + frameCount);
		}
		
		[Test]
		public void TestLoopControl()
		{
		}
		
        private void RunAnimationTest()
        {
			TimeSpan startTime = DateTime.Now.TimeOfDay;
			
            while (am.Running)
            {
                if (fpsTimer.Next())
					dm.DrawGame();
            }
			TimeSpan endTime = DateTime.Now.TimeOfDay;
			System.Console.WriteLine("  end: " + endTime);
			System.Console.WriteLine("start: " + startTime);
			System.Console.WriteLine(" diff: " + (endTime - startTime));
        }

		[Test]
		public void TestStringToPoints()
		{
			string a = "1,1 2,2 3,3";

			List<Point> l = BezierSprite.PointsStringToList(a);
			foreach (Point p in l)
                Console.WriteLine(p.ToString());
		}

		[Test]
		public void TestReflectPoint()
		{
			Point vertex = new Point(400,300);

			Point nw = new Point(300,200);
			Point ne = new Point(500,200);
			Point sw = new Point(300,400);
			Point se = new Point(500,400);

			Point rnw = BezierSprite.ReflectPoint(nw, vertex);
			Point rne = BezierSprite.ReflectPoint(ne, vertex);
			Point rsw = BezierSprite.ReflectPoint(sw, vertex);
			Point rse = BezierSprite.ReflectPoint(se, vertex);

			Assert.That(rnw.Equals(se));
			Assert.That(rne.Equals(sw));
			Assert.That(rsw.Equals(ne));
			Assert.That(rse.Equals(nw));
	
		}
		
		[Test]
		public void TestCalculateAngleBetweenPoints()
		{
			Point p1 = new Point(200,200);
			Point p2 = new Point(300,100);
			
			double result = BezierSprite.CalculateAngleBetweenPoints(p1, p2);
			Console.WriteLine("45: {0} {1} {2}", p1.ToString(), p2.ToString(), result);

			
			//{X=100,Y=500}, pt2={X=150,Y=100
			p1 = new Point(100, 500);
			p2 = new Point(150, 100);
			result = BezierSprite.CalculateAngleBetweenPoints(p1, p2);
			Console.WriteLine("{0} {1} {2}", p1.ToString(), p2.ToString(), result);
			
			p1 = new Point(200, 200);
			p2 = new Point(100, 100);
			result = BezierSprite.CalculateAngleBetweenPoints(p1, p2);
			Console.WriteLine("135: {0} {1} {2}", p1.ToString(), p2.ToString(), result);

			p2 = new Point(100, 300);
			result = BezierSprite.CalculateAngleBetweenPoints(p1, p2);
			Console.WriteLine("225: {0} {1} {2}", p1.ToString(), p2.ToString(), result);

			p2 = new Point(300, 300);
			result = BezierSprite.CalculateAngleBetweenPoints(p1, p2);
			Console.WriteLine("315: {0} {1} {2}", p1.ToString(), p2.ToString(), result);
		}

    }
}
