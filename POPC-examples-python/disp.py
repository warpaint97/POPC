import matplotlib.pyplot as plt

def display(samples, labels, title, size=1, cmap='Spectral', export_path=None):
        fig = plt.gcf()
        fig.set_size_inches(12, 7)

        #colors = ['b','g','r','c','m','y','k']
        labelsWithSamples = sorted(zip(samples, labels), key=lambda s: s[1])
        samples, labels = zip(*labelsWithSamples)

        X = []
        Y = []
        C = []
        n_rows = len(samples)
        for y in range(n_rows):
                n_cols = len(samples[y])
                for x in range(n_cols):
                        if samples[y][x] == 1:
                                X.append(x)
                                Y.append(y)
                                C.append(labels[y])

        plt.scatter(X, Y, c=C, cmap=cmap, s=size)
        plt.title(title)
        if export_path != None:
            plt.savefig(export_path, bbox_inches="tight")
        plt.show()

        
