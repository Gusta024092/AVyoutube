using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AVyoutube
{
    public class Video {
        private string url;
        public string Url { get { return url; } set { url = value; } }
        private static Dictionary<string, string> dicionario = new Dictionary<string, string>();
        public static Dictionary<string, string> Dicionario { get { return dicionario; } }
        private ProcessStartInfo inicioProcesso;
        private Process processo;
        private string comando_cmd = "";
        private bool rastreioStr;
        private int verificacao;
        private static List<string> formatos_ignorados = new List<string>
        {
            "m3u8", "opus", "m4a"
        };
        //Dict de leitura para resolução
        private static Dictionary<string, string> formatos_resolucao = new Dictionary<string, string>()
        {
            { "144p15", "x144     15" },
            { "144p24", "x144     24" },
            { "144p", "x144     30" },
            { "240p", "x240" },
            { "360p", "x360" },
            { "480p", "x480" },
            { "720p24", "x720    24" },
            { "720p", "x720    30" },
            { "720p60", "x720    60" },
            { "1080p24", "x1080   24" },
            { "1080p", "x1080   30" },
            { "1080p60", "x1080   60" }
        };
        private Match match;

        public Video(string url) 
        {
            this.url = url;
        }

        private string resolucao_formatada(string formato)
        {
            foreach (var formato_selecionado in formatos_resolucao)
            {
                if (formato_selecionado.Value == formato)
                {
                    return formato_selecionado.Key;
                }
            }
            return "Desconhecido";
        }

        private void progressoDownload(Label lblProcesso, System.Windows.Forms.ProgressBar progress, string dados)
        {

            match = Regex.Match(dados, @"\b\d+(?:\.\d+)?");
            int parteInteira = 0;
            string[] pedacos = null;
            if (match.Success)
            {
                pedacos = match.Value.Split('.');
                parteInteira = int.Parse(pedacos[0]);
                progress.BeginInvoke((MethodInvoker)(() => progress.Value = parteInteira));
                if (parteInteira == 100)
                {
                    verificacao += 1;
                }
            }
            if (verificacao == 2)
            {
                lblProcesso.BeginInvoke((MethodInvoker)(() => lblProcesso.Text = "Baixando Áudio...  " + parteInteira.ToString() + "%"));
            }
            else if (verificacao == 4)
            {
                lblProcesso.BeginInvoke((MethodInvoker)(() => lblProcesso.Text = "Concluído"));
            }
            else
            {
                lblProcesso.BeginInvoke((MethodInvoker)(() => lblProcesso.Text = "Baixando Video...  " + parteInteira.ToString() + "%"));
            }
        }

        private void listar(System.Windows.Forms.ComboBox cmb, List<System.Windows.Forms.Button> botoes)
        {
            System.Windows.Forms.Button btnAnalisar = botoes[0];
            System.Windows.Forms.Button btnSemEscolha = botoes[1];
            System.Windows.Forms.Button btnComEscolha = botoes[2];
            dicionario.Clear();
            comando_cmd = "yt-dlp " + this.url + " -F";
            inicioProcesso = parametrosTerminal(comando_cmd);
            //Task.Run(() =>
            //{
            /*btnAnalisar.BeginInvoke((MethodInvoker)(() => btnAnalisar.Enabled = false));
            btnComEscolha.BeginInvoke((MethodInvoker)(() => btnComEscolha.Enabled = false));
            btnSemEscolha.BeginInvoke((MethodInvoker)(() => btnSemEscolha.Enabled = false));*/
                btnAnalisar.Enabled = true;
                btnComEscolha.Enabled = true;
                btnSemEscolha.Enabled = true;


                using (processo = new Process())
                {
                    string dados = "";
                    processo.StartInfo = inicioProcesso;
                    Match video_id;
                    List<string> linhasProcessadas = new List<string>(); // lista de linhas processadas para evitar duplicatas

                    processo.OutputDataReceived += (sender, e) =>
                    {
                        if (e.Data == null || !Regex.IsMatch(e.Data, @"^\d{3}") || formatos_ignorados.Any(formato => e.Data.Contains(formato)))
                            return;

                        if (!e.Data.Contains("avc1"))
                            return;

                        foreach (var resolucao in formatos_resolucao)
                        {
                            if (!e.Data.Contains(resolucao.Value))
                                continue;

                            // Verifica linha processada e evita o risco mencionado
                            if (linhasProcessadas.Contains(e.Data))
                                continue;

                            string res = resolucao_formatada(resolucao.Value);
                            cmb.BeginInvoke((MethodInvoker)(() => cmb.Items.Add(res)));
                            linhasProcessadas.Add(e.Data); // Adiciona a linha à lista de linhas processadas

                            video_id = Regex.Match(e.Data, @"\d{3}");
                            if (video_id.Success)
                            {
                                dicionario[res] = video_id.Value;
                            }
                        }
                    };

                    processo.Start();
                    processo.BeginOutputReadLine();
                    processo.BeginErrorReadLine();

                    processo.WaitForExit();
                    if (processo.ExitCode == 0)
                    {
                        /*btnAnalisar.BeginInvoke((MethodInvoker)(() => btnAnalisar.Enabled = true));
                        btnComEscolha.BeginInvoke((MethodInvoker)(() => btnComEscolha.Enabled = true));
                        btnSemEscolha.BeginInvoke((MethodInvoker)(() => btnSemEscolha.Enabled = true));*/
                        btnAnalisar.Enabled = true;
                        btnComEscolha.Enabled = true;
                        btnSemEscolha.Enabled = true;
                    }
                }
            //});
        }

        private ProcessStartInfo parametrosTerminal(string args)
        {
            inicioProcesso = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/C {args}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            return inicioProcesso;
        }

        private void baixar(Label lblProcesso, System.Windows.Forms.ProgressBar progress, string comando_cmd)
        {
            lblProcesso.Text = "Aguarde... ";
            verificacao = 0;
            inicioProcesso = parametrosTerminal(comando_cmd);
            //Aloca este algoritmo para um outro processo
            Task.Run(() =>
            {
                using (processo = new Process())
                {
                    processo.StartInfo = inicioProcesso;
                    processo.OutputDataReceived += (sender, e) =>
                    {
                        // Verifique se e.Data é nulo antes de adicioná-lo à ListBox
                        if (e.Data != null)
                        {
                            string dados = e.Data.ToString();
                            if (e.Data.Contains("[download]") && !string.IsNullOrEmpty(e.Data))
                            {
                                progressoDownload(lblProcesso, progress, dados);
                            }
                        }
                    };

                    processo.Start();
                    processo.BeginOutputReadLine();
                    processo.BeginErrorReadLine();

                    processo.WaitForExit();
                    if (processo.ExitCode == 0)
                    {
                        MessageBox.Show("Download Finalizou", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Download Falhou", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            });
        }
        //Metodo para formato selecionado
        public void baixarFormatoSelecionado(Label lblProcesso, System.Windows.Forms.ProgressBar progress, string video_id)
        {
            comando_cmd = "yt-dlp " + this.url + " -f " + video_id + "+251";
            baixar(lblProcesso, progress, comando_cmd);
        }
        //Nao mexe mais nesse metodo
        public void baixarMelhorFormato(Label lblProcesso, System.Windows.Forms.ProgressBar progress)
        {
            comando_cmd = "yt-dlp " + this.url;
            baixar(lblProcesso, progress, comando_cmd);
        }
        //Método estático
        public static bool verificar(string executavel)
        {
            try
            {
                ProcessStartInfo inicioProcesso = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/C {executavel}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                using (Process processo = Process.Start(inicioProcesso))
                {
                    processo.WaitForExit();
                    return processo.ExitCode != 1;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        //Lista e add para combobox
        public void listarFormatos(System.Windows.Forms.ComboBox cmb, List<System.Windows.Forms.Button> botoes)
        {
            listar(cmb, botoes);
        }
    }
}
