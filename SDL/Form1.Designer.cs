namespace SDL {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.avlButton = new System.Windows.Forms.Button();
            this.rbButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btButton = new System.Windows.Forms.Button();
            this.patriciaButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // avlButton
            // 
            this.avlButton.Location = new System.Drawing.Point(16, 46);
            this.avlButton.Name = "avlButton";
            this.avlButton.Size = new System.Drawing.Size(75, 23);
            this.avlButton.TabIndex = 0;
            this.avlButton.Text = "AVL Tree";
            this.avlButton.UseVisualStyleBackColor = true;
            this.avlButton.Click += new System.EventHandler(this.avlButton_Click);
            // 
            // rbButton
            // 
            this.rbButton.Location = new System.Drawing.Point(16, 75);
            this.rbButton.Name = "rbButton";
            this.rbButton.Size = new System.Drawing.Size(75, 23);
            this.rbButton.TabIndex = 1;
            this.rbButton.Text = "RB Tree";
            this.rbButton.UseVisualStyleBackColor = true;
            this.rbButton.Click += new System.EventHandler(this.rbButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tree visualization";
            // 
            // btButton
            // 
            this.btButton.Location = new System.Drawing.Point(16, 104);
            this.btButton.Name = "btButton";
            this.btButton.Size = new System.Drawing.Size(75, 23);
            this.btButton.TabIndex = 3;
            this.btButton.Text = "B Tree";
            this.btButton.UseVisualStyleBackColor = true;
            this.btButton.Click += new System.EventHandler(this.btButton_Click);
            // 
            // patriciaButton
            // 
            this.patriciaButton.Location = new System.Drawing.Point(16, 133);
            this.patriciaButton.Name = "patriciaButton";
            this.patriciaButton.Size = new System.Drawing.Size(75, 23);
            this.patriciaButton.TabIndex = 4;
            this.patriciaButton.Text = "Patricia Trie";
            this.patriciaButton.UseVisualStyleBackColor = true;
            this.patriciaButton.Click += new System.EventHandler(this.patriciaButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 361);
            this.Controls.Add(this.patriciaButton);
            this.Controls.Add(this.btButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbButton);
            this.Controls.Add(this.avlButton);
            this.Name = "Form1";
            this.Text = "Project SDL";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button avlButton;
        private System.Windows.Forms.Button rbButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btButton;
        private System.Windows.Forms.Button patriciaButton;
    }
}

