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
        Regex regexKeywords = new Regex(@"IT'S SHOWTIME|HEY CHRISTMAS TREE|YOU SET US UP|GET YOUR ASS TO MARS|DO IT NOW|TALK TO THE HAND|YOU HAVE BEEN TERMINATED|LISTEN TO ME VERY CAREFULLY|I NEED YOUR CLOTHES YOUR BOOTS AND YOUR MOTORCYCLE|GIVE THESE PEOPLE AIR|GET TO THE CHOPPER|HERE IS MY INVITATION|HE HAD TO SPLIT|ENOUGH TALK|YOU'RE FIRED|GET DOWN|I'LL BE BACK|HASTA LA VISTA, BABY|BECAUSE I'M GOING TO SAY PLEASE|YOU HAVE NO RESPECT FOR LOGIC|I WANT TO ASK YOU A BUNCH OF QUESTIONS AND I WANT TO HAVE THEM ANSWERED IMMEDIATELY");
        
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
                if(keyword=="IT'S SHOWTIME")
                {
                    tokens.Add(new Tuple<String, String, String, int>(keyword, "Code Start Delimiter", "program", i));
                }else if(keyword=="YOU HAVE BEEN TERMINATED")
                {
                    tokens.Add(new Tuple<String, String, String, int>(keyword, "Code End Delimiter", "program", i));
                }else if (keyword == "HEY CHRISTMAS TREE") //single line codes
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
                else if (keyword == "BECAUSE I'M GOING TO SAY PLEASE")
                {
                    tokens.Add(new Tuple<String, String, String, int>(keyword, "If Function", "statement", i));
                }
                else if (keyword == "YOU HAVE NO RESPECT FOR LOGIC")
                {
                    tokens.Add(new Tuple<String, String, String, int>(keyword, "End-If Function", "statement", i));
                }
                else if (keyword == "I WANT TO ASK YOU A BUNCH OF QUESTIONS AND I WANT TO HAVE THEM ANSWERED IMMEDIATELY")
                {
                    tokens.Add(new Tuple<String, String, String, int>(keyword, "Scanning Function", "statement", i));
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
