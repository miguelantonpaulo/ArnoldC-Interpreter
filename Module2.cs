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

            AddLinebreaks(tokens);
            runGrammarCheck(codePerLine);

            for (int i = 0; i < codePerLine.Count; i++)
            {
                //runGrammarCheck(tokens.ElementAt(i), codeGrammar);
                Console.WriteLine(codePerLine.ElementAt(i));
            }

            for (int i = 0; i < dataForSemanticAnalyzer.Count; i++)
            {
                //runGrammarCheck(tokens.ElementAt(i), codeGrammar);
                Console.WriteLine(dataForSemanticAnalyzer.ElementAt(i));
            }
        }

        public void AddLinebreaks(List<Tuple<String, String, String, int>> tokens)
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
                    codePerLine.Add(new Tuple<string, string, int>(lexeme, type, lineNum + 1));
                }
                else
                {
                    if (prevLineNum != lineNum)
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
            int heyChristmasTreeLineNum = 0; //line number
            Boolean varDeclarationClosed = true; //variable declaration

            Boolean ifElseFlag = false; //BECAUSE I'M GOING TO SAY PLEASE
            int ifElseLineNum = 0; //line number
            Boolean ifElseClosed = true; //variable declaration

            Boolean OperationFlag = false; //GET TO THE CHOPPER
            int OperationLineNum = 0; //line number
            Boolean OperationClosed = true; //variable declaration

            Boolean opDeclaration = false;

            for (int i = 0; i < codePerLine.Count; i++)
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
                        error = (new Tuple<Boolean, String, int>(true, "An existing 'IT'S SHOWTIME' is present in the code", lineNum));
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
                                error = (new Tuple<Boolean, String, int>(true, "Expected line break", lineNum));
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
                            error = (new Tuple<Boolean, String, int>(true, "Expected line break", lineNum));
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
                            error = (new Tuple<Boolean, String, int>(true, "Expected variable after 'HEY CHRISTMAS TREE'", lineNum));
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
                                else if (nextLexeme == "I LIED")
                                {
                                    dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(nextLexeme, nextLexinfo, nextLineNum));
                                }
                                else if (nextLexeme == "NO PROBLEMO")
                                {
                                    dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(nextLexeme, nextLexinfo, nextLineNum));
                                }
                                else
                                {
                                    //error, dapat int or mac
                                    error = (new Tuple<Boolean, String, int>(true, "Expected integer or macro after 'YOU SET US UP'", lineNum));
                                }
                            }
                        }
                        else
                        {
                            //error YOU SET US UP does not follow HEY CHRISTMAS TREE
                            error = (new Tuple<Boolean, String, int>(true, "'YOU SET US UP' does not follow 'HEY CHRISTMAS TREE'", lineNum));
                        }
                    }
                    else
                    {
                        //error HEY CHRISTMAS TREE not found
                        error = (new Tuple<Boolean, String, int>(true, "'HEY CHRISTMAS TREE' not found", lineNum));
                    }
                }

                //GET TO THE CHOPPER
                else if (lexeme == "GET TO THE CHOPPER")
                {
                    dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(lexeme, lexinfo, lineNum));

                    OperationFlag = true;
                    OperationLineNum = lineNum;
                    OperationClosed = false;

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
                            error = (new Tuple<Boolean, String, int>(true, "Expected variable after 'GET TO THE CHOPPER'", lineNum));
                        }
                    }
                }

                //HERE IS MY INVITATION
                else if (lexeme == "HERE IS MY INVITATION")
                {
                    int currLine = codePerLine.ElementAt(i).Item3;
                    int nexLineNum = codePerLine.ElementAt(i + 1).Item3;
                    int prevLineNum = codePerLine.ElementAt(i - 1).Item3;

                    if (OperationFlag == true)
                    {
                        if (prevLineNum < currLine && currLine < nexLineNum)
                        {
                            dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(lexeme, lexinfo, lineNum));
                            opDeclaration = true;

                            if (i < codePerLine.Count)
                            {
                                String nextLexeme = codePerLine.ElementAt(i + 1).Item1;
                                String nextLexinfo = codePerLine.ElementAt(i + 1).Item2;
                                int nextLineNum = codePerLine.ElementAt(i + 1).Item3;

                                if (nextLexinfo == "VAR")
                                {
                                    dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(nextLexeme, nextLexinfo, nextLineNum));
                                }
                                else if (nextLexinfo == "INT")
                                {
                                    dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(nextLexeme, nextLexinfo, nextLineNum));
                                }
                                else
                                {
                                    //error, dapat VAR
                                    error = (new Tuple<Boolean, String, int>(true, "Expected variable or integer after 'HERE IS MY INVITATION'", lineNum));
                                }
                            }
                        }
                    }
                    else
                    {
                        error = (new Tuple<Boolean, String, int>(true, "'HERE IS MY INVITATION' not inside the 'GET TO THE CHOPPER' block", lineNum));
                    }
                }

                //GET UP
                else if (lexeme == "GET UP")
                {
                    if (OperationFlag == true)
                    {
                        if (opDeclaration == true)
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
                                else if (nextLexinfo == "INT")
                                {
                                    dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(nextLexeme, nextLexinfo, nextLineNum));
                                }
                                else
                                {
                                    //error, dapat VAR
                                    error = (new Tuple<Boolean, String, int>(true, "Expected variable or integer after 'GET UP'", lineNum));
                                }
                            }
                        }
                        else
                        {
                            error = (new Tuple<Boolean, String, int>(true, "'HERE IS MY INVITATION' not yet executed", lineNum));
                        }
                    }
                    else
                    {
                        error = (new Tuple<Boolean, String, int>(true, "'GET TO THE CHOPPER' not yet executed", lineNum));
                    }
                }

                //GET DOWN
                else if (lexeme == "GET DOWN")
                {
                    if (OperationFlag == true)
                    {
                        if (opDeclaration == true)
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
                                else if (nextLexinfo == "INT")
                                {
                                    dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(nextLexeme, nextLexinfo, nextLineNum));
                                }
                                else
                                {
                                    //error, dapat VAR
                                    error = (new Tuple<Boolean, String, int>(true, "Expected variable or integer after 'GET DOWN'", lineNum));
                                }
                            }
                        }
                        else
                        {
                            error = (new Tuple<Boolean, String, int>(true, "'HERE IS MY INVITATION' not yet executed", lineNum));
                        }
                    }
                    else
                    {
                        error = (new Tuple<Boolean, String, int>(true, "'GET TO THE CHOPPER' not yet executed", lineNum));
                    }
                }

                //YOU'RE FIRED
                else if (lexeme == "YOU'RE FIRED")
                {
                    if (OperationFlag == true)
                    {
                        if (opDeclaration == true)
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
                                else if (nextLexinfo == "INT")
                                {
                                    dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(nextLexeme, nextLexinfo, nextLineNum));
                                }
                                else
                                {
                                    //error, dapat VAR
                                    error = (new Tuple<Boolean, String, int>(true, "Expected variable or integer after 'YOU'RE FIRED'", lineNum));
                                }
                            }
                        }
                        else
                        {
                            error = (new Tuple<Boolean, String, int>(true, "'HERE IS MY INVITATION' not yet executed", lineNum));
                        }
                    }
                    else
                    {
                        error = (new Tuple<Boolean, String, int>(true, "'GET TO THE CHOPPER' not yet executed", lineNum));
                    }
                }

                //HE HAD TO SPLIT
                else if (lexeme == "HE HAD TO SPLIT")
                {
                    if (OperationFlag == true)
                    {
                        if (opDeclaration == true)
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
                                else if (nextLexinfo == "INT")
                                {
                                    dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(nextLexeme, nextLexinfo, nextLineNum));
                                }
                                else
                                {
                                    //error, dapat VAR
                                    error = (new Tuple<Boolean, String, int>(true, "Expected variable or integer after 'HE HAD TO SPLIT'", lineNum));
                                }
                            }
                        }
                        else
                        {
                            error = (new Tuple<Boolean, String, int>(true, "'HERE IS MY INVITATION' not yet executed", lineNum));
                        }
                    }
                    else
                    {
                        error = (new Tuple<Boolean, String, int>(true, "'GET TO THE CHOPPER' not yet executed", lineNum));
                    }
                }

                //I LET HIM GO
                else if (lexeme == "I LET HIM GO")
                {
                    if (OperationFlag == true)
                    {
                        if (opDeclaration == true)
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
                                else if (nextLexinfo == "INT")
                                {
                                    dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(nextLexeme, nextLexinfo, nextLineNum));
                                }
                                else
                                {
                                    //error, dapat VAR
                                    error = (new Tuple<Boolean, String, int>(true, "Expected variable or integer after 'I LET HIM GO'", lineNum));
                                }
                            }
                        }
                        else
                        {
                            error = (new Tuple<Boolean, String, int>(true, "'HERE IS MY INVITATION' not yet executed", lineNum));
                        }
                    }
                    else
                    {
                        error = (new Tuple<Boolean, String, int>(true, "'GET TO THE CHOPPER' not yet executed", lineNum));
                    }
                }

                //YOU ARE NOT YOU YOU ARE ME
                else if (lexeme == "YOU ARE NOT YOU YOU ARE ME")
                {
                    if (OperationFlag == true)
                    {
                        if (opDeclaration == true)
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
                                else if (nextLexinfo == "INT")
                                {
                                    dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(nextLexeme, nextLexinfo, nextLineNum));
                                }
                                else
                                {
                                    //error, dapat VAR
                                    error = (new Tuple<Boolean, String, int>(true, "Expected variable or integer after 'YOU ARE NOT YOU YOU ARE ME'", lineNum));
                                }
                            }
                        }
                        else
                        {
                            error = (new Tuple<Boolean, String, int>(true, "'HERE IS MY INVITATION' not yet executed", lineNum));
                        }
                    }
                    else
                    {
                        error = (new Tuple<Boolean, String, int>(true, "'GET TO THE CHOPPER' not yet executed", lineNum));
                    }
                }

                //LET OFF SOME STEAM BENNET
                else if (lexeme == "LET OFF SOME STEAM BENNET")
                {
                    if (OperationFlag == true)
                    {
                        if (opDeclaration == true)
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
                                else if (nextLexinfo == "INT")
                                {
                                    dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(nextLexeme, nextLexinfo, nextLineNum));
                                }
                                else
                                {
                                    //error, dapat VAR
                                    error = (new Tuple<Boolean, String, int>(true, "Expected variable or integer after 'LET OFF SOME STEAM BENNET'", lineNum));
                                }
                            }
                        }
                        else
                        {
                            error = (new Tuple<Boolean, String, int>(true, "'HERE IS MY INVITATION' not yet executed", lineNum));
                        }
                    }
                    else
                    {
                        error = (new Tuple<Boolean, String, int>(true, "'GET TO THE CHOPPER' not yet executed", lineNum));
                    }
                }

                //CONSIDER THAT A DIVORCE
                else if (lexeme == "CONSIDER THAT A DIVORCE")
                {
                    if (OperationFlag == true)
                    {
                        if (opDeclaration == true)
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
                                else if (nextLexinfo == "INT")
                                {
                                    dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(nextLexeme, nextLexinfo, nextLineNum));
                                }
                                else
                                {
                                    //error, dapat VAR
                                    error = (new Tuple<Boolean, String, int>(true, "Expected variable or integer after 'CONSIDER THAT A DIVORCE'", lineNum));
                                }
                            }
                        }
                        else
                        {
                            error = (new Tuple<Boolean, String, int>(true, "'HERE IS MY INVITATION' not yet executed", lineNum));
                        }
                    }
                    else
                    {
                        error = (new Tuple<Boolean, String, int>(true, "'GET TO THE CHOPPER' not yet executed", lineNum));
                    }
                }

                //KNOCK KNOCK
                else if (lexeme == "KNOCK KNOCK")
                {
                    if (OperationFlag == true)
                    {
                        if (opDeclaration == true)
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
                                else if (nextLexinfo == "INT")
                                {
                                    dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(nextLexeme, nextLexinfo, nextLineNum));
                                }
                                else
                                {
                                    //error, dapat VAR
                                    error = (new Tuple<Boolean, String, int>(true, "Expected variable or integer after 'KNOCK KNOCK'", lineNum));
                                }
                            }
                        }
                        else
                        {
                            error = (new Tuple<Boolean, String, int>(true, "'HERE IS MY INVITATION' not yet executed", lineNum));
                        }
                    }
                    else
                    {
                        error = (new Tuple<Boolean, String, int>(true, "'GET TO THE CHOPPER' not yet executed", lineNum));
                    }
                }

                //ENOUGH TALK
                else if (lexeme == "ENOUGH TALK")
                {
                    //check operationflag
                    if (OperationFlag == true)
                    {
                        dataForSemanticAnalyzer.Add(new Tuple<string, string, int>(lexeme, lexinfo, lineNum));

                        OperationFlag = false;
                        OperationClosed = true;
                        opDeclaration = false;

                        //next line should be empty or method na
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
                            error = (new Tuple<Boolean, String, int>(true, "Expected variable", lineNum));
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
                            error = (new Tuple<Boolean, String, int>(true, "An existing 'YOU HAVE NO RESPECT FOR LOGIC' is present in the code", lineNum));
                        }
                    }
                    else
                    {
                        //error, dapat may because im going to say please muna
                        error = (new Tuple<Boolean, String, int>(true, "Expected 'BECAUSE I'M GOING TO SAY PLEASE'", lineNum));
                    }
                }


                //BLOCK CHECKERS

                //check program block
                if (programClosed == false)
                {
                    //error, program not terminated properly
                    error = (new Tuple<Boolean, String, int>(true, "Missing 'YOU HAVE BEEN TERMINATED'", lineNum));
                }

                //check var declaration block
                if (varDeclarationClosed == false)
                {
                    //error, var declaration incomplete
                    error = (new Tuple<Boolean, String, int>(true, "Variable declaration not executed properly", lineNum));
                }

                //check ifelse block
                if (ifElseClosed == false)
                {
                    //error if else not closed
                    error = (new Tuple<Boolean, String, int>(true, "'BECAUSE I'M GOING TO SAY PLEASE' operation not executed properly", lineNum));
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
