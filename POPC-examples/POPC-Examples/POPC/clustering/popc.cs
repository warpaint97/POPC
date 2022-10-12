using System.Collections.Generic;

namespace POPC.clustering
{
    public class popc
    {
        public List<cluster> clusters;
        public int[] countsAll;

        public popc(DataSet dataSet)
        {
            countsAll = new int[dataSet.rows[0].features.Length];
            foreach (var r in dataSet.rows)
            {
                for (int i = 0; i < r.features.Length; i++)
                {
                    if (r.features[i])
                    {
                        countsAll[i]++;
                    }
                }
            }
            kmeans km = new kmeans(dataSet, dataSet.rows.Count / 2);
            clusters = km.clusters;
            DoPOPCClustering();
        }

        static public double ComputeEval(List<cluster> clusters, int[] countsAll)
        {
            double ret = 0;

            for(int i=0;i<clusters.Count;i++)
            {
                ret += clusters[i].getValue(countsAll, clusters.Count);
            }

            return ret;
        }

        private void DoPOPCClustering()
        {
            bool changed = true;
            while (changed)
            {
                changed = false;
                for (int i = 0; i < clusters.Count; i++)
                {
                    for (int j = 0; j < clusters[i].rows.Count; j++)
                    {
                        int largestGainWhere = -1;
                        double largestGain = double.MinValue;
                        double deltaBase = clusters[i].deltaIfRemoved(j, countsAll, clusters.Count);
                        for (int k = 0; k < clusters.Count; k++)
                        {
                            if (i != k)
                            {
                                double delta = deltaBase + clusters[k].deltaIfAdded(
                                    clusters[i].rows[j], countsAll, clusters.Count);
                                if (delta > largestGain)
                                {
                                    largestGain = delta;
                                    largestGainWhere = k;
                                }
                            }
                        }
                        if (largestGain > 0)
                        {
                            DataRow r = clusters[i].rows[j];
                            clusters[i].RemoveRow(j);
                            clusters[largestGainWhere].AddRow(r);
                            j--;
                            changed = true;
                        }
                    }
                    if (clusters[i].rows.Count == 0)
                    {
                        clusters.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }
}
