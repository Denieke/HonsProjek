using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Accord.Statistics;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Statistics.Kernels;
using Accord.Math.Optimization.Losses;
using Accord.MachineLearning.Bayes;
using Accord.Statistics.Distributions.Univariate;

namespace App1
{
    class Classify
    {
        
        public string ClassifyFish(int BodyShape, int Length, int TailShape, int MouthShape, int BackSpine)
        {
            //https://github.com/accord-net/framework/wiki/Classification#multi-class
            //http://accord-framework.net/docs/html/N_Accord_Statistics.htm

            // The following is simple auto association function where each input correspond 
            // to its own class. This problem should be easily solved by a Linear kernel.

            // Sample input data   //Length | Body Shape | Mouth Shape | Tail | Back
            double[][] inputs =
            {
                new double[] {6,0,0,0,0},
                new double[] {5,0,0,0,0},
                new double[] {6,0,0,0,0},
                new double[] {1,1,0,1,0},
                new double[] {1,1,0,1,0},
                new double[] {2,1,1,1,0},
                new double[] {2,1,1,1,0},
                new double[] {2,1,2,1,0},
                new double[] {1,1,2,1,0},
                new double[] {1,1,2,1,0},
                new double[] {1,1,0,1,0},
                new double[] {1,1,0,1,0},
                new double[] {1,1,0,1,0},
                new double[] {1,1,0,1,0},
                new double[] {1,1,0,1,0},
                new double[] {1,1,0,1,0},
                new double[] {1,1,0,1,0},
                new double[] {2,1,0,1,0},
                new double[] {1,1,0,1,0},
                new double[] {1,1,0,1,0},
                new double[] {0,1,0,1,0},
                new double[] {2,1,0,1,1},
                new double[] {1,1,0,1,2},
                new double[] {1,1,0,1,2},
                new double[] {2,1,0,1,2},
                new double[] {1,1,0,1,2},
                new double[] {1,1,0,1,2},
                new double[] {3,1,0,1,2},
                new double[] {2,1,0,1,2},
                new double[] {4,1,0,1,2},
                new double[] {3,1,0,1,2},
                new double[] {4,1,0,1,2},
                new double[] {5,1,0,1,2},
                new double[] {4,1,2,1,1},
                new double[] {5,1,2,1,3},
                new double[] {4,1,2,1,0},
                new double[] {5,1,2,1,0},
                new double[] {4,1,2,1,0},
                new double[] {4,1,1,1,0},
                new double[] {4,1,3,1,0},
                new double[] {4,1,1,1,0},
                new double[] {4,1,1,1,0},
                new double[] {4,1,2,1,0},
                new double[] {3,1,2,1,0},
                new double[] {4,1,2,1,0},
                new double[] {5,1,0,1,0},
                new double[] {3,1,0,1,0},
                new double[] {1,2,0,1,0},
                new double[] {1,3,1,2,4},
                new double[] {1,3,1,1,5},
                new double[] {1,3,1,3,1},
                new double[] {1,3,1,1,6},
                new double[] {4,3,1,1,1},
                new double[] {1,1,4,0,0},
                new double[] {1,1,4,0,0},
                new double[] {1,1,5,4,0},
                new double[] {1,1,5,5,0},
                new double[] {7,4,6,6,7},
                new double[] {7,4,6,6,7},
            };
            // Outputs for each of the inputs
            int[] outputs =
            {
                    0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,3,3,3,3,3,4,4,5,5,6,6
            };
            double[][] test =
            {

                new double[] {6,0,0,0,0},
                new double[] {1,1,2,1,0 },
                new double[] { 2,1,2,1,0},
                new double[] { 2,1,0,1,0},
                new double[] { 0,1,0,1,0},
                new double[] {5,1,0,1,1 },
                new double[] {4,1,2,1,0 },
                new double[] {3,1,2,1,0},
                new double[] { 1,1,0,1,0},
                new double[] {1,1,0,1,0},
                new double[] {2,1,0,1,0 },
                new double[] {1,3,1,3,1},
                new double[] {1,3,1,1,1},
                new double[] {1,1,4,0,0},
                new double[] {1,1,5,5,0},
            };

            int[] correctTestOutputs =
            {
                    0,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    2,
                    3,
                    3,
                    4,
                    5,
            };



            // Create the Multi-label learning algorithm for the machine
            var teacher = new MulticlassSupportVectorLearning<Linear>()
            {
                Learner = (p) => new SequentialMinimalOptimization<Linear>()
                {
                    Complexity = 10000.0 // Create a hard SVM
                }
            };

            // Learn a multi-label SVM using the teacher
            var svm = teacher.Learn(inputs, outputs);
            // Compute the machine answers for the inputs
            int[] answers = svm.Decide(test);
            double error = new ZeroOneLoss(correctTestOutputs).Loss(answers);
            int nrIncorrect = 0;
            string errorOutput = "";
            for (int i = 0; i < answers.Length; i++)
            {
                if (answers[i] != correctTestOutputs[i])
                {
                    nrIncorrect++;
                    errorOutput += "incorrect answer= " + answers[i] + " correct answer=" + correctTestOutputs[i] + " at line: " + i;
                }
            }
           

            //Use the input from the user and classify it
            double[] userInput = { Length, BodyShape, MouthShape, TailShape, BackSpine };
            int userAnswer = svm.Decide(userInput);
            string answerFamily="";
            if (userAnswer == 0)
                answerFamily = "Anguillidae - Freshwater eel";
            else if (userAnswer == 1)
                answerFamily = "Cyprinidae - Carp/Barbs/Yellowfishes";
            else if (userAnswer == 2)
                answerFamily = "Characidae - Characins";
            else if (userAnswer == 3)
                answerFamily = "Mochokidae - Catfishes";
            else if (userAnswer == 4)
                answerFamily = "Aplocheilidae - Annual Killifishes";
            else if (userAnswer == 5)
                answerFamily = "Poeciliidae - Live Bearers";
            else
                answerFamily = "Unknown";
            // Create a Naive Bayes learning algorithm
            // var teacher2 = new NaiveBayesLearning<NormalDistribution>();


            return answerFamily;
        }

   

