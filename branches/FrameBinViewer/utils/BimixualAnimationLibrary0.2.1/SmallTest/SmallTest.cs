using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Animation;

namespace SmallTest
{
    public partial class SmallTest : Form
    {
        public SmallTest()
        {
            InitializeComponent();

            LoopControl.SetAction(this, this.Go);
        }

        private void Go()
        {
        }
    }
}
