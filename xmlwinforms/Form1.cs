using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace xmlwinforms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadXmlFile("books.xml");
        }

        //Метод для загрузки XML-файла
        private void LoadXmlFile(string filePath)
        {
            try
            {
                // Очищаем ListBox перед загрузкой новых данных
                listBox1.Items.Clear();

                // Создаем новый XML-документ
                XmlDocument doc = new XmlDocument();

                // Загружаем XML-файл
                doc.Load(filePath);

                // Получаем все узлы "book"
                XmlNodeList bookNodes = doc.SelectNodes("//book");

                // Если нет книг в файле
                if (bookNodes == null || bookNodes.Count == 0)
                {
                    listBox1.Items.Add("В файле не найдены книги");
                    return;
                }

                // Перебираем все книги и добавляем в ListBox
                foreach (XmlNode bookNode in bookNodes)
                {
                    string title = bookNode.SelectSingleNode("title")?.InnerText ?? "Нет названия";
                    string author = bookNode.SelectSingleNode("author")?.InnerText ?? "Неизвестный автор";

                    // Добавляем информацию о книге в ListBox
                    listBox1.Items.Add($"Название: {title}, Автор: {author}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при чтении XML-файла: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    LoadXmlFile(openFileDialog.FileName);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
    }
}
