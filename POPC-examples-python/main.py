from sklearn.cluster import KMeans
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

                kmeans = KMeans(n_clusters=7, random_state=0).fit(X)
                result = zip(kmeans.labels_, X)
                display(result, 'kmeans example {}'.format(ex + 1))
                print('kmeans number of clusters [it was requested]: ', 7)

                labels = popc(X)
                result = []
                for i in range(len(X)):
                        result.append([labels[i], X[i]])
                display(result, 'popc example {}'.format(ex + 1))
                print('popc number of clusters: ', len(dict.fromkeys(labels, True)))
