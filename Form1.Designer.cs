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
            txtLink = new TextBox();
            label1 = new Label();
            btnAnalisar = new Button();
            cmbResolucao = new ComboBox();
            groupFiltros = new GroupBox();
            label2 = new Label();
            btnBaixarSemEscolha = new Button();
            btnBaixarComEscolha = new Button();
            progressBar1 = new ProgressBar();
            listBox1 = new ListBox();
            arquivoToolStripMenuItem = new ToolStripMenuItem();
            sairToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1 = new MenuStrip();
            groupFiltros.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // txtLink
            // 
            resources.ApplyResources(txtLink, "txtLink");
            txtLink.Name = "txtLink";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // btnAnalisar
            // 
            resources.ApplyResources(btnAnalisar, "btnAnalisar");
            btnAnalisar.Name = "btnAnalisar";
            btnAnalisar.UseVisualStyleBackColor = true;
            btnAnalisar.Click += btnAnalisar_Click;
            // 
            // cmbResolucao
            // 
            resources.ApplyResources(cmbResolucao, "cmbResolucao");
            cmbResolucao.FormattingEnabled = true;
            cmbResolucao.Name = "cmbResolucao";
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
            btnBaixarSemEscolha.Name = "btnBaixarSemEscolha";
            btnBaixarSemEscolha.UseVisualStyleBackColor = true;
            btnBaixarSemEscolha.Click += btnBaixarSemEscolha_Click;
            // 
            // btnBaixarComEscolha
            // 
            resources.ApplyResources(btnBaixarComEscolha, "btnBaixarComEscolha");
            btnBaixarComEscolha.Name = "btnBaixarComEscolha";
            btnBaixarComEscolha.UseVisualStyleBackColor = true;
            btnBaixarComEscolha.Click += button1_Click;
            // 
            // progressBar1
            // 
            resources.ApplyResources(progressBar1, "progressBar1");
            progressBar1.Name = "progressBar1";
            // 
            // listBox1
            // 
            resources.ApplyResources(listBox1, "listBox1");
            listBox1.FormattingEnabled = true;
            listBox1.Name = "listBox1";
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
            menuStrip1.Items.AddRange(new ToolStripItem[] { arquivoToolStripMenuItem });
            menuStrip1.Name = "menuStrip1";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(listBox1);
            Controls.Add(progressBar1);
            Controls.Add(groupFiltros);
            Controls.Add(btnAnalisar);
            Controls.Add(label1);
            Controls.Add(txtLink);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            groupFiltros.ResumeLayout(false);
            groupFiltros.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
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
        private Label label2;
        private ToolStripMenuItem arquivoToolStripMenuItem;
        private ToolStripMenuItem sairToolStripMenuItem;
        private MenuStrip menuStrip1;
    }
}
