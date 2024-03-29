﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Affine_cipher
{
    public partial class FormMain : Form
    {
        private string[] filesEncrypt;
        private string[] filesDecrypt;
        private static int aInversed = Inverse(21);

        public FormMain()
        {
            InitializeComponent();
        }

        private void listBoxEncrypt_DragDrop(object sender, DragEventArgs e)
        {
            filesEncrypt = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string file in filesEncrypt)
            {
                listBoxEncrypt.Items.Add(file);
            }
        }

        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            foreach (string file in filesEncrypt)
            {
                byte[] to_enc = System.IO.File.ReadAllBytes(file);
                byte[] enc = AffineEncrypt(to_enc);
                System.IO.File.WriteAllBytes(file, enc);
                listBoxEncrypt.Items.Remove(file);
            }
            Array.Clear(filesEncrypt, 0, filesEncrypt.Length);
        }

        private void listBoxDecrypt_DragDrop(object sender, DragEventArgs e)
        {
            filesDecrypt = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string file in filesDecrypt)
            {
                listBoxDecrypt.Items.Add(file);
            }
        }

        private void buttonDecrypt_Click(object sender, EventArgs e)
        {
            foreach (string file in filesDecrypt)
            {
                byte[] enc = System.IO.File.ReadAllBytes(file);
                byte[] dec = AffineDecrypt(enc);
                System.IO.File.WriteAllBytes(file, dec);
                listBoxDecrypt.Items.Remove(file);
            }
            Array.Clear(filesDecrypt, 0, filesDecrypt.Length);
        }

        public static byte[] AffineEncrypt(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                int x = Convert.ToInt32(bytes[i]);
                bytes[i] = Convert.ToByte((21 * x + 57) % 256);
            }
            MessageBox.Show("Зашифровано!");
            return bytes;
        }

        public static byte[] AffineDecrypt(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                int x = Convert.ToInt32(bytes[i]);
                if (x - 57 < 0) x += 256;
                bytes[i] = Convert.ToByte(aInversed * (x - 57) % 256);
            }
            MessageBox.Show("Расшифровано!");
            return bytes;
        }

        public static int Inverse(int a)
        {
            for (int x = 1; x < 257; x++) if ((a * x) % 256 == 1) return x;
            return 0;
        }

        private void listBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }
    }
}
