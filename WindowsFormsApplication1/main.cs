using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using MarketGen;

namespace WindowsFormsApplication1
{
    public partial class main : Form
    {
        private MarketGenerator mGen = new MarketGenerator();
        private byte[] dBytes;
        private ByteDs ds;

        public main()
        {
            InitializeComponent();
            ds = (ByteDs)BytesToObject(ref dBytes, typeof(ByteDs));
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


        private void label1_Click(object sender, EventArgs e)
        {
            // MarketEvent e = new MarketEvent();
            // DatGridView_RowUpdate(sender, (DataGridViewRowEventArgs)m);
            // BindData();
            ByteDs ds = (ByteDs)BytesToObject(ref dBytes, typeof(ByteDs));

            if (ds != null)
            {
                var dList = new List<ByteDs>()
                {

                    new ByteDs()
                    {
                        ID = ds.ID,
                        TradePrice = ds.TradePrice,
                        TradeQty = ds.TradeQty,
                        BidPrice = ds.BidPrice,
                        BidQty = ds.BidQty,
                        AskPrice = ds.AskPrice,
                        AskQty = ds.AskQty
                    }
                };
                label1.Text = dList.Count.ToString();

                foreach (var item in dList)
                {
                    Boolean exists = false;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells["ID"].Value != null)
                            label2.Text = row.Cells[0].Value.ToString();
                        if (item.ID != null)
                            label3.Text = item.ID.ToString();

                        if (row.Cells[0].Value == item.ID.ToString())
                        {
                            exists = true;
                            break;
                        }
                    }
                    if (!exists)
                    {
                        dataGridView1.Rows.Add(item.ID, 
                                                item.TradePrice, 
                                                item.TradeQty, 
                                                item.BidPrice, 
                                                item.BidQty, 
                                                item.AskPrice, 
                                                item.AskQty);
                    }

                }
            }

        }

        protected void DatGridView_RowUpdate(object sender, DataGridViewRowEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.Row.Index];
            
            
        }

        private void BindData()
        {
            ByteDs ds = (ByteDs)BytesToObject(ref dBytes, typeof(ByteDs));

            if (ds != null)
            {
                var dList = new List<ByteDs>()
                {

                    new ByteDs()
                    {
                        ID = ds.ID,
                        TradePrice = ds.TradePrice,
                        TradeQty = ds.TradeQty,
                        BidPrice = ds.BidPrice,
                        BidQty = ds.BidQty,
                        AskPrice = ds.AskPrice,
                        AskQty = ds.AskQty
                    }
                };
                label1.Text = dList.Count.ToString();
                var dBind = new BindingList<ByteDs>(dList);
                var source = new BindingSource(dBind, null);


                dataGridView1.DataSource = source;
            }
        }


        public static object BytesToObject(ref byte[] dataBytes, Type overlayType)
        {
            object result = null;

            var pinnedBytes = GCHandle.Alloc(dataBytes, GCHandleType.Pinned);
            try
            {
                var pinnedBytesPtr = pinnedBytes.AddrOfPinnedObject();

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
