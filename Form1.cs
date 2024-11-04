using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace vislice
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string beseda;
        List<Label> labels = new List<Label>();
        int zgresene = 0;
        enum DeliVislic
        {
            VisliNavpicno,
            VisliPrecno,
            Podpora,
            Vrv,
            Telo,
            Glava,
            DesnaRoka,
            LevaRoka,
            DesnaNoga,
            LevaNoga
        }

        void Risi()//preverjanje izrisa delov vislic
        {
            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.Black, 7);
            DrawDeliVislic(DeliVislic.VisliNavpicno);
            DrawDeliVislic(DeliVislic.Podpora);
            DrawDeliVislic(DeliVislic.VisliPrecno);
            DrawDeliVislic(DeliVislic.Vrv);
            DrawDeliVislic(DeliVislic.Glava);
            DrawDeliVislic(DeliVislic.Telo);
            DrawDeliVislic(DeliVislic.DesnaRoka);
            DrawDeliVislic(DeliVislic.LevaRoka);
            DrawDeliVislic(DeliVislic.DesnaNoga);
            DrawDeliVislic(DeliVislic.LevaNoga);


        }
        void DrawDeliVislic(DeliVislic v)
        {
            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.Black, 7);
            if (v == DeliVislic.VisliNavpicno)
                g.DrawLine(p, new Point(300, 350), new Point(300, 10));
            else if (v == DeliVislic.VisliPrecno)
                g.DrawLine(p, new Point(300, 10), new Point(140, 10));
            else if (v == DeliVislic.Podpora)
                g.DrawLine(p, new Point(300, 80), new Point(220, 10));
            else if (v == DeliVislic.Vrv)
                g.DrawLine(p, new Point(140, 80), new Point(140, 10));
            else if (v == DeliVislic.Glava)
                g.DrawEllipse(p, 115, 80, 50, 50);
            else if (v == DeliVislic.Telo)
                g.DrawLine(p, new Point(140, 250), new Point(140, 130));
            else if (v == DeliVislic.DesnaRoka)
                g.DrawLine(p, new Point(80, 180), new Point(140, 130));
            else if (v == DeliVislic.LevaRoka)
                g.DrawLine(p, new Point(200, 180), new Point(140, 130));
            else if (v == DeliVislic.DesnaNoga)
                g.DrawLine(p, new Point(80, 300), new Point(140, 250));
            else if (v == DeliVislic.LevaNoga)
                g.DrawLine(p, new Point(200, 300), new Point(140, 250));
        }

        void CharDash()
        {
            beseda = NaklucnaBeseda();
            char[] crke = beseda.ToCharArray();
            int presledek = 490 / crke.Length - 1;
            for (int i = 0; i < crke.Length; i++)
            {
                labels.Add(new Label());
                labels[i].Location = new Point((i * presledek) + 10, 100);
                labels[i].Text = "_";
                labels[i].Parent = groupBox2;
                labels[i].BringToFront();
                labels[i].CreateControl();

            }
        }

        string NaklucnaBeseda()
        {
            string[] besede = File.ReadAllLines("C:\\Pro 2 seminarska\\besede.txt");
            Random r = new Random();
            return besede[r.Next(0, besede.Length - 1)];

        }
        private void button1_Click(object sender, EventArgs e)
        {
            char crka = textBox1.Text.ToCharArray()[0];
            if (!char.IsLetter(crka))
            {
                MessageBox.Show("Niste vnesli crke!", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (beseda.Contains(crka))
            {
                char[] crke = beseda.ToCharArray();
                for (int i = 0; i < crke.Length; i++)
                {
                    if (crke[i] == crka)
                        labels[i].Text = crka.ToString();
                    textBox1.Text = "";
                }
                foreach (Label l in labels)
                    if (l.Text == "_") return;
                MessageBox.Show("Zmaga!!!!", "Congrats");
                ResetIgre();
            }
            else
            {
                MessageBox.Show("Crka ni v besedi, vnesi drugo crko", "Opala...");
                label1.Text += " " + crka.ToString() + ",";
                DrawDeliVislic((DeliVislic)zgresene);
                zgresene++;
                textBox1.Text = "";
                if (zgresene == 10)
                {
                    MessageBox.Show("Konec igre! Beseda je bila " + beseda);
                    ResetIgre();
                }
            }
        }

        void ResetIgre()
        {
            Graphics g = panel1.CreateGraphics();
            g.Clear(panel1.BackColor);
            NaklucnaBeseda();
            CharDash();
            label1.Text = "napacne crke:";
            textBox1.Text = "";
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == beseda)
            {
                MessageBox.Show("Zmaga!!!!", "Congrats");
                ResetIgre();
            }
            else
            {
                MessageBox.Show("Beseda, ki ste jo ugibali, ni pravilna", "Oprosti");
                DrawDeliVislic((DeliVislic)zgresene);
                zgresene++;
                if (zgresene == 10)
                {
                    MessageBox.Show("Konec igre! Beseda je bila " + beseda);
                    ResetIgre();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {


        }

        private void button3_Click(object sender, EventArgs e)
        {
            NaklucnaBeseda();
            CharDash();
            ResetIgre();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}