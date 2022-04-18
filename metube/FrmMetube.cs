using System.Diagnostics;

namespace metube
{
    public partial class FrmMetube : Form
    {
        private static readonly string CmdPath = @"C:\Windows\System32\cmd.exe"; 

        //异步回显（1-2-3-4）
        // 1.定义委托
        public delegate void DelReadStdOutput(string result);
        public delegate void DelReadErrOutput(string result);
        // 2.定义委托事件
        public event DelReadStdOutput ReadStdOutput;
        public event DelReadErrOutput ReadErrOutput;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmMetube()
        {
            InitializeComponent();
        }

        private void FrmMetube_Load(object sender, EventArgs e)
        {
            

        }

        /// <summary>
        /// 执行cmd命令
        /// 多命令请使用批处理命令连接符：
        /// <![CDATA[
        /// &:同时执行两个命令
        /// |:将上一个命令的输出,作为下一个命令的输入
        /// &&：当&&前的命令成功时,才执行&&后的命令
        /// ||：当||前的命令失败时,才执行||后的命令]]>
        /// 其他请百度
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="output"></param>
        public static void RunCmd(string cmd, out string output)
        {
            cmd = cmd.Trim().TrimEnd('&') + "&exit";//说明：不管命令是否成功均执行exit命令，否则当调用ReadToEnd()方法时，会处于假死状态
            using (Process p = new Process())
            {
                p.StartInfo.FileName = CmdPath;
                p.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;  //由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;   //重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;          //不显示程序窗口
                p.Start();//启动程序

                //向cmd窗口写入命令
                p.StandardInput.WriteLine(cmd);
                p.StandardInput.AutoFlush = true;

                //获取cmd窗口的输出信息
                output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();//等待程序执行完退出进程
                p.Close();
            }
        }

        /// <summary>
        /// 异步执行命令，跟踪处理进度
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public string RunCmd(string cmd)
        {

            //3.将相应函数注册到委托事件中
            ReadStdOutput += new DelReadStdOutput(ReadStdOutputAction);
            ReadErrOutput += new DelReadErrOutput(ReadErrOutputAction);

            try
            {
                //string strInput = Console.ReadLine();
                Process p = new Process();
                //设置要启动的应用程序
                p.StartInfo.FileName = "cmd.exe";
                //是否使用操作系统shell启动
                p.StartInfo.UseShellExecute = false;
                // 接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardInput = true;
                //输出信息
                p.StartInfo.RedirectStandardOutput = true;
                // 输出错误
                p.StartInfo.RedirectStandardError = true;
                //不显示程序窗口
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;

                p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
                p.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);

                //启用Exited事件
                p.EnableRaisingEvents = true;
                p.Exited += new EventHandler(Process_Exited);

                //启动程序
                p.Start();
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();
                p.StandardInput.AutoFlush = true;

                //输入命令
                p.StandardInput.WriteLine(cmd);
                p.StandardInput.WriteLine("exit");

                //获取输出信息
                //string strOuput = p.StandardOutput.ReadToEnd();
                //等待程序执行完退出进程
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
                // 4. 异步调用，需要invoke
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
            Console.WriteLine("命令执行完毕"); 
        }

        /// <summary>
        /// 获取视频信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetContent_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtVideoPath.Text.Trim()))
            {
                MessageBox.Show("请输入视频地址！");
                return;
            }


            string strCmd = @"yt-dlp -F " + this.txtVideoPath.Text.Trim();
            string output = "";

            try
            {
                RunCmd(strCmd, out output);

                this.lsbAudio.Items.Clear();
                this.lsbVideo.Items.Clear();

                //去掉头部，只保留正文
                output = output.Substring(output.LastIndexOf("------") + 6
                                            , output.Length - output.LastIndexOf("------") - 6).Trim();

                //正文拆分成数组
                string[] strRes = output.Split(new string[] { "\n" }, StringSplitOptions.None);

                //通过循环获取出音频和视频
                for (int i = 0; i < strRes.Length; i++)
                {
                    //判断是否是音频
                    if (strRes[i].IndexOf("audio only") > 0)
                    {
                        //音频添加一行的方法
                        lsbAudio.Items.Add(strRes[i]);
                    }
                    else
                    {
                        //视频添加一行的方法
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
        /// 下载视频
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtVideoPath.Text.Trim()))
            {
                MessageBox.Show("请输入视频地址！");
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
                    MessageBox.Show("请选择音频或者视频！");
                    return;
                }
                                

                //如果路径为空，则保存至默认路径
                if (string.IsNullOrEmpty(strFilePahth))
                {
                    strCmd += " " + this.txtVideoPath.Text.Trim() + " --merge-output-format mp4";
                    
                    //this.txtContent.AppendText("\r\n" + output); 
                }
                else//保存至指定路径
                {
                    strCmd += " " + this.txtVideoPath.Text.Trim() + " --merge-output-format mp4 --path " +  this.txtFilePath.Text.Trim();
                }

                RunCmd(strCmd);//执行命令
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }          

        }

        /// <summary>
        /// 选择视频下载保存路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bthPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtFilePath.Text = dialog.SelectedPath.Trim();
                //string foldPath = dialog.SelectedPath;
                //MessageBox.Show("已选择文件夹:" + foldPath, "选择文件夹提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 清空运行日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCleanLog_Click(object sender, EventArgs e)
        {
            this.txtContent.Clear();
        }
    }
}