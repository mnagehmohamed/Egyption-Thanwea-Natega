using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using WMPLib;
namespace Final_Thanweya
{
    public partial class Form1 : Form
    {
        Final2023Entities Fentities= new Final2023Entities();
        WindowsMediaPlayer clap= new WindowsMediaPlayer();  
        int lblknowspeed = 5;
        int starspeed = 10;
        int facespeed = 8;
        int[] nums = { 5,10,25,50};
        int[] numsface = { 20, 30, 60, 70 };
        Random rand=new Random();
       // SqlConnection con = new SqlConnection(@"Password=123456;Persist Security Info=True;User ID=sa;Initial Catalog=Final2023;Data Source=.");
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string isnumber = "[0-9]";
            if (txtentryseating.Text.Trim() == "")
                MessageBox.Show("Please Enter Seating Number", "Natega ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (!Regex.IsMatch(txtentryseating.Text,isnumber))
            {
                MessageBox.Show("Please Enter Valid Seating Number", "Natega ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                   // picload.Visible = true;  
                    
                    Stage_New_Search stagesearch = new Stage_New_Search();
                    List<Stage_New_Search> Allsearches= new List<Stage_New_Search>();
                    int seating = int.Parse(txtentryseating.Text);
                    Allsearches = Fentities.Stage_New_Search.OrderByDescending(x => x.total_degree).Where(s=>s.total_degree<=410).ToList();
                    stagesearch = Allsearches.FirstOrDefault(s=>s.seating_no==seating);
                    int index = Allsearches.IndexOf(stagesearch);
                    if (stagesearch == null)
                    {
                        MessageBox.Show(" Seating Number Doesnt Exist", "Natega ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    txtseating.Text = stagesearch.seating_no.ToString();
                    txtname.Text = stagesearch.arabic_name;
                    txtcase.Text = stagesearch.student_case_desc.ToString();
                    txtcasenum.Text = stagesearch.student_case.ToString();
                    txtfinalsum.Text = stagesearch.total_degree.ToString();
                    txtstanding.Text=(index+1).ToString();
                    double finalpercent = Math.Round(double.Parse(txtfinalsum.Text) * 100 / 410,2);
                    txtpercent.Text =finalpercent+"%";
                    if (txtcase.Text == "ناجح دور أول")
                    {
                        clap.URL = "clap.mp3";
                        clap.controls.play();
                        panel6.BackColor = Color.SpringGreen;
                    } 
                    if (txtcase.Text == "راسب دور أول")
                    {
                        panel6.BackColor = Color.DarkRed;
                    }
                    if (txtcase.Text == "دور ثان")
                    {
                        panel6.BackColor = Color.Red;
                    }
                        
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel6.BackColor = Color.FloralWhite;
            foreach (Control c in this.Controls)
            {
                if (c is Panel ) 
                {
                    foreach (Control t in c.Controls) 
                    {
                        if (t is TextBox)
                        {
                            t.Text = "";
                            txtentryseating.Focus();
                        }
                    }
                   
                    
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult isexit = MessageBox.Show("Are You Sure You Want To Exit","Natega",MessageBoxButtons.YesNo,MessageBoxIcon.Question) ;
            if (isexit==DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int x = rand.Next(nums.Length);
            int y = rand.Next(numsface.Length);
            star.Top += starspeed;
            face.Top += facespeed;
            if (star.Top > this.ClientSize.Height)
            {
                star.Top = 0;
                star.Location=new Point(nums[x], 0);
            }
            if (face.Top > this.ClientSize.Height)
            {
                face.Top = 0;
                face.Location=new Point(numsface[y], 0);
            }
           
            lblknow.Left += lblknowspeed;
            if(lblknow.Bounds.IntersectsWith(pic1.Bounds)|| lblknow.Bounds.IntersectsWith(pic2.Bounds))
                lblknowspeed=-lblknowspeed;

        }
    }
}
