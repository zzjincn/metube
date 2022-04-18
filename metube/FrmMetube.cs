using System.Diagnostics;

namespace metube
{
    public partial class FrmMetube : Form
    {
        private static readonly string CmdPath = @"C:\Windows\System32\cmd.exe"; 

        //�첽���ԣ�1-2-3-4��
        // 1.����ί��
        public delegate void DelReadStdOutput(string result);
        public delegate void DelReadErrOutput(string result);
        // 2.����ί���¼�
        public event DelReadStdOutput ReadStdOutput;
        public event DelReadErrOutput ReadErrOutput;

        /// <summary>
        /// ���캯��
        /// </summary>
        public FrmMetube()
        {
            InitializeComponent();
        }

        private void FrmMetube_Load(object sender, EventArgs e)
        {
            

        }

        /// <summary>
        /// ִ��cmd����
        /// ��������ʹ���������������ӷ���
        /// <![CDATA[
        /// &:ͬʱִ����������
        /// |:����һ����������,��Ϊ��һ�����������
        /// &&����&&ǰ������ɹ�ʱ,��ִ��&&�������
        /// ||����||ǰ������ʧ��ʱ,��ִ��||�������]]>
        /// ������ٶ�
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="output"></param>
        public static void RunCmd(string cmd, out string output)
        {
            cmd = cmd.Trim().TrimEnd('&') + "&exit";//˵�������������Ƿ�ɹ���ִ��exit������򵱵���ReadToEnd()����ʱ���ᴦ�ڼ���״̬
            using (Process p = new Process())
            {
                p.StartInfo.FileName = CmdPath;
                p.StartInfo.UseShellExecute = false;        //�Ƿ�ʹ�ò���ϵͳshell����
                p.StartInfo.RedirectStandardInput = true;   //�������Ե��ó����������Ϣ
                p.StartInfo.RedirectStandardOutput = true;  //�ɵ��ó����ȡ�����Ϣ
                p.StartInfo.RedirectStandardError = true;   //�ض����׼�������
                p.StartInfo.CreateNoWindow = true;          //����ʾ���򴰿�
                p.Start();//��������

                //��cmd����д������
                p.StandardInput.WriteLine(cmd);
                p.StandardInput.AutoFlush = true;

                //��ȡcmd���ڵ������Ϣ
                output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();//�ȴ�����ִ�����˳�����
                p.Close();
            }
        }

        /// <summary>
        /// �첽ִ��������ٴ������
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public string RunCmd(string cmd)
        {

            //3.����Ӧ����ע�ᵽί���¼���
            ReadStdOutput += new DelReadStdOutput(ReadStdOutputAction);
            ReadErrOutput += new DelReadErrOutput(ReadErrOutputAction);

            try
            {
                //string strInput = Console.ReadLine();
                Process p = new Process();
                //����Ҫ������Ӧ�ó���
                p.StartInfo.FileName = "cmd.exe";
                //�Ƿ�ʹ�ò���ϵͳshell����
                p.StartInfo.UseShellExecute = false;
                // �������Ե��ó����������Ϣ
                p.StartInfo.RedirectStandardInput = true;
                //�����Ϣ
                p.StartInfo.RedirectStandardOutput = true;
                // �������
                p.StartInfo.RedirectStandardError = true;
                //����ʾ���򴰿�
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;

                p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
                p.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);

                //����Exited�¼�
                p.EnableRaisingEvents = true;
                p.Exited += new EventHandler(Process_Exited);

                //��������
                p.Start();
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();
                p.StandardInput.AutoFlush = true;

                //��������
                p.StandardInput.WriteLine(cmd);
                p.StandardInput.WriteLine("exit");

                //��ȡ�����Ϣ
                //string strOuput = p.StandardOutput.ReadToEnd();
                //�ȴ�����ִ�����˳�����
                //p.WaitForExit();
                //p.Close();
                //return strOuput;
                return string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ex.Message;
            }
        }

        private void ReadStdOutputAction(string result)
        {
            this.txtContent.AppendText(result + "\r\n");
        }

        private void ReadErrOutputAction(string result)
        {
            this.txtContent.AppendText(result + "\r\n");
        }

