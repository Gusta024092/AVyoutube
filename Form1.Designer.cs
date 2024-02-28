namespace AVyoutube
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtLink = new TextBox();
            label1 = new Label();
            btnAnalisar = new Button();
            cmbResolucao = new ComboBox();
            groupFiltros = new GroupBox();
            btnBaixarComEscolha = new Button();
            progressBar1 = new ProgressBar();
            listBox1 = new ListBox();
            btnBaixarSemEscolha = new Button();
            groupFiltros.SuspendLayout();
            SuspendLayout();
            // 
            // txtLink
            // 
            txtLink.Location = new Point(50, 12);
            txtLink.Name = "txtLink";
            txtLink.Size = new Size(715, 23);
            txtLink.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 20);
            label1.Name = "label1";
            label1.Size = new Size(32, 15);
            label1.TabIndex = 1;
            label1.Text = "Link:";
            // 
            // btnAnalisar
            // 
            btnAnalisar.Location = new Point(771, 12);
            btnAnalisar.Name = "btnAnalisar";
            btnAnalisar.Size = new Size(75, 23);
            btnAnalisar.TabIndex = 2;
            btnAnalisar.Text = "Analisar Formatos";
            btnAnalisar.UseVisualStyleBackColor = true;
            btnAnalisar.Click += btnAnalisar_Click;
            // 
            // cmbResolucao
            // 
            cmbResolucao.FormattingEnabled = true;
            cmbResolucao.Location = new Point(6, 35);
            cmbResolucao.Name = "cmbResolucao";
            cmbResolucao.Size = new Size(319, 23);
            cmbResolucao.TabIndex = 3;
            // 
            // groupFiltros
            // 
            groupFiltros.Controls.Add(btnBaixarSemEscolha);
            groupFiltros.Controls.Add(cmbResolucao);
            groupFiltros.Controls.Add(btnBaixarComEscolha);
            groupFiltros.Location = new Point(50, 112);
            groupFiltros.Name = "groupFiltros";
            groupFiltros.Size = new Size(331, 145);
            groupFiltros.TabIndex = 4;
            groupFiltros.TabStop = false;
            groupFiltros.Text = "Filtros";
            // 
            // btnBaixarComEscolha
            // 
            btnBaixarComEscolha.Location = new Point(90, 81);
            btnBaixarComEscolha.Name = "btnBaixarComEscolha";
            btnBaixarComEscolha.Size = new Size(75, 58);
            btnBaixarComEscolha.TabIndex = 5;
            btnBaixarComEscolha.Text = "Baixar";
            btnBaixarComEscolha.UseVisualStyleBackColor = true;
            btnBaixarComEscolha.Click += button1_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(50, 308);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(796, 23);
            progressBar1.TabIndex = 6;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(480, 76);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(366, 199);
            listBox1.TabIndex = 7;
            // 
            // btnBaixarSemEscolha
            // 
            btnBaixarSemEscolha.Location = new Point(171, 81);
            btnBaixarSemEscolha.Name = "btnBaixarSemEscolha";
            btnBaixarSemEscolha.Size = new Size(80, 58);
            btnBaixarSemEscolha.TabIndex = 8;
            btnBaixarSemEscolha.Text = "Melhor Resolução";
            btnBaixarSemEscolha.UseVisualStyleBackColor = true;
            btnBaixarSemEscolha.Click += btnBaixarSemEscolha_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(901, 450);
            Controls.Add(listBox1);
            Controls.Add(progressBar1);
            Controls.Add(groupFiltros);
            Controls.Add(btnAnalisar);
            Controls.Add(label1);
            Controls.Add(txtLink);
            Name = "Form1";
            Text = "Form1";
            groupFiltros.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtLink;
        private Label label1;
        private Button btnAnalisar;
        private ComboBox cmbResolucao;
        private GroupBox groupFiltros;
        private Button btnBaixarComEscolha;
        private ProgressBar progressBar1;
        private ListBox listBox1;
        private Button btnBaixarSemEscolha;
    }
}
