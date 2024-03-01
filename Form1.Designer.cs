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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            progressBar1 = new ProgressBar();
            arquivoToolStripMenuItem = new ToolStripMenuItem();
            sairToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1 = new MenuStrip();
            sobreToolStripMenuItem = new ToolStripMenuItem();
            tableLayoutPanel2 = new TableLayoutPanel();
            lblProcesso1 = new Label();
            groupBox1 = new GroupBox();
            label1 = new Label();
            txtLink = new TextBox();
            lblStatus = new Label();
            btnAnalisar = new Button();
            groupFiltros = new GroupBox();
            label2 = new Label();
            btnBaixarSemEscolha = new Button();
            cmbResolucao = new ComboBox();
            btnBaixarComEscolha = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            menuStrip1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            groupBox1.SuspendLayout();
            groupFiltros.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // progressBar1
            // 
            resources.ApplyResources(progressBar1, "progressBar1");
            progressBar1.Name = "progressBar1";
            // 
            // arquivoToolStripMenuItem
            // 
            resources.ApplyResources(arquivoToolStripMenuItem, "arquivoToolStripMenuItem");
            arquivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { sairToolStripMenuItem });
            arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            // 
            // sairToolStripMenuItem
            // 
            resources.ApplyResources(sairToolStripMenuItem, "sairToolStripMenuItem");
            sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            sairToolStripMenuItem.Click += sairToolStripMenuItem_Click;
            // 
            // menuStrip1
            // 
            resources.ApplyResources(menuStrip1, "menuStrip1");
            menuStrip1.Items.AddRange(new ToolStripItem[] { arquivoToolStripMenuItem, sobreToolStripMenuItem });
            menuStrip1.Name = "menuStrip1";
            // 
            // sobreToolStripMenuItem
            // 
            resources.ApplyResources(sobreToolStripMenuItem, "sobreToolStripMenuItem");
            sobreToolStripMenuItem.Name = "sobreToolStripMenuItem";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(tableLayoutPanel2, "tableLayoutPanel2");
            tableLayoutPanel2.Controls.Add(lblProcesso1, 0, 0);
            tableLayoutPanel2.Controls.Add(progressBar1, 0, 1);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.Paint += tableLayoutPanel2_Paint;
            // 
            // lblProcesso1
            // 
            resources.ApplyResources(lblProcesso1, "lblProcesso1");
            lblProcesso1.Name = "lblProcesso1";
            // 
            // groupBox1
            // 
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(txtLink);
            groupBox1.Controls.Add(lblStatus);
            groupBox1.Controls.Add(btnAnalisar);
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // txtLink
            // 
            resources.ApplyResources(txtLink, "txtLink");
            txtLink.Name = "txtLink";
            // 
            // lblStatus
            // 
            resources.ApplyResources(lblStatus, "lblStatus");
            lblStatus.Name = "lblStatus";
            // 
            // btnAnalisar
            // 
            resources.ApplyResources(btnAnalisar, "btnAnalisar");
            btnAnalisar.ForeColor = SystemColors.ActiveCaption;
            btnAnalisar.Name = "btnAnalisar";
            btnAnalisar.UseVisualStyleBackColor = true;
            btnAnalisar.Click += btnAnalisar_Click;
            // 
            // groupFiltros
            // 
            resources.ApplyResources(groupFiltros, "groupFiltros");
            groupFiltros.Controls.Add(label2);
            groupFiltros.Controls.Add(btnBaixarSemEscolha);
            groupFiltros.Controls.Add(cmbResolucao);
            groupFiltros.Controls.Add(btnBaixarComEscolha);
            groupFiltros.Name = "groupFiltros";
            groupFiltros.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // btnBaixarSemEscolha
            // 
            resources.ApplyResources(btnBaixarSemEscolha, "btnBaixarSemEscolha");
            btnBaixarSemEscolha.BackColor = SystemColors.Control;
            btnBaixarSemEscolha.ForeColor = Color.Purple;
            btnBaixarSemEscolha.Name = "btnBaixarSemEscolha";
            btnBaixarSemEscolha.UseVisualStyleBackColor = false;
            btnBaixarSemEscolha.Click += btnBaixarSemEscolha_Click;
            // 
            // cmbResolucao
            // 
            resources.ApplyResources(cmbResolucao, "cmbResolucao");
            cmbResolucao.FormattingEnabled = true;
            cmbResolucao.Name = "cmbResolucao";
            // 
            // btnBaixarComEscolha
            // 
            resources.ApplyResources(btnBaixarComEscolha, "btnBaixarComEscolha");
            btnBaixarComEscolha.ForeColor = Color.DarkViolet;
            btnBaixarComEscolha.Name = "btnBaixarComEscolha";
            btnBaixarComEscolha.UseVisualStyleBackColor = true;
            btnBaixarComEscolha.Click += button1_Click;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(tableLayoutPanel1, "tableLayoutPanel1");
            tableLayoutPanel1.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel1.Controls.Add(groupFiltros, 0, 1);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveCaption;
            Controls.Add(tableLayoutPanel1);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupFiltros.ResumeLayout(false);
            groupFiltros.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ProgressBar progressBar1;
        private ToolStripMenuItem arquivoToolStripMenuItem;
        private ToolStripMenuItem sairToolStripMenuItem;
        private MenuStrip menuStrip1;
        private TableLayoutPanel tableLayoutPanel2;
        private GroupBox groupBox1;
        private Label label1;
        private TextBox txtLink;
        private Label lblStatus;
        private Button btnAnalisar;
        private GroupBox groupFiltros;
        private Label label2;
        private Button btnBaixarSemEscolha;
        private ComboBox cmbResolucao;
        private Button btnBaixarComEscolha;
        private Label lblProcesso1;
        private TableLayoutPanel tableLayoutPanel1;
        private ToolStripMenuItem sobreToolStripMenuItem;
    }
}
