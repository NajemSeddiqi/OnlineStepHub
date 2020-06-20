using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineStep.Helpers;
using OnlineStep.Models;
using OnlineStep.Services;
using OnlineStep.ViewModels;
using OnlineStep.Views;

namespace OnlineStep.Test

{
    [TestClass]
    public class GameificationTest
    {

        [TestMethod]
        public void AddXpTest()
        {

            /*
             * Test adding XP to Gamification 
             */

            //Arrange
            UserProgressDELETEME.Xp = 10;
            UserProgressDELETEME.Xp = 10;
            UserProgressDELETEME.Xp = 10;


            //Act
            int result = UserProgressDELETEME.Xp;

            //Assert
            Assert.AreEqual(30, result);

        }

        [TestMethod]
        public void AddPageResult()
        {

            /*
             * Test adding PageResult to Gamification 
             */

            //Arrange
            UserProgressDELETEME.maxScore = 5;
            UserProgressDELETEME.highestScore = 2;
            Trace.WriteLine("maxScore" + UserProgressDELETEME.maxScore);
            
            UserProgressDELETEME.AddPageResult(true);
            UserProgressDELETEME.AddPageResult(true);
            UserProgressDELETEME.AddPageResult(false);
            UserProgressDELETEME.AddPageResult(false);
            UserProgressDELETEME.AddPageResult(true);



            //Act
            UserProgressDELETEME.AddChapterResult();
            double results = UserProgressDELETEME.highestScoreProcentage;


            //Assert
            Assert.AreEqual(0.6, results);

        }

    }
}
