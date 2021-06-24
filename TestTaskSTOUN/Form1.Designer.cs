namespace TestTaskSTOUN {
    partial class Form1 {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            this.GetCurrencyRate = new System.Windows.Forms.Button();
            this.ListCurrency = new System.Windows.Forms.ComboBox();
            this.Calendar = new System.Windows.Forms.DateTimePicker();
            this.ResultText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GetCurrencyRate
            // 
            this.GetCurrencyRate.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GetCurrencyRate.Location = new System.Drawing.Point(26, 98);
            this.GetCurrencyRate.Name = "GetCurrencyRate";
            this.GetCurrencyRate.Size = new System.Drawing.Size(142, 47);
            this.GetCurrencyRate.TabIndex = 0;
            this.GetCurrencyRate.Text = "Получить";
            this.GetCurrencyRate.UseVisualStyleBackColor = true;
            this.GetCurrencyRate.Click += new System.EventHandler(this.GetCurrencyRate_Click);
            // 
            // ListCurrency
            // 
            this.ListCurrency.DropDownHeight = 100;
            this.ListCurrency.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ListCurrency.FormattingEnabled = true;
            this.ListCurrency.IntegralHeight = false;
            this.ListCurrency.Location = new System.Drawing.Point(26, 43);
            this.ListCurrency.MaxDropDownItems = 5;
            this.ListCurrency.Name = "ListCurrency";
            this.ListCurrency.Size = new System.Drawing.Size(317, 30);
            this.ListCurrency.TabIndex = 1;
            this.ListCurrency.Text = "Название валюты";
            // 
            // Calendar
            // 
            this.Calendar.Font = new System.Drawing.Font("Century", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Calendar.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Calendar.Location = new System.Drawing.Point(362, 43);
            this.Calendar.Name = "Calendar";
            this.Calendar.Size = new System.Drawing.Size(200, 30);
            this.Calendar.TabIndex = 2;
            // 
            // ResultText
            // 
            this.ResultText.AutoSize = true;
            this.ResultText.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ResultText.Location = new System.Drawing.Point(358, 109);
            this.ResultText.Name = "ResultText";
            this.ResultText.Size = new System.Drawing.Size(0, 22);
            this.ResultText.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 175);
            this.Controls.Add(this.ResultText);
            this.Controls.Add(this.Calendar);
            this.Controls.Add(this.ListCurrency);
            this.Controls.Add(this.GetCurrencyRate);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GetCurrencyRate;
        private System.Windows.Forms.ComboBox ListCurrency;
        private System.Windows.Forms.DateTimePicker Calendar;
        private System.Windows.Forms.Label ResultText;
    }
}

