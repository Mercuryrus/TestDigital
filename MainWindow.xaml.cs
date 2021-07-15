using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Resources;
using System.Windows.Forms;
using System.IO;
using TestDigital.Logic;
using System.Data;
using TestDigital.Models;
using TestDigital.Enums;

namespace TestDigital
{
    public partial class MainWindow : Window
    {
        private string _folder { get; set; }
        private List<List<int>> _res { get; set; }
        private List<CsvReadModel> _csvs { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var folder = new FolderBrowserDialog())
            {
                folder.ShowDialog();

                if (!string.IsNullOrEmpty(folder.SelectedPath))
                {
                    _folder = folder.SelectedPath;
                    DirectoryInfo directory = new DirectoryInfo(folder.SelectedPath);
                    csvFolder.Text = null;
                    csvFolder.Foreground = Brushes.Black;
                    foreach (FileInfo i in directory.GetFiles())
                    {
                        csvFolder.Text += $"{i.Name}\n";
                    }
                }
                else
                {
                    csvFolder.Text = null;
                    csvFolder.Foreground = Brushes.Red;
                    csvFolder.Text = "Папка не выбрана";
                }
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(_folder))
            { 
                CsvReader read = new CsvReader();
                CsvMath csvMath = new CsvMath();
                csvFolder.Text = null;
                _csvs = read.ReadCsv(_folder);
                if (_csvs.Count == 0)
                {
                    csvFolder.Foreground = Brushes.Red;
                    csvFolder.Text = null;
                    csvFolder.Text = "!!!Файлы формата ##.csv не обнаружены!!!";
                }

                else
                {
                    _res = csvMath.GetMainCsv(_csvs);
                    csvFolder.Foreground = Brushes.Green;
                    csvFolder.Text = "Файл обработан";

                }
            }
            else
            {
                csvFolder.Text = null;
                csvFolder.Foreground = Brushes.Red;
                csvFolder.Text = "Папка не выбрана";
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_folder))
            {
                if (_csvs.Count == 0)
                {
                    csvFolder.Foreground = Brushes.Red;
                    csvFolder.Text = null;
                    csvFolder.Text = "!!!Файлы формата ##.csv не обнаружены!!!";
                }
                else
                {
                    var dialog = new SaveFileDialog();
                    dialog.ShowDialog();
                    if (!string.IsNullOrEmpty(dialog.FileName))
                    {
                        CsvWriter write = new CsvWriter();
                        write.CsvWrite(dialog.FileName, _res);
                        csvFolder.Foreground = Brushes.Green;
                        csvFolder.Text = "Файл записан";
                    }
                    else
                    {
                        csvFolder.Foreground = Brushes.Red;
                        csvFolder.Text = "Выберите файл для сохранения";
                    }
                }
            }
            else
            {
                csvFolder.Text = null;
                csvFolder.Foreground = Brushes.Red;
                csvFolder.Text = "Папка не выбрана";
            }
        }
    }
}