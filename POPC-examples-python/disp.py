
import matplotlib as mpl
import matplotlib.pyplot as plt
import numpy as np

def display(samples, labels, title, xlabel='', ylabel='', size=1, cmap='Spectral', export_path=None, fig_w=12, fig_h=7):
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

        #plt.scatter(X, Y, c=C, cmap=cmap, s=size, label=Y)
        X = np.array(X)
        Y = np.array(Y)
        C = np.array(C)
        norm = mpl.colors.Normalize(vmin=min(C), vmax=max(C))
        n_clusters = len(np.unique(C))
        fig, ax = plt.subplots()
        fig.set_size_inches(fig_w, fig_h)
        for g in np.unique(C):
            ix = np.where(C == g)[0]
            if n_clusters > 1:
                colors = [g]*len(ix)
            else:
                colors = 'k'
            ax.scatter(X[ix], Y[ix], c=colors, cmap=cmap, label=g, s=size, norm=norm)
        if n_clusters > 1:
            ax.legend()

        plt.title(title)
        plt.xlabel(xlabel)
        plt.ylabel(ylabel)
        if export_path != None:
            plt.savefig(export_path, bbox_inches="tight")
        plt.show()

        
