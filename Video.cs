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

namespace AVyoutube
{
    public class Video {
        private static int nextID = 1;
        public static int NextID { get { return nextID; } }
        private string url;
        public string Url { get { return url; } set { url = value; } }
        /*private string id_video;
        public string Id_video { get { return id_video; } set { id_video = value; } }
        private string id_audio;
        public string Id_audio { get { return id_audio; } set { id_audio = value; } }*/
        private Dictionary<string, string> dicionario = new Dictionary<string, string>();
        public Dictionary<string, string> Dicionario { get { return dicionario; } }
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


        public void listarFormatos(ComboBox cmb)
        {
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
               processo.OutputDataReceived += (sender, e) => {
                //Filtra dados inúteis retornados pela aplicação CLI
                if (e.Data != null && Regex.IsMatch(e.Data, @"^\d{3}") && !formatos_ignorados.Any(formato => e.Data.Contains(formato)))
                {
                       if (e.Data.Contains("avc1"))
                        {
                            foreach (string resolucao in formatos_resolucao_listados)
                            {
                                if (e.Data.Contains(resolucao))
                                {
                                   cmb.BeginInvoke((MethodInvoker)(() => cmb.Items.Add(resolucao + "p") ));
                                   video_id = Regex.Match(e.Data, @"\d{3}");
                                   if (video_id.Success)
                                   {

                                       dicionario[resolucao] = video_id.Value;
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

        public void baixarFormatoSelecionado()
        {
            comando_cmd = "yt-dlp " + this.url + " -f " + this.id_video + "+251";
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
            }
        }

        //Nao mexe mais nesse metodo
        public void baixarMelhorFormato(ListBox listbox)
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
                processo.OutputDataReceived += (sender, e) => {
                     // Verifique se e.Data é nulo antes de adicioná-lo à ListBox
                     if (e.Data != null)
                     {
                        string dados = e.Data.ToString();
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
