using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AVyoutube
{
    public class Video {
        private static int nextID = 1;
        public static int NextID { get { return nextID; } }
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
        private static List<string> formatos_resolucao_listados = new List<string>
        {
            "x144     15", "x144     24", "x144     30", "x240", "x360", "x480", "x720    24", "x720    30", "x720    60", "x1080   24", "x1080   30", "x1080   60"
        };
        private static List<string> formatos_resolucao_legiveis = new List<string>
        {
            "144p15", "144p24","144p", "240p", "360p", "480p", "720p24", "720p", "720p60", "1080p24", "1080p", "1080p60"
        };
        private Match match;

        public Video(string url) 
        {
            this.url = url;
            nextID += 1;
        }

        private string resolucao_formatada(string formato)
        {
            for (int i = 0; i < formatos_resolucao_listados.Count; i++)
            {
                if (formato == formatos_resolucao_listados[i])
                {
                    return formatos_resolucao_legiveis[i];
                }
            }
            return "Desconhecido";
        }

        //Lista e add para combobox
        public void listarFormatos(System.Windows.Forms.ComboBox cmb, List<System.Windows.Forms.Button> botoes)
        {
            listar(cmb, botoes);
        }

        private void listar(System.Windows.Forms.ComboBox cmb, List<System.Windows.Forms.Button> botoes)
        {
            System.Windows.Forms.Button btnAnalisar = botoes[0];
            System.Windows.Forms.Button btnSemEscolha = botoes[1];
            System.Windows.Forms.Button btnComEscolha = botoes[2];

            
            dicionario.Clear();
            comando_cmd = "yt-dlp " + this.url + " -F";
            inicioProcesso = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/C {comando_cmd}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            Task.Run(() =>
            {
                btnAnalisar.BeginInvoke((MethodInvoker)(() => btnAnalisar.Enabled = false));
                btnComEscolha.BeginInvoke((MethodInvoker)(() => btnComEscolha.Enabled = false));
                btnSemEscolha.BeginInvoke((MethodInvoker)(() => btnSemEscolha.Enabled = false));


                using (processo = new Process())
                {
                    string dados = "";
                    processo.StartInfo = inicioProcesso;
                    Match video_id;
                    List<string> linhasProcessadas = new List<string>(); // lista de linhas processadas para evitar duplicatas

                    processo.OutputDataReceived += (sender, e) =>
                    {
                        if (e.Data != null && Regex.IsMatch(e.Data, @"^\d{3}"))
                        {
                            if (!formatos_ignorados.Any(formato => e.Data.Contains(formato)))
                            {
                                if (e.Data.Contains("avc1"))
                                {
                                    foreach (string resolucao in formatos_resolucao_listados)
                                    {
                                        if (e.Data.Contains(resolucao))
                                        {
                                            // Verifica linha processada e evita o risco mencionado
                                            if (!linhasProcessadas.Contains(e.Data))
                                            {
                                                string res = resolucao_formatada(resolucao);
                                                cmb.BeginInvoke((MethodInvoker)(() => cmb.Items.Add(res)));
                                                linhasProcessadas.Add(e.Data); // Adiciona a linha à lista de linhas processadas

                                                video_id = Regex.Match(e.Data, @"\d{3}");
                                                if (video_id.Success)
                                                {
                                                    dicionario[res] = video_id.Value;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    };

                    processo.Start();
                    processo.BeginOutputReadLine();
                    processo.BeginErrorReadLine();

                    processo.WaitForExit();
                    if (processo.ExitCode == 0)
                    {
                        btnAnalisar.BeginInvoke((MethodInvoker)(() => btnAnalisar.Enabled = true));
                        btnComEscolha.BeginInvoke((MethodInvoker)(() => btnComEscolha.Enabled = true));
                        btnSemEscolha.BeginInvoke((MethodInvoker)(() => btnSemEscolha.Enabled = true));
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

        private void baixar(Label lblProcesso, System.Windows.Forms.ProgressBar progress, string comando_cmd)
        {
            verificacao = 0;
            inicioProcesso = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/C {comando_cmd}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

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
                    MessageBox.Show(verificacao.ToString());
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
    }
}
