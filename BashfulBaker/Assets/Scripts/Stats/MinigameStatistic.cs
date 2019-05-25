using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Stats
{
    public class MinigameStatistic
    {
        public int totalHours;
        public int totalMinutes;
        public int totalSeconds;

        public int timesPlayed;

        public string averageTime
        {
            get
            {
                if (timesPlayed == 0) return "0:00:00";
                string avg = "";
                int fakeSeconds = (totalHours * 60 * 60) + (totalMinutes * 60) + totalSeconds;
                int avgTime = fakeSeconds / timesPlayed;

                int fakeHours=(avgTime / (3600));
                int fakeMins = (avgTime / 60);
                int reallyFakeSeconds = avgTime % 60;
                return fakeHours + ":" + fakeMins + ":" + reallyFakeSeconds;
            }
        }

        public string averageTimeMinSec
        {
            get
            {
                if (timesPlayed == 0) return "0:00:00";
                string avg = "";
                int fakeSeconds = (totalHours * 60 * 60) + (totalMinutes * 60) + totalSeconds;
                int avgTime = fakeSeconds / timesPlayed;

                int fakeHours = (avgTime / (3600));
                int fakeMins = (avgTime / 60);
                int reallyFakeSeconds = avgTime % 60;
                
                return fakeMins + ":" + ((reallyFakeSeconds<10) ? "0"+reallyFakeSeconds.ToString() : reallyFakeSeconds.ToString());
            }
        }

        public MinigameStatistic()
        {

        }

        public void addPlayTime(int hours, int mins, int seconds)
        {

            this.totalSeconds += seconds;
            if (seconds >= 60)
            {
                this.totalMinutes++;
                this.totalSeconds -= 60;
            }
            this.totalMinutes += mins;
            if (mins >= 60)
            {
                this.totalHours++;
                this.totalMinutes -= 60;
            }
            this.totalHours += hours;
            this.timesPlayed++;

        }
    }
}
