using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArnoldC_Interpreter
{
    
    class Module2
    {
        List<Tuple<String, String, int>> codePerLine = new List<Tuple<string, string, int>>();
        Tuple<Boolean, String, int> error = new Tuple<bool, string, int>(false, String.Empty, 0);
        List<Tuple<String, String, int>> dataForSemanticAnalyzer = new List<Tuple<string, string, int>>(); 

        public void SyntaAnalysis(List<Tuple<String, String, String, int>> tokens)
        {

            addLinebreaks(tokens);
            runGrammarCheck(codePerLine);

            for (int i = 0; i < codePerLine.Count; i++)
            {
                //runGrammarCheck(tokens.ElementAt(i), codeGrammar);
                Console.WriteLine(codePerLine.ElementAt(i));
            }

            for (int i=0; i< dataForSemanticAnalyzer.Count; i++)
            {
                //runGrammarCheck(tokens.ElementAt(i), codeGrammar);
                Console.WriteLine(dataForSemanticAnalyzer.ElementAt(i));
            }
        }

        public void addLinebreaks(List<Tuple<String, String, String, int>> tokens)
        {
            int prevLineNum = 0;
            for (int i = 0; i < tokens.Count; i++)
            {

                String lexeme = tokens.ElementAt(i).Item1;
                String lexinfo = tokens.ElementAt(i).Item2;
                String type = tokens.ElementAt(i).Item3;
                int lineNum = tokens.ElementAt(i).Item4;

                if (i == 0)
                {
                    codePerLine.Add(new Tuple<string, string, int> (lexeme, type, lineNum+1));
                }
                else
                {
                    if (prevLineNum!=lineNum)
                    {
                        codePerLine.Add(new Tuple<string, string, int>("LINEBRK", "Line Break", 0));
                        codePerLine.Add(new Tuple<string, string, int>(lexeme, type, lineNum + 1));
                        prevLineNum = lineNum;
                    }
                    else
                    {
                        codePerLine.Add(new Tuple<string, string, int>(lexeme, type, lineNum + 1));
                        prevLineNum = lineNum;
                    }
                }
            }
        }

        public void runGrammarCheck(List<Tuple<String, String, int>> codePerLine)
        {
            //flags
            Boolean itsShowtimeFlag = false; //IT'S SHOWTIME
            Boolean programClosed = true; //Program

            Boolean heyChristmasTreeFlag = false; //HEY CHRISTMAS TREE
            int heyChristmasTreeLineNum=0; //line number
            Boolean varDeclarationClosed = true; //variable declaration

            Boolean ifElseFlag = false; //HEY CHRISTMAS TREE
            int ifElseLineNum = 0; //line number
            Boolean ifElseClosed = true; //variable declaration

            Boolean OperationsFlag = false; //HEY CHRISTMAS TREE
            int OperationsLineNum = 0; //line number
            Boolean OperationsClosed = true; //variable declaration

            for (int i = 0; i<codePerLine.Count; i++)
            {
                String lexeme = codePerLine.ElementAt(i).Item1;
                String lexinfo = codePerLine.ElementAt(i).Item2;
                int lineNum = codePerLine.ElementAt(i).Item3;

                //if IT'S SHOWTIMME
                if (lexeme == "IT'S SHOWTIME")
                {
                    //check if there exist another IT'S SHOWTIME
                    if (itsShowtimeFlag == true)
                    {
                        //error, existing IT'S SHOWTIME
                    }
                    else
                    {
                        dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(lexeme, lexinfo, lineNum));

                        itsShowtimeFlag = true;
                        programClosed = false;

                        //check if nextLexeme is not LINEBRK
                        if (i < codePerLine.Count)
                        {
                            String nextLexeme = codePerLine.ElementAt(i + 1).Item1;
                            String nextLexinfo = codePerLine.ElementAt(i + 1).Item2;
                            int nextLineNum = codePerLine.ElementAt(i + 1).Item3;

                            if (nextLexeme != "LINEBRK")
                            {
                                //error, dapat line break
                            }
                        }
                    }
                }

                //TALK TO THE HAND
                else if (lexeme == "TALK TO THE HAND")
                {
                    dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(lexeme, lexinfo, lineNum));

                    if (i < codePerLine.Count)
                    {
                        String nextLexeme = codePerLine.ElementAt(i + 1).Item1;
                        String nextLexinfo = codePerLine.ElementAt(i + 1).Item2;
                        int nextLineNum = codePerLine.ElementAt(i + 1).Item3;

                        if (nextLexinfo == "VAR")
                        {
                            dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(nextLexeme, nextLexinfo, nextLineNum));
                        }
                        else if (nextLexinfo == "STR")
                        {
                            dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(nextLexeme, nextLexinfo, nextLineNum));
                        }
                        else
                        {
                            //error, dapat line break
                        }
                    }
                }

                //HEY CHRISTMAS TREE
                else if (lexeme == "HEY CHRISTMAS TREE")
                {
                    dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(lexeme, lexinfo, lineNum));

                    heyChristmasTreeFlag = true;
                    heyChristmasTreeLineNum = lineNum;
                    varDeclarationClosed = false;

                    if (i < codePerLine.Count)
                    {
                        String nextLexeme = codePerLine.ElementAt(i + 1).Item1;
                        String nextLexinfo = codePerLine.ElementAt(i + 1).Item2;
                        int nextLineNum = codePerLine.ElementAt(i + 1).Item3;

                        if (nextLexinfo == "VAR")
                        {
                            dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(nextLexeme, nextLexinfo, nextLineNum));
                        }
                        else
                        {
                            //error, dapat VAR
                        }
                    }
                }

                //YOU SET US UP
                else if (lexeme == "YOU SET US UP")
                {
                    //check if HEY CHRISTMAS TREE EXISTS
                    if (heyChristmasTreeFlag == true)
                    {
                        //check if YOU SET US UP follows
                        if (lineNum == heyChristmasTreeLineNum + 1)
                        {
                            dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(lexeme, lexinfo, lineNum));

                            heyChristmasTreeFlag = false;
                            varDeclarationClosed = true;

                            //check next lexeme if INT OR MACRO
                            if (i < codePerLine.Count)
                            {
                                String nextLexeme = codePerLine.ElementAt(i + 1).Item1;
                                String nextLexinfo = codePerLine.ElementAt(i + 1).Item2;
                                int nextLineNum = codePerLine.ElementAt(i + 1).Item3;

                                if (nextLexinfo == "INT")
                                {
                                    dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(nextLexeme, nextLexinfo, nextLineNum));
                                }
                                else if (nextLexeme == "@I LIED")
                                {
                                    dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(nextLexeme, nextLexinfo, nextLineNum));
                                }
                                else if (nextLexeme == "@NO PROBLEMO")
                                {
                                    dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(nextLexeme, nextLexinfo, nextLineNum));
                                }
                                else
                                {
                                    //error, dapat int or mac
                                }
                            }
                        }
                        else
                        {
                            //error YOU SET US UP does not follow HEY CHRISTMAS TREE
                        }
                    }
                    else
                    {
                        //error HEY CHRISTMAS TREE not found
                    }
                }

                //if YOU HAVE BEEN TERMINATED
                else if (lexeme == "YOU HAVE BEEN TERMINATED")
                {
                    //check itshowtimeflag
                    if (itsShowtimeFlag == true)
                    {
                        dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(lexeme, lexinfo, lineNum));

                        itsShowtimeFlag = false;
                        programClosed = true;

                        //next line should be empty or method na
                    }
                }

                //BECAUSE I'M GOING TO SAY PLEASE
                else if (lexeme == "BECAUSE I'M GOING TO SAY PLEASE")
                {
                    dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(lexeme, lexinfo, lineNum));

                    ifElseFlag = true;
                    ifElseLineNum = lineNum;
                    ifElseClosed = false;


                    if (i < codePerLine.Count)
                    {
                        String nextLexeme = codePerLine.ElementAt(i + 1).Item1;
                        String nextLexinfo = codePerLine.ElementAt(i + 1).Item2;
                        int nextLineNum = codePerLine.ElementAt(i + 1).Item3;

                        if (nextLexinfo == "VAR")
                        {
                            dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(nextLexeme, nextLexinfo, nextLineNum));
                        }
                        else
                        {
                            //error, dapat VAR
                        }
                    }
                }

                //YOU HAVE NO RESPECT FOR LOGIC
                else if (lexeme == "YOU HAVE NO RESPECT FOR LOGIC")
                {
                    if (ifElseFlag == true)
                    {
                        if (lineNum != ifElseLineNum)
                        {
                            dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(lexeme, lexinfo, lineNum));

                            ifElseFlag = false;
                            ifElseClosed = true;
                        }
                        else
                        {
                            //error, if else block delimiter found in the same line
                        }
                    }
                    else
                    {
                        //error, dapat may because im going to say please muna
                    }
                }

                //GET TO CHOPPER
                else if (lexeme == "GET TO CHOPPER")
                {
                    dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(lexeme, lexinfo, lineNum));

                    OperationsFlag = true;
                    OperationsLineNum = lineNum;
                    OperationsClosed = false;
                    

                    if (i < codePerLine.Count)
                    {
                        String nextLexeme = codePerLine.ElementAt(i + 1).Item1;
                        String nextLexinfo = codePerLine.ElementAt(i + 1).Item2;
                        int nextLineNum = codePerLine.ElementAt(i + 1).Item3;

                        if (nextLexinfo == "VAR")
                        {
                            dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(nextLexeme, nextLexinfo, nextLineNum));
                        }
                        else
                        {
                            //error, dapat VAR
                        }
                    }
                }

                //HERE IS MY INVITATION
                //check if within scope of ops



                //ENOUGH TALK
                else if (lexeme == "ENOUGH TALK")
                {
                    if (OperationsFlag == true)
                    {
                        if (lineNum != ifElseLineNum)
                        {
                            dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(lexeme, lexinfo, lineNum));

                            OperationsFlag = false;
                            OperationsClosed = true;
                        }
                        else
                        {
                            //error, operations block delimiter found in the same line
                        }
                    }
                    else
                    {
                        //error, dapat may enough talk
                    }
                }

                //BLOCK CHECKERS

                //check program block
                if (programClosed==false)
                {
                    //error, program not terminated properly
                }

                //check var declaration block
                if (varDeclarationClosed==false)
                {
                    //error, var declaration incomplete
                }

                //check ifelse block
                if (ifElseClosed == false)
                {
                    //error if else not closed
                }
            }
        }

        public Tuple<Boolean, String, int> getError()
        {
            return error;
        }

        public List<Tuple<String, String, int>> getDataForSemanticAnalyzer()
        {
            return dataForSemanticAnalyzer;
        }
    }
}
