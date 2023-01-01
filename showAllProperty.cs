using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
namespace Project_5_Miracle
{
    public partial class showAllProperty : Form
    {
        string alltext, firstSym = ": ", endSym = "|", resultSt = "", imageLoc, fileContact = "property.txt";
        int countContact = 0, allContact = 0, pagePrev = 1, pageNext, countImg=0; int decCount = 0;
        List<string> allLine = new List<string>();
        List<string> decPhotos = new List<string>();
        List<Label> allLabel;
        List<CheckBox> allcheckboxes;
        List<string> words = new List<string>();

        private void chbxFur_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnNextProp_Click(object sender, EventArgs e)
        {
            words.Clear();
            decPhotos.Clear();
            allContact = 0;
            countContact += 18;
            pagePrev++;
            btnNextPhoto.Enabled = false;
            btnPrevPhoto.Enabled = false;
            btnPrevProp.Enabled = true;
            for (int k = 0; k < allcheckboxes.Count; k++)
            {
                allcheckboxes[k].Checked = false;
            }
                scrollNextContact();
        }

        private void btnPrevProp_Click(object sender, EventArgs e)
        {
            words.Clear();
            decPhotos.Clear();
            allContact = 0;
            countContact -= 18;
            pagePrev--;
            btnNextProp.Enabled = true;
            btnNextPhoto.Enabled = false;
            btnPrevPhoto.Enabled = false;
            for (int k = 0; k < allcheckboxes.Count; k++)
            {
                allcheckboxes[k].Checked = false;
            }
            scrollNextContact();
        }

        
        public showAllProperty()
        {
            InitializeComponent();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.ShowDialog();
            this.Close();
        }

        private void btnNextPhoto_Click(object sender, EventArgs e)
        {
            countImg++;
            btnPrevPhoto.Enabled = true;
            pcbox.Image = Image.FromFile(decPhotos[countImg]);
            if (countImg + 2 == decPhotos.Count) btnNextPhoto.Enabled = false;
        }

        private void btnPrevPhoto_Click(object sender, EventArgs e)
        {
            countImg--;
            btnNextPhoto.Enabled = true;
            pcbox.Image = Image.FromFile(decPhotos[countImg]);
            if (countImg == 0) btnPrevPhoto.Enabled = false;
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
        private void scrollNextContact()
        {
            
            lblPages.Text = pagePrev + " out of " + pageNext;
            if (decCount >= 1)
            {
                btnNextPhoto.Enabled = true;
            }
            if (pagePrev == 1)
            {
                btnPrevProp.Enabled = false;
            }
            if (pagePrev == pageNext)
            {
                btnNextProp.Enabled = false;
            }
            for (int i = countContact; i < countContact + 18; i++)
            {
                alltext = allLine[i];
                if (Regex.IsMatch(alltext, "Options"))
                {
                    resultSt = getBetween(alltext, firstSym, endSym);
                    words.Clear();
                    words = resultSt.Split(',').ToList();
                    for(int j = 0; j < words.Count; j++)
                    {
                        for(int k=0; k < allcheckboxes.Count; k++)
                        {
                            if (words[j] == allcheckboxes[k].Text)
                            {
                                allcheckboxes[k].Checked = true;
                            }
                        }
                    }
                }
                else if (Regex.IsMatch(alltext, "File"))
                {
                    imageLoc = getBetween(alltext, firstSym, endSym);
                    //FileStream fs = new System.IO.FileStream(imageLoc, FileMode.Open, FileAccess.Read);
                    if (imageLoc == "")
                    {
                        pcbox.Image = Properties.Resources._1;
                        btnNextPhoto.Enabled = false;
                        btnPrevPhoto.Enabled = false;
                    }
                    else
                    {
                        decPhotos.Clear();
                        decPhotos = imageLoc.Split(',').ToList();
                        decCount = decPhotos.Count;
                        pcbox.Image = Image.FromFile(decPhotos[countImg]);
                    } break;
                }
                else
                {
                    resultSt = getBetween(alltext, firstSym, endSym);
                    allLabel[allContact].Text = resultSt;
                    allLabel[allContact].Visible = true;
                    allContact++;
                }
            }
        }
        private void showAllProperty_Load(object sender, EventArgs e)
        {
            allLabel = new List<Label>() { lblID, lblRooms, lblSize, lblBath, lblFloor, lblContract,
            lblAge, lblPrice, lblAddress, lblOname, lblOphone, lblOsurname, lblOemail, lblOdateBirth, lblOaddress};
            allcheckboxes = new List<CheckBox>() {chbxFur,chbxWater,chbxInduc,chbxwithOwner,chbxBath,
                    chbxBalc, chbxInternet, chbxAir, chbxSecur, chbxElevator, chbxPool, chbxGym};
            if (!File.Exists(fileContact))
            {
                FileStream fs = new FileStream("property.txt", FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                MessageBox.Show("The property.txt file does not exist!");
                gbxProperty.Enabled = false;
                gbxOwner.Enabled = false;
                fs.Close();
            }
            else //
            {
                FileStream f = new FileStream(fileContact, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(f);
                while (!sr.EndOfStream)
                {
                    allLine.Add(sr.ReadLine());
                }
                sr.Close();
                f.Close();
                pageNext = allLine.Count / 18;
                if (allLine.Count < 18)
                {
                    MessageBox.Show("There is no data to show!");
                    gbxProperty.Enabled = false;
                    gbxOwner.Enabled = false;
                    btnNextProp.Enabled = false;
                    btnPrevProp.Enabled = false;
                }
                else
                {
                    scrollNextContact();
                    if (decPhotos.Count > 1)
                    {
                        btnNextPhoto.Enabled = true;
                    }
                    
                }
                 
            }
        }
    }
}
