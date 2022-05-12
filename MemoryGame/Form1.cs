namespace MemoryGame
{
    public partial class Form1 : Form
    {
        private int counterForWin = 0;
        Random random = new Random();
        private List<string> pictures;
        private PictureBox firstClicked = null;
        private PictureBox secondClicked = null;

        public Form1()
        {
            InitializeComponent();
            LoadPictures();
            AddPicturesToSquares();
        }

        private void LoadPictures()
        {
            pictures = Directory.GetFiles(@"Pictures").ToList();
            pictures.AddRange(pictures); //Duplicate pictures
            
        }

        private void AddPicturesToSquares()
        {
            foreach (var control in tableLayoutPanel.Controls)
            {
                PictureBox pic = control as PictureBox;

                if (pic != null)
                {
                    int randomNum = random.Next(pictures.Count);
                    pic.Tag = @$"{pictures[randomNum]}";
                    pic.Image = Image.FromFile(@"question-mark.jpg");
                    pictures.RemoveAt(randomNum);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                return;
            }

            PictureBox picBox = sender as PictureBox;

            if (picBox != null)
            {
                if (firstClicked == null)
                {
                    firstClicked = picBox;
                    firstClicked.Image = Image.FromFile(picBox.Tag.ToString());

                    return;
                }

                secondClicked = picBox;
                secondClicked.Image = Image.FromFile(picBox.Tag.ToString());


                if (firstClicked.Tag == secondClicked.Tag)
                {
                    firstClicked = null;
                    secondClicked = null;
                    counterForWin++;
                    CheckIfWin();
                    return;
                }

                timer1.Start();

            }
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            
            firstClicked.Image = Image.FromFile(@"question-mark.jpg");
            secondClicked.Image = Image.FromFile(@"question-mark.jpg");

            firstClicked = null;
            secondClicked = null;
        }

        private void CheckIfWin()
        {
            if (counterForWin == 8)
            {
                MessageBox.Show("Congratulations! You find all matching pictures.");
                Close();
            }
        }
    }
}