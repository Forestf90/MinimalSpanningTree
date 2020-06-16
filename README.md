# Minimal Spanning Tree

Simple application for search mimimal spanning tre in graph wtitten in C# using WinForms.

The program uses the Kruskal algorithm to find the minimum spanning tree that can be traced step by step.

## Graph generation 

To generate graph user have to insert number of nodes in graph and set propability of egde between nodes via slider.

![](images/img1.png)


## Graph drawing

![](images/img2.png)

Program offers three ways of drawing:


|Random  |Circle |Grid   |
|--------|-------|-------|
|![](images/img3.png) | ![](images/img4.png) |![](images/img5.png)|

## Edit

- Move node: left click

![](images/am1.gif)


- Create/Delete edge: right click on first node and the on second to create or delete edge between them

![](images/am2.gif)


- Change edge weight - double click on egde, then insert nwe value on textbox and confirm with "Edit" button

![](images/am3.gif)


- Create/Delete node - middle click on free space to create node or on existing node to remove node

  ![](images/am4.gif)



## Check conectivity 

To find minimal spanning tree the graph need to be conected. User can check conectivity by clicking on "Check conectivity" button and see the animitation of depth-first search algorithm.

![](images/am5.gif)

## Minimal spanning tree

Kruskal algorithm can by run by clicking "Search MST" button.

![](images/am6.gif)