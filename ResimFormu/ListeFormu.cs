using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResimFormu
{
    public partial class ListeFormu : Form
    {
        public ListeFormu()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"server=WIN7-BILGISAYAR\SQLEXPRESS; database=net11db; integrated security=true");
        private void ListeFormu_Load(object sender, EventArgs e)
        {
            Markalar();
        }

        void Markalar()
        {
            SqlDataAdapter adp = new SqlDataAdapter("select * from Arabalar", con);
            DataSet ds = new DataSet();

            adp.Fill(ds, "Arabalar");

            lbMarka.DisplayMember = "Marka";
            lbMarka.ValueMember = "ArabaID";
            lbMarka.DataSource = ds.Tables["Arabalar"];
        }

        private void lbMarka_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select * from Arabalar where ArabaID=@id", con);
            adp.SelectCommand.Parameters.AddWithValue("@id", lbMarka.SelectedValue);

            #region Eğer AddWithValue SQL Tarafında Hata verirse (tür uyumsuzluğu hatası ortaya çıkarsa)
            //SqlParameter p1 = new SqlParameter("@id", SqlDbType.Int);
            //p1.Value = (int)lbMarka.SelectedValue; 
            #endregion

            DataSet ds = new DataSet();
            adp.Fill(ds);//bu haliyle ds içinde isimsiz bir tablo var

            txtModel.Text = ds.Tables[0].Rows[0]["Model"].ToString();
            txtYil.Text = ds.Tables[0].Rows[0]["Yil"].ToString();

            string resimYolu = ds.Tables[0].Rows[0]["Foto"].ToString();

            try
            {
                Bitmap bmp = new Bitmap(resimYolu);
                pbResim.Image = bmp;
            }
            catch{ }



        }
    }
}
