using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UIElements;


public class Maze
{
    
        private List<int[]> usedcells = new List<int[]>();
        private System.Random random = new System.Random();
        public int size;
        private string[] type = {"type0", "type1", "type2"}; 
        public Cell[,] mazee;
        private List<string> obstacleTypes = new List<string>();
        public Maze(int size)
        {
            this.size = size;
        }
        

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
            AddObjects(Category.trap);
            Keys();
            Final();
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
            int row, col, count = 0, i = 0;
            if(category == Category.obstacle) count = random.Next(size/2, size/2 + size/4);
            if(category == Category.trap) count = random.Next(size*3/5, size*5/3 + size/4);
            while(i < count)
            {
                row = random.Next(1, size-2);
                col = random.Next(1, size-2);
                if(mazee[row, col].category == Category.wall)
                {
                    if(ValidatePosition(row, col, category)) i++;
                }
            }
        }
        private void AddObstacleType(string type)
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

        private bool ValidatePosition(int row, int col, Category category )
        {
            if((mazee[row + 1, col].category == Category.wall && mazee[row - 1, col].category == Category.wall && 
            mazee[row, col + 1].category == Category.floor && mazee[row, col - 1].category == Category.floor))
            {
                mazee[row, col].category = category;
                mazee[row, col].modo = "horizontal";
                if(category == Category.obstacle)
                {
                    mazee[row, col].type = type[random.Next(0, 3)];
                    AddObstacleType(mazee[row,col].type);
                }
                
                return true;
            }
            if((mazee[row + 1, col].category == Category.floor && mazee[row - 1, col].category == Category.floor && 
            mazee[row, col + 1].category == Category.wall && mazee[row, col - 1].category == Category.wall))
            {
                mazee[row, col].category = category;
                mazee[row, col].modo = "vertical";
                if(category == Category.obstacle)
                {
                    mazee[row, col].type = type[random.Next(0, 3)];
                    AddObstacleType(mazee[row,col].type);
                } 
                return true;
            }
            return false;
        }

        private void Keys()
        {
            int row, col;
            for(int i = 0; i <= 2 ; i++)
            {
                for(int j=0; j < obstacleTypes.Count;)
                {
                    row = random.Next(1, size-2);
                    col = random.Next(1, size-2);
                    if(mazee[row, col].category == Category.floor)
                    {
                        mazee[row, col].category = Category.key;
                        mazee[row, col].type = obstacleTypes[j];
                        j++;
                    }
                }
            }
        }
        void Final()
        {
            int i = 0;
            System.Random random = new System.Random();
            int x, z;
            while(i < 1)
            {
                x = random.Next(0,size);
                z = random.Next(0,size);
                if(mazee[x, z].category == Category.wall || mazee[x, z].category == Category.floor)
                {
                    mazee[x, z].category = Category.final;
                    i++;
                }
            }  
        }


    }

