using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArnoldC_Interpreter
{
    public partial class mainEditorTB : Form
    {
        //lexical analyzer declarations
        String code;
        Module1 mod1;
        List<Tuple<String, String, string, int>> tokens;
        List<Tuple<String, String, int>> rejects;

        //syntax analyzer declarations
        Module2 mod2;

        //semantic analyzer declarations
        Module3 mod3;

        public mainEditorTB()
        {
            InitializeComponent();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = @"C:\";

            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            openFileDialog1.DefaultExt = "arnoldc";
            openFileDialog1.Filter = "(*.arnoldc)|*.arnoldc";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.ShowReadOnly = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileAddressTBox.Text = openFileDialog1.FileName;

                System.IO.StreamReader sr = new
                System.IO.StreamReader(openFileDialog1.FileName);

                //Read the first line of text
                String line = sr.ReadLine();

                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the lie to console window
                    editorTBox.AppendText(line + "\n");
                    //Read the next line
                    line = sr.ReadLine();
                }

                sr.Close();
            }

        }

        private void runButton_Click(object sender, EventArgs e)
        {
            //Start lexixal analysis
            mod1 = new Module1();
            code = editorTBox.Text;

            outputTBox.AppendText("Lexical Analyzer Running...\n");
            //functions
            mod1.LexicalAnalysis(code);
            tokens = mod1.getTokens();
            rejects = mod1.getRejects();

            //add tokens to table
            for (int i = 0; i < tokens.Count; i++)
            {
                string[] row = { tokens.ElementAt(i).Item1, tokens.ElementAt(i).Item2 };
                LexerSymbolTable.Rows.Add(row);

            }
            
            if (rejects != null)
            {
                //print all rejected matches
                for (int i = 0; i < rejects.Count; i++)
                {
                    outputTBox.AppendText("ERROR at line " + rejects.ElementAt(i).Item3 + ": " + "'" + rejects.ElementAt(i).Item1 + "' " + "has no match!\n");
                }
            }
            else
            {
                mod2 = new Module2();
                outputTBox.AppendText("Syntax Analyzer Running...\n");
                //start syntax analysis
                mod2.SyntaAnalysis(tokens);
                Tuple<bool, String, int> syntacticError = mod2.getError();

                if (syntacticError.Item1 == false) //if no syntax error
                {
                    List<Tuple<String, String, int>> dataForSemanticAnalyzer = mod2.getDataForSemanticAnalyzer();
                    mod3 = new Module3();
                    //start semantic analysis
                    outputTBox.AppendText("Semantic Analyzer Running...\n");
                    mod3.SemanticAnalysis(dataForSemanticAnalyzer);
                    
                    Tuple<bool, String, int> semanticError = mod3.getError();

                    if (semanticError.Item1==false) //if no semantic error, 
                    {
                        doGUIActions(mod3.getGUIActions()); //gui actions
                        //symbol table actions here
                        List<Tuple<String, String>> symbolTableList = mod3.getSymbolTableList();

                        //add variables to symbol table
                        for (int i = 0; i < symbolTableList.Count; i++)
                        {
                            string[] row = { symbolTableList.ElementAt(i).Item1, symbolTableList.ElementAt(i).Item2 };
                            SymbolTable.Rows.Add(row);

                        }
                    }
                    else//if there is semantic error
                    {
                        outputTBox.AppendText("SEMANTIC ERROR: " + semanticError.Item2 + "\n");
                    }
                }
                else //if there is syntax error, print error
                {
                    outputTBox.AppendText("SYNTAX ERROR: " + syntacticError.Item2 + "\n");
                }
            }
        }

        private void doGUIActions(List<Tuple<String, String>> GUIActions)
        {
            outputTBox.AppendText("\n-----OUTPUT-----\n");
            for(int i=0; i<GUIActions.Count; i++)
            {
                String action = GUIActions.ElementAt(i).Item1;
                String toPrint = GUIActions.ElementAt(i).Item2;

                //remove quotation marks here

                outputTBox.AppendText(toPrint + "\n");
            }
        }
    }
}
