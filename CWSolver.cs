using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class CWSolver
{


    private Stack<State> states = new Stack<State>();
    LinkedList<Word> words = new LinkedList<Word>();

    public CWSolver(LinkedList<Word> words)
    {
        this.words = words;
    }


    public void solve()
    {

        Console.WriteLine("Words Count: {0}", words.Count);

        //Creates an initial stack for BFS
        for (int i = 0; i < words.Count; i++)
        {
            LinkedList<Word> newWordsOne = new LinkedList<Word>();
            for (int w = 0; w < words.Count; w++)
            {
                newWordsOne.AddLast(new Word(words.ElementAt(w).GetName(), words.ElementAt(w).GetHint()));
            }
            State newStateOne = new State(newWordsOne);

            LinkedList<Word> newWordsTwo = new LinkedList<Word>();
            for (int w = 0; w < words.Count; w++)
            {
                newWordsTwo.AddLast(new Word(words.ElementAt(w).GetName(), words.ElementAt(w).GetHint()));
            }

            State newStateTwo = new State(newWordsTwo);



            Coordinate newCoord = new Coordinate(0, 0, words.ElementAt(i).GetName()[0]);




            newStateOne.addWord(words.ElementAt(i), newCoord, 0, "rightleft");

            Console.WriteLine("State Coordinates right-left.");
            for (int g = 0; g < newStateOne.stateCoordinates.Count; g++)
            {
                Console.WriteLine("(X: {0}, Y: {1}) & Letter: {2}", newStateOne.stateCoordinates.ElementAt(g).GetX(), newStateOne.stateCoordinates.ElementAt(g).GetY(), newStateOne.stateCoordinates.ElementAt(g).GetChar());
            }
            Console.WriteLine("Word Coordinates.");
            for (int g = 0; g < newStateOne.wordsAdded.ElementAt(0).GetName().Length; g++)
            {
                //Console.WriteLine("Number or word coordinates: {0}", newStateOne.wordsAdded.ElementAt(0).coordinates.Count);
                Console.WriteLine("(X: {0}, Y: {1}) & Letter: {2}", newStateOne.wordsAdded.ElementAt(0).coordinates.ElementAt(g).GetX(), newStateOne.wordsAdded.ElementAt(0).coordinates.ElementAt(g).GetY(), newStateOne.wordsAdded.ElementAt(0).coordinates.ElementAt(g).GetChar());
            }

            states.Push(newStateOne);








            newStateTwo.addWord(words.ElementAt(i), newCoord, 0, "updown");

            Console.WriteLine("State Coordinates up-down.");
            for (int g = 0; g < newStateTwo.stateCoordinates.Count; g++)
            {
                Console.WriteLine("(X: {0}, Y: {1}) & Letter: {2}", newStateTwo.stateCoordinates.ElementAt(g).GetX(), newStateTwo.stateCoordinates.ElementAt(g).GetY(), newStateTwo.stateCoordinates.ElementAt(g).GetChar());
            }
            Console.WriteLine("Word Coordinates.");
            for (int g = 0; g < newStateTwo.wordsAdded.ElementAt(0).GetName().Length; g++)
            {
                //Console.WriteLine("Number or word coordinates: {0}", newStateTwo.wordsAdded.ElementAt(0).coordinates.Count);
                Console.WriteLine("(X: {0}, Y: {1}) & Letter: {2}", newStateTwo.wordsAdded.ElementAt(0).coordinates.ElementAt(g).GetX(), newStateTwo.wordsAdded.ElementAt(0).coordinates.ElementAt(g).GetY(), newStateTwo.wordsAdded.ElementAt(0).coordinates.ElementAt(g).GetChar());
            }

            states.Push(newStateTwo);






            Console.WriteLine("Initial Words added");
        }







        int poppedStateIndex = 0;

        while (states.Count != 0)
        {
            poppedStateIndex++;
            State poppedState = states.Pop();
            //Console.WriteLine("Words Left: {0}", poppedState.wordsLeft.Count);

            for (int i = 0; i < poppedState.wordsLeft.Count; i++)
            {

                // 'w' represents the letter of the word being scanned for matching coordinates
                // every letter has a coordinate asscociated with it. Since this is a new word
                // new coordinates will be assigned, then evaluated
                for (int w = 0; w < poppedState.wordsLeft.ElementAt(poppedState.wordsLeft.Count - (i + 1)).GetName().Length; w++)
                {

                    //LinkedList for storing crossing coordinates
                    LinkedList<Coordinate> crossCoor = new LinkedList<Coordinate>();

                    // searching to see if 'w' is a letter already established with a coordinate
                    // if so, the coordinate is added to the crossCoor LinkedList
                    crossCoor = poppedState.getMatchCharCoor(poppedState.wordsLeft.ElementAt(poppedState.wordsLeft.Count - (i + 1)).GetName()[w]);

                    int CCCount = crossCoor.Count();
                    // loops through the cross coordinates, checking if the added word can
                    // be added to the state (crossword)
                    for (int z = 0; z < CCCount; z++)
                    {

                        // if statement to avoid added words to other words at the end or beginning
                        // of the word
                        if ((crossCoor.ElementAt(z).endLH == false) && (crossCoor.ElementAt(z).endRH == false))
                        {


                            // Console.WriteLine("*******WORD COORDINATES OF POPPED STATE {0} BEFORE NEWSTATE", poppedStateIndex);
                            // for (int g = 0; g < poppedState.wordsLeft.ElementAt(poppedState.wordsLeft.Count - (i + 1)).GetName().Length; g++)
                            // {
                            //     Console.WriteLine("(X: {0}, Y: {1}) & Letter: {2}", poppedState.wordsLeft.ElementAt(poppedState.wordsLeft.Count - (i + 1)).coordinates.ElementAt(g).GetX(),
                            //     poppedState.wordsLeft.ElementAt(poppedState.wordsLeft.Count - (i + 1)).coordinates.ElementAt(g).GetY(),
                            //     poppedState.wordsLeft.ElementAt(poppedState.wordsLeft.Count - (i + 1)).coordinates.ElementAt(g).GetChar());
                            // }

                            // creates a brand new state and copies the 
                            // contents of the poppedState into this new state
                            LinkedList<Word> newWords = new LinkedList<Word>();
                            for (int www = 0; www < words.Count; www++)
                            {
                                newWords.AddLast(new Word(words.ElementAt(www).GetName(), words.ElementAt(www).GetHint()));
                            }
                            State newState = new State(newWords);
                            foreach (Word wrd in poppedState.wordsAdded)
                            {
                                newState.addOldWord(wrd);
                            }
                            foreach (Coordinate crd in poppedState.stateCoordinates)
                            {
                                newState.stateCoordinates.AddLast(new Coordinate(crd.GetX(), crd.GetY(), crd.GetChar()));
                            }

                            // Console.WriteLine("*******WORD COORDINATES OF POPPED STATE {0} BEFORE ADDING", poppedStateIndex);
                            // for (int g = 0; g < poppedState.wordsLeft.ElementAt(poppedState.wordsLeft.Count - (i + 1)).GetName().Length; g++)
                            // {
                            //     Console.WriteLine("(X: {0}, Y: {1}) & Letter: {2}", poppedState.wordsLeft.ElementAt(poppedState.wordsLeft.Count - (i + 1)).coordinates.ElementAt(g).GetX(),
                            //     poppedState.wordsLeft.ElementAt(poppedState.wordsLeft.Count - (i + 1)).coordinates.ElementAt(g).GetY(),
                            //     poppedState.wordsLeft.ElementAt(poppedState.wordsLeft.Count - (i + 1)).coordinates.ElementAt(g).GetChar());
                            // }



                            // Console.WriteLine("*****FIRST ADD POPPEDSTATE STATE COORDINATES.");
                            // for (int g = 0; g < poppedState.stateCoordinates.Count; g++)
                            // {
                            //     Console.WriteLine("(X: {0}, Y: {1}) & Letter: {2}", poppedState.stateCoordinates.ElementAt(g).GetX(), poppedState.stateCoordinates.ElementAt(g).GetY(), poppedState.stateCoordinates.ElementAt(g).GetChar());
                            // }

                            // Console.WriteLine("*****FIRST ADD POPPEDSTATE WORD COORDINATES.");
                            // for (int g = 0; g < poppedState.wordsAdded.ElementAt(0).GetName().Length; g++)
                            // {
                            //     Console.WriteLine("(X: {0}, Y: {1}) & Letter: {2}", poppedState.wordsAdded.ElementAt(0).coordinates.ElementAt(g).GetX(), poppedState.wordsAdded.ElementAt(0).coordinates.ElementAt(g).GetY(), poppedState.wordsAdded.ElementAt(0).coordinates.ElementAt(g).GetChar());
                            // }











                            // Console.WriteLine("*****FIRST ADD NEWSTATE STATE COORDINATES BEFORE ADDING.");
                            // for (int g = 0; g < newState.stateCoordinates.Count; g++)
                            // {
                            //     Console.WriteLine("(X: {0}, Y: {1}) & Letter: {2}", newState.stateCoordinates.ElementAt(g).GetX(), newState.stateCoordinates.ElementAt(g).GetY(), newState.stateCoordinates.ElementAt(g).GetChar());
                            // }

                            // Console.WriteLine("*****FIRST ADD NEWSTATE WORD COORDINATES BEFORE ADDING.");
                            // for (int g = 0; g < newState.wordsAdded.ElementAt(0).GetName().Length; g++)
                            // {
                            //     Console.WriteLine("(X: {0}, Y: {1}) & Letter: {2}", newState.wordsAdded.ElementAt(0).coordinates.ElementAt(g).GetX(), newState.wordsAdded.ElementAt(0).coordinates.ElementAt(g).GetY(), newState.wordsAdded.ElementAt(0).coordinates.ElementAt(g).GetChar());
                            // }

                            Word testWord = newState.wordsLeft.ElementAt(newState.wordsLeft.Count - (i + 1));










                            // tries to add the word, if it cannot, this state does not get pushed 
                            // on the stack 
                            if (newState.addWord(newState.wordsLeft.ElementAt(newState.wordsLeft.Count - (i + 1)),
                             crossCoor.ElementAt(z), w, "rightleft"))
                            {

                                Console.WriteLine("*****NEWSTATE ADDED FOR RIGHTLEFT*********************");
                                Console.WriteLine("Word added if from poppedState: {0}", poppedState.wordsLeft.ElementAt(poppedState.wordsLeft.Count - (i + 1)).GetName());
                                Console.WriteLine("Worded added if from newState: {0}", testWord.GetName());
                                Console.WriteLine("Cross Coordinate: (X: {0}, Y: {1})", crossCoor.ElementAt(z).GetX(), crossCoor.ElementAt(z).GetY());



                                Console.WriteLine("*******STATE COORDINATES OF POPPED STATE {0} THAT SHOULD MATCH BELOW", poppedStateIndex);
                                for (int g = 0; g < poppedState.stateCoordinates.Count; g++)
                                {
                                    Console.WriteLine("(X: {0}, Y: {1}) & Letter: {2}", poppedState.stateCoordinates.ElementAt(g).GetX(), poppedState.stateCoordinates.ElementAt(g).GetY(), poppedState.stateCoordinates.ElementAt(g).GetChar());
                                }

                                // Console.WriteLine("*******WORD COORDINATES OF POPPED STATE {0} ", poppedStateIndex);
                                // for (int g = 0; g < poppedState.wordsLeft.ElementAt(poppedState.wordsLeft.Count - (i + 1)).GetName().Length; g++)
                                // {
                                //     Console.WriteLine("(X: {0}, Y: {1}) & Letter: {2}", poppedState.wordsLeft.ElementAt(poppedState.wordsLeft.Count - (i + 1)).coordinates.ElementAt(g).GetX(),
                                //     poppedState.wordsLeft.ElementAt(poppedState.wordsLeft.Count - (i + 1)).coordinates.ElementAt(g).GetY(),
                                //     poppedState.wordsLeft.ElementAt(poppedState.wordsLeft.Count - (i + 1)).coordinates.ElementAt(g).GetChar());
                                // }

                                Console.WriteLine("*******STATE COORDINATES OF ADDED NEW STATE");
                                for (int g = 0; g < newState.stateCoordinates.Count; g++)
                                {
                                    Console.WriteLine("(X: {0}, Y: {1}) & Letter: {2}", newState.stateCoordinates.ElementAt(g).GetX(), newState.stateCoordinates.ElementAt(g).GetY(), newState.stateCoordinates.ElementAt(g).GetChar());
                                }










                                newState.wordsAdded.ElementAt(newState.wordsAdded.Count - 1).direction = "rightleft";

                                if (newState.solution())
                                {
                                    Console.WriteLine("*******SOLUTION!!!");
                                    Console.WriteLine("State Coordinates.");
                                    for (int g = 0; g < newState.stateCoordinates.Count; g++)
                                    {
                                        Console.WriteLine("(X: {0}, Y: {1}) & Letter: {2}", newState.stateCoordinates.ElementAt(g).GetX(), newState.stateCoordinates.ElementAt(g).GetY(), newState.stateCoordinates.ElementAt(g).GetChar());
                                    }
                                }
                                else
                                {
                                    states.Push(newState);
                                    Console.WriteLine("PUSH HAPPENED");

                                }

                            }
                        }





                        if ((crossCoor.ElementAt(z).endTV == false) && (crossCoor.ElementAt(z).endBV == false))
                        {

                            // creates a brand new state and copies the 
                            // contents of the poppedState into this new state
                            LinkedList<Word> newWords = new LinkedList<Word>();
                            for (int ww = 0; ww < words.Count; ww++)
                            {
                                newWords.AddLast(new Word(words.ElementAt(ww).GetName(), words.ElementAt(ww).GetHint()));
                            }
                            State newState = new State(newWords);
                            foreach (Word wrd in poppedState.wordsAdded)
                            {
                                newState.addOldWord(wrd);
                            }
                            foreach (Coordinate crd in poppedState.stateCoordinates)
                            {
                                newState.stateCoordinates.AddLast(new Coordinate(crd.GetX(), crd.GetY(), crd.GetChar()));
                            }

                            Word testWord = newState.wordsLeft.ElementAt(newState.wordsLeft.Count - (i + 1));

                            if (newState.addWord(newState.wordsLeft.ElementAt(newState.wordsLeft.Count - (i + 1)),
                            crossCoor.ElementAt(z), w, "updown"))
                            {





                                Console.WriteLine("*****NEWSTATE ADDED FOR UPDOWN****************");
                                Console.WriteLine("Word added if from poppedState: {0}", poppedState.wordsLeft.ElementAt(poppedState.wordsLeft.Count - (i + 1)).GetName());
                                Console.WriteLine("Worded added if from newState: {0}", testWord.GetName());
                                Console.WriteLine("Cross Coordinate: (X: {0}, Y: {1})", crossCoor.ElementAt(z).GetX(), crossCoor.ElementAt(z).GetY());



                                Console.WriteLine("*******STATE COORDINATES OF POPPED STATE {0} THAT SHOULD MATCH BELOW", poppedStateIndex);
                                for (int g = 0; g < poppedState.stateCoordinates.Count; g++)
                                {
                                    Console.WriteLine("(X: {0}, Y: {1}) & Letter: {2}", poppedState.stateCoordinates.ElementAt(g).GetX(), poppedState.stateCoordinates.ElementAt(g).GetY(), poppedState.stateCoordinates.ElementAt(g).GetChar());
                                }

                                // Console.WriteLine("*******WORD COORDINATES OF POPPED STATE {0} FROM WORDS LEFT", poppedStateIndex);
                                // for (int g = 0; g < poppedState.wordsLeft.ElementAt(poppedState.wordsLeft.Count - (i + 1)).GetName().Length; g++)
                                // {
                                //     Console.WriteLine("(X: {0}, Y: {1}) & Letter: {2}", poppedState.wordsLeft.ElementAt(poppedState.wordsLeft.Count - (i + 1)).coordinates.ElementAt(g).GetX(),
                                //     poppedState.wordsLeft.ElementAt(poppedState.wordsLeft.Count - (i + 1)).coordinates.ElementAt(g).GetY(),
                                //     poppedState.wordsLeft.ElementAt(poppedState.wordsLeft.Count - (i + 1)).coordinates.ElementAt(g).GetChar());
                                // }

                                // Console.WriteLine("*******WORD COORDINATES OF POPPED STATE {0} FROM WORDS ADDED", poppedStateIndex);
                                // for (int gg = 0; gg < poppedState.wordsAdded.ElementAt(wordsAdded.Count - 1).GetName().Length; gg++)
                                // {
                                //     Console.WriteLine("(X: {0}, Y: {1}) & Letter: {2}", 
                                //     poppedState.wordsAdded.ElementAt(wordsAdded.Count - 1).coordinates.ElementAt(gg).GetX(),
                                //     poppedState.wordsAdded.ElementAt(wordsAdded.Count - 1).coordinates.ElementAt(gg).GetY(),
                                //     poppedState.wordsAdded.ElementAt(wordsAdded.Count - 1).coordinates.ElementAt(gg).GetChar());
                                // }

                                Console.WriteLine("*******STATE COORDINATES OF ADDED NEW STATE");
                                for (int g = 0; g < newState.stateCoordinates.Count; g++)
                                {
                                    Console.WriteLine("(X: {0}, Y: {1}) & Letter: {2}", newState.stateCoordinates.ElementAt(g).GetX(), newState.stateCoordinates.ElementAt(g).GetY(), newState.stateCoordinates.ElementAt(g).GetChar());
                                }



                                newState.wordsAdded.ElementAt(newState.wordsAdded.Count - 1).direction = "updown";

                                if (newState.solution())
                                {
                                    Console.WriteLine("*******SOLUTION!!!");
                                    Console.WriteLine("State Coordinates.");
                                    for (int g = 0; g < newState.stateCoordinates.Count; g++)
                                    {
                                        Console.WriteLine("(X: {0}, Y: {1}) & Letter: {2}", newState.stateCoordinates.ElementAt(g).GetX(), newState.stateCoordinates.ElementAt(g).GetY(), newState.stateCoordinates.ElementAt(g).GetChar());
                                    }
                                }
                                else
                                {
                                    states.Push(newState);
                                    Console.WriteLine("PUSH HAPPENED");

                                }
                            }
                        }
                    }
                }
            }
        }
    }







}