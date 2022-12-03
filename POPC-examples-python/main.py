from sklearn.cluster import KMeans
import numpy as np
import random
from disp import display
from popc import popc
from dataset import createDataSet

if __name__ == '__main__':
        random.seed()
        for ex in range(3):
                if ex == 0:
                        X = createDataSet(7, 100, 200, 1)
                if ex == 1:
                        X = createDataSet(7, 100, 200, 2)
                if ex == 2:
                        X = createDataSet(7, 20, 30, 3)

                y_kmeans = KMeans(n_clusters=7, random_state=0).fit(X).labels_
                display(X, y_kmeans, 'kmeans example {}'.format(ex + 1))
                print('kmeans number of clusters [it was requested]: ', 7)

                y_popc = popc(X)
                display(X, y_popc, 'popc example {}'.format(ex + 1))
                print('popc number of clusters: ', len(np.unique(y_popc)))