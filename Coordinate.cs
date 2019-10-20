using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Coordinate{

    private int x;
    private int y;
    private char letter;
    public bool endTV = false;
    public bool endBV = false;
    public bool endLH = false;
    public bool endRH = false;



    public Coordinate(int x, int y, char letter){
        this.x = x;
        this.y = y;
        this.letter = letter;
    }

    public int GetX(){
        return x;
    }


    public int GetY(){
        return y;
    }

    public char GetChar(){
        return letter;
    }


}