using System;
using System.Collections.Generic;

namespace POPC.clustering
{
    public class cluster
    {
        public List<DataRow> rows;

        int[] counts;

        public cluster(int numFeatures)
        {
            rows = new List<DataRow>();
            if (numFeatures != 0)
            {
                counts = new int[numFeatures];
            }
        }

        public void AddRow(DataRow r)
        {
            rows.Add(r);
            for (int i = 0; i < counts.Length; i++)
            {
                if (r.features != null)
                {
                    if (r.features[i])
                    {
                        counts[i]++;
                    }
                }
            }
        }

        public void RemoveRow(int which)
        {
            DataRow r = rows[which];
            rows.RemoveAt(which);
            for (int i = 0; i < counts.Length; i++)
            {
                if (r.features[i])
                {
                    counts[i]--;
                }
            }
        }

        // for popc
        public double getValue(int[] countsAll, int numClusters)
        {
            double ret = 0;

            for(int i=0;i<counts.Length;i++)
            {
                ret += Math.Pow((double)(counts[i] * 1000.0 + 1.0) / (countsAll[i] * 1000.0 + numClusters + 0.0), 10.0);
            }
            return ret;
        }

        // for popc
        public double deltaIfRemoved(int which, int[] countsAll, int numClusters)
        {
            double ret = 0.0;
            for (int i = 0; i < counts.Length; i++)
            {
                if (rows[which].features[i])
                {
                    ret -= Math.Pow((double)(counts[i] * 1000.0 + 1.0) / (countsAll[i] * 1000.0 + numClusters + 0.0), 10.0);
                    ret += Math.Pow((double)((counts[i] - 1.0) * 1000.0 + 1.0) / (countsAll[i] * 1000.0 + numClusters + 0.0), 10.0);
                }
            }

            return ret;
        }

        // for popc
        public double deltaIfAdded(DataRow r, int[] countsAll, int numClusters)
        {
            double ret = 0.0;
            for (int i = 0; i < counts.Length; i++)
            {
                if (r.features[i])
                {
                    ret += Math.Pow((double)((counts[i] + 1.0) * 1000.0 + 1.0) / (countsAll[i] * 1000.0 + numClusters + 0.0), 10.0);
                    ret -= Math.Pow((double)(counts[i] * 1000.0 + 1.0) / (countsAll[i] * 1000.0 + numClusters + 0.0), 10.0);
                }
            }

            return ret;
        }

        // for k-means
        public double getDistance(DataRow r)
        {
            double ret = 0;
            if (rows.Count == 0)
            {
                return 0;
            }
            for (int i = 0; i < counts.Length; i++)
            {
                double tmp = (counts[i] + 0.0) / (0.0 + rows.Count) - (r.features[i] ? 1.0 : 0.0);
                ret += tmp * tmp;
            }
            return ret;
        }

    }
}
