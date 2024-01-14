namespace Willprint_Reservation_System
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.exeDeal = new System.Windows.Forms.Button();
            this.cancelDel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 26);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(295, 20);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Type the ID to Delete:";
            // 
            // exeDeal
            // 
            this.exeDeal.BackColor = System.Drawing.Color.White;
            this.exeDeal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.exeDeal.Location = new System.Drawing.Point(12, 52);
            this.exeDeal.Name = "exeDeal";
            this.exeDeal.Size = new System.Drawing.Size(295, 31);
            this.exeDeal.TabIndex = 2;
            this.exeDeal.Text = "Delete";
            this.exeDeal.UseVisualStyleBackColor = false;
            // 
            // cancelDel
            // 
            this.cancelDel.BackColor = System.Drawing.Color.White;
            this.cancelDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cancelDel.Location = new System.Drawing.Point(12, 89);
            this.cancelDel.Name = "cancelDel";
            this.cancelDel.Size = new System.Drawing.Size(295, 31);
            this.cancelDel.TabIndex = 3;
            this.cancelDel.Text = "Cancel";
            this.cancelDel.UseVisualStyleBackColor = false;
            this.cancelDel.Click += new System.EventHandler(this.cancelDel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Crimson;
            this.ClientSize = new System.Drawing.Size(319, 133);
            this.Controls.Add(this.cancelDel);
            this.Controls.Add(this.exeDeal);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Delete Data";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button exeDeal;
        private System.Windows.Forms.Button cancelDel;
    }
}