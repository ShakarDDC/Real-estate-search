using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_5_Miracle
{
    public partial class AddProperty : Form
    {
        List<string> strPhotos = new List<string>();
        List<string> decPhotos = new List<string>();
        string imageDec, imageLoc;
        int countImg = 0;
        int countClass = 0;
        int posImg = 0;
        private static Random random = new Random();
        private void btnAddPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFl = new OpenFileDialog();
            openFl.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            
            if (openFl.ShowDialog() == DialogResult.OK)
            {
                imageDec = @"img\"; 
                imageLoc = "";
                Image img = new Bitmap(openFl.FileName);
                imageLoc = Path.GetDirectoryName(openFl.FileName).ToString() + @"\" + openFl.SafeFileName;
                strPhotos.Add(imageLoc);
                imageDec += openFl.SafeFileName;
                decPhotos.Add(imageDec);
                pcbox.Image = Image.FromFile(strPhotos[countImg]);
                openFl.RestoreDirectory = true;
                if (strPhotos.Count>1)
                {
                    btnNextPhoto.Enabled = true;
                }
            }
        }
        public AddProperty()
        {
            InitializeComponent();
        }
        public static string RandomUniqe()
        {
            string uniqe;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            uniqe = new string(Enumerable.Repeat(chars, 1)
              .Select(s => s[random.Next(s.Length)]).ToArray()) + "-" + random.Next(9999);
            List<string> allIds = new List<string>();
            FileStream f = new FileStream("id.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(f);
            while (!sr.EndOfStream)
            {
                allIds.Add(sr.ReadLine());
            }
            sr.Close();
            f.Close();
            for (int i = 0; i < allIds.Count; i++)
            {
                if (allIds[i] == uniqe)
                {
                    return RandomUniqe();
                }
            }
            FileStream fs = new FileStream("id.txt", FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            sw.WriteLine(uniqe);
            sw.Close();
            fs.Close();
            return uniqe;
        }
        public static bool IsNumber(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if ((text[i] < '0') || (text[i] > '9'))
                {
                    return false;
                }
            }
            return true;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.ShowDialog();
            this.Close();
        }
        private void emptyVar()
        {
            tbxAge.Text = "";
            tbxAddress.Text = "";
            tbxBath.Text = "";
            tbxFloor.Text = "";
            tbxOaddress.Text = "";
            dateObirth.Text = "";
            tbxOemail.Text = "";
            tbxOname.Text = "";
            tbxOphone.Text = "";
            tbxOsurname.Text = "";
            tbxPrice.Text = "";
            tbxRooms.Text = "";
            tbxSize.Text = "";
            chbxAir.Checked = false;
            chbxBalc.Checked = false;
            chbxBath.Checked = false;
            chbxElevator.Checked = false;
            chbxFur.Checked = false;
            chbxGym.Checked = false;
            chbxInduc.Checked = false;
            chbxInternet.Checked = false;
            chbxPool.Checked = false;
            chbxSecur.Checked = false;
            chbxWater.Checked = false;
            chbxwithOwner.Checked = false;
            cmbxType.SelectedItem = null;
            lblID.Text = RandomUniqe();
            pcbox.Image = Properties.Resources._1;
            strPhotos.Clear();
            decPhotos.Clear();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
                string id = lblID.Text;
                string contractType = cmbxType.Text;
                string options = "";
                List<CheckBox> allcheckbox = new List<CheckBox> {chbxFur,chbxWater,chbxInduc,chbxwithOwner,chbxBath,
                    chbxBalc, chbxInternet, chbxAir, chbxSecur, chbxElevator, chbxPool, chbxGym};
                List<string> allproperty = new List<string>();
                List<TextBox> alltextboxes = new List<TextBox> {tbxRooms, tbxSize,tbxBath,tbxFloor,tbxAge, tbxPrice,tbxAddress,
                    tbxOname,tbxOphone,tbxOsurname,tbxOemail,tbxOaddress};
                for (int i = 0; i < allcheckbox.Count; i++)
                {
                    if (allcheckbox[i].Checked)
                    {
                        options += allcheckbox[i].Text + ",";
                    }
                }
               
                if (!IsNumber(tbxRooms.Text) || tbxRooms.Text=="" ||!IsNumber(tbxSize.Text) || tbxSize.Text=="" 
                    || contractType == "" || tbxBath.Text==""|| tbxAge.Text==""|| tbxPrice.Text==""||
                    !IsNumber(tbxBath.Text)  || !IsNumber(tbxAge.Text)  || !IsNumber(tbxPrice.Text) ||
                    tbxFloor.Text==""|| tbxOphone.Text==""||
                    !IsNumber(tbxFloor.Text) || !IsNumber(tbxOphone.Text) || tbxAddress.Text=="" || 
                    tbxOname.Text=="" || dateObirth.Text ==""||tbxOemail.Text==""||tbxOaddress.Text=="")
                {
                    MessageBox.Show("Use only numbers to phone,age,size,price,floor or Fill all data!");
                }
                else
                {
                    for (int i = 0; i < alltextboxes.Count; i++)
                    {
                        allproperty.Add(alltextboxes[i].Text);
                    }
                    allproperty.Add(dateObirth.Text);
                    AllPropertycs.allApartments.Add(new Apartments(id, allproperty[0], allproperty[1], allproperty[2], allproperty[3],
                    contractType, allproperty[4], allproperty[5], allproperty[6], options, allproperty[7],
                    allproperty[8], allproperty[9], allproperty[10], allproperty[12], allproperty[11]));
                    FileStream fs = new FileStream("property.txt", FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                    StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                    countClass = AllPropertycs.allApartments.Count;
                    sw.WriteLine("id: " + AllPropertycs.allApartments[countClass - 1].Id + "|" +
                        "\nRooms: " + AllPropertycs.allApartments[countClass - 1].Rooms + "|" +
                        "\nSize: " + AllPropertycs.allApartments[countClass - 1].Size + "|" +
                        "\nBathrooms: " + AllPropertycs.allApartments[countClass - 1].Bathrooms + "|" +
                        "\nFloor: " + AllPropertycs.allApartments[countClass - 1].Floor + "|" +
                        "\nContract type: " + AllPropertycs.allApartments[countClass - 1].ContactType + "|" +
                        "\nAge: " + AllPropertycs.allApartments[countClass - 1].Age + "|" +
                        "\nPrice: " + AllPropertycs.allApartments[countClass - 1].Price + "|" +
                        "\nAddress: " + AllPropertycs.allApartments[countClass - 1].Address + "|" +
                        "\nOptions: " + AllPropertycs.allApartments[countClass - 1].Options + "|" +
                        "\nOwner Name: " + AllPropertycs.allApartments[countClass - 1].OwnerName + "|" +
                        "\nOwner Phone: " + AllPropertycs.allApartments[countClass - 1].OwnerPhone + "|" +
                        "\nOwner Surname: " + AllPropertycs.allApartments[countClass - 1].OwnerSurename + "|" +
                        "\nOwner Email: " + AllPropertycs.allApartments[countClass - 1].OwnerEmail + "|" +
                        "\nOwner Date of Birth: " + AllPropertycs.allApartments[countClass - 1].OwnerDatebirth + "|" +
                        "\nOwner Address: " + AllPropertycs.allApartments[countClass - 1].OwnerAddress + "|");
                    string temp = "";
                    for (int i = 0; i < strPhotos.Count; i++)
                    {
                        File.Copy(strPhotos[i],
                            Path.GetDirectoryName(Application.ExecutablePath) + @"\img\" + id + "_" + i + Path.GetExtension(decPhotos[i]), true);
                        temp += @"img\" + id + "_" + i + Path.GetExtension(decPhotos[i]) + ",";
                    }
                    sw.WriteLine("File: " + temp + "|" +
                        "\n++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                    emptyVar();
                    MessageBox.Show("All data is correctly saved");
                    sw.Close();
                    fs.Close();
                }
        }
        //0 1,2
        private void btnNextPhoto_Click(object sender, EventArgs e)
        {
            btnPrevPhoto.Enabled = true;
            posImg++;
            pcbox.Image = Image.FromFile(strPhotos[posImg]);
            if (posImg+1==strPhotos.Count)btnNextPhoto.Enabled = false;
        }

        private void btnPrevPhoto_Click(object sender, EventArgs e)
        {
            btnNextPhoto.Enabled = true;
            posImg--;
            pcbox.Image = Image.FromFile(strPhotos[posImg]);
            if (posImg==0)btnPrevPhoto.Enabled = false;
        }

        private void AddProperty_Load(object sender, EventArgs e)
        {
            lblID.Text = RandomUniqe();
            pcbox.Image = Properties.Resources._1;
        }
    }
}
