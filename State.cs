using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class State
{

    private LinkedList<Word> words = new LinkedList<Word>();
    public LinkedList<Word> wordsLeft = new LinkedList<Word>();
    public LinkedList<Word> wordsAdded = new LinkedList<Word>();
    public LinkedList<Coordinate> stateCoordinates = new LinkedList<Coordinate>();


    public State(LinkedList<Word> addedWords)
    {

        foreach (Word wrd in addedWords)
        {
            wordsLeft.AddLast(wrd);
            words.AddLast(wrd);
        }
    }


    public void addOldWord(Word word)
    {

        // for (int i = 0; i < word.coordinates.Count; i++)
        // {
        //     Console.WriteLine("Coordinates from addOldWord: X: {0}, Y: {1}, Char: {2}.", word.coordinates.ElementAt(i).GetX(), word.coordinates.ElementAt(i).GetY(), word.coordinates.ElementAt(i).GetChar());
        // }
        // Console.WriteLine("{0}", word.GetName());

        Word newWord = new Word(word.GetName(), word.GetHint());
        LinkedList<Coordinate> newWordCoord = new LinkedList<Coordinate>();

        foreach (Coordinate crd in word.coordinates)
        {
            //stateCoordinates.AddLast(crd);
            newWordCoord.AddLast(crd);

        }

        newWord.SetCoordinates(newWordCoord);

        wordsAdded.AddLast(newWord);

        for (int i = 0; i < wordsLeft.Count; i++)
        {
            if (wordsLeft.ElementAt(i).GetName().Equals(word.GetName()))
            //if((string.Compare(wordsLeft.ElementAt(i).GetName(), word.GetName())) == 0)
            {
                wordsLeft.Remove(wordsLeft.ElementAt(i));
            }
        }
    }



    public bool addWord(Word word, Coordinate crossCoor, int wordCrossIndex, string direction)
    {
        //if first word, return true
        if (wordsLeft.Count == words.Count)
        {

            Word addedWord = word;



            for (int i = 0; i < wordsLeft.Count; i++)
            {
                if (wordsLeft.ElementAt(i).GetName().Equals(addedWord.GetName()))
                {
                    wordsLeft.Remove(wordsLeft.ElementAt(i));

                }

            }




            wordsAdded.AddLast(addedWord);


            if (direction.Equals("rightleft"))
            {
                LinkedList<Coordinate> firstWordCoord = new LinkedList<Coordinate>();
                for (int i = 0; i < word.GetName().Length; i++)
                {
                    firstWordCoord.AddLast(new Coordinate(i, 0, word.GetName()[i]));
                }
                addedWord.SetCoordinates(firstWordCoord);

                foreach (Coordinate crd in firstWordCoord)
                {
                    stateCoordinates.AddLast(crd);
                }
            }


            if (direction.Equals("updown"))
            {
                LinkedList<Coordinate> firstWordCoord = new LinkedList<Coordinate>();
                for (int i = 0; i < word.GetName().Length; i++)
                {
                    firstWordCoord.AddLast(new Coordinate(0, i, word.GetName()[i]));
                }
                addedWord.SetCoordinates(firstWordCoord);

                foreach (Coordinate crd in firstWordCoord)
                {
                    stateCoordinates.AddLast(crd);
                }

            }

            return true;


        }



        //For adding words if not the first word added to the state
        else
        {
            Word addedWord = word;
            for (int p = 0; p < wordsLeft.Count; p++)
            {
                if (wordsLeft.ElementAt(p).GetName().Equals(addedWord.GetName()))
                {
                    wordsLeft.Remove(wordsLeft.ElementAt(p));

                }

            }
            wordsAdded.AddLast(addedWord);

            if (direction.Equals("rightleft"))
            {
                //create new coordinates
                LinkedList<Coordinate> coords = new LinkedList<Coordinate>();

                for (int i = 0; i < addedWord.GetName().Length; i++)
                {
                    coords.AddLast(new Coordinate(crossCoor.GetX() - (wordCrossIndex - i), crossCoor.GetY(), addedWord.GetName()[i]));
                    if (i == 0)
                    {
                        coords.ElementAt(0).endLH = true;
                    }

                    if (i == (addedWord.GetName().Length - 1))
                    {
                        coords.ElementAt(addedWord.GetName().Length - 1).endRH = true;
                    }

                }
                //Coordinates are added to word here
                addedWord.SetCoordinates(coords);

                //Checks previous coordinates against new word coordinates
                //If matching coordinates are found, the char for the coordinate
                //must match in order to add the word
                for (int i = 0; i < addedWord.GetName().Length; i++)
                {
                    for (int z = 0; z < stateCoordinates.Count; z++)
                    {
                        if ((stateCoordinates.ElementAt(z).GetX() == addedWord.coordinates.ElementAt(i).GetX()) && (stateCoordinates.ElementAt(z).GetY() == addedWord.coordinates.ElementAt(i).GetY()))
                        {
                            if (addedWord.coordinates.ElementAt(i).GetChar().Equals(stateCoordinates.ElementAt(z).GetChar()));
                            else return false;
                        }
                    }
                }

                //Adds new word coordinates to state coordinates
                foreach (Coordinate cr in coords)
                {
                    if (cr.GetChar().Equals(crossCoor.GetChar()) && cr.GetX().Equals(crossCoor.GetX()) && cr.GetY().Equals(crossCoor.GetY()));
                    else stateCoordinates.AddLast(cr);
                }
            }

            if (direction.Equals("updown"))
            {
                //create new coordinates
                LinkedList<Coordinate> coords = new LinkedList<Coordinate>();
                for (int i = 0; i < addedWord.GetName().Length; i++)
                {
                    coords.AddLast(new Coordinate(crossCoor.GetX(), crossCoor.GetY() - (wordCrossIndex - i), addedWord.GetName()[i]));

                    if (i == 0)
                    {
                        coords.ElementAt(0).endTV = true;
                    }

                    if (i == (addedWord.GetName().Length - 1))
                    {
                        coords.ElementAt(addedWord.GetName().Length - 1).endBV = true;
                    }

                }
                addedWord.SetCoordinates(coords);

                //Checks previous coordinates against new word coordinates
                //If matching coordinates are found, the char for the coordinate
                //must match in order to add the word
                for (int i = 0; i < addedWord.GetName().Length; i++)
                {
                    for (int z = 0; z < stateCoordinates.Count; z++)
                    {

                        //Matchting coordinate check
                        if ((stateCoordinates.ElementAt(z).GetX() == addedWord.coordinates.ElementAt(i).GetX()) && (stateCoordinates.ElementAt(z).GetY() == addedWord.coordinates.ElementAt(i).GetY()))
                        {
                            //Matching letter check
                            if (addedWord.coordinates.ElementAt(i).GetChar().Equals(stateCoordinates.ElementAt(z).GetChar()));
                            else return false;
                        }
                    }
                }

                //Adds new word coordinates to state coordinates
                foreach (Coordinate cr in coords)
                {
                    if (cr.GetChar().Equals(crossCoor.GetChar()) && cr.GetX().Equals(crossCoor.GetX()) && cr.GetY().Equals(crossCoor.GetY())) ;
                    else stateCoordinates.AddLast(cr);

                }


            }

            return true;

        }
    }


    public LinkedList<Coordinate> getMatchCharCoor(char letter)
    {
        LinkedList<Coordinate> coords = new LinkedList<Coordinate>();
        for (int i = 0; i < stateCoordinates.Count; i++)
        {


            if (letter.Equals(stateCoordinates.ElementAt(i).GetChar()))
            {

                //if(stateCoordinates.ElementAt(i).GetX + 1 && stateCoordinates.ElementAt(i).GetY + 1 )
                coords.AddLast(stateCoordinates.ElementAt(i));
            }


        }

        return coords;

    }

    public bool solution()
    {
        if (wordsLeft.Count == 0)
        {
            return true;
        }
        else return false;
    }


}