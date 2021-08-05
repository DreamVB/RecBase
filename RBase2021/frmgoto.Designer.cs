namespace RBase2021
{
    partial class frmgoto
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
            this.txtRecNum = new System.Windows.Forms.NumericUpDown();
            this.rFirst = new System.Windows.Forms.RadioButton();
            this.rLast = new System.Windows.Forms.RadioButton();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.rRecord = new System.Windows.Forms.RadioButton();
            this.lbl3dLine = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtRecNum)).BeginInit();
            this.SuspendLayout();
            // 
            // txtRecNum
            // 
            this.txtRecNum.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecNum.Location = new System.Drawing.Point(13, 88);
            this.txtRecNum.Name = "txtRecNum";
            this.txtRecNum.Size = new System.Drawing.Size(120, 22);
            this.txtRecNum.TabIndex = 0;
            this.txtRecNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // rFirst
            // 
            this.rFirst.AutoSize = true;
            this.rFirst.Checked = true;
            this.rFirst.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rFirst.Location = new System.Drawing.Point(14, 15);
            this.rFirst.Name = "rFirst";
            this.rFirst.Size = new System.Drawing.Size(52, 18);
            this.rFirst.TabIndex = 0;
            this.rFirst.TabStop = true;
            this.rFirst.Text = "First";
            this.rFirst.UseVisualStyleBackColor = true;
            this.rFirst.CheckedChanged += new System.EventHandler(this.rFirst_CheckedChanged);
            // 
            // rLast
            // 
            this.rLast.AutoSize = true;
            this.rLast.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rLast.Location = new System.Drawing.Point(13, 38);
            this.rLast.Name = "rLast";
            this.rLast.Size = new System.Drawing.Size(52, 18);
            this.rLast.TabIndex = 1;
            this.rLast.Text = "Last";
            this.rLast.UseVisualStyleBackColor = true;
            this.rLast.CheckedChanged += new System.EventHandler(this.rLast_CheckedChanged);
            // 
            // cmdOK
            // 
            this.cmdOK.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOK.Location = new System.Drawing.Point(180, 18);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 4;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Location = new System.Drawing.Point(180, 47);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 5;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // rRecord
            // 
            this.rRecord.AutoSize = true;
            this.rRecord.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rRecord.Location = new System.Drawing.Point(13, 62);
            this.rRecord.Name = "rRecord";
            this.rRecord.Size = new System.Drawing.Size(68, 18);
            this.rRecord.TabIndex = 2;
            this.rRecord.Text = "Record";
            this.rRecord.UseVisualStyleBackColor = true;
            // 
            // lbl3dLine
            // 
            this.lbl3dLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl3dLine.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl3dLine.Location = new System.Drawing.Point(156, 12);
            this.lbl3dLine.Name = "lbl3dLine";
            this.lbl3dLine.Size = new System.Drawing.Size(1, 89);
            this.lbl3dLine.TabIndex = 7;
            // 
            // frmgoto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 124);
            this.Controls.Add(this.lbl3dLine);
            this.Controls.Add(this.rRecord);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.rLast);
            this.Controls.Add(this.rFirst);
            this.Controls.Add(this.txtRecNum);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmgoto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Goto Record";
            this.Load += new System.EventHandler(this.frmgoto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtRecNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown txtRecNum;
        private System.Windows.Forms.RadioButton rFirst;
        private System.Windows.Forms.RadioButton rLast;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.RadioButton rRecord;
        private System.Windows.Forms.Label lbl3dLine;
    }
}