        public int determineScaleBodyShape(List<string> body)
        {
            int number = -1;
            bool elongated = false;
            bool fusiform = false;
            bool compressed = false;
            bool depressed = false;

            foreach (string bodyItem in body)
            {
                string item = bodyItem.ToString().ToUpper();
                if (item == "ELONGATED")
                {
                    elongated = true;
                }
                if (item == "FUSIFORM")
                {
                    fusiform = true;
                }
                if (item == "COMPRESSED")
                {
                    compressed = true;
                }
                if (item == "DEPRESSED")
                {
                    depressed = true;
                }
            }
            if (elongated && !fusiform && !compressed && !depressed)
                number = 0;
            else if (!elongated && fusiform && !compressed && !depressed)
                number = 1;
            else if (!elongated && !fusiform && compressed && !depressed)
                number = 2;
            else if (!elongated && !fusiform && !compressed && depressed)
                number = 3;
            else
                number = 4;
            return number;
        }
        //Length | Body Shape | Mouth Shape | Tail | Back
        //
        public int determineScaleLength(string length)
        {
            int number = -1;
            if (length == "0-39 mm")
                number = 0;
            else if (length == "40-100 mm")
                number = 1;
            else if (length == "101-150 mm")
                number = 2;
            else if (length == "151-300 mm")
                number = 3;
            else if (length == "301-600 mm")
                number = 4;
            else if (length == "601-1000 mm")
                number = 5;
            else if (length == ">1000 mm")
                number = 6;
            else number = 7;
            return number;
        }

