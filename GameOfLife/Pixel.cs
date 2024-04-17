namespace GameOfLife;

public class Pixel
{
    public char content { get; set; } //Indicates if the pixel is activated or not

    //Properties indicating adjacent pixels
    public Pixel? top { get; set; }
    public Pixel? bottom { get; set; }
    public Pixel? left { get; set; }
    public Pixel? right { get; set; }
    public Pixel? topleft { get; set; }
    public Pixel? bottomleft { get; set; }
    public Pixel? topright { get; set; }
    public Pixel? bottomright { get; set; }

    public Pixel(char content)
    {
        this.content = content;
    }

    public int Adjacents() //Function that returns how many adjacent pixels are activated
    {
        int i = 0;
        if (top != null && top.content == 'O') { i++; }
        if (topright != null && topright.content == 'O') { i++; }
        if (right != null && right.content == 'O') { i++; }
        if (bottomright != null && bottomright.content == 'O') { i++; }
        if (bottom != null && bottom.content == 'O') { i++; }
        if (bottomleft != null && bottomleft.content == 'O') { i++; }
        if (left != null && left.content == 'O') { i++; }
        if (topleft != null && topleft.content == 'O') { i++; }

        return i;

    }

    public char Print() //Prints a pixel
    { 
        return content;
    }

}
