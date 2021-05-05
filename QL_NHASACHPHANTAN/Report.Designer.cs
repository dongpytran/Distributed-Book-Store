
namespace QL_NHASACHPHANTAN
{
    partial class Report
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtcn = new System.Windows.Forms.Label();
            this.dtht = new System.Windows.Forms.Label();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.slnvText = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.slnvht = new System.Windows.Forms.Label();
            this.slnvcn = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(68, 53);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(327, 334);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 422);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Doanh thu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 462);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(199, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "TỔNG DOANH THU TRÊN HỆ THỐNG";
            // 
            // dtcn
            // 
            this.dtcn.AutoSize = true;
            this.dtcn.Location = new System.Drawing.Point(284, 422);
            this.dtcn.Name = "dtcn";
            this.dtcn.Size = new System.Drawing.Size(57, 13);
            this.dtcn.TabIndex = 3;
            this.dtcn.Text = "Doanh thu";
            // 
            // dtht
            // 
            this.dtht.AutoSize = true;
            this.dtht.Location = new System.Drawing.Point(284, 462);
            this.dtht.Name = "dtht";
            this.dtht.Size = new System.Drawing.Size(57, 13);
            this.dtht.TabIndex = 4;
            this.dtht.Text = "Doanh thu";
            // 
            // chart2
            // 
            chartArea2.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart2.Legends.Add(legend2);
            this.chart2.Location = new System.Drawing.Point(589, 53);
            this.chart2.Name = "chart2";
            this.chart2.Size = new System.Drawing.Size(327, 334);
            this.chart2.TabIndex = 5;
            this.chart2.Text = "chart2";
            // 
            // slnvText
            // 
            this.slnvText.AutoSize = true;
            this.slnvText.Location = new System.Drawing.Point(603, 422);
            this.slnvText.Name = "slnvText";
            this.slnvText.Size = new System.Drawing.Size(57, 13);
            this.slnvText.TabIndex = 6;
            this.slnvText.Text = "Doanh thu";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(603, 462);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(218, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "SỐ LƯỢNG NHÂN VIÊN TRÊN HỆ THỐNG";
            // 
            // slnvht
            // 
            this.slnvht.AutoSize = true;
            this.slnvht.Location = new System.Drawing.Point(844, 462);
            this.slnvht.Name = "slnvht";
            this.slnvht.Size = new System.Drawing.Size(57, 13);
            this.slnvht.TabIndex = 8;
            this.slnvht.Text = "Doanh thu";
            // 
            // slnvcn
            // 
            this.slnvcn.AutoSize = true;
            this.slnvcn.Location = new System.Drawing.Point(844, 422);
            this.slnvcn.Name = "slnvcn";
            this.slnvcn.Size = new System.Drawing.Size(57, 13);
            this.slnvcn.TabIndex = 9;
            this.slnvcn.Text = "Doanh thu";
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 509);
            this.Controls.Add(this.slnvcn);
            this.Controls.Add(this.slnvht);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.slnvText);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.dtht);
            this.Controls.Add(this.dtcn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chart1);
            this.Name = "Report";
            this.Text = "Report";
            this.Load += new System.EventHandler(this.Report_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label dtcn;
        private System.Windows.Forms.Label dtht;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.Label slnvText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label slnvht;
        private System.Windows.Forms.Label slnvcn;
    }
}