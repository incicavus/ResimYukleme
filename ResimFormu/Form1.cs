using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResimFormu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnResimSec_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            //sadece resim dosyası seçilebilmeli Filter
            open.Filter = "Resim Dosyası (.jpg)|*.jpg|Resim Dosyası (.jpeg)|*.jpeg";
            open.ShowDialog();
            //img1478.jpg
            //img1478.jpg
            try
            {
                res = new Bitmap(open.FileName);//c:\desktop\barış\remote.jpg
                resim = Guid.NewGuid().ToString();//resimAdi olarak 
                string uzanti = Path.GetExtension(open.FileName);
                resim += uzanti;//konumu ve uzantısı dahil
                pbResim.Image = res;
                btnKaydet.Enabled = true;
            }
            catch
            {
                btnKaydet.Enabled = false;
            }
        }
        string resim; Bitmap res;
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog save = new FolderBrowserDialog();
                save.ShowDialog();

                string saveLocation = save.SelectedPath + @"\" + resim;

                SqlConnection con = new SqlConnection(@"server=WIN7-BILGISAYAR\SQLEXPRESS; database=net11db; integrated security=true");
                SqlCommand komut = new SqlCommand("insert Arabalar values(@marka, @model, @yil, @yol)", con);
                komut.Parameters.AddWithValue("@marka", txtMarka.Text);
                komut.Parameters.AddWithValue("@model", txtModel.Text);
                komut.Parameters.AddWithValue("@yil", txtYil.Text);
                komut.Parameters.AddWithValue("@yol", saveLocation);
                con.Open();
                komut.ExecuteNonQuery();//işlem sadece veritabanı kaydını gerçekleştirir
                con.Close();
                //ayrıca seçilen resmi, seçilen lokasyona fiziksel olarak kaydetmemiz gerekir.
                res.Save(saveLocation);//resim dosyasını kaydet

                pbResim.Image = null;
                txtYil.Clear();
                txtModel.Clear();
                txtMarka.Clear();
            }
            catch{}

        }

        private void btnListe_Click(object sender, EventArgs e)
        {
            ListeFormu frm = new ListeFormu();
            frm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
