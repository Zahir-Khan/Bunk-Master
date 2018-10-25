﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Bunk_Master
{
    public class TelemetModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int dayCount;

        public int ID { get; set; }

        public int Daycount
        {
            get { return dayCount; }
            set
            {
                if (dayCount != value)
                {
                    dayCount = value;

                    OnPropertyChanged();
                }
            }
        }


        public double Tdyattnd { get; set; }
        public double Tdybnk { get; set; }

        public double Tomattnd { get; set; }
        public double Tombnk { get; set; }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            
        }

        public override string ToString()
        {
            return $"{Daycount},{Tdyattnd},{Tdybnk},{Tomattnd},{Tombnk}";
        }
    }
}
