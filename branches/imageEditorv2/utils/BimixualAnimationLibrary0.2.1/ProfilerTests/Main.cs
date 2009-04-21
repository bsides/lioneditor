using System;
using System.Collections.Generic;
using Animation;
using System.Threading;
using System.Windows.Forms;

namespace ProfilerTests
{
	class MainClass
	{
		static List<double> list;
		static int LENGTH = 10000000;
		
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World! - profiler test methods - oh yeah!");
			
			//list = new List<double>();

			//for (double i = 0.0; i < LENGTH; i += 1.0d)
			//{
			//	list.Add(i);
			//}
			//MainClass.ListCopying();
			//MainClass.ListReading();
			
			//MainClass.TestFps();
			
//			MainClass.TestLoopControl();
		}
//		
//		public static void TestLoopControl()
//		{
//			FpsTimer f = new FpsTimer(36);
//			Form form = new Form();
//			
//			LoopControl.SetAction(form, loopControl);
//			LoopControl.FpsTimer = f;
//			
//			Application.Run(form);
//		}
//		
//		private static int count = 0;
//		public static void loopControl()
//		{
//			Console.Write("{0},",count++);
//		}
		
		public static void TestFps()
		{
			FpsTimer f = new FpsTimer(36);
			
			f.Reset();
			f.Start();
			TestFpsLoop(f,10);
		}
		
		public static void TestFpsLoop(FpsTimer p_f, int p_seconds)
		{
			for (int i = 0; i < p_seconds * p_f.FPS; i++)
			{
				while(!p_f.TimeElapsed());
				p_f.Reset();
				p_f.Start();
			}
		}
		
		public static void ListCopying()
		{
			for (int i = 0; i < LENGTH; i++)
				MainClass.TestGetRangeCopy();

			for (int i = 0; i < LENGTH; i++)
				MainClass.TestToArrayCopy();

		}
		
		public static void ListReading()
		{
			double [] a;
			
			a = list.GetRange(0,LENGTH).ToArray();
			
			MainClass.TestReadArray(a); // 20.963 ms
			MainClass.TestReadList(list); // 18491.792 ms
		}
		
		public static void TestReadArray(double [] a) // 20.963 ms
		{
			double t = 0.0d;
			
			for (int i = 0; i < LENGTH; i++)
				t = a[i];
		}
		
		public static void TestReadList(List<double> a) // 18491.792 ms
		{
			double t = 0.0d;
			
			for (int i = 0; i < LENGTH; i++)
				t = a[i];
		}

		public static void TestGetRangeCopy()
		{
			List<double> a;
			
			a = list.GetRange(0,LENGTH);
		}
		
		public static void TestToArrayCopy()
		{
			double [] a;
			
			a = list.GetRange(0,LENGTH).ToArray();
		}
	}
}