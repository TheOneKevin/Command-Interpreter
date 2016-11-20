//===========================================================//
//How does the standard library work?

//In ILib.Print

public static plug printLn(string); //This
public static void printLn(string); //... or this

//In StdLib.Print

//This is the same
//A plug acts as a boolean, indicating whether a command has been executed or not
public static plug printLn(string input) //More perferred
{
	return "say " + input;
}
//... as this
public static void printLn(string input)
{
	_raw("say" + input);
}

//===========================================================//

//strings are to be interpreted as constants only
string s; //This is invalid
string s = ""; //This is valid, however will throw warning
const string s = ""; //This is ideal, however very long to type

//integers are flexable
int i; //This is valid
int i = 0; //This is redundant
const int i = 0; //This is a declaration, cannot be modified

//booleans are special, they represent an output
bool b; //This is valid
bool b = false; //This is redundant
const bool b = false; //This is a declaration, cannot be modified

//long is not implemented yet

//===========================================================//
//Block booleans

//In ILib.Testfor
public static plug testForBlock(Block);

//In ILib.Block
public class Block
{
	string x, y, z;
	string blockName;
	
	public Block(int x, int y, int z, string blockName)
	{
		//Note the automatic conversion... will touch upon later
		this.x = x; //Note the "this" keyword
		this.y = y;
		this.z = z;
		this.blockName = blockName;
	}
}

//In StdLib.Testfor
public static plug testForBlock(Block block) //This must be exactly the same as the declaration in ILib
{
	return "testforblock " + block.x + " " + block.y + " " block.z + " " + block.blockName;
}

//In your main code
if(testForBlock(new Block(~, ~1, ~, "wool"))) //Note the shorthand "~", is equal to: this.x, this.y + 1, this.z
	printLn("Found wool above us!");

//===========================================================//
//Conversions

string s = 0; //String will be "0"
int i = 10;
s = i; //String will be "10"
//ASCII to string not supported

bool isThere = testForBlock(/*blebleble*/); //Convert plug to bool automatically

//===========================================================//
