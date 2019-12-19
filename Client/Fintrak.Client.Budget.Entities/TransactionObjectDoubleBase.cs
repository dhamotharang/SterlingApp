using System;
using System.Linq;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class TransactionObjectDoubleBase : ObjectBase
    {
        double _Month1;
        double _Month2;
        double _Month3;
        double _Month4;
        double _Month5;
        double _Month6;
        double _Month7;
        double _Month8;
        double _Month9;
        double _Month10;
        double _Month11;
        double _Month12;

        public double Month1
        {
            get { return _Month1; }
            set
            {
                if (_Month1 != value)
                {
                    _Month1 = value;
                    OnPropertyChanged(() => Month1);
                }
            }
        }

        public double Month2
        {
            get { return _Month2; }
            set
            {
                if (_Month2 != value)
                {
                    _Month2 = value;
                    OnPropertyChanged(() => Month2);
                }
            }
        }

        public double Month3
        {
            get { return _Month3; }
            set
            {
                if (_Month3 != value)
                {
                    _Month3 = value;
                    OnPropertyChanged(() => Month3);
                }
            }
        }

        public double Month4
        {
            get { return _Month4; }
            set
            {
                if (_Month4 != value)
                {
                    _Month4 = value;
                    OnPropertyChanged(() => Month4);
                }
            }
        }

        public double Month5
        {
            get { return _Month5; }
            set
            {
                if (_Month5 != value)
                {
                    _Month5 = value;
                    OnPropertyChanged(() => Month5);
                }
            }
        }

        public double Month6
        {
            get { return _Month6; }
            set
            {
                if (_Month6 != value)
                {
                    _Month6 = value;
                    OnPropertyChanged(() => Month6);
                }
            }
        }

        public double Month7
        {
            get { return _Month7; }
            set
            {
                if (_Month7 != value)
                {
                    _Month7 = value;
                    OnPropertyChanged(() => Month7);
                }
            }
        }

        public double Month8
        {
            get { return _Month8; }
            set
            {
                if (_Month8 != value)
                {
                    _Month8 = value;
                    OnPropertyChanged(() => Month8);
                }
            }
        }

        public double Month9
        {
            get { return _Month9; }
            set
            {
                if (_Month9 != value)
                {
                    _Month9 = value;
                    OnPropertyChanged(() => Month9);
                }
            }
        }

        public double Month10
        {
            get { return _Month10; }
            set
            {
                if (_Month10 != value)
                {
                    _Month10 = value;
                    OnPropertyChanged(() => Month10);
                }
            }
        }


        public double Month11
        {
            get { return _Month11; }
            set
            {
                if (_Month11 != value)
                {
                    _Month11 = value;
                    OnPropertyChanged(() => Month11);
                }
            }
        }

        public double Month12
        {
            get { return _Month12; }
            set
            {
                if (_Month12 != value)
                {
                    _Month12 = value;
                    OnPropertyChanged(() => Month12);
                }
            }
        }
    }
}
