using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_5_Miracle
{
    public partial class searchProperty : Form
    {
        string firstSym = ": ", endSym = "|", resultSt = "", fileContact = "property.txt";
        int countContact = 0, allContact =0, countImg = 0;
        int countData = 0, minPrice = 0, maxPrice = 0, posData=0;
        string contractType = "", rooms = "", temp = "";
        int checkMin = 0;
        List<string> allLine = new List<string>();
        List<RadioButton> rdbSearch = new List<RadioButton>();
        List<string> decPhotos = new List<string>();
        List<Label> allLabel;
        List<string> allPrices = new List<string>();
        List<string> allRooms = new List<string>();
        List<string> allType = new List<string>();
        List<string> searchResult = new List<string>();
        private void rbnPrice_CheckedChanged(object sender, EventArgs e)
        {
            tbxMaxPrice.Enabled = true;
            tbxMinPrice.Enabled = true;
            tbxRooms.Enabled = false;
            tbxRooms.Text = "";
            cmbxType.Enabled = false;
            cmbxType.Text = "";
        }

        private void rbnRooms_CheckedChanged(object sender, EventArgs e)
        {
            tbxMaxPrice.Enabled = false;
            tbxMaxPrice.Text = "";
            tbxMinPrice.Enabled = false;
            tbxMinPrice.Text = "";
            tbxRooms.Enabled = true;
            cmbxType.Enabled = false;
            cmbxType.Text = "";
        }

        private void rbnContract_CheckedChanged(object sender, EventArgs e)
        {
            tbxMaxPrice.Enabled = false;
            tbxMaxPrice.Text = "";
            tbxMinPrice.Enabled = false;
            tbxMinPrice.Text = "";
            tbxRooms.Enabled = false;
            tbxRooms.Text = "";
            cmbxType.Enabled = true;
        }

        private void btnNextProp_Click(object sender, EventArgs e)
        {
            btnPrevProp.Enabled = true;
            posData++;
            countImg = 0;
            decPhotos.Clear();
            labelUpdate();
        }

        private void btnPrevProp_Click(object sender, EventArgs e)
        {
            posData--;
            countImg = 0;
            decPhotos.Clear();
            labelUpdate();
        }


        private void btnPrevPhoto_Click(object sender, EventArgs e)
        {
            countImg--;
            btnNextPhoto.Enabled = true;
            if (countImg == 0) btnPrevPhoto.Enabled = false; 
            pbxImage.Image = Image.FromFile(decPhotos[countImg]); 
        }

        private void btnNextPhoto_Click(object sender, EventArgs e)
        {
            countImg++;
            btnPrevPhoto.Enabled = true;
            if (countImg == allContact - 2) btnNextPhoto.Enabled = false; 
            pbxImage.Image = Image.FromFile(decPhotos[countImg]);
           

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
        private bool isBigger(int min, int max, int cheking)
        {
            if(cheking>min && cheking < max)return true;
            return false;
        }
        private bool checkEmptyNumber(string a)//a
        {
            if (a != "" && IsNumber(a)) return true;
            return false;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
                cleanDate();
                if (rbnPrice.Checked)
                {
                    if (checkEmptyNumber(tbxMaxPrice.Text) && checkEmptyNumber(tbxMinPrice.Text))
                    {
                        minPrice = Convert.ToInt32(tbxMinPrice.Text);
                        maxPrice = Convert.ToInt32(tbxMaxPrice.Text);
                        for (int i = 0; i < allPrices.Count; i++)
                        {
                            checkMin = Convert.ToInt32(allPrices[i]);
                            if (isBigger(minPrice, maxPrice, checkMin))
                            {
                                for (int k = 18 * i; k < (i + 1) * 18; k++) searchResult.Add(allLine[k]);
                                labelUpdate();
                            }
                        }
                        if (searchResult.Count < 18)
                        {
                            lblPages.Text = "0 out of 0";
                            cleanDate();
                            btnNextProp.Enabled = false;
                            btnPrevProp.Enabled = false;
                            btnNextPhoto.Enabled = false;
                            btnPrevPhoto.Enabled = false;
                            lblPages.Visible = true;
                    }
                    }
                    else MessageBox.Show("Write only numbers or Write something!");
                }
                else if (rbnRooms.Checked)
                {
                    if (tbxRooms.Text != "" || IsNumber(tbxRooms.Text))
                    {
                        for (int i = 0; i < allRooms.Count; i++)
                        {
                            if (tbxRooms.Text == allRooms[i])
                            {
                                for (int k = 18 * i; k < (i + 1) * 18; k++) searchResult.Add(allLine[k]);
                                labelUpdate();
                            }
                        }
                        if (searchResult.Count <18 ) { 
                            lblPages.Text = "0 out of 0"; 
                            cleanDate(); 
                            lblPages.Visible = true;
                            btnNextProp.Enabled = false;
                            btnPrevProp.Enabled = false;
                            btnNextPhoto.Enabled = false;
                            btnPrevPhoto.Enabled = false;
                        }
                    }
                    else MessageBox.Show("Write only numbers or Write something!");
                }
                else
                {
                    if (cmbxType.Text != "")
                    {
                        for (int i = 0; i < allType.Count; i++)
                        {
                            if (cmbxType.Text == allType[i])
                            {
                                for (int k = 18 * i; k < (i + 1) * 18; k++) 
                                searchResult.Add(allLine[k]);
                                labelUpdate();
                            }
                        }
                        if (searchResult.Count < 18)
                        {
                            lblPages.Text = "0 out of 0";
                            cleanDate();
                            btnNextProp.Enabled = false;
                            btnPrevProp.Enabled = false;
                            btnNextPhoto.Enabled = false;
                            btnPrevPhoto.Enabled = false;
                            lblPages.Visible = true;
                        }
                    }
                    else MessageBox.Show("Choose contract type!");
                }
}

        
        private void labelUpdate()
        {
            countData = searchResult.Count / 18;
            if (countData > 1) btnNextProp.Enabled = true;
            if (posData == countData - 1) btnNextProp.Enabled = false;
            if (posData == 0) btnPrevProp.Enabled = false;
            countContact = posData * 18;
            lblPages.Text = (posData+1)+" out of " + countData;
            lblPages.Visible = true;
            lblID.Text = getBetween(searchResult[countContact], firstSym, endSym);
            lblID.Visible = true;
            lblRooms.Text = getBetween(searchResult[countContact + 1], firstSym, endSym);
            lblRooms.Visible = true;
            lblSize.Text = getBetween(searchResult[countContact + 2], firstSym, endSym);
            lblSize.Visible = true;
            lblBath.Text = getBetween(searchResult[countContact + 3], firstSym, endSym);
            lblBath.Visible = true;
            lblFloor.Text = getBetween(searchResult[countContact + 4], firstSym, endSym);
            lblFloor.Visible = true;
            lblContract.Text = getBetween(searchResult[countContact + 5], firstSym, endSym);
            lblContract.Visible = true;
            lblAge.Text = getBetween(searchResult[countContact + 6], firstSym, endSym);
            lblAge.Visible = true;
            lblPrice.Text = getBetween(searchResult[countContact + 7], firstSym, endSym);
            lblPrice.Visible = true;
            lblAddress.Text = getBetween(searchResult[countContact + 8], firstSym, endSym);
            lblAddress.Visible = true;
            tbxOptions.Text = getBetween(searchResult[countContact + 9], firstSym, endSym);
            tbxOptions.Visible = true;
            lblOname.Text = getBetween(searchResult[countContact + 10], firstSym, endSym);
            lblOname.Visible = true;
            lblOsurname.Text = getBetween(searchResult[countContact + 11], firstSym, endSym);
            lblOsurname.Visible = true;
            lblOphone.Text = getBetween(searchResult[countContact + 12], firstSym, endSym);
            lblOphone.Visible = true;
            lblOemail.Text = getBetween(searchResult[countContact + 13], firstSym, endSym);
            lblOemail.Visible = true;
            lblODateBirth.Text = getBetween(searchResult[countContact + 14], firstSym, endSym);
            lblODateBirth.Visible = true;
            lblOaddress.Text = getBetween(searchResult[countContact + 15], firstSym, endSym);
            lblOaddress.Visible = true;
            temp = getBetween(searchResult[countContact + 16], firstSym, endSym);
            btnNextPhoto.Enabled = false;
            btnPrevPhoto.Enabled = false;
            if (temp.Length <= 1)
            {
                pbxImage.Image = Properties.Resources._1;
            }
            else
            {
                decPhotos.Clear();
                decPhotos = temp.Split(",").ToList();
                allContact = decPhotos.Count;
                if (allContact > 1)
                {
                    btnNextPhoto.Enabled = true;
                    pbxImage.Image = Image.FromFile(decPhotos[countImg]);
                }
                else {
                    btnNextPhoto.Enabled = false;
                    btnPrevPhoto.Enabled = false;
                }
            }  
        }
        public searchProperty()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.ShowDialog();
            this.Close();
        }
        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }

            return "";
        }
        private void disableGr()
        {
            gbxOwner.Enabled = false;
            gbxProperty.Enabled = false;
            gbxSearch.Enabled = false;
            gbxPropertyData.Enabled = false;
            btnNextPhoto.Enabled = false;
            btnPrevPhoto.Enabled = false;
            btnNextProp.Enabled = false;
            btnPrevProp.Enabled = false;
        }
        private void cleanDate()
        {
            posData = 0;
            pbxImage.Image = Properties.Resources._1;
            tbxOptions.Text = "";
            decPhotos.Clear();
            countData = 0;
            lblID.Visible = false;
            lblRooms.Visible = false;
            lblSize.Visible = false;
            lblBath.Visible = false;
            lblFloor.Visible = false;
            lblContract.Visible = false;
            lblAge.Visible = false;
            lblPrice.Visible = false;
            lblAddress.Visible = false;
            lblOname.Visible = false;
            lblOsurname.Visible = false;
            lblOphone.Visible = false;
            lblOemail.Visible = false;
            lblODateBirth.Visible = false;
            lblOaddress.Visible = false;
            searchResult.Clear();
        }
        private void searchProperty_Load(object sender, EventArgs e)
        {
            if (!File.Exists(fileContact))
            {
                FileStream fs = new FileStream("property.txt", FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                MessageBox.Show("The property.txt file does not exist!");
                disableGr();
                fs.Close();
            }
            else
            {
                FileStream f = new FileStream(fileContact, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(f);
                while (!sr.EndOfStream)allLine.Add(sr.ReadLine());
                sr.Close(); f.Close();
                for (int i=0; i< allLine.Count;i++)
                {
                    if (Regex.IsMatch(allLine[i], "Rooms"))
                    {
                        resultSt = getBetween(allLine[i], firstSym, endSym);
                        allRooms.Add(resultSt);
                    }
                    else if (Regex.IsMatch(allLine[i], "Contract type"))
                    {
                        resultSt = getBetween(allLine[i], firstSym, endSym);
                        allType.Add(resultSt);
                    }
                    else if (Regex.IsMatch(allLine[i], "Price"))
                    {
                        resultSt = getBetween(allLine[i], firstSym, endSym);
                        allPrices.Add(resultSt);
                    }
                }
                if (allLine.Count < 18)
                {
                    MessageBox.Show("There is no data to show!");
                    disableGr();
                }
            }
        }
    }
}
