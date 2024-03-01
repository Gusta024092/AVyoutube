using System;
using System.Security.Policy;
using System.Threading.Tasks;

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
        private List<Button> lista_botoes = new List<Button>();
        public Form1()
        {
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
            lista_botoes.Add(btnAnalisar);
            lista_botoes.Add(btnBaixarComEscolha);
            lista_botoes.Add(btnBaixarSemEscolha);
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

        private async void btnAnalisar_Click(object sender, EventArgs e)
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
            await Task.Run(() => video.listarFormatos(cmbResolucao, lista_botoes));
            Cursor = Cursors.Default;
            if (cmbResolucao.Items.Count > 0)
            {
                lblStatus.Text = "Status: Finalizado";
                lblStatus.ForeColor = Color.Blue;
                cmbResolucao.SelectedIndex = 0;
            }
            else
            {
                lblStatus.Text = "Status: Erro na busca";
                lblStatus.ForeColor = Color.Red;
            }
        }

        private void btnBaixarSemEscolha_Click(object sender, EventArgs e)
        {
            if (url_verificada != null && cmbResolucao.Items.Count > 0)
            {
                video = new Video(url_verificada);
                video.baixarMelhorFormato(lblProcesso1, progressBar1);
            }
            else
            {
                sem_Links();
            }

        }

        private void sem_Links()
        {
            MessageBox.Show("Insira a URL que deseja baixar", "Sem url na busca", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //Botao para baixar com base na escolha
        private void button1_Click(object sender, EventArgs e)
        {
            if (url_verificada != null)
            {
                video = new Video(url_verificada);
                foreach (KeyValuePair<string, string> valor in Video.Dicionario)
                {
                    if (cmbResolucao.SelectedItem != null && valor.Key == cmbResolucao.SelectedItem.ToString())
                    {
                        video.baixarFormatoSelecionado(lblProcesso1, progressBar1, valor.Value);
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
                sem_Links();
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

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {
            //
        }
    }
}
