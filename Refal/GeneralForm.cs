using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Refal
{
    public partial class GeneralForm : Form
    {
        public GeneralForm()
        {
            InitializeComponent();
            l = new Logic();
        }

        Encoding encoding = Encoding.UTF8;
        Logic l;

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myOpen();
        }

        void myOpen()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Текстовые файлы(*.txt)|*.txt";
            string filename = dialog.FileName;
            dialog.Multiselect = false;
            char[] sep = { '\\' };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.FilterIndex == 1)
                {
                    try
                    {
                        richTextBoxEdit.Text = File.ReadAllText(dialog.FileName);
                        ActiveForm.Text = dialog.FileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                        return;
                    }
                }
            }
        }

        void mySave()
        {
            try
            {
                SaveFileDialog sfd = saveFileDialog1;
                saveFileDialog1.Filter = "Текстовый файл|*.txt|Все форматы|*.*";
                saveFileDialog1.Title = "Сохранить файл как...";
                char[] sep = { '\\' };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string fileName = sfd.FileName;
                    string strFilExtn = fileName.Remove(0, fileName.Length - 3);
                    switch (strFilExtn)
                    {
                        case "txt":
                            saveToTxt(fileName);
                            ActiveForm.Text = "Файл \"" + fileName + "\" сохранен";
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                richTextBoxEdit.Text += ex.Message + "\n" + ex.StackTrace + "\n";
            }
        }

        void saveToTxt(string filename)
        {
            StreamWriter outStream = new StreamWriter(filename, false, encoding, 1000);
            outStream.WriteLine(richTextBoxEdit.Text);
            outStream.Close();
        }

        // сохраняет только имя файла
        string shortName(string file)
        {
            char[] sep = { '\\' };
            string[] shortName = file.Split(sep);
            return shortName[shortName.Length - 1];
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mySave();
        }

        private void запуститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < richTextBoxEdit.Lines.Length; i++)
            {
                if (Logic.checkExpression(richTextBoxEdit.Lines[i]))
                { }
                else
                {
                    MessageBox.Show(i + "-я строка имеет неверный формат.\n", "Синтаксическая ошибка");
                }
            }
        }

        private void распечататьСписокНаЭкранToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxEdit.Text += "\n" + l.writeList();
        }



        /**
         * то, что загрузили, сразу в массив
         * каждый элемент содержит ссылку на предшественника и последователя - двусторонний массив
         * поле зрения - левая часть предложения замены (структурные скобки, переменные, символы)
         * цикл while
         * своппинг - временное сохранение информации на диски с последующей загрузкой в ОП
         * класс, который будет порождать переменные по предложениям языка определенного типа
         * поставить её в отождествление первой и если не отождествилось, то ошибка
         * */
    }
}
