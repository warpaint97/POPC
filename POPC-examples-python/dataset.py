from sklearn.cluster import KMeans
import numpy as np
import random

def createDataSet(numClust, numFeat, numSamples, caseNum):
        ret = []
        feat_belongs = {}
        if caseNum == 1:
                for i in range(numFeat):
                        feat_belongs[i] = random.randrange(numClust)
                for i in range(numSamples):
                        clust_num = random.randrange(numClust)
                        feats = [0 for j in range(numFeat)]
                        for j in range(numFeat):
                                if feat_belongs[j] == clust_num:
                                        if random.random() < 0.8:
                                                feats[j] = 1
                        ret.append(feats)
        if caseNum == 2:
                for i in range(numFeat):
                        if i < 0.9 * numFeat:
                                feat_belongs[i] = random.randrange(numClust)
                        else:
                                feat_belongs[i] = -1
                for i in range(numSamples):
                        clust_num = random.randrange(numClust)
                        feats = [0 for j in range(numFeat)]
                        for j in range(numFeat):
                                if feat_belongs[j] == clust_num:
                                        if random.random() < 0.8:
                                                feats[j] = 1
                                if feat_belongs[j] == -1:
                                        if random.random() < 0.2:
                                                feats[j] = 1
                        ret.append(feats)
        if caseNum == 3:
                for i in range(numClust):
                        probs = []
                        for k in range(numFeat):
                                if k < numClust:
                                        if k == i:
                                                probs.append(1.0)
                                        else:
                                                probs.append(0.0)
                                else:
                                        probs.append(0.5)


                        for j in range(numSamples):
                                feats = [0 for j in range(numFeat)]
                                for k in range(numFeat):
                                        if random.random() < probs[k]:
                                                feats[k] = 1
                                ret.append(feats)

        return np.array(ret)