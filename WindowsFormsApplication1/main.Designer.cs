using MarketGen;

namespace WindowsFormsApplication1
{
    partial class main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TradePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TradeQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BidPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BidQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AskPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AskQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.TradePrice,
            this.TradeQty,
            this.BidPrice,
            this.BidQty,
            this.AskPrice,
            this.AskQty});
            this.dataGridView1.Location = new System.Drawing.Point(2, 62);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(744, 353);
            this.dataGridView1.TabIndex = 8;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // TradePrice
            // 
            this.TradePrice.HeaderText = "TradePrice";
            this.TradePrice.Name = "TradePrice";
            this.TradePrice.ReadOnly = true;
            // 
            // TradeQty
            // 
            this.TradeQty.HeaderText = "TradeQty";
            this.TradeQty.Name = "TradeQty";
            this.TradeQty.ReadOnly = true;
            // 
            // BidPrice
            // 
            this.BidPrice.HeaderText = "BidPrice";
            this.BidPrice.Name = "BidPrice";
            this.BidPrice.ReadOnly = true;
            // 
            // BidQty
            // 
            this.BidQty.HeaderText = "BidQty";
            this.BidQty.Name = "BidQty";
            this.BidQty.ReadOnly = true;
            // 
            // AskPrice
            // 
            this.AskPrice.HeaderText = "AskPrice";
            this.AskPrice.Name = "AskPrice";
            this.AskPrice.ReadOnly = true;
            // 
            // AskQty
            // 
            this.AskQty.HeaderText = "AskQty";
            this.AskQty.Name = "AskQty";
            this.AskQty.ReadOnly = true;
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 419);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Name = "main";
            this.Text = "Market Generator";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TradePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn TradeQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn BidPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn BidQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn AskPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn AskQty;

    }
}

