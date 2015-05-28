using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusApp.Evaluation.DataModel
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

        public DataSource(String aQuestion)
            : this(aQuestion, "", "", "", "", "", "")
        {
            this._bigAnswer = "";
            this._isBigAnswer = true;

        }

        public DataSource (String aQuestion, String aAnswer1, String aAnswer2, String aAnswer3, String aAnswer4, String aAnswer5, String aAnswer6)
        {
            this._question = aQuestion;
            this._answer1 = aAnswer1;
            this._answer2 = aAnswer2;
            this._answer3 = aAnswer3;
            this._answer4 = aAnswer4;
            this._answer5 = aAnswer5;
            this._answer6 = aAnswer6;
            this._isBigAnswer = false;

        }


        public string Question
        {
            get { return this._question; }
            set { this.SetProperty(ref this._question, value); }
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

        public bool IsBigAnswer
        {
            get {  return this._isBigAnswer; }
        }
    }
}
