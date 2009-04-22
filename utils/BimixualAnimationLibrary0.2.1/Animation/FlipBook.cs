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
using System.Drawing;

namespace Animation
{
    /// <summary>
    /// Change the sprite image over time. The image can change at set time intervals.
    /// </summary>
    public class FlipBook : AlterationInterface
    {
        List<Bitmap> bitmaps;   // bitmaps to flip through
        List<double> times;     // each item indicates how long (in seconds) to display each bitmap
        bool loop; public bool Loop { get { return loop; } set { loop = value; } }
        int curFrame;
        long lastTick;  // timer for how long each individual pic should show

        /// <summary>
        /// Create a FlipBook alteration which changes the sprite image at given time intervals
        /// </summary>
        /// <param name="p_bitmaps">List&lt;Bitmap&gt; of bitmaps to flip through</param>
        /// <param name="p_times">List&lt;double&gt; of times each bitmap should show. This list must be the same length as list of bitmaps</param>
        public FlipBook(List<Bitmap> p_bitmaps, List<double> p_times)
        {
            bitmaps = p_bitmaps;
            times = p_times;
            loop = false;
            curFrame = 0;
        }

        /// <summary>
        /// Create a FlipBook alteration which changes the sprite image at the given time
        /// </summary>
        /// <param name="p_bitmaps">List&lt;Bitmap&gt; of bitmaps to flip through</param>
        /// <param name="p_perFrame">How long to show each bitmap - ie: all the same amount of time</param>
        public FlipBook(List<Bitmap> p_bitmaps, double p_perFrame)
        {
            bitmaps = p_bitmaps;
            times = null;
            loop = false;
            curFrame = 0;

            // make a list with all the same times in it so we can use the same methodology as if they
            // provided their own list.
            times = new List<double>(p_bitmaps.Count);

            for (int i = 0; i < bitmaps.Count; i++)
                times.Add(p_perFrame);
        }

        /// <summary>
        /// Fulfill the alteratioin contract
        /// </summary>
        /// <param name="p_g">Graphics contract</param>
        /// <param name="p_point">Point where sprite is drawn</param>
        /// <param name="p_bitmap">ref Bitmap that we change to point to a bitmap of the given list of bitmaps</param>
        public void ApplyAlteration(Graphics p_g, Point p_point, ref Bitmap p_bitmap)
        {
            if (bitmaps.Count == 0) return;

            p_bitmap = bitmaps[curFrame];

            if ( !Paused )
            {
                if ( DateTime.Now.Ticks - lastTick > times[curFrame] * 10000000 )
                {
                    curFrame++;
                    lastTick = DateTime.Now.Ticks;
                }

                if ( curFrame == bitmaps.Count )
                {
                    if ( loop )
                        curFrame = 0;
                    else
                        curFrame--;
                }
            }
        }

        public void BackOneFrame()
        {
            curFrame = Math.Max( 0, curFrame - 1 );
        }

        public void ForwardOneFrame()
        {
            curFrame++;

            if ( curFrame == bitmaps.Count )
            {
                if ( loop )
                {
                    curFrame = 0;
                }
                else
                {
                    curFrame--;
                }
            }
        }

        public bool Paused { get; set; }
        
        public void Pause()
        {
            Paused = true;
        }

        public void Unpause()
        {
            Paused = false;
        }
    }
}
