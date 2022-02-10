
namespace operating_personnel
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
			this.Content_of_work = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// Content_of_work
			// 
			this.Content_of_work.AutoSize = true;
			this.Content_of_work.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Content_of_work.ForeColor = System.Drawing.SystemColors.Info;
			this.Content_of_work.Location = new System.Drawing.Point(12, 9);
			this.Content_of_work.Name = "Content_of_work";
			this.Content_of_work.Size = new System.Drawing.Size(165, 24);
			this.Content_of_work.TabIndex = 0;
			this.Content_of_work.Text = "Content_of_work";
			this.Content_of_work.Click += new System.EventHandler(this.label1_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.ClientSize = new System.Drawing.Size(1084, 607);
			this.Controls.Add(this.Content_of_work);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Content_of_work;
    }
}

