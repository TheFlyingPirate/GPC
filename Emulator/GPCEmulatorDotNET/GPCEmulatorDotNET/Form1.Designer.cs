namespace GPCEmulatorDotNET
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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnTick = new System.Windows.Forms.Button();
            this.lb1 = new System.Windows.Forms.ListBox();
            this.txtWrite = new System.Windows.Forms.TextBox();
            this.btnWrite = new System.Windows.Forms.Button();
            this.timer1 = new System.Timers.Timer();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblPointer = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize) (this.timer1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(713, 415);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnTick
            // 
            this.btnTick.Location = new System.Drawing.Point(632, 415);
            this.btnTick.Name = "btnTick";
            this.btnTick.Size = new System.Drawing.Size(75, 23);
            this.btnTick.TabIndex = 1;
            this.btnTick.Text = "Tick";
            this.btnTick.UseVisualStyleBackColor = true;
            this.btnTick.Click += new System.EventHandler(this.btnTick_Click);
            // 
            // lb1
            // 
            this.lb1.FormattingEnabled = true;
            this.lb1.Location = new System.Drawing.Point(12, 12);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(291, 433);
            this.lb1.TabIndex = 2;
            // 
            // txtWrite
            // 
            this.txtWrite.Location = new System.Drawing.Point(309, 12);
            this.txtWrite.Name = "txtWrite";
            this.txtWrite.Size = new System.Drawing.Size(100, 20);
            this.txtWrite.TabIndex = 3;
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(415, 10);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(75, 23);
            this.btnWrite.TabIndex = 4;
            this.btnWrite.Text = "Write";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000D;
            this.timer1.SynchronizingObject = this;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(309, 422);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblPointer
            // 
            this.lblPointer.Location = new System.Drawing.Point(496, 10);
            this.lblPointer.Name = "lblPointer";
            this.lblPointer.Size = new System.Drawing.Size(100, 23);
            this.lblPointer.TabIndex = 6;
            this.lblPointer.Text = "Pointer: 00000000";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblPointer);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.txtWrite);
            this.Controls.Add(this.lb1);
            this.Controls.Add(this.btnTick);
            this.Controls.Add(this.btnStart);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize) (this.timer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblPointer;

        private System.Windows.Forms.Button btnRefresh;

        private System.Timers.Timer timer1;

        private System.Windows.Forms.ListBox lb1;
        private System.Windows.Forms.TextBox txtWrite;

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnTick;
        private System.Windows.Forms.Button btnWrite;

        #endregion
    }
}