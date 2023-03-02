using System.IO;
using Microsoft.VisualBasic;

namespace _08
{
    public partial class Form1 : Form
    {
        ImageList largeGalery = new ImageList();
        ImageList smalGalery = new ImageList();


        public Form1()
        {
            InitializeComponent();
            foreach (string drive in Directory.GetLogicalDrives())
            {
                TreeNode rootNode = new TreeNode(drive);
                rootNode.Tag = drive;
                treeView1.Nodes.Add(rootNode);

                try
                {
                    foreach (string directory in Directory.GetDirectories(drive))
                    {
                        TreeNode childNode = new TreeNode(Path.GetFileName(directory));
                        childNode.Tag = directory;
                        rootNode.Nodes.Add(childNode);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //for (int i = 0; i < 5; i++)
            //{
            //    TreeNode tv = new TreeNode($"TreeView {i}");
            //    for (int j = 0; j < 5; j++)
            //    {
            //        tv.Nodes.Add(new TreeNode($"Дочірній нод{j}"));
            //    }
            //    treeView1.Nodes.Add(tv);
            //}

            //ListView



            listView1.Columns.Add("Ім'я",300);            
            listView1.Columns.Add("Формат",300);            
            listView1.Columns.Add("Дата",300);          
            listView1.Columns.Add("Розмір",300);          
            listView1.Columns.Add("Атрибут",300);          
            listView1.Columns.Add("Кількість файлів",300);


            largeGalery.ImageSize = new Size(150, 150);
            smalGalery.ImageSize = new Size(50, 50);
            listView1.LargeImageList = largeGalery;
            listView1.SmallImageList = smalGalery;
            Bitmap bitmap = new Bitmap(@"C:\\Users\\dvlad\\Desktop\\images.jpg");
            Bitmap bitmap2 = new Bitmap(@"C:\\Users\\dvlad\\Desktop\\images2.jpg");

            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.UtcNow;
            DateTime date3 = DateTime.Today;
            string[] date = { $"{date1},{date2},{date3}" };
            listView1.Items.Add(new ListViewItem(new string[] { "Тест 1", "Fail png", date[0] },0));
            listView1.Items.Add(new ListViewItem(new string[] { "тест 2", "Fail png", date[0] },1));

            largeGalery.Images.Add(bitmap);
            smalGalery.Images.Add(bitmap2);

            listView1.View = View.Details;

            richTextBox1.AllowDrop= true;
            richTextBox1.DragDrop += RichTextBox1_DragDrop;

            richTextBox1.DragEnter += RichTextBox1_DragEnter;

            treeView1.AfterSelect += TreeView1_AfterSelect;

        }

        private void TreeView1_AfterSelect(object? sender, TreeViewEventArgs e)
        {
            listView1.Items.Clear();
            if (e.Node.Tag is string)
            {
                string path = (string)e.Node.Tag;
                try
                {
                    foreach (string file in Directory.GetFiles(path))
                    {
                        FileInfo fileInfo = new FileInfo(file);

                        ListViewItem item = new ListViewItem(fileInfo.Name);
                        item.SubItems.Add(Path.GetExtension(fileInfo.Name));
                        item.SubItems.Add(fileInfo.CreationTime.ToString());
                        item.SubItems.Add(fileInfo.Length.ToString());
                        item.SubItems.Add(fileInfo.Attributes.ToString());
                        item.SubItems.Add(fileInfo.Attributes.ToString());

                        item.Tag = file;

                        listView1.Items.Add(item);
                    }
                    int numFiles = Directory.GetFiles(path).Length;
                    foreach (string directory in Directory.GetDirectories(path))
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo(directory);

                        ListViewItem item = new ListViewItem(directoryInfo.Name);
                        item.SubItems.Add("");
                        item.SubItems.Add(directoryInfo.CreationTime.ToString());
                        item.SubItems.Add(directoryInfo.Attributes.ToString() + " " );
                        item.SubItems.Add("");
                        item.SubItems.Add( numFiles.ToString() + " files");
                        
                        item.Tag = directory;

                        listView1.Items.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка: " + ex.Message);
                }
            }
        }

        private void RichTextBox1_DragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void RichTextBox1_DragDrop(object? sender, DragEventArgs e)
        {

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0 && File.Exists(files[0]))
            {
                richTextBox1.Text = File.ReadAllText(files[0]);
            }
        }

        private void largeIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;
            
        }

        private void smallIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.SmallIcon;
            

        }

        private void tileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.Tile;
            

        }

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            
        }
        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {

            listView1.View = View.List;

        }
        

        
    }
}