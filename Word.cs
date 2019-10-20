using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Word{


    private string name;
    private string hint;
    public string direction;
    public LinkedList<Coordinate> coordinates;


    public Word(string name, string hint){

        this.name = name.ToLower();
        //removes white space
        name = name.Replace(" ", string.Empty);
        this.hint = hint;

    }

    public string GetName(){
        return name;
    }

    public string GetHint(){
        return hint;
    }

    public string GetDirection(){
        return direction;
    }


    public void SetCoordinates(LinkedList<Coordinate> coordinates){
        this.coordinates = coordinates;
    }

}