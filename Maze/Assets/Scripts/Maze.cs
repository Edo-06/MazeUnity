using UnityEngine;
using System;
using System.Collections.Generic;


public class Maze
{
    private List<int[]> usedcells = new List<int[]>();
        private System.Random random = new System.Random();
        public int size = 5;
        private int type; 
        public Cell[,] mazee;
        private List<int> obstacleTypes = new List<int>();
        

        public void Generator(int mazesize)
        {
            size = mazesize;
            InicializarMatriz();
            usedcells.Add(new int[] {random.Next(1, size-1), random.Next(1, size-1) });
            mazee[usedcells[0][0], usedcells[0][1]].category = Category.floor; 
            int[] cell;
            List<int[]> neighbords = new List<int[]>();
            int row, col, newrow, newcol, indice;

            while (usedcells.Count > 0)
            {
                
                cell = usedcells[usedcells.Count - 1];
                row = cell[0];
                col = cell[1];
                neighbords.Clear(); 

                
                if (row >= 2 && mazee[row - 2, col].category == Category.wall)
                    neighbords.Add(new int[] { row - 2, col });
                if (col >= 2 && mazee[row, col - 2].category == Category.wall)
                    neighbords.Add(new int[] { row, col - 2 });
                if (row <= size - 3 && mazee[row + 2, col].category == Category.wall)
                    neighbords.Add(new int[] { row + 2, col });
                if (col <= size - 3 && mazee[row, col + 2].category == Category.wall)
                    neighbords.Add(new int[] { row, col + 2 });

                if (neighbords.Count > 0)
                {
                    
                    indice = random.Next(0, neighbords.Count-1);
                    newrow = neighbords[indice][0];
                    newcol = neighbords[indice][1];
                    
                    mazee[newrow, newcol].category = Category.floor;
                    
                    mazee[(row + newrow) / 2, (col + newcol) / 2].category = Category.floor;

                    usedcells.Add(new int[] { newrow, newcol });
                }
                else
                {
                    usedcells.RemoveAt(usedcells.Count - 1);
                }
            }
            AddObjects(Category.obstacle);
            AddObjects(Category.tramp);
        }

        public void Print()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    //mazee[i, j].Print(i, j);
                }
            }
        }

    private void InicializarMatriz()
        {
            mazee = new Cell[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    mazee[i, j] = new Cell(Category.wall);
                }
            }
        }
        private void AddObjects(Category category)
        {
            int row, col, newrow, newcol, indice;
            
            
            List<int[]> neighbords = new List<int[]>();

            for(int i=0; i < Math.Pow(size,2)/100; i++)
            {
                row = random.Next(1, size-2);
                col = random.Next(1, size-2);
                if(mazee[row, col].category == Category.wall)
                {
                    ValidatePosition(row, col, category);
                }
                else
                {
                    if (row >= 2 && mazee[row - 1, col].category == Category.wall)
                        neighbords.Add(new int[] { row - 1, col });
                    if (col >= 2 && mazee[row, col - 1].category == Category.wall)
                        neighbords.Add(new int[] { row, col - 1 });
                    if (row <= size - 3 && mazee[row + 1, col].category == Category.wall)
                        neighbords.Add(new int[] { row + 1, col });
                    if (col <= size - 3 && mazee[row, col + 1].category == Category.wall)
                        neighbords.Add(new int[] { row, col + 1 });
                    

                    if (neighbords.Count > 0)
                    {
                        
                        indice = random.Next(0, neighbords.Count-1);
                        newrow = neighbords[indice][0];
                        newcol = neighbords[indice][1];
                        ValidatePosition(newrow, newcol, category);
                        
                    }
                }
            }
        }
        private void AddObstacleType(int type)
        {
            int i = 0 ;
            bool found = false;
            if(obstacleTypes.Count == 0)
            {
                obstacleTypes.Add(type);
            } else 
            {
                while( !found && i < obstacleTypes.Count)
                {
                    if(type != obstacleTypes[i])
                        i++;
                    else
                        found = true;
                }
                if(!found)
                    obstacleTypes.Add(type);
                
            
            }
        }

        private void ValidatePosition(int row, int col, Category category )
        {
            if((mazee[row + 1, col].category == Category.wall && mazee[row - 1, col].category == Category.wall && 
            mazee[row, col + 1].category == Category.floor && mazee[row, col - 1].category == Category.floor))
            {
                mazee[row, col].category = category;
                type =  random.Next(0, 3);
                mazee[row, col].type = type;
                AddObstacleType(type);
                mazee[row, col].modo = "horizontal";
            }
            if((mazee[row + 1, col].category == Category.floor && mazee[row - 1, col].category == Category.floor && 
            mazee[row, col + 1].category == Category.wall && mazee[row, col - 1].category == Category.wall))
            {
                mazee[row, col].category = category;
                type =  random.Next(0, 3);
                mazee[row, col].type = type;
                AddObstacleType(type);
                mazee[row, col].modo = "vertical";
            }
        }
    }

