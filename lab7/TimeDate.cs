using System;

namespace lab7
{
    class TimeDate
    {
        private int year;
        private string month;
        private int day;
        private int time;

        public TimeDate()
        {
            this.year = 2021;
            this.month = "January";
            this.day = 1;
            this.time = 0;
        }

        public TimeDate(int year, string month, int day, int time)
        {
            this.year = year;
            this.month = month;
            this.day = day;
            this.time = time;
        }

        public int CountTimeToDepartInMinutes(int isDelayed)
        {
            return year * 525600 + GetNumberOfMonth(month) * 43200 + day * 1440 + time + isDelayed;
        }

        public int CountTimeToDepartInMinutes()
        {
            return year * 525600 + GetNumberOfMonth(month) * 43200 + day * 1440 + time;
        }

        public int GetNumberOfMonth(string month)
        {
            if (month == "January") return 1;
            if (month == "February") return 2;
            if (month == "March") return 3;
            if (month == "April") return 4;
            if (month == "May") return 5;
            if (month == "June") return 6;
            if (month == "July") return 7;
            if (month == "August") return 8;
            if (month == "September") return 9;
            if (month == "October") return 10;
            if (month == "November") return 11;
            if (month == "December") return 12;
            return 1;
        }

        public override string ToString()
        {
            int hours = this.time / 60;
            int minutes = this.time % 60;
            if (minutes < 10)
            {
                return string.Format($"{this.month} {this.day}, {this.year} - {hours}:0{minutes}");
            }
            return string.Format($"{this.month} {this.day}, {this.year} - {hours}:{minutes}");
        }
    }
}
