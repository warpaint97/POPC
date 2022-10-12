using System.Collections.Generic;

namespace POPC.clustering
{
    public class kmeans
    {
        public List<cluster> clusters;

        public kmeans(DataSet dataSet, int numClusters)
        {
            clusters = new List<cluster>();
            for (int i = 0; i < numClusters; i++)
            {
                clusters.Add(new cluster(dataSet.rows[0].features.Length));
            }
            for (int i = 0; i < dataSet.rows.Count; i++)
            {
                clusters[Program.r.Next(clusters.Count)].AddRow(dataSet.rows[i]);
            }
            DoClustering();
        }

        private void DoClustering()
        {
            bool changed = true;
            while(changed)
            {
                changed = false;
                for(int i=0;i<clusters.Count;i++)
                {
                    for(int j=0;j<clusters[i].rows.Count;j++)
                    {
                        int where = -1;
                        double minv = clusters[i].getDistance(clusters[i].rows[j]);
                        for(int k=0;k<clusters.Count;k++)
                        {
                            if(k!=i)
                            {
                                double tmp = clusters[k].getDistance(clusters[i].rows[j]);
                                if(tmp<minv)
                                {
                                    minv = tmp;
                                    where = k;
                                    changed = true;
                                }
                            }
                        }
                        if(where!=-1)
                        {
                            clusters[where].AddRow(clusters[i].rows[j]);
                            clusters[i].RemoveRow(j);
                        }
                    }
                }
            }
        }
    }
}
