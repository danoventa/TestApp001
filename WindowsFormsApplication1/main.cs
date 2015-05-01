using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
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
            DataGridViewCellStyle hStyle = dataGridView1.ColumnHeadersDefaultCellStyle.Clone();
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Red;

            foreach (DataGridViewColumn item in dataGridView1.Columns)
            {
                item.HeaderCell.Style = hStyle;
            }
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
            // BindData();
        }

        protected void DatGridView_RowUpdate(object sender, DataGridViewRowEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.Row.Index];
            BindData();
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

                foreach (var item in dList)
                {
                    Boolean exists = false;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        row.DefaultCellStyle.BackColor = Color.Black;
                        row.DefaultCellStyle.ForeColor = Color.White;

                        if (row.Cells["ID"].Value != null && item.ID != null)
                        if (row.Cells["ID"].Value.ToString() == item.ID.ToString())
                        {
                            exists = true;
                            foreach (DataGridViewColumn col in dataGridView1.Columns)
                            {
                                var cell = row.Cells[col.Index].Style;
                                switch (col.Index)
                                {
                                    case 1:
                                        if (row.Cells[col.Index].Value.ToString() != item.TradePrice.ToString())
                                            cell.BackColor = Color.DarkRed;
                                        else
                                            cell.BackColor = Color.Black;
                                        break;
                                    case 2:
                                        if (row.Cells[col.Index].Value.ToString() != item.TradePrice.ToString())
                                            cell.BackColor = Color.Black;
                                        else
                                            cell.BackColor = Color.Black;
                                        break;
                                    case 3:
                                        if (row.Cells[col.Index].Value.ToString() != item.TradePrice.ToString())
                                            cell.BackColor = Color.DarkRed;
                                        else
                                            cell.BackColor = Color.Black;
                                        break;
                                    case 4:
                                        if (row.Cells[col.Index].Value.ToString() != item.TradePrice.ToString())
                                            cell.BackColor = Color.DarkRed;
                                        else
                                            cell.BackColor = Color.Black;
                                        break;
                                    case 5:
                                        if (row.Cells[col.Index].Value.ToString() != item.TradePrice.ToString())
                                            cell.BackColor = Color.DarkRed;
                                        else
                                            cell.BackColor = Color.Black;
                                        break;
                                    case 6:
                                        if (row.Cells[col.Index].Value.ToString() != item.TradePrice.ToString())
                                            cell.BackColor = Color.DarkRed;
                                        else
                                            cell.BackColor = Color.Black;
                                        break;
                                }
                            }

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
                        dataGridView1.DefaultCellStyle.BackColor = Color.Black;
                        dataGridView1.DefaultCellStyle.ForeColor = Color.White;
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

        private void label2_Click(object sender, EventArgs e)
        {
            mGen.Start();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            mGen.Stop();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            BindData();
        }

    }

}
