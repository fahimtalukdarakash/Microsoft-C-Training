namespace ex_LINQ
{
    partial class Form1
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
            this.Q1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Q2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Q3 = new System.Windows.Forms.Button();
            this.Q4 = new System.Windows.Forms.Button();
            this.Q5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // Q1
            // 
            this.Q1.Location = new System.Drawing.Point(86, 13);
            this.Q1.Name = "Q1";
            this.Q1.Size = new System.Drawing.Size(75, 23);
            this.Q1.TabIndex = 0;
            this.Q1.Text = "btnQ1";
            this.Q1.UseVisualStyleBackColor = true;
            this.Q1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(41, 114);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(714, 309);
            this.dataGridView1.TabIndex = 1;
            // 
            // Q2
            // 
            this.Q2.Location = new System.Drawing.Point(225, 12);
            this.Q2.Name = "Q2";
            this.Q2.Size = new System.Drawing.Size(75, 23);
            this.Q2.TabIndex = 2;
            this.Q2.Text = "btnQ2";
            this.Q2.UseVisualStyleBackColor = true;
            this.Q2.Click += new System.EventHandler(this.Q2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(360, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 3;
            // 
            // Q3
            // 
            this.Q3.Location = new System.Drawing.Point(521, 11);
            this.Q3.Name = "Q3";
            this.Q3.Size = new System.Drawing.Size(75, 23);
            this.Q3.TabIndex = 4;
            this.Q3.Text = "btnQ3";
            this.Q3.UseVisualStyleBackColor = true;
            this.Q3.Click += new System.EventHandler(this.Q3_Click);
            // 
            // Q4
            // 
            this.Q4.Location = new System.Drawing.Point(680, 15);
            this.Q4.Name = "Q4";
            this.Q4.Size = new System.Drawing.Size(75, 23);
            this.Q4.TabIndex = 5;
            this.Q4.Text = "btnQ4";
            this.Q4.UseVisualStyleBackColor = true;
            this.Q4.Click += new System.EventHandler(this.Q4_Click);
            // 
            // Q5
            // 
            this.Q5.Location = new System.Drawing.Point(86, 55);
            this.Q5.Name = "Q5";
            this.Q5.Size = new System.Drawing.Size(75, 23);
            this.Q5.TabIndex = 6;
            this.Q5.Text = "btnQ5";
            this.Q5.UseVisualStyleBackColor = true;
            this.Q5.Click += new System.EventHandler(this.Q5_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Q5);
            this.Controls.Add(this.Q4);
            this.Controls.Add(this.Q3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Q2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Q1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Q1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button Q2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Q3;
        private System.Windows.Forms.Button Q4;
        private System.Windows.Forms.Button Q5;
    }
}

