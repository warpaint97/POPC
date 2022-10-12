using System.Collections.Generic;

namespace POPC
{
    public class DataSet
    {
        public List<DataRow> rows;

        public DataSet(int numExpectedClusters, int numFeatures, int samples, int example)
        {
            rows = new List<DataRow>();

            switch (example)
            {
                case 1:
                    {
                        int[] featureBelongsToCluster = new int[numFeatures];
                        for (int i = 0; i < numFeatures; i++)
                        {
                            featureBelongsToCluster[i] = Program.r.Next(numExpectedClusters);
                        }
                        for (int j = 0; j < samples; j++)
                        {
                            int clusterNum = Program.r.Next(numExpectedClusters);
                            DataRow dr = new DataRow();
                            dr.features = new bool[numFeatures];
                            for (int i = 0; i < numFeatures; i++)
                            {
                                if (clusterNum == featureBelongsToCluster[i])
                                {
                                    if (Program.r.NextDouble() < 0.8)
                                    {
                                        dr.features[i] = true;
                                    }
                                }
                            }
                            rows.Add(dr);
                        }
                        break;
                    }
                case 2:
                    {
                        int[] featureBelongsToCluster = new int[numFeatures];
                        for (int i = 0; i < numFeatures; i++)
                        {
                            if (i < 0.9 * numFeatures)
                            {
                                featureBelongsToCluster[i] = Program.r.Next(numExpectedClusters);
                            }
                            else
                            {
                                // does not belong to anyone
                                featureBelongsToCluster[i] = -1;
                            }
                        }
                        for (int j = 0; j < samples; j++)
                        {
                            int clusterNum = Program.r.Next(numExpectedClusters);
                            DataRow dr = new DataRow();
                            dr.features = new bool[numFeatures];
                            for (int i = 0; i < numFeatures; i++)
                            {
                                if (clusterNum == featureBelongsToCluster[i])
                                {
                                    if (Program.r.NextDouble() < 0.8)
                                    {
                                        dr.features[i] = true;
                                    }
                                }
                                if (featureBelongsToCluster[i] == -1)
                                {
                                    if (Program.r.NextDouble() < 0.2)
                                    {
                                        dr.features[i] = true;
                                    }
                                }
                            }
                            rows.Add(dr);
                        }
                        break;
                    }
                case 3:
                    {
                        for (int i = 0; i < numExpectedClusters; i++)
                        {
                            double[] probs = new double[numFeatures];
                            for (int k = 0; k < numFeatures; k++)
                            {
                                if (k < numExpectedClusters)
                                {
                                    if (k == i)
                                    {
                                        probs[k] = 1.0;
                                    }
                                    else
                                    {
                                        probs[k] = 0.0;
                                    }
                                }
                                else
                                {
                                    probs[k] = 0.5;
                                }
                            }

                            for (int j = 0; j < samples; j++)
                            {
                                bool[] feats = new bool[numFeatures];

                                for (int k = 0; k < numFeatures; k++)
                                {
                                    feats[k] = Program.r.NextDouble() < probs[k];
                                }
                                DataRow dr = new DataRow();
                                dr.features = feats;
                                rows.Add(dr);
                            }
                        }
                        break;
                    }
                default:
                    break;
            }
        }
    }
}