        public int determineScaleBackSpine(List<string> backSpine)
        {
            int number = -1;
            bool none = false;
            bool dorsal = false;
            bool serrated = false;
            bool shortDorsal = false;
            bool longDorsal = false;

            foreach (string backItem in backSpine)
            {
                string item = backItem.ToString().ToUpper();
                if (item == "NONE")
                {
                    none = true;
                }
                if (item == "DORSAL SPINE")
                {
                    dorsal = true;
                }
                if (item == "SERRATED")
                {
                    serrated = true;
                }
                if (item == "SHORT DORSAL SPINE")
                {
                    shortDorsal = true;
                }
                if (item == "LONG DORSAL SPINE")
                {
                    longDorsal = true;
                }
            }
            if (none && !dorsal && !serrated && !shortDorsal && !longDorsal)
                number = 0;
            else if (!none && dorsal && !serrated && !shortDorsal && !longDorsal)
                number = 1;
            else if (!none && !dorsal && serrated && !shortDorsal && !longDorsal)
                number = 2;
            else if (none && dorsal && !serrated && !shortDorsal && !longDorsal)
                number = 3;
            else if (!none && dorsal && !serrated && shortDorsal && !longDorsal)
                number = 4;
            else if (!none && !dorsal && !serrated && !shortDorsal && longDorsal)
                number = 5;
            else if (!none && dorsal && serrated && !shortDorsal && !longDorsal)
                number = 6;
            else
                number = 7;
            return number;
        }

        public int determineScaleMouthShape(List<string> mouth)
        {
            int number = -1;
            bool Terminal = false;
            bool Inferior = false;
            bool SubTerminal = false;
            bool Dorsal = false;
            bool Upturned = false;

            foreach (string mouthItem in mouth)
            {
                string item = mouthItem.ToString().ToUpper();
                if (item == "TERMINAL")
                {
                    Terminal = true;
                }
                if (item == "INFERIOR")
                {
                    Inferior = true;
                }
                if (item == "SUB TERMINAL")
                {
                    SubTerminal = true;
                }
                if (item == "DORSAL")
                {
                    Dorsal = true;
                }
                if (item == "UPTURNED")
                {
                    Upturned = true;
                }
            }
            if (Terminal && !Inferior && !SubTerminal && !Dorsal && !Upturned)
                number = 0;
            else if (!Terminal && Inferior && !SubTerminal && !Dorsal && !Upturned)
                number = 1;
            else if (!Terminal && !Inferior && SubTerminal && !Dorsal && !Upturned)
                number = 2;
            else if (!Terminal && Inferior && SubTerminal && !Dorsal && !Upturned)
                number = 3;
            else if (!Terminal && !Inferior && !SubTerminal && Dorsal && Upturned)
                number = 4;
            else if (!Terminal && !Inferior && !SubTerminal && !Dorsal && Upturned)
                number = 5;
            else
                number = 6;
            return number;
        }

        public int determineScaleTailShape(List<string> tail)
        {
            int number = -1;
            bool Rounded = false;
            bool Forked = false;
            bool Pointed = false;
            bool Emarginate = false;
            bool SemiTruncate = false;

            foreach (string mouthItem in tail)
            {
                string item = mouthItem.ToString().ToUpper();
                if (item == "ROUNDED")
                {
                    Rounded = true;
                }
                if (item == "FORKED")
                {
                    Forked = true;
                }
                if (item == "POINTED")
                {
                    Pointed = true;
                }
                if (item == "EMARGINATE")
                {
                    Emarginate = true;
                }
                if (item == "SEMI TRUNCATE")
                {
                    SemiTruncate = true;
                }
            }
            if (Rounded && !Forked && !Pointed && !Emarginate && !SemiTruncate)
                number = 0;
            else if (!Rounded && Forked && !Pointed && !Emarginate && !SemiTruncate)
                number = 1;
            else if (!Rounded && !Forked && Pointed && !Emarginate && !SemiTruncate)
                number = 2;
            else if (!Rounded && !Forked && !Pointed && Emarginate && !SemiTruncate)
                number = 3;
            else if (!Rounded && !Forked && !Pointed && !Emarginate && SemiTruncate)
                number = 4;
            else if (Rounded && !Forked && !Pointed && !Emarginate && SemiTruncate)
                number = 5;
            else
                number = 6;
            return number;
        }

    }
}


