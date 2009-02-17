using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using PatcherLib.Utilities;

namespace FFTorgASM
{
    public partial class MainForm : Form
    {
        AsmPatch[] patches;
        public MainForm()
        {
            InitializeComponent();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml( FFTorgASM.Properties.Resources.DefaultHacks );
            patches = PatchXmlReader.GetPatches( doc.SelectSingleNode( "/Patches" ) ).ToArray();
            checkedListBox1.Items.AddRange( patches );
        }
    }
}
