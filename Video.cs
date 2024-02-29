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
        private static List<string> formatos_ignorados = new List<string>
        {
            "m3u8", "opus", "m4a"
        };
        private static List<string> formatos_resolucao_listados = new List<string>
        {
            "144", "240", "360", "480", "720", "1080"
        };

        public Video(string url) 
        {
            this.url = url;
            nextID += 1;
        }

        //Lista e add para combobox
        public void listarFormatos(System.Windows.Forms.ComboBox cmb, ListBox lst)
        {
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

            using (processo = new Process())
            {
                string dados = "";
                processo.StartInfo = inicioProcesso;
                MessageBox.Show("Processo foi iniciado!!! ");
                Match video_id;
                List<string> linhasProcessadas = new List<string>(); // lista de linhas processadas para evitar duplicatas

                processo.OutputDataReceived += (sender, e) => {
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
                                            cmb.BeginInvoke((MethodInvoker)(() => cmb.Items.Add(resolucao + "p")));
                                            lst.BeginInvoke((MethodInvoker)(() => lst.Items.Add(e.Data)));
                                            linhasProcessadas.Add(e.Data); // Adiciona a linha à lista de linhas processadas

                                            video_id = Regex.Match(e.Data, @"\d{3}");
                                            if (video_id.Success)
                                            {
                                                dicionario[resolucao + "p"] = video_id.Value;
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
            }
        }

        //Metodo para formato selecionado
        public void baixarFormatoSelecionado(string video_id)
        {
            comando_cmd = "yt-dlp " + this.url + " -f " + video_id + "+251";
            inicioProcesso = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/C {comando_cmd}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (processo = new Process())
            {
                string dados = "";
                processo.StartInfo = inicioProcesso;
                MessageBox.Show("Processo foi iniciado!!! ");
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
        }

        //Nao mexe mais nesse metodo
        public void baixarMelhorFormato(ListBox listbox, System.Windows.Forms.ProgressBar progress)
        {
            comando_cmd = "yt-dlp " + this.url;

            inicioProcesso = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/C {comando_cmd}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (processo = new Process())
            {
                processo.StartInfo = inicioProcesso;
                MessageBox.Show("Processo foi iniciado!!! ");
                Match match;
                double valorPercentual = 0;
                processo.OutputDataReceived += (sender, e) => {
                     // Verifique se e.Data é nulo antes de adicioná-lo à ListBox
                     if (e.Data != null)
                     {
                        string dados = e.Data.ToString();
                        match = Regex.Match(dados, @"\b\d+(?:\.\d+)?");
                        if (match.Success && e.Data.Contains("[download]") && !string.IsNullOrEmpty(e.Data))
                        {
                            valorPercentual = Convert.ToDouble(match.Value);
                            progress.BeginInvoke((MethodInvoker)(() => progress.Value = (int)valorPercentual));
                        }
                        listbox.BeginInvoke((MethodInvoker)(() => listbox.Items.Add(dados)));

                    }
                 };

                 processo.ErrorDataReceived += (sender, e) => {
                     if (e.Data != null)
                     {
                         // Adicione o erro ao ListBox na thread principal
                         listbox.BeginInvoke((MethodInvoker)(() => listbox.Items.Add(e.Data)));
                     }
                 };

                processo.Start();
                processo.BeginOutputReadLine();
                processo.BeginErrorReadLine();

                processo.WaitForExit();
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
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
