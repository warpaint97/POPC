using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POPC
{
    public partial class clusterGraph : Form
    {
        public clusterGraph()
        {
            InitializeComponent();
        }

        private void clusterGraph_Resize(object sender, EventArgs e)
        {
            this.chart1.Top = 0;
            this.chart1.Left = 0;
            this.chart1.Width = this.ClientSize.Width;
            this.chart1.Height = this.ClientSize.Height;
        }
    }
}
