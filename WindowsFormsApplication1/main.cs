using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarketGen;

namespace WindowsFormsApplication1
{
    public partial class main : Form
    {
        MarketGenerator market_generator = new MarketGenerator();

        public main()
        {
            InitializeComponent();
            market_generator.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        public MarketEvent MarketUpdateEvent(object sender, MarketEvent m)
        {
            return m;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            market_generator.MarketUpdate = (MarketEvent) e;
            label1.Text = 0.ToString();
        }

    }
}
