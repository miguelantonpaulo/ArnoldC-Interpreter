using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ArnoldC_Interpreter
{
    class Module1
    {
        //literals
        Regex regexIntegers = new Regex(@"\d+");
        Regex regexMacros = new Regex(@"@I LIED|@NO PROBLEMO");
        Regex regexStrings = new Regex(@"""([""]|"""")?.*""");

        //identifiers
        Regex regexVariables = new Regex(@"[a-zA-Z][a-zA-Z0-9_]*");
        Regex regexFunctions = new Regex(@"[a-zA-Z][a-zA-Z0-9_]*");

        //keywords
        Regex regexKeywords = new Regex(@"IT'S SHOWTIME|YOU HAVE BEEN TERMINATED|HEY CHRISTMAS TREE|YOU SET US UP|GET YOUR ASS TO MARS|DO IT NOW|TALK TO THE HAND|LISTEN TO ME VERY CAREFULLY|I NEED YOUR CLOTHES YOUR BOOTS AND YOUR MOTORCYCLE|GIVE THESE PEOPLE AIR|WHAT THE FUCK DID I DO WRONG|GET TO THE CHOPPER|HERE IS MY INVITATION|HE HAD TO SPLIT|ENOUGH TALK|YOU'RE FIRED|GET UP|GET DOWN|I'LL BE BACK|I LET HIM GO|YOU ARE NOT YOU YOU ARE ME|LET OFF SOME STEAM BENNET|CONSIDER THAT A DIVORCE|KNOCK KNOCK|STICK AROUND|I NEED YOUR CLOTHES YOUR BOOTS AND YOUR MOTORCYCLE|I'LL BE BACK|HASTA LA VISTA, BABY|BECAUSE I'M GOING TO SAY PLEASE|YOU HAVE NO RESPECT FOR LOGIC|I WANT TO ASK YOU A BUNCH OF QUESTIONS AND I WANT TO HAVE THEM ANSWERED IMMEDIATELY|BULLSHIT");
        List<Tuple<String, String, String, int>> tokens = new List<Tuple<String, String, String, int>>();
        List<Tuple<String, String, int>> rejects;

        String[] lines;

        public void LexicalAnalysis(String code)
        {
            lines = splitCodePerLine(code);

            for(int i = 0; i<lines.Count(); i++)
            {
                startLexicalAnalysis(lines[i], i);
            }
        }

        public String[] splitCodePerLine(String code)
        {
            String[] lineArray;
            lineArray = code.Split('\n');
            return lineArray;
        }

        public void startLexicalAnalysis(String line, int i)
        {

            String lineToMatch = line;

            while (lineToMatch != "")
            {
                //Console.WriteLine(lineToMatch);
                lineToMatch = matchPerLine(lineToMatch, i);
                
            }
            
        }

        public String matchPerLine(String lineToMatch, int i)
        {
            Match matchKeywords = regexKeywords.Match(lineToMatch);
            Match matchInt = regexIntegers.Match(lineToMatch);
            Match matchMacro = regexMacros.Match(lineToMatch);
            Match matchString = regexStrings.Match(lineToMatch);
            Match matchVar = regexVariables.Match(lineToMatch);
            Match matchFxn = regexFunctions.Match(lineToMatch);

            if (matchKeywords.Success)
            {
                String keyword = matchKeywords.Value;
                
                //delimeters
                if (keyword == "IT'S SHOWTIME")
                {
                    tokens.Add(new Tuple<String, String, String, int>(keyword, "Code Start Delimiter", "program", i));
                }
                else if (keyword == "YOU HAVE BEEN TERMINATED")
                {
                    tokens.Add(new Tuple<String, String, String, int>(keyword, "Code End Delimiter", "program", i));
                }
                else if (keyword == "HEY CHRISTMAS TREE") //single line codes
                {
                    tokens.Add(new Tuple<String, String, String, int>(keyword, "Variable Declaration", "statement", i));
                }
                else if (keyword == "YOU SET US UP")
                {
                    tokens.Add(new Tuple<String, String, String, int>(keyword, "Variable Initializer", "statement", i));
                }
                else if (keyword == "TALK TO THE HAND")
                {
                    tokens.Add(new Tuple<String, String, String, int>(keyword, "Print Function", "statement", i));
                }
                else if (keyword == "GET TO THE CHOPPER")
                {
                    tokens.Add(new Tuple<String, String, String, int>(keyword, "Operation Declaration Start", "statement", i));
                }
                else if (keyword == "HERE IS MY INVITATION")
                {
                    tokens.Add(new Tuple<String, String, String, int>(keyword, "Operation Operand", "statement", i));
                }
                else if (keyword == "ENOUGH TALK")
                {
                    tokens.Add(new Tuple<String, String, String, int>(keyword, "Operation Declaration End", "statement", i));
                }
                else if (keyword == "GET UP")
                {
                    tokens.Add(new Tuple<String, String, String, int>(keyword, "Addition Operation", "statement", i));
                }
                else if (keyword == "GET DOWN")
                {
                    tokens.Add(new Tuple<String, String, String, int>(keyword, "Subtraction Operation", "statement", i));
                }
                else if (keyword == "YOU'RE FIRED")
                {
                    tokens.Add(new Tuple<String, String, String, int>(keyword, "Multiplication Operation", "statement", i));
                }
                else if (keyword == "HE HAD TO SPLIT")
                {
                    tokens.Add(new Tuple<String, String, String, int>(keyword, "Division Operation", "statement", i));
                }
                else if (keyword == "I LET HIM GO")
                {
                    tokens.Add(new Tuple<String, String, String, int>(keyword, "Modulo Operation", "statement", i));
                }
                else if (keyword == "YOU ARE NOT YOU YOU ARE ME")
                {
                    tokens.Add(new Tuple<String, String, String, int>(keyword, "Equal To Operation", "statement", i));
                }
                else if (keyword == "LET OFF SOME STEAM BENNET")
                {
                    tokens.Add(new Tuple<string, string, string, int>(keyword, "Greater Than Operation", "statement", i));
                }
                else if (keyword == "CONSIDER THAT A DIVORCE")
                {
                    tokens.Add(new Tuple<string, string, string, int>(keyword, "Or Operator", "statement", i));
                }
                else if (keyword == "KNOCK KNOCK")
                {
                    tokens.Add(new Tuple<string, string, string, int>(keyword, "And Operator", "statement", i));
                }
                else if (keyword == "GET YOUR ASS TO MARS") // multi-line operation
                {
                    tokens.Add(new Tuple<string, string, string, int>(keyword, "Scan Operation Variable Initialization", "statement", i));
                }
                else if (keyword == "DO IT NOW")
                {
                    tokens.Add(new Tuple<string, string, string, int>(keyword, "Scan Operation Variable Assignment", "statement", i));
                }
                else if (keyword == "I WANT TO ASK YOU A BUNCH OF QUESTIONS AND I WANT TO HAVE THEM ANSWERED IMMEDIATELY")
                {
                    tokens.Add(new Tuple<string, string, string, int>(keyword, "Scan Operation End Statement", "statement", i));
                }
                else if (keyword == "BECAUSE I'M GOING TO SAY PLEASE")
                {
                    tokens.Add(new Tuple<string, string, string, int>(keyword, "IF Operator", "statement", i));
                }
                else if (keyword == "YOU HAVE NO RESPECT FOR LOGIC")
                {
                    tokens.Add(new Tuple<string, string, string, int>(keyword, "IF Operator End Statement", "statement", i));
                }
                else if (keyword == "BULLSHIT")
                {
                    tokens.Add(new Tuple<string, string, string, int>(keyword, "Else Operator", "statement", i));
                }
                else if (keyword == "STICK AROUND")
                {
                    tokens.Add(new Tuple<string, string, string, int>(keyword, "While Operator", "statement", i));
                }
                else if (keyword == "LISTEN TO ME VERY CAREFULLY")
                {
                    tokens.Add(new Tuple<string, string, string, int>(keyword, "Method Operator Start Statement", "statement", i));
                }
                else if (keyword == "HASTA LA VISTA, BABY")
                {
                    tokens.Add(new Tuple<string, string, string, int>(keyword, "Method Operator End Statement", "statement", i));
                }
                else if (keyword == "I NEED YOUR CLOTHES YOUR BOOTS AND YOUR MOTORCYCLE")
                {
                    tokens.Add(new Tuple<string, string, string, int>(keyword, "Non-void Operator", "statement", i));
                }
                else if (keyword == "I'LL BE BACK")
                {
                    tokens.Add(new Tuple<string, string, string, int>(keyword, "Return Operator", "statement", i));
                }
                else if (keyword == "WHAT THE FUCK DID I DO WRONG")
                {
                    tokens.Add(new Tuple<string, string, string, int>(keyword, "Error Operator", "statement", i));
                }

                lineToMatch = lineToMatch.Replace(keyword, String.Empty);
                return lineToMatch.Trim();
            }
            else
            {
                if (matchMacro.Success)
                {
                    String keyword = matchMacro.Value;
                    tokens.Add(new Tuple<String, String, String, int>(keyword, "Macro Literal", "MAC", i));
                    lineToMatch = lineToMatch.Replace(keyword, String.Empty);
                    return lineToMatch.Trim();

                }
                else
                {
                    if (matchString.Success)
                    {
                        String keyword = matchString.Value;
                        tokens.Add(new Tuple<String, String, String, int>(keyword, "String Literal", "STR", i));
                        lineToMatch = lineToMatch.Replace(keyword, String.Empty);
                        return lineToMatch.Trim();

                    }
                    else
                    {
                        if (matchVar.Success)
                        {
                            String keyword = matchVar.Value;
                            tokens.Add(new Tuple<String, String, String, int>(keyword, "Variable Identifier", "VAR", i));
                            lineToMatch = lineToMatch.Replace(keyword, String.Empty);
                            return lineToMatch.Trim();

                        }
                        else
                        {
                            if (matchInt.Success)
                            {
                                String keyword = matchInt.Value;
                                tokens.Add(new Tuple<String, String, String, int>(keyword, "Integer Literal", "INT", i));
                                lineToMatch = lineToMatch.Replace(keyword, String.Empty);
                                return lineToMatch.Trim();

                            }
                            else
                            {
                                rejects.Add(new Tuple<String, String, int>(lineToMatch, "--nomatch--", i));
                                lineToMatch = String.Empty;
                                return lineToMatch;

                            }
                        }
                    }
                }
            }
        }

        public List<Tuple<String, String, String, int>> getTokens()
        {
            return tokens;
        }

        public List<Tuple<String, String, int>> getRejects()
        {
            return rejects;
        }

    }

}
