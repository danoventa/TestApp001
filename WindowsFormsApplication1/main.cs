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

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            mGen.Stop();
        }


        private void r_MarketUpdate(object sender, MarketEvent e)
        {
            dBytes = e.Data;

        }


        private void label1_Click(object sender, EventArgs e)
        {
            // MarketEvent e = new MarketEvent();
            // DatGridView_RowUpdate(sender, (DataGridViewRowEventArgs)m);
            BindData();
            

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

                foreach (var item in dList)
                {
                    Boolean exists = false;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        row.DefaultCellStyle.BackColor = Color.White;

                        if (row.Cells["ID"].Value != null && item.ID != null)
                        if (row.Cells["ID"].Value.ToString() == item.ID.ToString())
                        {
                            exists = true;
                            row.DefaultCellStyle.BackColor = Color.White;
                            foreach (DataGridViewColumn col in dataGridView1.Columns)
                            {
                                
                                var checkString = dataGridView1[col.Index, row.Index].Value.ToString();
                                row.Cells[col.Index].Style.BackColor = Color.White;
                                label7.Text = col.Index.ToString();
                                // need this fixed. logic error. If checks everything, then need to make sure that only some things are checked, not all, cause it leads to the entire column being colored. 
                                if (row.Cells[1].Value.ToString() != item.TradePrice.ToString())
                                {
                                    label8.Text = col.Index.ToString();
                                    label6.Text = row.Cells[1].Value.ToString();
                                    row.Cells[1].Style.BackColor = Color.Aquamarine;
                                    // dataGridView1.Rows[row.Index].Cells[col.Index].Style.BackColor = Color.Aquamarine;
                                }
                                /*if (checkString != item.TradeQty.ToString())
                                {
                                    row.Cells[col.Index].Style.BackColor = Color.Aquamarine;
                                    // dataGridView1.Rows[row.Index].Cells[col.Index].Style.BackColor = Color.Aquamarine;
                                }
                                if (checkString != item.BidPrice.ToString())
                                {
                                    row.Cells[col.Index].Style.BackColor = Color.Aquamarine;
                                    // dataGridView1.Rows[row.Index].Cells[col.Index].Style.BackColor = Color.Aquamarine;
                                }
                                if (checkString != item.BidQty.ToString())
                                {
                                    row.Cells[col.Index].Style.BackColor = Color.Aquamarine;
                                    // dataGridView1.Rows[row.Index].Cells[col.Index].Style.BackColor = Color.Aquamarine;
                                }
                                if (checkString != item.AskPrice.ToString())
                                {
                                    row.Cells[col.Index].Style.BackColor = Color.Aquamarine;
                                    // dataGridView1.Rows[row.Index].Cells[col.Index].Style.BackColor = Color.Aquamarine;
                                }
                                if (checkString != item.AskQty.ToString())
                                {
                                    row.Cells[col.Index].Style.BackColor = Color.Aquamarine;
                                    // dataGridView1.Rows[row.Index].Cells[col.Index].Style.BackColor = Color.Aquamarine;
                                }*/
                            }

                            // row.DefaultCellStyle.BackColor = Color.Aquamarine;

                            row.SetValues(item.ID, 
                                item.TradePrice,
                                item.TradeQty,
                                item.BidPrice, 
                                item.BidQty,
                                item.AskPrice, 
                                item.AskQty);
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

        // convert the byte array to the ByteDs datastructure for access
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
        // Datastructure to unpack the data from provided market class
        [StructLayout(LayoutKind.Sequential, Size = 40, Pack = 1, CharSet = CharSet.Ansi)]
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

}
