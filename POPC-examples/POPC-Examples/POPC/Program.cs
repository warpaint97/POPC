using System;
using POPC.clustering;
using System.Collections.Generic;

namespace POPC
{
    class Program
    {
        static public Random r = new Random();

        static void Main(string[] args)
        {
            RunExample1();
            RunExample2();
            RunExample3();
        }

        private static void RunExample1()
        {
            int numExpctedClusters = 7;
            int numFeatures = 100;
            int samples = 200;
            DataSet dataSet = new DataSet(numExpctedClusters, numFeatures, samples, 1);
            popc pop = new popc(dataSet);

            if(pop.clusters.Count != numExpctedClusters)
            {
                throw new Exception("theory not confirmed");
            }

            DisplayClusters(pop.clusters);
        }

        private static void RunExample2()
        {
            int numExpctedClusters = 7;
            int numFeatures = 100;
            int samples = 200;
            DataSet dataSet = new DataSet(numExpctedClusters, numFeatures, samples, 2);
            popc pop = new popc(dataSet);

            if (pop.clusters.Count != numExpctedClusters)
            {
                throw new Exception("theory not confirmed");
            }

            DisplayClusters(pop.clusters);
        }

        private static void RunExample3()
        {
            // scenario 3
            int numExpctedClusters = 7;
            int numFeatures = 20;
            int samplesPerCluser = 30;
            DataSet dataSet = new DataSet(numExpctedClusters, numFeatures, samplesPerCluser, 3);
            popc pop = new popc(dataSet);

            DisplayClusters(pop.clusters);

            //creates 7 clusters as expected
            if (pop.clusters.Count != numExpctedClusters)
            {
                throw new Exception("theory not confirmed");
            }

            // results close expected 7
            double vPOPC = popc.ComputeEval(pop.clusters, pop.countsAll);

            if (Math.Abs(vPOPC - numExpctedClusters) > 0.1)
            {
                throw new Exception("theory not confirmed");
            }

            // pretend we know correct number of clusters
            kmeans km = new kmeans(dataSet, pop.clusters.Count);

            DisplayClusters(km.clusters);
            // even with knowledge of amount of correct clusters
            // we get score close to 0 due to k-means clusters
            // concentrates on noisy features and does not find
            // ideal clusters, hence score close to 0
            double vKMEANS = popc.ComputeEval(km.clusters, pop.countsAll);

            if (Math.Abs(vKMEANS - 0.0) > 0.1)
            {
                throw new Exception("theory not confirmed");
            }
        }

        static public void DisplayClusters(List<cluster> clusters)
        {
            clusterGraph cg = new clusterGraph();
            int exampleNumber = 0;
            for (int i = 0; i < clusters.Count; i++)
            {
                string clusterName = "cluster " + i.ToString();
                cg.chart1.Series.Add(clusterName);
                cg.chart1.Series[clusterName].ChartType 
                    = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                for(int j=0;j<clusters[i].rows.Count;j++)
                {
                    for(int k=0;k<clusters[i].rows[j].features.Length;k++)
                    {
                        if (clusters[i].rows[j].features[k])
                        {
                            cg.chart1.Series[clusterName].Points.AddXY(k + 1, exampleNumber);
                        }
                    }
                    exampleNumber++;
                }
            }
            cg.chart1.ChartAreas[0].AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            cg.chart1.ChartAreas[0].AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;

            cg.ShowDialog();
        }

    }
}
