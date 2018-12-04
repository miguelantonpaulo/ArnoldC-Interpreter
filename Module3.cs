using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArnoldC_Interpreter
{
    class Module3
    {

        Tuple<Boolean, String, int> error = new Tuple<bool, string, int>(false, String.Empty, 0);
        List<Tuple<String, String>> GUIActions = new List<Tuple<string, string>>();
        List<Tuple<String, String>> symbolTableList = new List<Tuple<string, string>>();

        public void SemanticAnalysis(List<Tuple<String, String, int>> dataForSemanticAnalyzer)
        {
            List<Tuple<String, String, int>>programData = dataForSemanticAnalyzer;
            Boolean skipLine = false;

            //locate blocks
            //program blocks
            int programStart = 0;
            int programEnd = 0;
            for (int i = 0; i < programData.Count; i++)
            {
                String lexeme = programData.ElementAt(i).Item1;
                int lineNum = programData.ElementAt(i).Item3;
                //locate IT'S SHOWTIME
                if (lexeme== "IT'S SHOWTIME")
                {
                    programStart = lineNum;
                }
                //locate YOU HAVE BEEN TERMINATED
                else if (lexeme == "YOU HAVE BEEN TERMINATED")
                {
                    programEnd = lineNum;
                }
            }

            //if block
            int ifStart = 0;
            int ifEnd = 0;
            int ifEndIndex = 0;
            for (int i = 0; i < programData.Count; i++)
            {
                String lexeme = programData.ElementAt(i).Item1;
                int lineNum = programData.ElementAt(i).Item3;
                //locate IT'S SHOWTIME
                if (lexeme == "BECAUSE I'M GOING TO SAY PLEASE")
                {
                    ifStart = lineNum;
                }
                //locate YOU HAVE BEEN TERMINATED
                else if (lexeme == "YOU HAVE NO RESPECT FOR LOGIC")
                {
                    ifEnd = lineNum;
                    ifEndIndex = i;
                }
            }

            //program
            for (int i = 0; i<programData.Count; i++)
            {
                String lexeme = programData.ElementAt(i).Item1;
                String lexInfo = programData.ElementAt(i).Item2;
                int lineNum = programData.ElementAt(i).Item3;

                String nextLexeme;
                String nextLexInfo;
                int nextLineNum;

                //printing
                if (lexeme == "TALK TO THE HAND")
                {
                    //check if within program scope
                    if (lineNum > programStart && lineNum < programEnd)
                    {
                        nextLexeme = programData.ElementAt(i + 1).Item1;
                        nextLexInfo = programData.ElementAt(i + 1).Item2;
                        nextLineNum = programData.ElementAt(i + 1).Item3;

                        //check if nextlexeme is within program scope
                        if (lineNum > programStart && lineNum < programEnd)
                        {
                            //check if next lexeme info is STR
                            if (nextLexInfo == "STR")
                            {
                                //add to toPrint
                                GUIActions.Add(new Tuple<string, string>("PRINT", nextLexeme));
                            }
                            //check if next lexeme info is VAR
                            else if (nextLexInfo == "VAR")
                            {
                                //get varname
                                String variable = nextLexeme;

                                //check if existing
                                int varIndex = varNameExists(variable);
                                if (varIndex != 0)
                                {
                                    //get value
                                    String value = symbolTableList.ElementAt(varIndex - 1).Item2;

                                    //print value
                                    GUIActions.Add(new Tuple<string, string>("PRINT", value));
                                }
                                else
                                {
                                    //error, varname exists
                                }
                            }
                        }
                    }
                    else
                    {
                        //error not in scope of program
                    }
                }

                //variable declaration
                else if (lexeme == "HEY CHRISTMAS TREE")
                {
                    if (lineNum > programStart && lineNum < programEnd)
                    {
                        //get varmame
                        String variable = programData.ElementAt(i + 1).Item1;
                        //get value
                        String value = programData.ElementAt(i + 3).Item1;

                        //check if existing
                        if (symbolTableList == null)
                        {
                            //add to symbol table
                            symbolTableList.Add(new Tuple<string, string>(variable, value));
                        }
                        else
                        {
                            if (varNameExists(variable) == 0)
                            {
                                //add to symbol table
                                symbolTableList.Add(new Tuple<string, string>(variable, value));
                            }
                            else
                            {
                                //error, varname exists
                            }
                        }
                    }
                    else
                    {
                        //error not in scope of program
                    }
                }

                //if-then
                else if (lexeme == "BECAUSE I'M GOING TO SAY PLEASE")
                {
                    if (lineNum > programStart && lineNum < programEnd)
                    {
                        //get varname
                        String variable = programData.ElementAt(i + 1).Item1;

                        //check if existing
                        int varIndex = varNameExists(variable);
                        if (varIndex != 0)
                        {
                            //get value
                            String value = symbolTableList.ElementAt(varIndex - 1).Item2;

                            //compare value
                            if (value=="@I LIED")
                            {
                                //run everything form ifStart to ifEnd
                                Console.WriteLine(ifEndIndex+"");
                                i = ifEndIndex;
                            }
                        }
                        else
                        {
                            //error, varname does not exist
                        }
                    }
                    else
                    {
                        //error not in scope of program
                    }
                }
            }

            //functions
        }

        public int varNameExists(String variable)
        {
            int result=0;

            for (int i=1; i<=symbolTableList.Count; i++)
            {
                String compareToVar = symbolTableList.ElementAt(i-1).Item1;
                
                if (compareToVar == variable)
                {
                    result = i;
                }
            }

            return result;
        }

        public Tuple<Boolean, String, int> getError()
        {
            return error;
        }

        public List<Tuple<String, String>> getGUIActions()
        {
            return GUIActions;
        }

        public List<Tuple<String, String>> getSymbolTableList()
        {
            return symbolTableList;
        }
    }
}
