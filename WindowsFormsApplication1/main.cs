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
using System.Windows;
using MarketGen;

namespace WindowsFormsApplication1
{
    public partial class main : Form
    {
        // needed throughout various methods. 
        private MarketGenerator mGen = new MarketGenerator();
        private byte[] dBytes;
        private ByteDs ds;
        private int count = 0;

        public main()
        {
            InitializeComponent();

        }

        private void Main_Load(object sender, EventArgs e)
        {
            mGen.MarketUpdate += r_MarketUpdate;
            mGen.Start();
            // data structure will be needed to get access to all of the data within the market. 
            ds = (ByteDs)BytesToObject(ref dBytes, typeof(ByteDs));
            DataGridViewCellStyle hStyle = dataGridView1.ColumnHeadersDefaultCellStyle.Clone();
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Red;

            foreach (DataGridViewColumn item in dataGridView1.Columns)
            {
                item.HeaderCell.Style = hStyle;
            }
            FormClosed += Main_Closed;
        }

        /* This was needed to stop the market update process before closing a window, 
         * otherwise the process will continue after the window was closed, and attempt
         * to update and fail. Hence causing an error, and stopping the program 
         */
        private void Main_Closed(object sender, FormClosedEventArgs e)
        {
            mGen.Stop();
        }

        // receive update from market library
        private void r_MarketUpdate(object sender, MarketEvent e)
        {
           dBytes = e.Data;
           /* since the market update works on a different thread, it is required to 
            * explicitally invoke this function on the thread it was created. Otherwise
            * it would fail, giving an error from the datagridview function specifically
            * made to give safe thread access.
            */
           BeginInvoke(new MethodInvoker(BindData));
        }

        /* This method binds the date from the byte[] into a datastructure, and updates the 
         * datagridview accordingly. So, as long as something is stored in the Byte it will run.
         * It will also highlight the proper cells, based on change from the previous data stored
         * to the current data. If no changes occur, it will unhighlight that data. 
         */
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
                                        if (row.Cells[col.Index].Value.ToString() != item.TradeQty.ToString())
                                            cell.BackColor = Color.DarkRed;
                                        else
                                            cell.BackColor = Color.Black;
                                        break;
                                    case 3:
                                        if (row.Cells[col.Index].Value.ToString() != item.BidPrice.ToString())
                                            cell.BackColor = Color.DarkRed;
                                        else
                                            cell.BackColor = Color.Black;
                                        break;
                                    case 4:
                                        if (row.Cells[col.Index].Value.ToString() != item.BidQty.ToString())
                                            cell.BackColor = Color.DarkRed;
                                        else
                                            cell.BackColor = Color.Black;
                                        break;
                                    case 5:
                                        if (row.Cells[col.Index].Value.ToString() != item.AskPrice.ToString())
                                            cell.BackColor = Color.DarkRed;
                                        else
                                            cell.BackColor = Color.Black;
                                        break;
                                    case 6:
                                        if (row.Cells[col.Index].Value.ToString() != item.AskQty.ToString())
                                            cell.BackColor = Color.DarkRed;
                                        else
                                            cell.BackColor = Color.Black;
                                        break;
                                }
                            }
                            // Update the row data
                            row.SetValues(item.ID, 
                                item.TradePrice,
                                item.TradeQty,
                                item.BidPrice, 
                                item.BidQty,
                                item.AskPrice, 
                                item.AskQty);
                        }
                    }
                    // if there are no instances of this data, by ID, it will create a new row, and add the 
                    // new values. 
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
            count += 1;
            main newMain = new main();
            newMain.Show();
            newMain.Text = "New Window";
        }

    }

}