        private void p_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                // 4. �첽���ã���Ҫinvoke
                this.Invoke(ReadStdOutput, new object[] { e.Data });
                //Console.WriteLine(e.Data);
            }
        }

        private void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                this.Invoke(ReadErrOutput, new object[] { e.Data });
                //Console.WriteLine(e.Data);
            }
        }

        private void Process_Exited(object sender, EventArgs e)
        { 
            Console.WriteLine("����ִ�����"); 
        }

        /// <summary>
        /// ��ȡ��Ƶ��Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetContent_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtVideoPath.Text.Trim()))
            {
                MessageBox.Show("��������Ƶ��ַ��");
                return;
            }


            string strCmd = @"yt-dlp -F " + this.txtVideoPath.Text.Trim();
            string output = "";

            try
            {
                RunCmd(strCmd, out output);

                this.lsbAudio.Items.Clear();
                this.lsbVideo.Items.Clear();

                //ȥ��ͷ����ֻ��������
                output = output.Substring(output.LastIndexOf("------") + 6
                                            , output.Length - output.LastIndexOf("------") - 6).Trim();

                //���Ĳ�ֳ�����
                string[] strRes = output.Split(new string[] { "\n" }, StringSplitOptions.None);

                //ͨ��ѭ����ȡ����Ƶ����Ƶ
                for (int i = 0; i < strRes.Length; i++)
                {
                    //�ж��Ƿ�����Ƶ
                    if (strRes[i].IndexOf("audio only") > 0)
                    {
                        //��Ƶ���һ�еķ���
                        lsbAudio.Items.Add(strRes[i]);
                    }
                    else
                    {
                        //��Ƶ���һ�еķ���
                        lsbVideo.Items.Add(strRes[i]);
                    }
                }

                //this.txtContent.AppendText(output);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ������Ƶ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtVideoPath.Text.Trim()))
            {
                MessageBox.Show("��������Ƶ��ַ��");
                return;
            }

            string strCmd = @"yt-dlp -f ";
            string strTempAudo = string.Empty;
            string strTempVideo = string.Empty;
            string strFilePahth = this.txtFilePath.Text.Trim();

            try
            {                
                if (this.lsbAudio.SelectedIndex > -1)
                {
                    strCmd += this.lsbAudio.SelectedItems[0].ToString().Substring(0, this.lsbAudio.SelectedItems[0].ToString().IndexOf(' '));

                    if (this.lsbVideo.SelectedIndex > -1)
                    {
                        strCmd += "+" + this.lsbVideo.SelectedItems[0].ToString().Substring(0, this.lsbVideo.SelectedItems[0].ToString().IndexOf(' '));
                    }
                }
                else if (this.lsbVideo.SelectedIndex > -1)
                {
                    strCmd += this.lsbVideo.SelectedItems[0].ToString().Substring(0, this.lsbVideo.SelectedItems[0].ToString().IndexOf(' '));
                }
                else
                {
                    MessageBox.Show("��ѡ����Ƶ������Ƶ��");
                    return;
                }
                                

                //���·��Ϊ�գ��򱣴���Ĭ��·��
                if (string.IsNullOrEmpty(strFilePahth))
                {
                    strCmd += " " + this.txtVideoPath.Text.Trim() + " --merge-output-format mp4";
                    
                    //this.txtContent.AppendText("\r\n" + output); 
                }
                else//������ָ��·��
                {
                    strCmd += " " + this.txtVideoPath.Text.Trim() + " --merge-output-format mp4 --path " +  this.txtFilePath.Text.Trim();
                }

                RunCmd(strCmd);//ִ������
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }          

        }

        /// <summary>
        /// ѡ����Ƶ���ر���·��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bthPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "��ѡ���ļ�·��";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtFilePath.Text = dialog.SelectedPath.Trim();
                //string foldPath = dialog.SelectedPath;
                //MessageBox.Show("��ѡ���ļ���:" + foldPath, "ѡ���ļ�����ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// ���������־
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCleanLog_Click(object sender, EventArgs e)
        {
            this.txtContent.Clear();
        }
    }
}