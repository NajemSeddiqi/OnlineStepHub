using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.XPath;
using Akavache.Sqlite3.Internal;
using PropertyChanged;

namespace OnlineStep.Services
{
    public static class UserProgressDELETEME
    {
        public static int xp = 0;
        public static int userLevel = 1;

        // Logic for 1 chapter
        public static int maxScore;
        public static int highestScore = 0;
        public static double highestScoreProcentage;
        public static int currentChapterScore;

        //Private variables
        private static double _minUnlockScore = 0.5; 


        public static int Xp
        {
            get => xp;
            set
            {
                xp += value;
                Debug.WriteLine("New XP: "+xp);
            }
        }

        public static int UserLevel
        {
            get => userLevel;
            set
            {
                userLevel += value;
                Debug.WriteLine("New unlockedLevels: " + userLevel);
            }
        }

        public static void AddPageResult (bool rightAnswer)
        {
            if (maxScore == 0)
            {
                maxScore = PageNavigator.PageList.Count;
            }

            if (rightAnswer)
            {
                Debug.WriteLine("Right answer");
                currentChapterScore += 1;
                Xp += 10;
            }
            else
            {
                Debug.WriteLine("Wrong answer");
            }

            Debug.WriteLine("CurrentChapterScore: " + currentChapterScore + "\nXP:" + Xp);
            
        }

        public static void AddChapterResult()
        {
            Debug.WriteLine("currentChapterScore: " + currentChapterScore);
            Debug.WriteLine("highestScore: " + highestScore);
            if (currentChapterScore > highestScore)
            {
                
                double scoreInProcent = Math.Round(1.0 /maxScore * currentChapterScore,2);
                Debug.WriteLine("scoreInProcent: " + scoreInProcent);
                if (scoreInProcent > _minUnlockScore)
                {
                    Debug.WriteLine("Chapter Unlocked");
                    if (scoreInProcent > highestScoreProcentage)
                    {
                        Debug.WriteLine("Old highscore for chapter: " + highestScoreProcentage);
                        Debug.WriteLine("New highscore for chapter: " + scoreInProcent);
                        highestScoreProcentage = scoreInProcent;
                    }
                }
            }
        }

        public static void ChapterCompleted()
        {
            maxScore = 0;
            currentChapterScore = 0;
        }
    }
}
