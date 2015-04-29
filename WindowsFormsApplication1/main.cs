using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarketGen;

namespace WindowsFormsApplication1
{
    public partial class main : Form
    {
        private MarketGenerator mGen = new MarketGenerator();
        private byte[] dBytes;

        public main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            mGen.MarketUpdate += r_MarketUpdate;
            mGen.Start();
        }

        private void r_MarketUpdate(object sender, MarketEvent e)
        {
            dBytes = e.Data;
        }


        private void label1_Click(object sender, EventArgs m)
        {
            //MarketEvent e = new MarketEvent();

            label8.Text = dBytes.Length.ToString();
            ByteDs ds = (ByteDs) BytesToObject(ref dBytes, typeof (ByteDs));
            label1.Text = ds.ID.ToString();
            label2.Text = ds.TradePrice.ToString();
            label3.Text = ds.TradeQty.ToString();
            label4.Text = ds.BidPrice.ToString();
            label5.Text = ds.BidQty.ToString();
            label6.Text = ds.AskPrice.ToString();
            label7.Text = ds.AskQty.ToString();


        }

        public static object BytesToObject(ref byte[] dataBytes, Type overlayType)
        {
            object result = null;

            GCHandle pinnedBytes = GCHandle.Alloc(dataBytes, GCHandleType.Pinned);
            try
            {
                IntPtr pinnedBytesPtr = pinnedBytes.AddrOfPinnedObject();

                result = Marshal.PtrToStructure(pinnedBytesPtr, overlayType);
            }
            finally
            {
                pinnedBytes.Free();
            }
            return result;
        }


    }

    [StructLayout(LayoutKind.Sequential, Size=40, Pack=1, CharSet=CharSet.Ansi)]
    public class ByteDs
    {
        public int ID;
        public double TradePrice;
        public int TradeQty;
        public double BidPrice;
        public int BidQty;
        public double AskPrice;
        public int AskQty;
    }
}
