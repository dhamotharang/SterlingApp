using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class PostingDetailContracts : ObjectBase
    {
        int _ID;
        string _Refno;
        DateTime _date_pmt;
        string _Category;
        double _PrincipalOustandingBal;
        double _AmortizedCost;
        bool _Active;

        public int ID
        {
            get { return _ID; }
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    OnPropertyChanged(() => ID);
                }
            }
        }
   

        public string Refno
        {
            get { return _Refno; }
            set
            {
                if (_Refno != value)
                {
                    _Refno = value;
                    OnPropertyChanged(() => Refno);
                }
            }
        }


        public DateTime date_pmt
        {
            get { return _date_pmt; }
            set
            {
                if (_date_pmt != value)
                {
                    _date_pmt = value;
                    OnPropertyChanged(() => date_pmt);
                }
            }
        }


        public string Category
        {
            get { return _Category; }
            set
            {
                if (_Category != value)
                {
                    _Category = value;
                    OnPropertyChanged(() => Category);
                }
            }
        }

        public double PrincipalOustandingBal
        {
            get { return _PrincipalOustandingBal; }
            set
            {
                if (_PrincipalOustandingBal != value)
                {
                    _PrincipalOustandingBal = value;
                    OnPropertyChanged(() => PrincipalOustandingBal);
                }
            }
        }

        public double AmortizedCost
        {
            get { return _AmortizedCost; }
            set
            {
                if (_AmortizedCost != value)
                {
                    _AmortizedCost = value;
                    OnPropertyChanged(() => AmortizedCost);
                }
            }
        }

       
        public bool Active
        {
            get { return _Active; }
            set
            {
                if (_Active != value)
                {
                    _Active = value;
                    OnPropertyChanged(() => Active);
                }
            }
        }


        
        class PostingDetailContractsValidator : AbstractValidator<PostingDetailContracts>
        {
            public PostingDetailContractsValidator()
            {
               
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new PostingDetailContractsValidator();
        }
    }
}
