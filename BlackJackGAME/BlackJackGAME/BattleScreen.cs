using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackJackGAME
{
    public partial class BattleScreen : Form
    {
        private Dictionary<string,int> numbers = new Dictionary<string, int>()
        {
            {"A",1},{"2",2}, {"3",3},{"4",4},{"5",5},{"6",6},{"7",7},{"8",8},{"9",9},{"T",10},{"J",11},{"Q",12},{"K",13}
        };
        private string[] types = { "S", "D", "H", "C" };
        private List<Card> list = new List<Card>();

        private int myScore;
        private int enemyScore;

        public BattleScreen()
        {
            //Listにカードをセット
            foreach (string type in types)
            {
                foreach (string number in numbers.Keys)
                {
                    list.Add(new Card(type, number));
                }
            }
            list=list.OrderBy(a => Guid.NewGuid()).ToList();

            //画面の認識
            InitializeComponent();

            //ディーラー２枚プレイヤー２枚セット
            enemyCardLabel1.Text= removeCard();
            enemyCardLabel2.Text=removeCard();
            myCardlabel1.Text=removeCard();
            myCardlabel2.Text=removeCard();

            //残り枚数表示
            remainingNumberLabel.Text=list.Count.ToString();


            
        }

        private void hitButton_Click(object sender, EventArgs e)
        {
            //カードをドロー
            myCardLabel3.Text=removeCard();

            //残り枚数表示
            remainingNumberLabel.Text = list.Count.ToString();

            //カード表示
            myCardLabel3.Visible = true;
            pictureBox6.BorderStyle = BorderStyle.FixedSingle;

            myScore += numbers[myCardLabel3.Text];

            //スコア表示
            myScoreLabel.Text = myScore + "";

            if (myScore > 21)
            {
                MessageBox.Show(
                   "21より大きいです",
                   "敗北",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning
                   );

                reset();
            }



        }

        private void standButton_Click(object sender, EventArgs e)
        {
            enemyScore += numbers[enemyCardLabel1.Text];
            enemyScore += numbers[enemyCardLabel2.Text];

            enemyCardLabel1.Visible = true;
            enemyScoreLabel.Text=enemyScore+"";

            while (enemyScore<=16)
            {
                //カードドロー
                enemyCardLabel3.Text = removeCard();
                enemyCardLabel3.Visible = true;

                //残り枚数表示
                remainingNumberLabel.Text = list.Count.ToString();

                enemyScore += numbers[enemyCardLabel3.Text];

                pictureBox3.BorderStyle = BorderStyle.FixedSingle;

                //スコア表示
                enemyScoreLabel.Text = enemyScore + "";
            }

            if (enemyScore>21)
            {
                MessageBox.Show(
                   "ディーラーは\n21より大きいです",
                   "勝利",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Asterisk
                   );
                reset();
                return;
            }

            if (enemyScore == myScore)
            {
                MessageBox.Show(
                   "引き分けです",
                   "引分け",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Stop
                   );
                reset();
                return;
            }

            if (myScore>enemyScore)
            {
                MessageBox.Show(
                   "プレイヤー勝利",
                   "勝利",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Asterisk
                   );
                reset();
                return;
            }

            MessageBox.Show(
                   "プレイヤー敗北",
                   "敗北",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning
                   );
            reset();

        }

        private void startButton_Click(object sender, EventArgs e)
        {

            int chip = 0;
            if (!int.TryParse(betTextBox.Text, out chip))
            {
                MessageBox.Show(
                    "正しい値を入力してください",
                    "入力値エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                betTextBox.Text = "";
                return;
            }

            //スコアリセット
            myScore = 0;
            enemyScore = 0;

            //カード3再非表示
            myCardLabel3.Visible=false;
            pictureBox6.BorderStyle = BorderStyle.None;

            //カード表示
            enemyCardLabel2.Visible = true;
            myCardlabel1.Visible = true;
            myCardlabel2.Visible=true;

            //初期スコア表示
            myScore += numbers[myCardlabel1.Text];
            myScore += numbers[myCardlabel2.Text];

            //スコア表示
            myScoreLabel.Text = myScore+"";

            if (myScore > 21)
            {
                MessageBox.Show(
                   "21より大きいです",
                   "プレイヤー敗北",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning
                   );
                reset();
            }
        }
        //Listからカードを減らして中身を返却
        private string removeCard()
        {
            Card card = list[0];
            list.RemoveAt(0);
            return card.getNumber();
        }

        private void reset()
        {
            new BattleScreen().Show();
            Close();
        }
    }

    class Card
    {
        private readonly string _type;
        private readonly string _number;

        public Card(string type,string number)
        {
            this._type = type;
            this._number = number;
        }

        public string getType()
        {
            return this._type;
        }

        public string getNumber()
        {
            return this._number;
        }
    }
}
