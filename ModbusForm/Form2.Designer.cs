namespace ModbusForm
{
    partial class Form2
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
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartAdd = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.buttonWyb = new System.Windows.Forms.Button();
            this.wybBox = new System.Windows.Forms.TextBox();
            this.InteruptBtn2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartAdd)).BeginInit();
            this.SuspendLayout();
            // 
            // chartAdd
            // 
            chartArea1.Name = "ChartArea1";
            this.chartAdd.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartAdd.Legends.Add(legend1);
            this.chartAdd.Location = new System.Drawing.Point(113, 13);
            this.chartAdd.Name = "chartAdd";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Wartość";
            this.chartAdd.Series.Add(series1);
            this.chartAdd.Size = new System.Drawing.Size(675, 425);
            this.chartAdd.TabIndex = 0;
            this.chartAdd.Text = "chart1";
            // 
            // buttonWyb
            // 
            this.buttonWyb.Location = new System.Drawing.Point(7, 128);
            this.buttonWyb.Name = "buttonWyb";
            this.buttonWyb.Size = new System.Drawing.Size(100, 48);
            this.buttonWyb.TabIndex = 1;
            this.buttonWyb.Text = "Wykonaj";
            this.buttonWyb.UseVisualStyleBackColor = true;
            this.buttonWyb.Click += new System.EventHandler(this.buttonWyb_Click);
            // 
            // wybBox
            // 
            this.wybBox.Location = new System.Drawing.Point(7, 77);
            this.wybBox.Name = "wybBox";
            this.wybBox.Size = new System.Drawing.Size(100, 26);
            this.wybBox.TabIndex = 2;
            // 
            // InteruptBtn2
            // 
            this.InteruptBtn2.Location = new System.Drawing.Point(7, 183);
            this.InteruptBtn2.Name = "InteruptBtn2";
            this.InteruptBtn2.Size = new System.Drawing.Size(100, 46);
            this.InteruptBtn2.TabIndex = 3;
            this.InteruptBtn2.Text = "Przerwij";
            this.InteruptBtn2.UseVisualStyleBackColor = true;
            this.InteruptBtn2.Click += new System.EventHandler(this.InteruptBtn2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Adres";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.InteruptBtn2);
            this.Controls.Add(this.wybBox);
            this.Controls.Add(this.buttonWyb);
            this.Controls.Add(this.chartAdd);
            this.Name = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.chartAdd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartAdd;
        private System.Windows.Forms.Button buttonWyb;
        private System.Windows.Forms.TextBox wybBox;
        private System.Windows.Forms.Button InteruptBtn2;
        private System.Windows.Forms.Label label1;
    }
}