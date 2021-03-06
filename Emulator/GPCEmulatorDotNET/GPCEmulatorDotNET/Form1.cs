﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using GPCEmulator;
namespace GPCEmulatorDotNET
{
    public partial class Form1 : Form
    {
        private Device device;
        public Form1()
        {
            InitializeComponent();
           
   
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            device.RAM.Mem[lb1.SelectedIndex] = byte.Parse(txtWrite.Text, System.Globalization.NumberStyles.HexNumber);
            lb1.Items[lb1.SelectedIndex]=(lb1.SelectedIndex.ToString("X")+ ": " +byte.Parse(txtWrite.Text, System.Globalization.NumberStyles.HexNumber).ToString("X"));
        }

        private void populateLB()
        {
         lb1.Items.Clear();
            int i = 0;
            foreach (byte b in device.RAM.Mem)
            {
                lb1.Items.Add(i.ToString("X")+ ": " +b.ToString("X"));
                i++;
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            device = new Device();
            populateLB();
        }
        private void tick()
        {
            device.Tick();
            lblPointer.Text = "Pointer: " + device.Cpu.getMemoryPointer().ToString("X");

            lb1.Items[device.RAM.lastChanged] = device.RAM.lastChanged.ToString("X") + ": " +
                                                device.RAM.Mem[device.RAM.lastChanged].ToString("X");
         

        }

        private void btnTick_Click(object sender, EventArgs e)
        {
            tick();
            
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            timer1.Interval = Convert.ToDouble(textBox1.Text);
            timer1.Enabled = true;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            populateLB();
        }

        private void timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            tick();
        }

        private void loadMemoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.ShowDialog();
            byte[] bytes;
            bytes = File.ReadAllBytes(od.FileName);
            for (int i = 0; i < bytes.Length; i++)
            {
                device.RAM.Mem[i] = bytes[i];
            }
            populateLB();
        }

       
    }
}