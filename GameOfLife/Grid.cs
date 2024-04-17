using System.Text;

namespace GameOfLife;

public class Grid
{
    
    private Pixel[][] pixels { get; set; } //Grid pixels

    public Grid(char[][] matrix) //Grid constructor, it receives a char matrix and transforms it into a pixel matrix
    {
        pixels = new Pixel[matrix.Length][];

        for (int i = 0; i < matrix.Length; i++)
        {
            pixels[i] = new Pixel[matrix[i].Length];
            for (int j = 0; j < matrix[i].Length; j++)
            {
                pixels[i][j] = new Pixel(matrix[i][j]);
            }
        }

        this.pixels = pixels;
    }

    public Pixel? GetPixel(int row, int column) //Function to get a pixel in a specific location in the grid
    {
        if (pixels[row][column] != null)
            return pixels[row][column];
        else
            return null;
                
    }

    public void Build() // Function to attribute top, left, etc pixels to each pixel in the grid
    {
       for (int i = 0; i < pixels.Length; i++)
        {
            for(int j = 0; j < pixels[i].Length; j++)
            {   
                if (i > 0) //If not in top row, it has a top
                    pixels[i][j].top = pixels[i - 1][j]; //attributes the top pixel as the top for the pixel
                if (i > 0  && j < (pixels[i].Length - 1)) //if not in top row and not in right col, it has a topright, the rest of this function has the same logic
                    pixels[i][j].topright = pixels[i - 1][j + 1];
                if (j < pixels[i].Length - 1)
                    pixels[i][j].right = pixels[i][j + 1];
                if (i < pixels.Length - 1 && j < pixels[i].Length - 1)
                    pixels[i][j].bottomright = pixels[i + 1][j + 1];
                if (i < pixels.Length - 1)
                    pixels[i][j].bottom = pixels[i + 1][j];
                if (i < pixels.Length - 1 && j > 0)
                    pixels[i][j].bottomleft = pixels[i + 1][j - 1];
                if (j > 0)
                    pixels[i][j].left = pixels[i][j - 1];
                if (i > 0 && j > 0)
                    pixels[i][j].topleft = pixels[i - 1][j - 1];
            }
        }
    }


    public void Refresh() //Applies the Conway's Game of Life logic here, refreshing the grid once
    {
        Pixel[][] aux = new Pixel[pixels.Length][];
 
        
        for (int i = 0; i < pixels.Length; i++) //This iteration copies the content of the original grid to an auxiliary one so that it doesnt mess up the logic
        {
            aux[i] = new Pixel[pixels[i].Length];
            for (int j = 0; j < pixels[i].Length; j++)
            {
                aux[i][j] = new Pixel(pixels[i][j].content); 
            }
        }


        for (var i = 0; i < pixels.Length; i++) //Logic
        {
            for (var j = 0; j < pixels[i].Length; j++)
            {
                if (pixels[i][j].content == 'O' && (pixels[i][j].Adjacents() <= 1 || pixels[i][j].Adjacents() >= 4)) //A live pixel that has <=1 or >=4 neighbors dies
                {
                    aux[i][j].content = ' ';
                }
                else if (pixels[i][j].content == ' ' && pixels[i][j].Adjacents() == 3) //An empty pixel that has exactly 3 neighbors becomes activated
                {
                    aux[i][j].content = 'O';
                }
                //Else, a live pixel that has 2 or 3 neighbors survives
            }
        }

        for (int i = 0; i < pixels.Length; i++) //Rebuilds the pixels matrix so that it can be used in the next iteration
        {
            for (int j = 0; j < pixels[i].Length; j++)
            {
                pixels[i][j].content = aux[i][j].content;
            }
        }
    }

    public override string ToString() //Prints the grid
    {
        StringBuilder grid = new StringBuilder();
        foreach (var row in pixels)
        {
            foreach (var elem in row)
            {
                grid.Append(elem.content);
            }
            grid.Append("\n");
        }
        return grid.ToString();
    }
}
