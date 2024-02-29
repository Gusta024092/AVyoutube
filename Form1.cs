using System;
using System.Security.Policy;

namespace AVyoutube
{
    public partial class Form1 : Form
    {
        private Video video;
        private MessageBoxIcon icon;
        private MessageBoxButtons buttons;
        private string msg = "";
        private string title = "";
        private string url_verificada = null;
        public Form1()
        {
            this.title = "";
            InitializeComponent();
            cmbResolucao.DropDownStyle = ComboBoxStyle.DropDownList;
            bool ytdlp = Video.verificar("yt-dlp");
            bool ffmpeg = Video.verificar("ffmpeg -i"); //Ffmpeg retornou apenas 1, colocado a flag -i resolve o problema
            msg = "O executável não foi encontrado no seu sistema, \n verifique se foi adicionado corretamente as variáveis de ambiente";
            icon = MessageBoxIcon.Error;
            buttons = MessageBoxButtons.OK;
            if (!ytdlp)
            {
                title = "Falha ao encontrar yt-dlp";
                MessageBox.Show(msg, title, buttons, icon);
            }
            if (!ffmpeg)
            {
                title = "Falha ao encontrar ffmpeg";
                MessageBox.Show(msg, title, buttons, icon);
            }

        }

        private string verificarUrl()
        {
            string url = txtLink.Text;
            icon = MessageBoxIcon.Error;
            buttons = MessageBoxButtons.OK;
            msg = "Preencha a url corretamente";
            if (url.Length == 0 || string.IsNullOrEmpty(url))
            {
                title = "Link Vazio";
                MessageBox.Show(msg, title, buttons, icon);
                return null;
            }
            if (url.Length < 5)
            {
                title = "Link Curto";
                MessageBox.Show(msg, title, buttons, icon);
                return null;
            }

            return url;
        }

        private void btnAnalisar_Click(object sender, EventArgs e)
        {
            limpar_cmbBox();
            string url = verificarUrl();
            if (url == null)
            {
                return;
            }
            url_verificada = url;
            video = new Video(url_verificada);
            lblStatus.Text = "Status: Aguarde";
            lblStatus.ForeColor = Color.Gray;
            Cursor = Cursors.WaitCursor;
            video.listarFormatos(cmbResolucao, listBox1);
            Cursor = Cursors.Default;
            lblStatus.Text = "Status: Finalizado";
            lblStatus.ForeColor = Color.Blue;
            if (cmbResolucao.SelectedIndex != -1)
            {
                cmbResolucao.SelectedIndex = 0;
            }
        }

        private void btnBaixarSemEscolha_Click(object sender, EventArgs e)
        {
            if (url_verificada != null)
            {
                video = new Video(url_verificada);
                video.baixarMelhorFormato(listBox1, progressBar1);
            }
            else
            {
                MessageBox.Show("Insira a URL que deseja baixar");
            }

        }

        //Botao para baixar com base na escolha
        private void button1_Click(object sender, EventArgs e)
        {
            if (url_verificada != null)
            {
                video = new Video(url_verificada);
                foreach (KeyValuePair<string, string> valor in Video.Dicionario)
                {
                        if (cmbResolucao.SelectedItem != null && valor.Key == cmbResolucao.SelectedItem.ToString() )
                        {
                            video.baixarFormatoSelecionado(listBox1, progressBar1, valor.Value);
                            return;
                        } 
                }
                icon = MessageBoxIcon.Error;
                buttons = MessageBoxButtons.OK;
                MessageBox.Show("Nenhuma resolução foi selecionado", "Sem dados", buttons, icon);
                return;

            }
            else
            {
                MessageBox.Show("Insira a URL que deseja baixar");
            }
        }

        public void limpar_cmbBox()
        {
            cmbResolucao.Items.Clear();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
