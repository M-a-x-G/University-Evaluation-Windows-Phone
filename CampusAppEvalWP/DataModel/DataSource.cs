using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace CampusAppEvalWP.DataModel
{
    class DataSource : BindableBase
    {
        private String _question;
        private String _answer1;
        private String _answer2;
        private String _answer3;
        private String _answer4;
        private String _answer5;
        private String _answer6;
        private String _bigAnswer;

        private Boolean _answer1State;
        private Boolean _answer2State;
        private Boolean _answer3State;
        private Boolean _answer4State;
        private Boolean _answer5State;
        private Boolean _answer6State;

        private Boolean _isBigAnswer;

        private Brush _answer1Color;
        private Brush _answer2Color;
        private Brush _answer3Color;
        private Brush _answer4Color;
        private Brush _answer5Color;
        private Brush _answer6Color;     

        private Color activColor;
        private Color normalColor;

        public DataSource(String aQuestion)
            : this(aQuestion, "", "", "", "", "", "")
        {
            this._bigAnswer = "";
            this._isBigAnswer = true;

        }

        public DataSource(String aQuestion, String aAnswer1, String aAnswer2, String aAnswer3, String aAnswer4, String aAnswer5, String aAnswer6)
        {          
            this._question = aQuestion;
            this._answer1 = aAnswer1;
            this._answer2 = aAnswer2;
            this._answer3 = aAnswer3;
            this._answer4 = aAnswer4;
            this._answer5 = aAnswer5;
            this._answer6 = aAnswer6;
            this._isBigAnswer = false;

            activColor = Color.FromArgb(255, 127, 0, 25);
            normalColor = Color.FromArgb(255, 204, 10, 47);

            //activColor = Color.FromArgb(255, 185, 35, 32);
            //normalColor = Color.FromArgb(255, 215, 85, 72);

            Answer1Color = new SolidColorBrush(normalColor);
            Answer2Color = new SolidColorBrush(normalColor);
            Answer3Color = new SolidColorBrush(normalColor);
            Answer4Color = new SolidColorBrush(normalColor);
            Answer5Color = new SolidColorBrush(normalColor);
            Answer6Color = new SolidColorBrush(normalColor);
           

        }
       
        public string Question
        {
            get { return this._question; }
            set 
            { 
                this.SetProperty(ref this._question, value); 
            }
        }

        public string Answer1
        {
            get { return this._answer1; }
            set { this.SetProperty(ref this._answer1, value); }
        }

        public string Answer2
        {
            get { return this._answer2; }
            set { this.SetProperty(ref this._answer2, value); }
        }

        public string Answer3
        {
            get { return this._answer3; }
            set { this.SetProperty(ref this._answer3, value); }
        }

        public string Answer4
        {
            get { return this._answer4; }
            set { this.SetProperty(ref this._answer4, value); }
        }

        public string Answer5
        {
            get { return this._answer5; }
            set { this.SetProperty(ref this._answer5, value); }
        }

        public string Answer6
        {
            get { return this._answer6; }
            set { this.SetProperty(ref this._answer6, value); }
        }

        public string BigAnswer
        {
            get { return this._bigAnswer; }
            set { this.SetProperty(ref this._bigAnswer, value); }
        }

        public bool Answer1State
        {
            get { return this._answer1State; }
            set 
            { 
                this.SetProperty(ref this._answer1State, value); 
                if (this._answer1State)
                {
                    this.Answer2State = false;
                    this.Answer3State = false;
                    this.Answer4State = false;
                    this.Answer5State = false;
                    this.Answer6State = false;
                }
            }
        }

        public bool Answer2State
        {
            get { return this._answer2State; }
            set 
            { 
                this.SetProperty(ref this._answer2State, value);
                if (this._answer2State)
                {
                    this.Answer1State = false;
                    this.Answer3State = false;
                    this.Answer4State = false;
                    this.Answer5State = false;
                    this.Answer6State = false;
                }
            }
        }

        public bool Answer3State
        {
            get { return this._answer3State; }
            set 
            { 
                this.SetProperty(ref this._answer3State, value);
                if (this._answer3State)
                {
                    this.Answer1State = false;
                    this.Answer2State = false;
                    this.Answer4State = false;
                    this.Answer5State = false;
                    this.Answer6State = false;
                }
            }
        }

        public bool Answer4State
        {
            get { return this._answer4State; }
            set 
            { 
                this.SetProperty(ref this._answer4State, value);
                if (this._answer4State)
                {
                    this.Answer1State = false;
                    this.Answer2State = false;
                    this.Answer3State = false;
                    this.Answer5State = false;
                    this.Answer6State = false;
                }
            }
        }

        public bool Answer5State
        {
            get { return this._answer5State; }
            set 
            { 
                this.SetProperty(ref this._answer5State, value);
                if (this._answer5State)
                {
                    this.Answer1State = false;
                    this.Answer2State = false;
                    this.Answer3State = false;
                    this.Answer4State = false;
                    this.Answer6State = false;
                }
            }
        }

        public bool Answer6State
        {
            get { return this._answer6State; }
            set 
            {
                this.SetProperty(ref this._answer6State, value);
                if (this._answer6State)
                {
                    this.Answer1State = false;
                    this.Answer2State = false;
                    this.Answer3State = false;
                    this.Answer4State = false;
                    this.Answer5State = false;
                }
            }
        }

        public Brush Answer1Color
        {
            get { return this._answer1Color; }
            set
            {
                this.SetProperty(ref this._answer1Color, value);
                
            }
        }

        public Brush Answer2Color
        {
            get { return this._answer2Color; }
            set
            {
                this.SetProperty(ref this._answer2Color, value);

            }
        }

        public Brush Answer3Color
        {
            get { return this._answer3Color; }
            set
            {
                this.SetProperty(ref this._answer3Color, value);

            }
        }

        public Brush Answer4Color
        {
            get { return this._answer4Color; }
            set
            {
                this.SetProperty(ref this._answer4Color, value);

            }
        }

        public Brush Answer5Color
        {
            get { return this._answer5Color; }
            set
            {
                this.SetProperty(ref this._answer5Color, value);

            }
        }

        public Brush Answer6Color
        {
            get { return this._answer6Color; }
            set
            {
                this.SetProperty(ref this._answer6Color, value);

            }
        }

        public bool IsBigAnswer
        {
            get {  return this._isBigAnswer; }
        }
      

        public void setButtonAnswer(int id)
        {
            switch (id)
            {
                case 1:
                    Answer1State = true;
                    Answer1Color = new SolidColorBrush(activColor);
                    Answer2Color = new SolidColorBrush(normalColor);
                    Answer3Color = new SolidColorBrush(normalColor);
                    Answer4Color = new SolidColorBrush(normalColor);
                    Answer5Color = new SolidColorBrush(normalColor);
                    Answer6Color = new SolidColorBrush(normalColor);
                    break;
                case 2:
                    Answer2State = true;
                    Answer1Color = new SolidColorBrush(normalColor);
                    Answer2Color = new SolidColorBrush(activColor);
                    Answer3Color = new SolidColorBrush(normalColor);
                    Answer4Color = new SolidColorBrush(normalColor);
                    Answer5Color = new SolidColorBrush(normalColor);
                    Answer6Color = new SolidColorBrush(normalColor);
                    break;
                case 3:
                    Answer3State = true;
                    Answer1Color = new SolidColorBrush(normalColor);
                    Answer2Color = new SolidColorBrush(normalColor);
                    Answer3Color = new SolidColorBrush(activColor);
                    Answer4Color = new SolidColorBrush(normalColor);
                    Answer5Color = new SolidColorBrush(normalColor);
                    Answer6Color = new SolidColorBrush(normalColor);
                    break;
                case 4:
                    Answer4State = true;
                    Answer1Color = new SolidColorBrush(normalColor);
                    Answer2Color = new SolidColorBrush(normalColor);
                    Answer3Color = new SolidColorBrush(normalColor);
                    Answer4Color = new SolidColorBrush(activColor);
                    Answer5Color = new SolidColorBrush(normalColor);
                    Answer6Color = new SolidColorBrush(normalColor);
                    break;
                case 5:
                    Answer5State = true;
                    Answer1Color = new SolidColorBrush(normalColor);
                    Answer2Color = new SolidColorBrush(normalColor);
                    Answer3Color = new SolidColorBrush(normalColor);
                    Answer4Color = new SolidColorBrush(normalColor);
                    Answer5Color = new SolidColorBrush(activColor);
                    Answer6Color = new SolidColorBrush(normalColor);
                    break;
                case 6:
                    Answer6State = true;
                    Answer1Color = new SolidColorBrush(normalColor);
                    Answer2Color = new SolidColorBrush(normalColor);
                    Answer3Color = new SolidColorBrush(normalColor);
                    Answer4Color = new SolidColorBrush(normalColor);
                    Answer5Color = new SolidColorBrush(normalColor);
                    Answer6Color = new SolidColorBrush(activColor);
                    break;

            }
        }

        public bool hasAnswer()
        {
            bool result = false;

            if (this._isBigAnswer)
            {
                if (!this.BigAnswer.Equals(""))
                    result = true;
            }
            else
            {
                if (this.Answer1State || this.Answer2State || this.Answer3State || this.Answer4State || this.Answer5State || this.Answer6State)
                    result = true;
            }
            return result;
        }
    }
}
