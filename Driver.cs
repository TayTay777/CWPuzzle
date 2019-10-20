using System;
using System.Collections.Generic;
using System.Text;


class Driver{

    static public void Main(String[] args){
        
        
        LinkedList<Word> collection = new LinkedList<Word>();

        Word wordOne = new Word("mindset", "A particular way of thinking");
        collection.AddLast(wordOne);
        Word wordTwo = new Word("CoNC", "This metric measures costs incurred from failure to meet product qualily requirements");
        collection.AddLast(wordTwo);
        Word wordThree = new Word("escalate", "up the ante");
        collection.AddLast(wordThree);
        Word wordFour = new Word("customer event", "When a customer doesn't want to party due to stop ships");
        collection.AddLast(wordFour);
        Word wordFive = new Word("shiftleft", "The opposite of shift right");
        collection.AddLast(wordFive);
        Word wordSix = new Word("transfer", "To share quality messaging");
        collection.AddLast(wordSix);
        Word wordSeven = new Word("zero defects", "Opposite of abundant errors; what Micron aspires to");
        collection.AddLast(wordSeven);
        Word wordEight = new Word("proactive", "Synomym for shift left");
        collection.AddLast(wordEight);
        Word wordNine = new Word("feel", "1 of 4 parts of a culture of quality");
        collection.AddLast(wordNine);


        // Word wordEight = new Word("apl", "Synomym for shift left");
        // collection.AddLast(wordEight);
        // Word wordNine = new Word("pek", "1 of 4 parts of a culture of quality");
        // collection.AddLast(wordNine);




        CWSolver solver = new CWSolver(collection);

        Console.WriteLine("Main happened");

        solver.solve();


    }




}