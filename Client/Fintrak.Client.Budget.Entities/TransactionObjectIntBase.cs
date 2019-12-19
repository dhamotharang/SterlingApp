using System;
using System.Linq;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.Budget.Entities
{
    public class TransactionObjectIntBase : ObjectBase
    {
        int _Month1;
        int _Month2;
        int _Month3;
        int _Month4;
        int _Month5;
        int _Month6;
        int _Month7;
        int _Month8;
        int _Month9;
        int _Month10;
        int _Month11;
        int _Month12;

        public int Month1
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

        public int Month2
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

        public int Month3
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

        public int Month4
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

        public int Month5
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

        public int Month6
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

        public int Month7
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

        public int Month8
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

        public int Month9
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

        public int Month10
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


        public int Month11
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

        public int Month12
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
